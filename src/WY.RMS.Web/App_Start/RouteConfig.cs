using System.Web.Mvc;
using System.Web.Routing;

namespace WY.RMS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WY.RMS.Web.Areas.Common.Controllers" }
            ).DataTokens.Add("Area", "Common");
        }
    }
}