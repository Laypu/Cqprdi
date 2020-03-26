using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Oaww.Entity;
using Oaww.Business;
using System.Web.Helpers;

namespace ASP
{
    public  static partial class CommonRazorFunctions
    {
        public static string GetAntiForgery()
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return string.Concat(cookieToken + ":" + formToken);
        }

        public static string UserName()
        {

            var identity = (ClaimsIdentity)System.Web.WebPages.HelperPage.User.Identity;

            var name = identity.FindFirst(ClaimTypes.Name);

            return name == null ? "" : name.Value;
        }

        public static bool CheckIsAdmin()
        {            
            var identity = (ClaimsIdentity)System.Web.WebPages.HelperPage.User.Identity;
            var Role = identity.FindFirst(ClaimTypes.Role);

            string RoleName = Role == null ? "" : Role.Value;
            return RoleName == "administrators";
        }

        public static SiteLayout GetSiteLayout()
        {
            CommonService _commonService = new CommonService();

            return _commonService.GetSiteLayout();
        }
        public static SiteLayout GetSiteLayout(string Lang)
        {
            CommonService _commonService = new CommonService();

            return _commonService.GetSiteLayout(Lang);
        }
    }
}