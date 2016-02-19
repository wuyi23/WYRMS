using System.Web.Mvc;
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
