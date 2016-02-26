using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WY.RMS.Component.Data.EF;
using WY.RMS.Component.Tools;
using WY.RMS.CoreBLL.Service;
using WY.RMS.Domain.Model.Member;
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

        private readonly EFDbContext _db = new EFDbContext();
        //
        // GET: /Member/Module/
        [Layout]
        public ActionResult Index()
        {
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
    }
}
