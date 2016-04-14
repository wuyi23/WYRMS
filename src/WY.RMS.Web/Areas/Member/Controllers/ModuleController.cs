using Newtonsoft.Json;
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
    public class ModuleController : Controller
    {
        private readonly IModuleService _moduleService;
        public ModuleController(IModuleService moduleService)
        {
            this._moduleService = moduleService;
        }

        //
        // GET: /Member/Module/
        [Layout]
        public ActionResult Index()
        {
            GetButtonPermissions();
            var enabledItems = DataSourceHelper.GetIsTrue();
            ViewBag.EnableItems = enabledItems;
            return View();
        }

        public JsonResult GetModules(int limit, int offset, string moduleName, int enable)
        {
            int total = 0;
            Expression<Func<Module, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(moduleName))
            {
                wh = wh.And(c => c.Name.Contains(moduleName));
            }
            if (enable >= 0)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var result = _moduleService.GetListModuleVM(wh, limit, offset, out total);

            return Json(new { total = total, rows = result }, JsonRequestBehavior.AllowGet);
        }

        #region 新增
        public ActionResult Create()
        {
            var vm = new ModuleVM();
            ViewBag.ParentModuleList = _moduleService.Modules
                                        .Where(c => c.IsMenu == true && c.Enabled == true && c.ParentId == null)
                                        .Select(c => new SelectListItem() { Text = c.Name, Value = SqlFunctions.StringConvert((double)c.Id).Trim() })
                                        .ToList();
            return PartialView(vm);

        }
        [HttpPost]
        public ActionResult Create(ModuleVM vm)
        {
            if (!ModelState.IsValid) return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新输入"));
            var result = _moduleService.Insert(vm);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }
        #endregion

        #region 修改
        //
        // GET: /Member/Module/Edit/5
        [IsAjax]
        public ActionResult Edit(int id = 0)
        {
            var module = _moduleService.Modules.FirstOrDefault(c => c.Id == id);
            if (module == null) return PartialView("Create", new ModuleVM());
            ViewBag.ParentModuleList = _moduleService.Modules
                            .Where(c => c.IsMenu == true && c.Enabled == true && c.ParentId == null)
                            .Select(c => new SelectListItem() { Text = c.Name, Value = SqlFunctions.StringConvert((double)c.Id).Trim(), Selected = (module.ParentId.HasValue && (module.ParentId.Value == c.Id)) })
                            .ToList();
            var model = new ModuleVM()
            {
                Id = module.Id,
                Name = module.Name,
                ParentId = module.ParentId,
                LinkUrl = module.LinkUrl,
                IsMenu = module.IsMenu,
                Code = module.Code,
                Description = module.Description,
                Enabled = module.Enabled,
            };
            return PartialView("Create", model);
        }

        //
        // POST: /Member/Module/Edit

        [HttpPost]
        public ActionResult Edit(ModuleVM moduleVM)
        {
            if (!ModelState.IsValid) return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新检查输入"));
            var result = _moduleService.Update(moduleVM);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }
        #endregion

        #region 删除
        //
        // POST: /Member/Module/Delete

        [HttpPost]
        public ActionResult Delete()
        {
            var rolelist = Request.Form["arrselections"];
            IEnumerable<ModuleVM> list = JsonConvert.DeserializeObject<List<ModuleVM>>(rolelist);
            var result = _moduleService.Delete(list);
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
            Permission addModuleButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.AddModule.ToString());
            ViewBag.AddModuleButton = addModuleButton;
            //修改按钮
            Permission updateModuleButton =
                permissionCache.FirstOrDefault(c => c.Enabled == true && c.Code == EnumPermissionCode.UpdateModule.ToString());
            ViewBag.UpdateModuleButton = updateModuleButton;
        }
        #endregion
    }
}
