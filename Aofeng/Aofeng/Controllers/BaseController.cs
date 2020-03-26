using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Entity;
using System.Web.Caching;
using Oaww.Utility;
using System.Configuration;
using System.IO;

namespace Aofeng.Controllers
{
    public class BaseController : Controller
    {
        private readonly object _siteMapLock = new object();
        private const string Portal_Context = "_PortalContext_";
        CommonService _commonService = new CommonService();
        MenuService _menuService = new MenuService();
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private int? _Lang = 1;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            int? langid = 1;

            if (requestContext.HttpContext.Session == null)
            {
                requestContext.HttpContext.Session["LangID"] = langid.ToString();
                Session.Timeout = 600;
                _Lang = langid;
            }
            else if (requestContext.HttpContext.Session["LangID"] == null)
            {
                var DefaultLang = System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultLang"];

                var alllang = _commonService.GetGeneralList<Lang>();
                if (alllang != null)
                {
                    if (alllang.Any(v => v.Lang_Name == DefaultLang))
                    {
                        langid = alllang.Where(v => v.Lang_Name == DefaultLang).First().ID;
                    }
                }
                requestContext.HttpContext.Session["LangID"] = langid.ToString();
                Session.Timeout = 600;
                _Lang = langid;
            }
            else
            {

                int _langid = 1;
                if (int.TryParse(requestContext.HttpContext.Session["LangID"].ToString(), out _langid) == false)
                {
                    langid = 1;
                    _Lang = langid;
                }
                else
                {
                    langid = _langid;
                    _Lang = langid;
                }

            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            lock (_siteMapLock)
            {
                if (filterContext.HttpContext.Cache.Get(Portal_Context + _Lang.ToString()) != null)
                {
                    ViewBag.TopMenu = filterContext.HttpContext.Cache.Get(Portal_Context + _Lang.ToString());
                    ViewBag.UpMenu = filterContext.HttpContext.Cache.Get(Portal_Context + _Lang.ToString() + "UP");
                    ViewBag.DownMenu = filterContext.HttpContext.Cache.Get(Portal_Context + _Lang.ToString() + "Down");
                    ViewBag.SEO = filterContext.HttpContext.Cache.Get("SEO" + _Lang.ToString());
                    ViewBag.SiteFlow = filterContext.HttpContext.Cache.Get("SiteFlow" + _Lang.ToString());
                    ViewBag.ADKanban = filterContext.HttpContext.Cache.Get("ADKanban" + _Lang.ToString());                    
                    ViewBag.ListLang = filterContext.HttpContext.Cache.Get("Lang");
                }
                else
                {
                    #region MainMenu
                    List<Menu> TopMenu = _commonService.GetMenu(_Lang.ToString(), "1", true).ToList();

                    filterContext.HttpContext.Cache.Insert(Portal_Context + _Lang.ToString(), TopMenu, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);

                    ViewBag.TopMenu = TopMenu;
                    #endregion

                    #region UpMenu
                    List<Menu> UpMenu = _commonService.GetMenu(_Lang.ToString(), "2", true).ToList();

                    filterContext.HttpContext.Cache.Insert(Portal_Context + _Lang.ToString() + "UP", UpMenu, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);

                    ViewBag.UpMenu = UpMenu;
                    #endregion

                    #region DownMenu
                    List<Menu> DownMenu = _commonService.GetMenu(_Lang.ToString(), "3", true).ToList();

                    filterContext.HttpContext.Cache.Insert(Portal_Context + _Lang.ToString() + "Down", DownMenu, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);

                    ViewBag.DownMenu = DownMenu;
                    #endregion

                    #region SEO
                    SEO SEO = _commonService.GetSEOByTypeNameAndID("Main", _Lang.ToString(), "");
                    SEO = SEO ?? new SEO();
                    filterContext.HttpContext.Cache.Insert("SEO" + _Lang.ToString(), SEO, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
                    ViewBag.SEO = SEO;
                    #endregion

                    #region SiteFlow
                    SiteFlow siteFlow = _commonService.GetSiteFlow();
                    siteFlow = siteFlow ?? new SiteFlow();
                    filterContext.HttpContext.Cache.Insert("SiteFlow" + _Lang.ToString(), siteFlow, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
                    ViewBag.SiteFlow = siteFlow;
                    #endregion

                    #region About Us
                    ADKanban ADKanban = _commonService.GetKanbanByID("6", this.LanguageID.ToString());
                    ADKanban = ADKanban ?? new ADKanban();
                    filterContext.HttpContext.Cache.Insert("ADKanban" + _Lang.ToString(), ADKanban, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
                    ViewBag.ADKanban = ADKanban;
                    #endregion

                    #region 多語系

                    List<Lang> ListLang = _commonService.GetGeneralList<Lang>("Enabled=1 and Published=1").ToList();
                    filterContext.HttpContext.Cache.Insert("Lang" , ListLang, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
                    ViewBag.ListLang = ListLang;
                    #endregion
                }
            }

            #region ModelName for title
            List<Menu> listMenu = (List<Menu>)ViewBag.TopMenu;
            List<Menu> listUpMenu = (List<Menu>)ViewBag.UpMenu;
            List<Menu> listDownMenu = (List<Menu>)ViewBag.DownMenu;

            List<Menu> AllMenu = new List<Menu>();
            AllMenu.AddRange(listMenu);
            AllMenu.AddRange(listUpMenu);
            AllMenu.AddRange(listDownMenu);
            ViewBag.AllMenu = AllMenu;

            string modelName = string.Empty;
            var mid = string.Empty;

            if (filterContext.HttpContext.Request.HttpMethod == "POST")
            {
                mid = filterContext.ActionParameters.ContainsKey("mid") == false ? "" : filterContext.ActionParameters["mid"].ToString();
            }
            else
            {
                mid = filterContext.HttpContext.Request.QueryString["mid"];
            }

            if (!string.IsNullOrEmpty(mid))
            {
                modelName = AllMenu.Where(t => t.ID.ToString() == mid).First().MenuName;
            }

            string controller = filterContext.RouteData.Values["controller"] == null ? "" : filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"] == null ? "" : filterContext.RouteData.Values["action"].ToString();

            if (controller == "Public" && action == "GenericError")
            {
                SiteLayout siteLayout = _commonService.GetSiteLayout();
                modelName = siteLayout.Page404Title;
            }


            ViewBag.modelName = modelName;
            ViewBag.mid = mid.AntiXss();

            #endregion

            #region unitSetting
            UnitSetting unitSetting = new UnitSetting();


            SiteLayout SiteLayout = _commonService.GetGeneral<SiteLayout>("LangID=@LangID",new Dictionary<string, string>() { {"LangID", _Lang.ToString() } });
            string StyleImage = "~/UploadImage/SiteLayout/" + SiteLayout.InnerLogoImgNameOri;

            if (!string.IsNullOrEmpty(mid))
            {
                Menu menu = AllMenu.Where(t => t.ID.ToString() == mid).First();

                if(menu.ImgNameOri.IsNullOrEmpty() == false)
                {
                    StyleImage = "~/UploadImage/MenuImage/" + menu.ImgNameOri;
                }
                

                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                //取得UnitSetting
                if (menu.ModelID.HasValue && (menu.ModelID.Value == 1 || menu.ModelID.Value == 11))
                {
                    unitSetting = _commonService.GetUnitSetting("PageUnitSetting", menu.ModelItemID.ToString());
                    if(menu.ModelID == 1)
                    {
                        ViewBag.PrintUrl = urlHelper.Action("Index", "Page", new { @itemid = menu.ModelItemID, @mid = menu.ID, @print= true });
                        ViewBag.ForwardUrl = urlHelper.Action("Index", "Page", new { @itemid = menu.ModelItemID, @mid = menu.ID });
                    }
                    else if (menu.ModelID == 11)
                    {
                        ViewBag.PrintUrl = urlHelper.Action("Index", "Active", new { @itemid = menu.ModelItemID, @mid = menu.ID, @print = true });
                        ViewBag.ForwardUrl = urlHelper.Action("Index", "Active", new { @itemid = menu.ModelItemID, @mid = menu.ID });
                    }
                   
                }
                else if (menu.ModelID.HasValue && _menuService.ChkModelInMessage(menu.ModelID.Value.ToString()))
                {
                    string ItemID = filterContext.ActionParameters["itemid"] == null ? "" : filterContext.ActionParameters["itemid"].ToString();

                    unitSetting = _commonService.GetUnitSetting("MessageUnitSetting", menu.ModelItemID.ToString());

                    if(menu.ModelID == 2)
                    {
                        ViewBag.PrintUrl = urlHelper.Action("MessageView", "Message", new { @itemid = ItemID, @mid = menu.ID, @isPrint = true });
                        ViewBag.ForwardUrl = urlHelper.Action("Index", "Message", new { @itemid = menu.ModelItemID, @mid = menu.ID });
                    }
                    else if(menu.ModelID == 3)
                    {
                        ViewBag.PrintUrl = urlHelper.Action("MessageView", "FileDownload", new { @itemid = ItemID, @mid = menu.ID, @isPrint = true });
                        ViewBag.ForwardUrl = urlHelper.Action("Index", "FileDownload", new { @itemid = menu.ModelItemID, @mid = menu.ID });
                    }
                    else if (menu.ModelID == 7)
                    {
                        ViewBag.PrintUrl = urlHelper.Action("MessageView", "Gallery", new { @itemid = ItemID, @mid = menu.ID, @isPrint = true });
                        ViewBag.ForwardUrl = urlHelper.Action("Index", "Gallery", new { @itemid = menu.ModelItemID, @mid = menu.ID });
                    }
                    else if (menu.ModelID == 10)
                    {
                        ViewBag.PrintUrl = urlHelper.Action("MessageView", "History", new { @itemid = ItemID, @mid = menu.ID, @isPrint = true });
                        ViewBag.ForwardUrl = urlHelper.Action("Index", "History", new { @itemid = menu.ModelItemID, @mid = menu.ID });
                    }
                }
                else if (menu.ModelID.HasValue && menu.ModelID.Value == 3)
                {
                    string nowpage = string.Empty;
                    string group = string.Empty;
                    string group2 = string.Empty;
                    string jumpPage = string.Empty;
                    nowpage = filterContext.ActionParameters["nowpage"] == null ? "0" : filterContext.ActionParameters["nowpage"].ToString();
                    group = filterContext.ActionParameters["group"] == null ? "-1" : filterContext.ActionParameters["group"].ToString();
                    group2 = filterContext.ActionParameters["group2"] == null ? "0" : filterContext.ActionParameters["group2"].ToString();
                    jumpPage = filterContext.ActionParameters["jumpPage"] == null ? "0" : filterContext.ActionParameters["jumpPage"].ToString();

                    unitSetting = _commonService.GetUnitSetting("FileDownloadUnitSetting", menu.ModelItemID.ToString());
                    ViewBag.PrintUrl = urlHelper.Action("Index", "FileDownload", new { @itemid = menu.ModelItemID, @mid = menu.ID, @group2 = group2, @group = group, @nowpage = nowpage, @jumpPage = jumpPage, @Print = true });
                    ViewBag.ForwardUrl = urlHelper.Action("Index", "FileDownload", new { @itemid = menu.ModelItemID, @mid = menu.ID, @group2 = group2, @group = group, @nowpage = nowpage, @jumpPage = jumpPage });
                }

                else if (menu.ModelID.HasValue && menu.ModelID.Value == 9)
                {
                    string ItemID = filterContext.ActionParameters["itemID"] == null ? "" : filterContext.ActionParameters["itemID"].ToString();

                    unitSetting = _commonService.GetUnitSetting("VideoUnitSetting", menu.ModelItemID.ToString());
                    ViewBag.PrintUrl = urlHelper.Action("VideoView", "Video", new { @itemid = ItemID, @mid = menu.ID, @print = true });
                    ViewBag.ForwardUrl = urlHelper.Action("Index", "Video", new { @itemid = menu.ModelItemID, @mid = menu.ID });
                }

                //如果沒有設定的話，預設10筆
                unitSetting.ShowCount = unitSetting.ShowCount.HasValue ? unitSetting.ShowCount.Value : 10;
            }

            ViewBag.StyleImage = StyleImage;


            ViewBag.UnitSetting = unitSetting == null ? new UnitSetting() : unitSetting;
            #endregion

            ViewBag.Url = filterContext.HttpContext.Request.Url.AbsoluteUri.AntiXss();
        }

        public ActionResult FileDownLoad(string FID, bool Multi = false)
        {
            string filepath = string.Empty;
            string oldfilename = string.Empty;
            if (Multi == false)
            {
                MessageItem messageItem = _commonService.GetGeneral<MessageItem>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", FID } });
                filepath = messageItem.UploadFilePath;
                oldfilename = messageItem.UploadFileName;
            }
            else
            {
                MessageFile messageFile = _commonService.GetGeneral<MessageFile>("FID=@FID", new Dictionary<string, string>() { { "FID", FID } });
                filepath = messageFile.UploadFilePath;
                oldfilename = messageFile.UploadFileName;
            }


            var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];
            if (uploadfilepath.IsNullOrEmpty())
            {
                uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile";
            }
            if (filepath != "")
            {
                string filename = System.IO.Path.GetFileName(filepath);
                if (string.IsNullOrEmpty(oldfilename)) { oldfilename = filename; }
                Stream iStream = new FileStream(uploadfilepath + filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return File(iStream, "application/octet-stream", oldfilename);
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        public int LanguageID
        {
            get
            {
                return _Lang.Value;

            }
        }
    }
}