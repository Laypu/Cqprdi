using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aofeng.Controllers
{
    public class PublicController : BaseController
    {
        // GET: Public
        public ActionResult FileNotFound()
        {
            return View();
        }
    }
}