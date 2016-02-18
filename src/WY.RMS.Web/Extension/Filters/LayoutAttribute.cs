/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/18 9:39:57  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WY.RMS.Component.Tools.helpers;
using WY.RMS.CoreBLL.Service;
using WY.RMS.Domain.Data.Repositories.Member;
using WY.RMS.Domain.Model.Enum;
using WY.RMS.Domain.Model.Member;


namespace WY.RMS.Web.Extension.Filters
{
    public class LayoutAttribute : ActionFilterAttribute
    {
        //private readonly IRoleService _RoleService;
        //private readonly IModuleService _ModuleService;
        //public LayoutAttribute(IRoleService roleService, IModuleService moduleService)
        //{
        //    this._RoleService = roleService;
        //    this._ModuleService = moduleService;
        //}
        #region Autofac属性注入
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

        private List<Module> InitSidebarMenu()
        {
            List<Module> parentMenuList = new List<Module>();
            string userId = ((System.Web.Security.FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            if (!string.IsNullOrEmpty(userId))
            {
                object permissionCache = CacheHelper.GetCache(CacheKey.StrPermissionsByUid + "_" + userId);
                List<int> permissionIds;
                if (permissionCache != null && ((List<int>)permissionCache).Count > 0)
                {
                    permissionIds = (List<int>)permissionCache;
                }
                else
                {
                    #region 设置权限Cache
                    int id = Convert.ToInt32(userId);
                    User user = _UserRepository.GetEntitiesByEager(new List<string> { "Roles", "UserGroups" }).FirstOrDefault(c => c.Id == id);
                    var roleIdsByUser = user.Roles.Select(r => r.Id).ToList();
                    var roleIdsByUserGroup = user.UserGroups.SelectMany(g => g.Roles).Select(r => r.Id).ToList();
                    roleIdsByUser.AddRange(roleIdsByUserGroup);
                    var roleIds = roleIdsByUser.Distinct().ToList();
                    List<int> permissions = _RoleService.Roles.Where(t => roleIds.Contains(t.Id) && t.Enabled == true).SelectMany(c => c.Permissions).Select(c => c.Id).Distinct().ToList();
                    var strKey = CacheKey.StrPermissionsByUid + "_" + user.Id;
                    CacheHelper.SetCache(strKey, permissions);
                    #endregion
                    permissionIds = permissions;
                }
                List<int> moduleIds = _PermissionService.Permissions.Where(p => p.PermissionType == (int)EnumPermissionType.Module && permissionIds.Contains(p.Id)).Select(p => p.TypeKey).Distinct().ToList();
                ////取出所有父菜单的节点
                parentMenuList = _ModuleService.GetEntitiesByEager(new List<string> { "ChildModules" }).Where(m => moduleIds.Contains(m.Id) && m.IsMenu == true && m.ParentId == null).OrderBy(t => t.Code).ToList();
                //parentMenuList = _ModuleService.Modules.Where(m => moduleIds.Contains(m.Id) && m.IsMenu == true && m.ParentId == null).OrderBy(t => t.Code).ToList();
            }
            return parentMenuList;
            //var permissions = _RoleService.Roles.Where(t => roleIds.Contains(t.Id) && t.IsDeleted == false).SelectMany(c => c.Permissions).ToList();
            //List<int> moduleIds = permissions.Where(p => p.IsDeleted == false && p.PermissionType == (int)EnumPermissionType.Module).Select(p => p.TypeKey).Distinct().ToList();

            //var parentModuleIdList = RoleModulePermissionService.RoleModulePermissions.Where(t => RoleIds.Contains(t.RoleId) && t.PermissionId == null && t.IsDeleted == false).Select(t => t.ModuleId).Distinct().ToList();
            //var childModuleIdList = RoleModulePermissionService.RoleModulePermissions.Where(t => RoleIds.Contains(t.RoleId) && t.PermissionId != null && t.IsDeleted == false).Select(t => t.ModuleId).Distinct().ToList();

            //foreach (var pmId in parentModuleIdList)
            //{
            //    //取出父菜单
            //    var parentModule = ModuleService.Modules.FirstOrDefault(t => t.Id == pmId);
            //    if (parentModule != null)
            //    {
            //        var sideBarMenu = new SideBarMenuVM
            //        {
            //            Id = parentModule.Id,
            //            ParentId = parentModule.ParentId,
            //            Name = parentModule.Name,
            //            Code = parentModule.Code,
            //            Icon = parentModule.Icon,
            //            LinkUrl = parentModule.LinkUrl,
            //        };

            //        //取出子菜单
            //        foreach (var cmId in childModuleIdList)
            //        {
            //            var childModule = ModuleService.Modules.FirstOrDefault(t => t.Id == cmId);
            //            if (childModule != null && childModule.ParentId == sideBarMenu.Id)
            //            {
            //                var childSideBarMenu = new SidebarMenuModel
            //                {
            //                    Id = childModule.Id,
            //                    ParentId = childModule.ParentId,
            //                    Name = childModule.Name,
            //                    Code = childModule.Code,
            //                    Icon = childModule.Icon,
            //                    Area = childModule.Area,
            //                    Controller = childModule.Controller,
            //                    Action = childModule.Action
            //                };
            //                sideBarMenu.ChildMenuList.Add(childSideBarMenu);
            //            }
            //        }

            //        //子菜单排序
            //        sideBarMenu.ChildMenuList = sideBarMenu.ChildMenuList.OrderBy(t => t.Code).ToList();
            //        model.Add(sideBarMenu);
            //    }
            //    //父菜单排序
            //    model = model.OrderBy(t => t.Code).ToList();
            //}

            //return model;
        }
    }
}