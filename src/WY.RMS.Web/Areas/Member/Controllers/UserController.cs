using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WY.RMS.Component.Data;
using WY.RMS.Component.Data.EF;
using WY.RMS.Component.Tools;
using WY.RMS.Component.Tools.helpers;
using WY.RMS.CoreBLL.Service;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;
using WY.RMS.Web.Extension.Common;
using WY.RMS.Web.Extension.Filters;

namespace WY.RMS.Web.Areas.Member.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        private readonly EFDbContext _db = new EFDbContext();
        //
        // GET: /Member/User/
        [Layout]
        public ActionResult Index()
        {
            var enabledItems = new List<SelectListItem> { 
                new SelectListItem { Text = "--- 请选择 ---", Value = "-1", Selected = true }, 
                new SelectListItem { Text = "是", Value = "1" }, 
                new SelectListItem { Text = "否", Value = "0" }
            };
            ViewBag.EnableItems = enabledItems;
            return View(_userService.Users.ToList());
        }

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
            var model = new UserVM() { Enabled = true };
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


    }
}
