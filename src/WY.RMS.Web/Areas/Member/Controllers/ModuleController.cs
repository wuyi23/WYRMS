using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WY.RMS.Component.Data.EF;
using WY.RMS.CoreBLL.Service;
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
            var enabledItems = new List<SelectListItem> { 
                new SelectListItem { Text = "--- 请选择 ---", Value = "-1", Selected = true }, 
                new SelectListItem { Text = "是", Value = "1" }, 
                new SelectListItem { Text = "否", Value = "0" }
            };
            ViewBag.EnableItems = enabledItems;
            return View(_moduleService.Modules.ToList());
        }

    }
}
