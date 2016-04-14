using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WY.RMS.Component.Data.Enum;
using WY.RMS.Component.Tools;
using WY.RMS.Component.Tools.helpers;
using WY.RMS.CoreBLL.Service.Member.Interface;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;
using WY.RMS.Web.Extension.Common;
using WY.RMS.Web.Extension.Filters;

namespace WY.RMS.Web.Areas.Member.Controllers
{
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IModuleService _moduleService;
        public PermissionController(IPermissionService permissionService, IModuleService moduleService)
        {
            this._permissionService = permissionService;
            this._moduleService = moduleService;
        }

        //
        // GET: /Member/Permission/
        [Layout]
        public ActionResult Index()
        {
            GetButtonPermissions();
            var enabledItems = DataSourceHelper.GetIsTrue();
            ViewBag.EnableItems = enabledItems;
            return View();
        }

        public JsonResult GetPermissions(int limit, int offset, string permissionName, int enable)
        {
            int total = 0;
            Expression<Func<Permission, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(permissionName))
            {
                wh = wh.And(c => c.Name.Contains(permissionName.Trim()));
            }
            if (enable >= 0)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var result = _permissionService.GetListPermissionVM(wh, limit, offset, out total);

            return Json(new { total = total, rows = result }, JsonRequestBehavior.AllowGet);
        }

        #region 新增
        public ActionResult Create()
        {
            var vm = new PermissionVM();
            ViewBag.ModuleList = _moduleService.Modules
                                        .Where(c => c.Enabled == true && c.ChildModules.Count == 0)
                                        .Select(c => new SelectListItem() { Text = c.Name, Value = SqlFunctions.StringConvert((double)c.Id).Trim() })
                                        .ToList();
            return PartialView(vm);

        }
        [HttpPost]
        public ActionResult Create(PermissionVM vm)
        {
            if (!ModelState.IsValid) return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新输入"));
            var result = _permissionService.Insert(vm);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }
        #endregion

        #region 修改
        //
        // GET: /Member/Permission/Edit/5
        [IsAjax]
        public ActionResult Edit(int id = 0)
        {
            var permission = _permissionService.Permissions.FirstOrDefault(c => c.Id == id);
            if (permission == null) return PartialView("Create", new PermissionVM());
            ViewBag.ModuleList = _moduleService.Modules
                             .Where(c => c.Enabled == true && c.ChildModules.Count == 0)
                            .Select(c => new SelectListItem() { Text = c.Name, Value = SqlFunctions.StringConvert((double)c.Id).Trim(), Selected = (permission.ModuleId == c.Id) })
                            .ToList();
            var entity = new PermissionVM()
            {
                Id = permission.Id,
                Name = permission.Name,
                ModuleId = permission.ModuleId,
                Code = permission.Code,
                Description = permission.Description,
                Enabled = permission.Enabled,
            };
            return PartialView("Create", entity);
        }

        //
        // POST: /Member/Module/Edit

        [HttpPost]
        public ActionResult Edit(PermissionVM permissionVM)
        {
            if (!ModelState.IsValid) return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新检查输入"));
            var result = _permissionService.Update(permissionVM);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 获取页面按钮可见权限
        /// </summary>
        [NonAction]
        private void GetButtonPermissions()
        {
            string userId = ((System.Web.Security.FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
            List<Permission> permissionCache =
                (List<Permission>)CacheHelper.GetCache(CacheKey.StrPermissionsByUid + "_" + userId);
            //新增按钮
            Permission addPermissionButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.AddPermission.ToString());
            ViewBag.AddPermissionButton = addPermissionButton;
            //修改按钮
            Permission updatePermissionButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.UpdatePermission.ToString());
            ViewBag.UpdatePermissionButton = updatePermissionButton;
        }
        #endregion
    }
}
