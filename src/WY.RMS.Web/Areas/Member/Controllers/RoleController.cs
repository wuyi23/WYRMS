using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WY.RMS.Component.Data.EF;
using WY.RMS.Component.Tools;
using WY.RMS.CoreBLL.Service;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel;
using WY.RMS.ViewModel.Member;
using WY.RMS.Web.Extension.Common;
using WY.RMS.Web.Extension.Filters;

namespace WY.RMS.Web.Areas.Member.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        private readonly EFDbContext _db = new EFDbContext();


        //
        // GET: /Member/Role/
        [Layout]
        public ActionResult Index()
        {
            var enabledItems = DataSourceHelper.GetIsTrue();
            ViewBag.EnableItems = enabledItems;
            return View();
        }

        public JsonResult GetRoles(int limit, int offset, string roleName, int enable)
        {
            Expression<Func<Role, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(roleName))
            {
                wh = wh.And(c => c.RoleName.Contains(roleName));
            }
            if (enable >= 0)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var roles = _roleService.Roles.Where(wh);
            var total = roles.Count();
            var lstRoles = roles.OrderBy(t => t.OrderSort).ThenBy(t => t.Id).Skip(offset).Take(limit).ToList();
            var result = new List<RoleVM>();
            if (lstRoles.Count > 0)
            {
                result = lstRoles.Select(t => new RoleVM()
                 {
                     Id = t.Id,
                     RoleName = t.RoleName,
                     Description = t.Description,
                     Enabled = t.Enabled,
                     UpdateDate = t.UpdateDate
                 }).ToList();
            }
            return Json(new { total = total, rows = result }, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Member/Role/Create

        public ActionResult Create()
        {
            var model = new RoleVM();
            return PartialView(model);
        }

        //
        // POST: /Member/Role/Create

        [HttpPost]
        public ActionResult Create(RoleVM roleVm)
        {
            if (!ModelState.IsValid) return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新检查输入"));
            var result = _roleService.Insert(roleVm);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }

        //
        // GET: /Member/Role/Edit/5
        [IsAjax]
        public ActionResult Edit(int id = 0)
        {
            var role = _roleService.Roles.FirstOrDefault(c => c.Id == id);
            if (role == null) return PartialView("Create", new RoleVM());
            var model = new RoleVM()
            {
                Id = role.Id,
                RoleName = role.RoleName,
                Description = role.Description,
                OrderSort = role.OrderSort,
                Enabled = role.Enabled,
            };
            return PartialView("Create", model);
        }

        //
        // POST: /Member/Role/Edit

        [HttpPost]
        public ActionResult Edit(RoleVM roleVM)
        {
            if (!ModelState.IsValid) return Json(new OperationResult(OperationResultType.ParamError, "参数错误，请重新检查输入"));
            var result = _roleService.Update(roleVM);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }

        //
        // POST: /Member/Role/Delete

        [HttpPost]
        public ActionResult Delete()
        {
            var rolelist = Request.Form["arrselections"];
            IEnumerable<RoleVM> list = JsonConvert.DeserializeObject<List<RoleVM>>(rolelist);
            var result = _roleService.Delete(list);
            result.Message = result.Message ?? result.ResultType.GetDescription();
            return Json(result);
        }
        [IsAjax]
        public ActionResult AuthorizePermission(int id = 0)
        {
            IList<ZTreeVM> nodes = _roleService.GetListZTreeVM(id);
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AuthorizePermission(int roleid, string ids)
        {
            string[] idArray = ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            double[] idsDoubles = Array.ConvertAll<string, double>(idArray, s => Convert.ToDouble(s));
            int[] idInts = Array.ConvertAll<double, int>(idsDoubles, s => Convert.ToInt32(s - 0.5));
            OperationResult result = _roleService.UpdateAuthorize(roleid, idInts);
            //var result = _roleService.Update(roleVM);
            //result.Message = result.Message ?? result.ResultType.GetDescription();
            //return Json(result);
            return Json(new OperationResult(OperationResultType.Success, "成功啦"));
        }

    }
}