using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WY.RMS.Domain.Model.Member;
using WY.RMS.Web.Extension.Filters;

namespace WY.RMS.Web.Areas.Common.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [LayoutAttribute]
        public ActionResult Index()
        {
            return View();
        }

    }
}
