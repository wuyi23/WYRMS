using System;
using System.Web.Mvc;
using WY.RMS.Component.Tools;
using WY.RMS.CoreBLL.Service;
using WY.RMS.CoreBLL.Service.Member.Interface;
using WY.RMS.ViewModel;

namespace WY.RMS.Web.Areas.Common.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Common/Login/

        public ActionResult Index()
        {
            return View();
        }
        private readonly IAccountService _AccountService;
        public LoginController(IAccountService accountService)
        {
            this._AccountService = accountService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//防止XSS攻击
        public ActionResult Index(LoginVM loginVM)
        {
            //if (ValidateUser(email, password))
            //{
            //    FormsAuthentication.SetAuthCookie(loginVM.LoginName, false);//此处设置用户已验证，Set完后可以用User.Identity.Name来获取当前已登录用户的用户名，User.Identity.IsAuthenticated指示是否验证了用户
            //    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        return Redirect(loginVM.ReturnUrl);
            //    }
            //}
            //return View();
            try
            {
                OperationResult result = _AccountService.Login(loginVM);
                string msg = result.Message ?? result.ResultType.GetDescription();
                if (result.ResultType == OperationResultType.Success)
                {
                    //  return Redirect(Url.Action("Index", "Home"));
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", msg);
                return View(loginVM);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e);
                return View(loginVM);
            }
        }

        public ActionResult Logout()
        {
            _AccountService.Logout();
            return RedirectToAction("Index", "Home");
        }


    }
}
