/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/18 9:39:57  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using WY.RMS.Component.Tools.helpers;
using WY.RMS.CoreBLL.Service;
using WY.RMS.CoreBLL.Service.Member.Interface;
using WY.RMS.Domain.Data.Repositories.Member;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;


namespace WY.RMS.Web.Extension.Filters
{
    public class LayoutAttribute : ActionFilterAttribute
    {
        #region Autofac属性注入,Filter的注入不同于Controller, Controller的注入是通过构造函数注入的，而Filter是通过属性注入的
        public IRoleService _RoleService { get; set; }
        public IModuleService _ModuleService { get; set; }
        public IPermissionService _PermissionService { get; set; }
        public IUserRepository _UserRepository { get; set; }
        #endregion

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            //顶部菜单
            //((ViewResult)filterContext.Result).ViewBag.LoginName = user.LoginName;

            //左侧菜单
            ((ViewResult)filterContext.Result).ViewBag.SidebarMenuModel = InitSidebarMenu();


        }

        private List<ModuleVM> InitSidebarMenu()
        {
            List<ModuleVM> parentMenuList = new List<ModuleVM>();
            string userId = ((System.Web.Security.FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            if (!string.IsNullOrEmpty(userId))
            {
                object permissionCache = CacheHelper.GetCache(CacheKey.StrPermissionsByUid + "_" + userId);
                List<int> permissionIds;
                if (permissionCache != null)
                {
                    List<Permission> permissionList = (List<Permission>)permissionCache;
                    permissionIds = permissionList.Select(p => p.Id).ToList();
                }
                else
                {
                    #region 设置权限Cache

                    int id = Convert.ToInt32(userId);
                    User user =
                        _UserRepository.GetEntitiesByEager(new List<string> { "Roles", "UserGroups" })
                            .FirstOrDefault(c => c.Id == id);
                    var roleIdsByUser = user.Roles.Select(r => r.Id).ToList();
                    var roleIdsByUserGroup = user.UserGroups.SelectMany(g => g.Roles).Select(r => r.Id).ToList();
                    roleIdsByUser.AddRange(roleIdsByUserGroup);
                    var roleIds = roleIdsByUser.Distinct().ToList();
                    List<Permission> permissions =
                        _RoleService.Roles.Where(t => roleIds.Contains(t.Id) && t.Enabled == true)
                            .SelectMany(c => c.Permissions)
                            .Distinct()
                            .ToList();
                    var strKey = CacheKey.StrPermissionsByUid + "_" + user.Id;
                    //设置Cache滑动过期时间为1天
                    CacheHelper.SetCache(strKey, permissions, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0, 0));

                    #endregion
                    permissionIds = permissions.Select(p => p.Id).ToList();
                }
                List<Module> childModules =
                    _PermissionService.Permissions.Where(p => permissionIds.Contains(p.Id) && p.Enabled == true)
                        .Select(p => p.module)
                        .Distinct()
                        .ToList();
                if (childModules.Count > 0)
                {
                    parentMenuList = childModules.Select(c => c.ParentModule).Distinct().Select(c => new ModuleVM { Id = c.Id, Name = c.Name, LinkUrl = c.LinkUrl, Code = c.Code }).ToList();
                    foreach (var item in parentMenuList.OrderBy(c => c.Code).ToList())
                    {
                        var children = childModules.Where(c => c.ParentId == item.Id).OrderBy(c => c.Code).Select(c => new ModuleVM { Name = c.Name, LinkUrl = c.LinkUrl }).ToList();
                        if (children.Count > 0)
                        {
                            item.ChildModules = children;
                        }
                    }
                }
            }
            return parentMenuList;
        }
    }
}