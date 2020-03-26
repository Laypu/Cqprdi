using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.ViewModel;
using Oaww.Business;
using Oaww.Entity;
using Oaww.Utility;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class DictionaryController : BaseController
    {
        CommonService _commonService = new CommonService();
        DictionaryService _service = new DictionaryService();

        #region Grid
        public ActionResult Index(string Category = "")
        {
            var para = _commonService.GetGeneralList<DictionaryPara>();
            para.Add(new DictionaryPara() { ParaValue = "", ParaName = "" });
            ViewBag.grouplist = para;

            ViewBag.Category = Category;
            return View();
        }

        public ActionResult PagingMain(SearchModelBase model)
        {

            return Json(_service.Paging(model));
        }

        public ActionResult EditSeq(int? id, int seq, string Category)
        {
            Dictionary Dictionary = _commonService.GetGeneral<Dictionary>("ID=@ID", new Dictionary<string, string>() { { "ID", id.ToString() } });

            string result = _service.UpdateSeq(id.Value, seq, Category);


            return Json(result);
        }

        [HttpPost]
        public ActionResult SetMainDelete(string[] idlist, string Category)
        {
            string result = _service.Delete(idlist, Category);

            return Content(result);
        }

        public ActionResult SetStatus(string id, bool status)
        {
            string result = _commonService.SetStatus<Dictionary>(id, status);

            return Content(result);
        }

        public ActionResult DictionaryEdit(string ID, string Category, string Type = "")
        {
            Dictionary dictionary = new Dictionary();
            if (ID.IsNullOrEmpty())
            {
                dictionary.Category = Category;
                dictionary.IsInsert = "A";
                dictionary.Type = Type;


            }
            else
            {
                dictionary = _commonService.GetGeneral<Dictionary>("ID=@ID", new Dictionary<string, string>() { { "ID", ID } });
                dictionary.Type = Type;
                dictionary.IsInsert = "M";
            }

           


            return View(dictionary);
        }

        public ActionResult SaveItem(Dictionary model)
        {
            return Content(_service.SaveItem(model, this.Account));
        }
        #endregion


    }
}