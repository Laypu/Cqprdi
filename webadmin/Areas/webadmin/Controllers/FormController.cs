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
using System.Data.SqlClient;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class FormController : BaseController
    {
        MenuService _menuService = new MenuService();
        CommonService _commonService = new CommonService();
        FormService _service = new FormService();

        List<SET_MENU_SUB> ListMenuSub;

        public FormController()
        {
            ListMenuSub = _commonService.GetGeneralList<SET_MENU_SUB>("M_MenuSub04='4' and M_MenuSub04='1' order by Sort").ToList();
        }

        #region Grid
        [AuthFilter(_FuncionID = "Model/Index")]
        public ActionResult Index()
        {
            ViewBag.Title = "表單管理";
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
            ModelFormMain modelAMLMain = _service.GetModelFormMain(id.ToString());

            string result = _commonService.UpdateSeq<ModelFormMain>(id.Value, seq, this.LanguageID, this.Account, this.UserName);

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
                str = _menuService.AddUnit<ModelFormMain>(name, this.LanguageID, this.Account, 4) ? "新增成功" : "新增失敗";

                if (str == "新增成功")
                {

                    var itemNew = _commonService.GetGeneralList<ModelFormMain>().Last().ID;


                    FormItemSettingModel Fmodel = new FormItemSettingModel()
                    {
                        MainID = itemNew,
                        SelList = null,
                        Description = "",
                        Title = "姓名",
                        ItemMode = 1,//是textbox
                        TextLength = "20",
                        RowNum = null,
                        ColumnNum = "10",
                        DefaultText = ""


                    };
                    _service.EditSelItem(Fmodel);
                    FormItemSettingModel FmodelEmail = new FormItemSettingModel()
                    {
                        MainID = itemNew,
                        SelList = null,
                        Description = "",
                        Title = "Email",
                        ItemMode = 1,//是textbox
                        TextLength = "30",
                        RowNum = null,
                        ColumnNum = "20",
                        DefaultText = ""


                    };
                    _service.EditSelItem(FmodelEmail);
                }
            
            }
            else
            {

                str = _menuService.UpdateUnit<ModelFormMain>(name, mainid, this.Account, "4") ? "修改成功" : "修改失敗";
               
            }
            return Json(str);

        }
        #endregion

        #region Main Mail

        public ActionResult MailModelItem(string mainid)
        {
            mainid = mainid.AntiXss();

            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }

            ViewBag.mainid = mainid;
            ViewBag.mainname = _service.MainModelName(mainid);


            ViewBag.SET_MENU_SUB = ListMenuSub;

            return View();
        }

        public ActionResult PagingMailItem(MailSearchModel model)
        {
            model.LangId = this.LanguageID;
            return Json(_service.PagingMail(model));
        }

        public ActionResult SetMailDelete(string[] idlist, string delaccount, string type)
        {

            string result = _service.SetMailDelete(idlist, delaccount, this.Account, this.UserName);


            return Content(result);


        }

        public ActionResult ExportExcel(MailSearchModel searchModel, string fname)
        {

            if (fname.IsNullOrEmpty()) { fname = "資料下載"; }
            string _fname = System.Web.HttpUtility.UrlEncode(fname + ".xlsx", System.Text.Encoding.UTF8);
            Response.AddHeader("Content-Disposition", "attachment; filename='" + _fname + "';filename*=utf-8''" + _fname);
            return File(_service.GetExport(searchModel), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult MailProcess(string itemid)
        {
            if (itemid.IsNullOrEmpty()) { return RedirectToAction("Index"); }

            var model = _service.GetMailInput(itemid);
            model.MainID = model.MainID;
            var id = model.InputKey.IndexOf("聯絡人");
            if (id <= 0)
            {
                id = model.InputKey.IndexOf("姓名");
                
            }

            if (id > 0)
            {
                ViewBag.Title = model.InputValue[id];
            }
            
            return View(model);
        }

        public ActionResult SaveProgressNote(string text, string id)
        {
            return Content(_service.SaveProgressNote(text, id, Account));  //update 到DB因此不寫log
        }

        public ActionResult SaveProgress(string progress, string id)
        {
            return Content(_service.SaveProgress(progress, id)); //update 到DB因此不寫log
        }

        public ActionResult SaveReply(string text, string progress, string id)
        {
            return Content(_service.SaveReply(text, id, Account)); //update 到DB因此不寫log
        }
        #endregion

        #region FormManager

        public ActionResult FormManager(string mainid)
        {
            mainid = mainid.AntiXss();

            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }
            ViewBag.isview = Request["isview"] == null ? "" : Request["isview"].ToString().AntiXss();


            ViewBag.mainid = mainid;
            ViewBag.mainname = _service.MainModelName(mainid);

            ViewBag.SET_MENU_SUB = ListMenuSub;

            return View();
        }

        public ActionResult PagingSelItem(SearchModelBase model)
        {
            model.LangId = this.LanguageID;
            return Json(_service.PagingSelItem(model));
        }

        public ActionResult AddSelItem(int Index)
        {
            ViewBag.index = Request["index"] == null ? "0" : Request["index"];
            ViewBag.seqindex = int.Parse(ViewBag.index) + 1;
            return PartialView();
        }

        public ActionResult SaveSelItem(FormItemSettingModel model)
        {
            string result = _service.EditSelItem(model);

            return Content(result);
        }

        public ActionResult UpdateItemSeq(int modelid, int id, int seq, string type)
        {
            string result = _service.UpdateItemSeq(modelid.ToString(), id, seq, this.Account, this.UserName);

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetItemIsMust(string id, bool status, string type)
        {
            string result = _service.SetItemIsMust(id, status, this.Account, this.UserName);

            return Content(result);
        }

        public ActionResult SetItemDelete(string[] idlist, string delaccount, string type)
        {

            string result = _service.DeleteItem(idlist, delaccount, this.Account, this.UserName);

            return Content(result);

        }


        public ActionResult ItemEdit(string mainid, string id)
        {
            FormItemSettingModel model = null;

            if (id.IsNullOrEmpty() == false)
            {
                model = _service.GetSelItemByID(id);
            }
            else
            {
                model = new FormItemSettingModel();
                model.MainID = int.Parse(mainid);
            }
            ViewBag.mainname = _service.MainModelName(mainid);
            return View(model);
        }
        #endregion

        #region FormSetting
        public ActionResult FormSetting(string mainid)
        {
            mainid = mainid.AntiXss();

            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }

            ViewBag.mainid = mainid;
            var model = _service.GetFormSetting(mainid);
            ViewBag.mainname = _service.MainModelName(mainid);

            ViewBag.SET_MENU_SUB = ListMenuSub;

            return View(model);
        }

        public ActionResult Upload(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string result = "";
            var filename = "";
            var imageUrl = "";
            if (upload != null && upload.ContentLength > 0)
            {
                var last = upload.FileName.Split('.').Last();
                filename = DateTime.Now.Ticks + "." + last;
                var root = Request.PhysicalApplicationPath + "/UploadImage/FormItem/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/UploadImage/FormItem/" + filename);
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
                var root = Request.PhysicalApplicationPath + "/UploadFile/FormItem/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/UploadFile/FormItem/" + filename);
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

        public ActionResult SaveSetting(FormSettingModel model)
        {

            model.FormDesc = HttpUtility.UrlDecode(model.FormDesc);
            model.ConfirmContent = HttpUtility.UrlDecode(model.ConfirmContent);

            string result = _service.SaveSetting(model);


            return Content(result);

        }


        #endregion
    }
}