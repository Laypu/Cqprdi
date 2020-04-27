using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Entity;
using Oaww.ViewModel;

namespace Aofeng.Controllers
{
    public class HomeController : BaseController
    {
        HomeService _service = new HomeService();
        CommonService _commonService = new CommonService();
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.ListMainRoller = _commonService.GetAdMainByID("1", this.LanguageID);
            //model.ListMainMobileRoller = _commonService.GetAdMainByID("7", this.LanguageID);
            model.ListThemeAD = _commonService.GetAdMainByID("2", this.LanguageID);
            model.DynamicNews = _commonService.GetKanbanByID("3", this.LanguageID.ToString());
            var LisNewId = model.DynamicNews.Link_Href.Split('=')[1].Split('&')[0];
            model.ListNews = _service.GetMessageItemByModelID(LisNewId, 8, this.LanguageID);
            model.ListFaq = this.LanguageID==1?_service.GetMessageItemByModelID("1024", 5, this.LanguageID): _service.GetMessageItemByModelID("1027", 5, this.LanguageID);//所務訊息
            model.AboutLink = this.LanguageID == 1 ? _service.GetMessageItemByModelID("1025", 5, this.LanguageID): _service.GetMessageItemByModelID("1028", 5, this.LanguageID);//市場動態  
            model.ListImage= this.LanguageID == 1 ? _service.GetMessageItemByModelID("1026", 5, this.LanguageID): _service.GetMessageItemByModelID("1029", 5, this.LanguageID);//研討會訊息
            model.ListDownload= this.LanguageID == 1 ? _service.GetMessageItemByModelID("1023", 5, this.LanguageID): _service.GetMessageItemByModelID("1030", 5, this.LanguageID);//	比賽訊息
            model.QuickLink = _commonService.GetAdMainByID("6", this.LanguageID); //快速連結專區
            model.ListIcon = _commonService.GetAdMainByID("5", this.LanguageID);
            model.NowLanguage = this.LanguageID;
            return View(model);
        }

        public ActionResult ChangeLang(string lang)
        {
            var langlist = _commonService.GetGeneralList<Lang>();
            IList<Lang> uselang;
            if (lang != null)
            {
                uselang = langlist.Where(v => v.ID == int.Parse(lang)).ToList();
            }
            else
            {
                var nowlang = this.LanguageID;
                uselang = langlist.Where(v => v.ID != nowlang).ToList();
                if (uselang.Count() > 0)
                {
                    lang = uselang.First().ID.ToString();
                }
                else
                {
                    lang = this.LanguageID.ToString();
                }
            }
            if (uselang.Count() > 0)
            {
                var dtype = uselang.First().Domain_Type;
                if (dtype == "1")
                {
                    Session["LangID"] = lang;
                }
                else if (dtype == "2")
                {
                    Session["LangID"] = uselang.First().Link_Lang_ID;
                }
                else if (dtype == "3")
                {
                    return Json(uselang.First().Link_Href);
                }
                Session.Timeout = 600;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}