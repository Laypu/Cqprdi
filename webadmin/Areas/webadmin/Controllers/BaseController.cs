using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using System.Security.Claims;
using Oaww.Entity;
using Oaww.Entity.SET;
using System.Web.Caching;
using System.Web.Routing;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class BaseController : Controller
    {
        
        public static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly object _siteMapLock = new object();
        private const string SITEMAP_CONTEXT = "_SiteMapContext_";
        private const string MENU_CONTEXT = "_MenuContext_";
        private const string PORTAL_CONTEXT = "_PortalMapContext_";
        private const string BASE_CONTEXT = "_BaseContext_";
        private const string ADMIN_CONTEXT = "_AdminContext_";
        //protected string LanguageID = "1";

        CommonService _commonService = new CommonService();
        ExclusiveLayoutService _layoutService = new ExclusiveLayoutService();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.IsAuthenticated == false)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller", "Auth"},
                        {"action", "Login"},
                        {"area", "webadmin"}
                    }
                );
            }
            else
            {
                string loginId = LoginId();

                if (Session["menutype"] == null)
                {
                    if(_layoutService.GetLayoutMenuType(this.Account) == "")
                    {
                        Session["menutype"] = "1";
                    }
                    else
                    {
                        Session["menutype"] = _layoutService.GetLayoutMenuType(this.Account);
                    }
                }
                
                

                lock (_siteMapLock)
                {
                    if (filterContext.HttpContext.Cache.Get(SITEMAP_CONTEXT + loginId) != null)
                    {
                        ViewBag.AuthList = filterContext.HttpContext.Cache.Get(SITEMAP_CONTEXT + loginId);
                        ViewBag.SET_BASE = filterContext.HttpContext.Cache.Get(BASE_CONTEXT + loginId);
                        ViewBag.ListAdminFunction = filterContext.HttpContext.Cache.Get(ADMIN_CONTEXT);
                    }
                    else
                    {

                        List<AdminFunction> selfLayoutFunctions = _layoutService.GetSelfLayout(this.Account,this.LanguageID);
                        List<AdminFunction> SelfMenu = selfLayoutFunctions.Select(t => new AdminFunction() { 
                            ID = t.ID - 20,
                            GroupID = 1,
                            ItemName=t.ItemName,
                            Url= "Home/Index",
                            Controller= "Home",
                            Action= "Index",
                            Parameter = t.Parameter,
                            ParentGroupID = -1
                        }).ToList();


                        List<AdminFunctionAuth> SelfAuth = selfLayoutFunctions.Concat(SelfMenu).Select(t => new AdminFunctionAuth()
                        {
                            GroupID = "",
                            LangID= int.Parse(this.LanguageID),
                            ItemID = t.ID,
                            Type = 0,
                            GID = t.GroupID.Value,
                        }).ToList();

                        //sysmenu                    
                        string[] roles = Roles;
                        List<AdminFunctionAuth> ListMenu = _commonService.GetSiteMapMenuByUser(roles).ToList().Concat(SelfAuth).ToList();
                        filterContext.HttpContext.Cache.Insert(SITEMAP_CONTEXT + loginId, ListMenu, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(5));
                        ViewBag.AuthList = ListMenu;   
                        //Base Setting
                        SET_BASE SET_BASE = _commonService.GetGeneral<SET_BASE>();
                        filterContext.HttpContext.Cache.Insert(BASE_CONTEXT + loginId, SET_BASE, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(5));
                        ViewBag.SET_BASE = SET_BASE;

                        //admin Menu
                        List<AdminFunction> adminFunctions = _commonService.GetGeneralList<AdminFunction>().OrderBy(t => t.ID).ToList()
                                                                           .Concat(selfLayoutFunctions)
                                                                           .Concat(SelfMenu)
                                                                           .ToList(); ;

                    
                        filterContext.HttpContext.Cache.Insert(ADMIN_CONTEXT, adminFunctions, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(5));                        
                        ViewBag.ListAdminFunction = adminFunctions;

                    }

                }

                ViewBag.Role = Roles;
                ViewBag.Lang = _commonService.GetGeneralList<Lang>("Enabled=1 and Deleted=0 and Published=1").ToList();
                ViewBag.Lan = this.LanguageID;
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 因為ActionExecuted是controller執行完
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string loginId = LoginId();

            lock (_siteMapLock)
            {
                if (filterContext.HttpContext.Cache.Get(SITEMAP_CONTEXT + loginId + Session["menutype"].ToString() + this.LanguageID) != null)
                {

                    ViewBag.MenuList = filterContext.HttpContext.Cache.Get(MENU_CONTEXT + loginId);
                }
                else
                {
                    string[] roles = Roles;

                    List<Menu> LeftMenu = _commonService.GetMenuByRoles(roles, this.LanguageID, CheckIsAdmin(), Session["menutype"].ToString(),this.Account).ToList();

                    ViewBag.MenuList = LeftMenu;

                    filterContext.HttpContext.Cache.Insert(MENU_CONTEXT + loginId + Session["menutype"].ToString() + this.LanguageID, LeftMenu, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
                }
            }

            base.OnActionExecuted(filterContext);
        }

        public string LoginId()
        {
            var identity = (ClaimsIdentity)User.Identity;

            var name = identity.FindFirst(ClaimTypes.NameIdentifier);

            return name == null ? "" : name.Value;
        }



        protected bool CheckIsAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var Role = identity.FindFirst(ClaimTypes.Role);

            string RoleName = Role == null ? "" : Role.Value;
            return RoleName == "administrators";
        }

        public string id
        {
            get
            {
                var user = Request.GetOwinContext().Authentication.User;

                return user.FindFirst("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider").Value;
            }
        }

        public string LanguageID
        {
            get
            {
                var user = Request.GetOwinContext().Authentication.User;
                var langclaim = user.FindFirst("Language");
                if (langclaim != null) { return langclaim.Value; }
                else
                {
                    return "1";
                }

            }
        }

        public string Account
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;

                var name = identity.FindFirst(ClaimTypes.NameIdentifier);

                return name == null ? "" : name.Value;
            }
        }
        public string UserName
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;

                var name = identity.FindFirst(ClaimTypes.Name);

                return name == null ? "" : name.Value;
            }
        }
        public string[] Roles
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.FindFirst(ClaimTypes.Role);

                return name == null ? new string[] { } : new string[] { name.Value };
            }

        }

    }
}