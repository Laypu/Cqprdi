using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aofeng.Controllers
{
    public class EmptyController : Controller
    {
        public ActionResult Pswp_box()
        {
            return PartialView();
        }
    }
}