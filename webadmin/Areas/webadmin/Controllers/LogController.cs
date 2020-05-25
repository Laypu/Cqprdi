using Oaww.Business;
using Oaww.Utility;
using Oaww.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class LogController : BaseController
    {
        LogService _Servies = new LogService();
        // GET: webadmin/Log
        public ActionResult Index()
        {

            List<SelectListItem> ListCategory = _Servies.GetCategory().Select(t => new SelectListItem() { Value = t, Text = t })
                                                                     .ToList()
                                                                     .AddDefault();
            List<SelectListItem> ListGetGroup = _Servies.GetGroup().Select(t => new SelectListItem() { Value = t.ID, Text = t.Group_Name})
                                                                  .ToList()
                                                                  .AddDefault();
            ViewData["ListCategory"] = ListCategory;
            ViewData["ListGetGroup"] = ListGetGroup;
            return View();
        }
        public ActionResult Paging(LogSearchModel model)
        {
            return Json(_Servies.Paging(model));
        }
    }
}