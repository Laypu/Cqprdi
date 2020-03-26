using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Filter;
using Oaww.Entity.SET;
using Oaww.Entity;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class ModelController : BaseController
    {
        CommonService _commonService = new CommonService();
        ModelService _service = new ModelService();

        [AuthFilter(_FuncionID = "Model/Index")]
        public ActionResult Index()
        {
            Session["menuNowID"] = 0;

            List<SET_MENU> ListSET_MENU = _commonService.GetGeneralList<SET_MENU>("M_Menu03=1").ToList();

            return View(ListSET_MENU);
        }

        /// <summary>
        /// 全文檢索管理
        /// </summary>
        /// <returns></returns>
        [AuthFilter(_FuncionID = "Model/IndexSetting")]
        public ActionResult IndexSetting()
        {
            Session["menuNowID"] = 0;

            var model = _service.GetPageIndexSetting(this.LanguageID);
            return View(model);
        }

        /// <summary>
        /// 全文檢索存檔
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SaveIndexSetting(PageIndexSetting model)
        {
            string result = _service.SetPageIndexSettingModel(model, this.LanguageID, this.Account);          

            return Content(result);
        }

        /// <summary>
        /// 照片上傳
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="CKEditorFuncNum"></param>
        /// <param name="CKEditor"></param>
        /// <param name="langCode"></param>
        /// <returns></returns>
        public ActionResult Upload(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string result = "";
            var filename = "";
            var imageUrl = "";
            if (upload != null && upload.ContentLength > 0)
            {
                //儲存圖片至Server
                var last = upload.FileName.Split('.').Last();
                filename = DateTime.Now.Ticks + "." + last;
                var root = Request.PhysicalApplicationPath + "/UploadImage/PageIndex/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/UploadImage/PageIndex/" + filename);
                var vMessage = string.Empty;
                result = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + imageUrl + "\", \"" + vMessage + "\");</script></body></html>";
            }
            return Json(new
            {
                uploaded = 1,
                fileName = filename,
                url = imageUrl
            });

        }

        public ActionResult UploadFile(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string result = "";
            var filename = "";
            var imageUrl = "";
            if (upload != null && upload.ContentLength > 0)
            {
                var last = upload.FileName.Split('.').Last();
                filename = DateTime.Now.Ticks + "." + last;
                var root = Request.PhysicalApplicationPath + "/UploadFile/PageIndex/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/UploadFile/PageIndex/" + filename);
                var vMessage = string.Empty;
                result = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + imageUrl + "\", \"" + vMessage + "\");</script></body></html>";
            }
            return Json(new
            {
                uploaded = 1,
                fileName = filename,
                url = imageUrl
            });


        }
    }
}