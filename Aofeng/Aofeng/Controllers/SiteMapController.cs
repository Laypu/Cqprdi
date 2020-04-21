using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Entity;
using Oaww.Business;
using Oaww.Utility;
using Oaww.ViewModel;
using Oaww.ViewModel.SiteMap;
using Oaww.Entity.SiteMap;

namespace Aofeng.Controllers
{
    public class SiteMapController : BaseController
    {
        // GET: SiteMap
        SiteMapService _service = new SiteMapService();
        public ActionResult Index(string itemid)
        {
            if (itemid.IsNullOrEmpty()) { return RedirectToAction("Index", "Home"); }

            
            SiteMapItem SiteMapitemmodel = _service.GetSiteMapItemByModelID(itemid);
            List<SiteMapKey> SiteMapKeymodel = _service.GetListSiteMapKeyByModelID(SiteMapitemmodel.ModelID.ToString());
            ViewBag.SiteMapHtmlContent = SiteMapitemmodel.HtmlContent;
            ViewBag.SiteMapmodel = SiteMapKeymodel;
            return View();
        }
    }
}