using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Filter;
using Oaww.ViewModel;
using Oaww.ViewModel.Search;
using Oaww.Entity;
using Oaww.Entity.SET;
using Oaww.Business;
using Oaww.Utility;
using System.Configuration;
using System.IO;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class EPaperController : BaseController
    {
        // GET: webadmin/EPaper
        //[AuthFilter(_FuncionID = "Model/Index")]
        CommonService _commonService = new CommonService();
        public ActionResult Index(string mainid)
        {
            //ViewBag.Title = SET_MESSAGE.M_MESSAGE05;
            //ViewBag.Link = SET_MESSAGE.M_MESSAGE06;

            var grouplist = _commonService.GetAllGroupSelectList<GroupMessage>(mainid);
            ViewBag.grouplist = grouplist;
            return View();
        }

        //public ActionResult PagingMain(SearchModelBase model)
        //{
        //    model.LangId = this.LanguageID;
        //    return Json(_service.Paging(model, _ModelID));
        //}

    }
}