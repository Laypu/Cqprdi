using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Filter;
using Oaww.ViewModel;
using Oaww.ViewModel.SiteMap;
using Oaww.Entity;
using Oaww.Entity.SET;
using Oaww.Business;
using Oaww.Utility;
using System.Configuration;
using System.IO;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class SiteMapController : BaseController
    {
        CommonService _commonService = new CommonService();
        MenuService _menuService = new MenuService();
        SiteMapService _service = new SiteMapService();
        #region Grid

        public ActionResult Index()
        {
            ViewBag.Title = "網站導覽";
            return View();
        }

        /// <summary>
        /// Grid Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PagingMain(SearchModelBase model)
        {
            model.LangId = this.LanguageID;
            return Json(_service.Paging(model));
        }

        /// <summary>
        /// update seq
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seq"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult EditSeq(int? id, int seq, string type)
        {
            ModelSiteMapMain modelAMLMain = _service.GetModelSiteMapMain(id.ToString());

            string result = _commonService.UpdateSeq<ModelSiteMapMain>(id.Value, seq, this.LanguageID, this.Account, this.UserName);
           
            return Json(result);
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="delaccount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetMainDelete(string[] idlist, string delaccount)
        {
            string result = _service.Delete(idlist, delaccount, this.LanguageID, this.Account, this.UserName);

           

            return Content(result);
        }

        /// <summary>
        /// 新增刪除Unit
        /// </summary>
        /// <param name="mainid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult EditUnit(string mainid, string name)
        {

            var str = "";
            if (mainid == "-1" || string.IsNullOrEmpty(mainid))
            {
                str = _menuService.AddUnit<ModelSiteMapMain>(name, this.LanguageID, this.Account, 15) ? "新增成功" : "新增失敗";

               
            }
            else
            {
                str = _menuService.UpdateUnit<ModelSiteMapMain>(name, mainid, this.Account, "15") ? "修改成功" : "修改失敗";

                ModelSiteMapMain modelAMLMain = _service.GetModelSiteMapMain(mainid);

            }
            return Json(str);

        }

        #endregion

        #region Item Page
        public ActionResult SiteMapEdit(string mainid, string itemid = "-1")
        {
            mainid = mainid.AntiXss();

            ViewBag.isview = Request["isview"] == null ? "" : Request["isview"].ToString().AntiXss();
            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }



            SiteMapEditModel model = null;
            model = _service.GetModelByID(mainid);
            model.SET_BASE = _commonService.GetGeneral<SET_BASE>();


            return View(model);
        }
        public ActionResult SaveItem(SiteMapEditModel model)
        {
            model.SiteMapItem.HtmlContent = HttpUtility.UrlDecode(model.SiteMapItem.HtmlContent);

            if (model.siteMapKeys != null)
            {
                model.siteMapKeys.ForEach(t =>
                {
                    t.ModelID = model.SiteMapItem.ModelID.Value;
                });
            }

            if (model.SiteMapItem.ItemID <= 0)
            {

                string result = _service.CreateItem(model, this.LanguageID, this.Account, this.UserName);
               

                return Content(result);
            }
            else
            {
                SiteMapEditModel Premodel = _service.GetModelByID(model.SiteMapItem.ModelID.ToString());

                string result = _service.UpdateItem(model, this.LanguageID, this.Account, this.UserName);               

                return Content(result);
            }
        }
        #endregion
    }
}