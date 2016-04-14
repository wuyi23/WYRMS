using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WY.RMS.Component.Data.Enum;
using WY.RMS.Component.Tools;
using WY.RMS.Component.Tools.helpers;
using WY.RMS.CoreBLL.Service.Member.Interface;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel;
using WY.RMS.ViewModel.Member;
using WY.RMS.Web.Extension.Common;
using WY.RMS.Web.Extension.Filters;

namespace WY.RMS.Web.Areas.Member.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserGroupService _userGroupService;
        public UserController(IUserService userService, IRoleService roleService, IUserGroupService userGroupService)
        {
            this._userService = userService;
            this._roleService = roleService;
            this._userGroupService = userGroupService;
        }

        //
        // GET: /Member/User/
        [Layout]
        public ActionResult Index()
        {
            GetButtonPermissions();
            var enabledItems = DataSourceHelper.GetIsTrue();
            ViewBag.EnableItems = enabledItems;
            return View();
        }
        //
        // GET: /Member/User/GetUsers
        public JsonResult GetUsers(int limit, int offset, string userName, int enable)
        {
            Expression<Func<User, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(userName))
            {
                wh = wh.And(c => c.TrueName.Contains(userName.Trim()));
            }
            if (enable >= 0)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var users = _userService.Users.Where(wh);
            var total = users.Count();
            var lstUsers = users.OrderByDescending(t => t.UpdateDate).Skip(offset).Take(limit).ToList();
            var result = new List<UserVM>();
            if (lstUsers.Count > 0)
            {
                result = lstUsers.Select(t => new UserVM()
                {
                    Id = t.Id,
                    UserName = t.UserName,
                    TrueName = t.TrueName,
                    Email = t.Email,
                    Phone = t.Phone,
                    Address = t.Address,
                    Enabled = t.Enabled,
                    UpdateDate = t.UpdateDate
                }).ToList();
            }
            return Json(new { total = total, rows = result }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Member/User/Create

        public ActionResult Create()
        {
            var model = new UserVM();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(UserVM userVm)
        {
            ValidationHelper.RemoveValidationError(ModelState, "Password");//移除Modle中不需要验证的属性Password
            if (!ModelState.IsValid) return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新检查输入"));
            userVm.Password = EncryptionHelper.GetMd5Hash("123456");
            var result = _userService.Insert(userVm);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }
        //
        // GET: /Member/User/Edit/5
        [IsAjax]
        public ActionResult Edit(int id = 0)
        {
            var user = _userService.Users.FirstOrDefault(c => c.Id == id);
            if (user == null) return PartialView("Create", new UserVM());
            var model = new UserVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                TrueName = user.TrueName,
                Address = user.Address,
                Phone = user.Phone,
                Email = user.Email,
                Enabled = user.Enabled,
            };
            return PartialView("Create", model);
        }

        //
        // POST: /Member/User/Edit

        [HttpPost]
        public ActionResult Edit(UserVM userVM)
        {
            ValidationHelper.RemoveValidationError(ModelState, "Password");//移除Modle中不需要验证的属性Password
            if (!ModelState.IsValid) return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新检查输入"));
            var result = _userService.Update(userVM);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }

        //
        // POST: /Member/User/Delete
        [HttpPost]
        public ActionResult Delete()
        {
            var userlist = Request.Form["arrselections"];
            IEnumerable<UserVM> list = JsonConvert.DeserializeObject<List<UserVM>>(userlist);
            var result = _userService.Delete(list);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }
        //
        // POST: /Member/User/ResetPassword
        [HttpPost]
        public ActionResult ResetPassword()
        {
            var userlist = Request.Form["arrselections"];
            IEnumerable<UserVM> list = JsonConvert.DeserializeObject<List<UserVM>>(userlist);
            var result = _userService.ResetPassword(list);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }


        #region 设置角色
        // GET: /Member/User/SetRoles
        [IsAjax]
        public ActionResult SetRoles(int id = 0)
        {
            ViewBag.KeyId = id;
            var user = _userService.Users.Include(c => c.Roles).FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return PartialView("Create", new UserVM());
            }
            else
            {
                List<int> ids = user.Roles.Select(c => c.Id).ToList();
                var list = _roleService.Roles.Where(c => c.Enabled == true).Select(c => new CheckBoxVM()
            {
                Name = "chkRoles",
                Value = c.Id,
                Discription = c.RoleName,
                IsChecked = ids.Contains(c.Id)

            }).ToList();
                return PartialView("_SetCheckBox", list);
            }
        }
        [HttpPost]
        public ActionResult SetRoles(int keyId, string[] chkRoles)
        {
            if (keyId <= 0)
            {
                return Json(new OperationResult(OperationResultType.ParamError, "参数错误!"));
            }
            OperationResult result = _userService.UpdateUserRoles(keyId, chkRoles);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }
        #endregion


        #region 设置用户组
        // GET: /Member/User/SetUserGroups
        [IsAjax]
        public ActionResult SetUserGroups(int id = 0)
        {
            ViewBag.KeyId = id;
            var user = _userService.Users.Include(c => c.UserGroups).FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return PartialView("Create", new UserVM());
            }
            else
            {
                List<int> ids = user.UserGroups.Select(c => c.Id).ToList();
                var list = _userGroupService.UserGroups.Where(c => c.Enabled == true).Select(c => new CheckBoxVM()
            {
                Name = "chkUserGroups",
                Value = c.Id,
                Discription = c.GroupName,
                IsChecked = ids.Contains(c.Id)

            }).ToList();
                return PartialView("_SetCheckBox", list);
            }
        }
        [HttpPost]
        public ActionResult SetUserGroups(int keyId, string[] chkUserGroups)
        {
            if (keyId <= 0)
            {
                return Json(new OperationResult(OperationResultType.ParamError, "参数错误!"));
            }
            OperationResult result = _userService.UpdateUserGroups(keyId, chkUserGroups);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }
        #endregion

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
            Permission addUserButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.AddUser.ToString());
            ViewBag.AddUserButton = addUserButton;
            //修改按钮
            Permission updateUserButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.UpdateUser.ToString());
            ViewBag.UpdateUserButton = updateUserButton;
            //删除按钮
            Permission deleteUserButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.DeleteUser.ToString());
            ViewBag.DeleteUserButton = deleteUserButton;
            //重置密码按钮
            Permission resetPwdUserButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.ResetPwdUser.ToString());
            ViewBag.ResetPwdUserButton = resetPwdUserButton;
            //设置用户组
            Permission setGroupUserButton =
           permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.SetGroupUser.ToString());
            ViewBag.SetGroupUserButton = setGroupUserButton;
            //设置角色
            Permission setRolesUserButton =
           permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.SetRolesUser.ToString());
            ViewBag.SetRolesUserButton = setRolesUserButton;
        }
        #endregion

    }
}
