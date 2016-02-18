using System.Web.Mvc;

namespace WY.RMS.Web.Areas.Common
{
    public class CommonAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Common";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Common_default",
                "Common/{controller}/{action}/{id}",
                new {controller="Login", action = "Index", id = UrlParameter.Optional },
                new string[] { "WY.RMS.Web.Areas.Common.Controllers" }
            );
        }
    }
}
