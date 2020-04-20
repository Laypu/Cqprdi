using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Filter;
using Oaww.ViewModel;
using Oaww.Entity;
using Oaww.Entity.SET;
using Oaww.Utility;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class EPaperController : BaseController
    {
        // GET: webadmin/EPaper
        [AuthFilter(_FuncionID = "Model/Index")]
        public ActionResult Index()
        {
            //ViewBag.Title = SET_MESSAGE.M_MESSAGE05;
            //ViewBag.Link = SET_MESSAGE.M_MESSAGE06;
            return View("~/Areas/webadmin/Views/EPaper/Index.cshtml");
        }

    }
}