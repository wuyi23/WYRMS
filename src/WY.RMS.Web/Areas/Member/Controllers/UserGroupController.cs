using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WY.RMS.Component.Data.Enum;
using WY.RMS.Component.Tools;
using WY.RMS.Component.Tools.helpers;
using WY.RMS.CoreBLL.Service;
using WY.RMS.CoreBLL.Service.Member.Interface;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;
using WY.RMS.Web.Extension.Common;
using WY.RMS.Web.Extension.Filters;

namespace WY.RMS.Web.Areas.Member.Controllers
{
    public class UserGroupController : Controller
    {
        private readonly IUserGroupService _userGroupService;
        public UserGroupController(IUserGroupService userGroupService)
        {
            this._userGroupService = userGroupService;
        }
        //
        // GET: /Member/UserGroup/
        [Layout]
        public ActionResult Index()
        {
            GetButtonPermissions();
            //获取下拉框数据源
            var enabledItems = DataSourceHelper.GetIsTrue();
            ViewBag.EnableItems = enabledItems;
            return View();
        }
        //
        // GET: /Member/UserGroup/GetUserGroups/
        public JsonResult GetUserGroups(int limit, int offset, string userGroupName, int enable)
        {
            Expression<Func<UserGroup, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(userGroupName))
            {
                wh = wh.And(c => c.GroupName.Contains(userGroupName));
            }
            if (enable >= 0)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var groups = _userGroupService.UserGroups.Where(wh);
            var total = groups.Count();
            var lstGroups = groups.OrderBy(t => t.OrderSort).ThenBy(t => t.Id).Skip(offset).Take(limit).ToList();
            var result = new List<UserGroupVM>();
            if (lstGroups.Count > 0)
            {
                result = lstGroups.Select(t => new UserGroupVM()
                {
                    Id = t.Id,
                    GroupName = t.GroupName,
                    Description = t.Description,
                    Enabled = t.Enabled,
                    UpdateDate = t.UpdateDate
                }).ToList();
            }
            return Json(new { total = total, rows = result }, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Member/UserGroup/Create

        public ActionResult Create()
        {
            var model = new UserGroupVM();
            return PartialView(model);
        }

        //
        // POST: /Member/UserGroup/Create

        [HttpPost]
        public ActionResult Create(UserGroupVM groupVm)
        {
            if (!ModelState.IsValid) return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新检查输入"));
            var result = _userGroupService.Insert(groupVm);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }


        #region 私有函数
        /// <summary>
        /// 获取按钮可见权限
        /// </summary>
        [NonAction]
        private void GetButtonPermissions()
        {
            string userId = ((System.Web.Security.FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
            List<Permission> permissionCache =
                (List<Permission>)CacheHelper.GetCache(CacheKey.StrPermissionsByUid + "_" + userId);
            //新增按钮
            Permission addUserGroupButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.AddUserGroup.ToString());
            ViewBag.AddUserGroupButton = addUserGroupButton;
            //修改按钮
            Permission updateUserGroupButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.UpdateUserGroup.ToString());
            ViewBag.UpdateUserGroupButton = updateUserGroupButton;
            //删除按钮
            Permission deleteUserGroupButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.DeleteUserGroup.ToString());
            ViewBag.DeleteUserGroupButton = deleteUserGroupButton;
        }
        #endregion
    }
}
