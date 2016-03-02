using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WY.RMS.Component.Data.EF;
using WY.RMS.Component.Tools;
using WY.RMS.CoreBLL.Service;
using WY.RMS.Domain.Model.Member;
using WY.RMS.ViewModel.Member;
using WY.RMS.Web.Extension.Common;
using WY.RMS.Web.Extension.Filters;

namespace WY.RMS.Web.Areas.Member.Controllers
{
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            this._permissionService = permissionService;
        }

        //
        // GET: /Member/Permission/
        [Layout]
        public ActionResult Index()
        {
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
    }
}
