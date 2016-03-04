/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2016/3/4 16:28:26  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WY.RMS.Web.Extension.Filters
{
    public class IsAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                filterContext.Result = new RedirectResult("/Login/Index", true);
            }
        }
    }
}