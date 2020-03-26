using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: webadmin/Home
        public ActionResult Index(string menutype = "1", string Auth = "Y")
        {
            Session["menuNowID"] = 0;

            if(Session["menutype"] != null && (Session["menutype"].ToString()=="1" || Session["menutype"].ToString() == "2" || Session["menutype"].ToString() == "3"))
            {
                Session["menutype"] = menutype;
            }
            
            Session.Timeout = 600;

            ViewData["Auth"] = Auth;

            return View();
        }
    }
}