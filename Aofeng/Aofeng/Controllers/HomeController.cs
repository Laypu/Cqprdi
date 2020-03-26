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
            model.ListThemeAD = _commonService.GetAdMainByID("2", this.LanguageID);
            model.DynamicNews = _commonService.GetKanbanByID("3", this.LanguageID.ToString());
            model.ListNews = _service.GetMessageItemByModelID("1022", 8, this.LanguageID);

            model.QuickLink = _commonService.GetAdMainByID("6", this.LanguageID); //快速連結專區
            model.ListIcon = _commonService.GetAdMainByID("5", this.LanguageID);
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