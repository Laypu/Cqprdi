using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.ViewModel;
using Oaww.Utility;
using Microsoft.Owin.Security;
using System.Security.Cryptography;
using System.Text;
using Oaww.Business;
using Oaww.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Oaww.Entity.SET;
using static Oaww.Business.CommonService;

namespace Template.webadmin.Areas.webadmin.Controllers
{

    public class AuthController : Controller
    {
        AuthService _service = new AuthService();
        CommonService _commonService = new CommonService();

        private const string salt = "+LoOWf5Fqxjfsa5BM0jq9w==";
        protected IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        public ActionResult Login(string ReturnUrl = "")
        {

            SET_LOGIN SET_LOGIN = _commonService.GetGeneral<SET_LOGIN>();

            ViewData["ReturnUrl"] = ReturnUrl;

            return View(SET_LOGIN);
        }

        [HttpPost]
        [CaptchaMvc.Attributes.CaptchaVerify("Captcha is not valid")]
        public ActionResult LoginCheck(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                model.Password = EncodePassword(model.Password.DecryptStringAES());

                Users User = _service.ValidateUser(model.Login.DecryptStringAES(), model.Password);

                if (User == null || User.Account.IsNullOrEmpty())
                {
                    TempData["Error"] = "登入驗證錯誤,請確認帳號或是密碼！";
                    return RedirectToAction("Login");
                }
                else if (User.Enabled.HasValue == false || User.Enabled == false)
                {
                    TempData["Error"] = "登入驗證錯誤,該帳號已被停用！";
                    return RedirectToAction("Login");
                }
                else
                {
                    var DefaultLang = System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultLang"];

                    var alllang = _commonService.GetGeneralList<Lang>(); ;
                    var langid = 1;

                    if (alllang.Count() > 0)
                    {
                        if (alllang.Any(v => v.Lang_Name == DefaultLang))
                        {
                            langid = alllang.Where(v => v.Lang_Name == DefaultLang).First().ID;
                        }
                    }

                    var identity = new ClaimsIdentity(
                     new[] {
                            new Claim(ClaimTypes.Name,User.User_Name),
                            new Claim(ClaimTypes.Role,User.Group_ID),
                            new Claim(ClaimTypes.NameIdentifier,User.Account),
                            new Claim("Language", langid.ToString()),
                            new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",User.ID.ToString())
                     },
                     DefaultAuthenticationTypes.ApplicationCookie,
                     ClaimTypes.Name,
                     ClaimTypes.Role);

                    Authentication.SignIn(new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.SpecifyKind(DateTime.Now.AddMinutes(180), DateTimeKind.Local)
                    }, identity);
                    _commonService.InsertLog(Operation.Login,User.Account,"", "",
                       "Login"
                       , ""
                       , $"登入IP: { GetClientIP()}");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Error"] = "驗證碼輸入錯誤！";
                return RedirectToAction("Login");
            }
        }

        public ActionResult SetLang(string lang)
        {

            var langStr = lang.AntiXss();

            var user = System.Web.HttpContext.Current.Request.GetOwinContext().Authentication.User;
            var Identity = (ClaimsIdentity)user.Identity;
            Identity.RemoveClaim(Identity.FindFirst("Language"));
            Identity.AddClaim(new Claim("Language", langStr));
            Authentication.SignOut();

            var identity = new ClaimsIdentity(
            Identity.Claims,
            DefaultAuthenticationTypes.ApplicationCookie,
            ClaimTypes.Name,
            ClaimTypes.Role);
            Authentication.SignIn(new AuthenticationProperties
            {
                ExpiresUtc = DateTime.SpecifyKind(DateTime.Now.AddMinutes(20), DateTimeKind.Local)
            }, identity);


            return Json("");
        }

        [AllowAnonymous]
        public ActionResult Logout()
        { 
            var user = System.Web.HttpContext.Current.Request.GetOwinContext().Authentication.User;
            var identity = (ClaimsIdentity)User.Identity;

            var name = identity.FindFirst(ClaimTypes.NameIdentifier);
            _commonService.InsertLog(Operation.Logout, name == null ? "" : name.Value, "", "",
                   "LogOut"
                   , ""
                   , $"登出IP: { GetClientIP()}");
            Authentication.SignOut();
            Session.Abandon();
           
            return RedirectToAction("Login", "Auth", new { area = "webadmin" });
        }

        internal string EncodePassword(string pass)
        {


            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            byte[] bRet = null;

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);

            using (SHA256 s = SHA256.Create())
            {
                bRet = s.ComputeHash(bAll);
            }

            return Convert.ToBase64String(bRet);
        }
        private string GetClientIP()
        {
            string result =string.Empty;
            //string strHostName = System.Net.Dns.GetHostName();
            //result = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
            if (Request.ServerVariables["HTTP_VIA"] != null) // using proxy
            {
                result = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP.
            }
            else// not using proxy or can’t get the Client IP
            {
                result = Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can’t get the Client IP, it will return proxy IP.
            }
            //result = Request.UserHostAddress;
            return result;

          
        }
    }
}