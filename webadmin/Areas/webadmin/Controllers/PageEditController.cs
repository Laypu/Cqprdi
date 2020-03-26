using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Filter;
using Oaww.ViewModel;
using Oaww.Business;
using Oaww.Entity;
using Oaww.Entity.SET;
using Oaww.Utility;
using System.Configuration;
using System.IO;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class PageEditController : BaseController
    {
        PageService _service;
        MenuService _menuService = new MenuService();
        CommonService _commonService = new CommonService();
        protected int _ModelID = 1;
        protected string _Path = "PageEdit";
        private SET_PAGE SET_PAGE;
        public PageEditController()
        {
            _service = new PageService(_ModelID);           
            SET_PAGE = _commonService.GetGeneral<SET_PAGE>("M_PAGE03=@ModelID", new Dictionary<string, string>() { { "ModelID", _ModelID.ToString() } });
        }
        public PageEditController(int ModelID = 1,string Path= "PageEdit")
        {            
            _service = new PageService(ModelID);
            _ModelID = ModelID;
            _Path = Path;
            SET_PAGE = _commonService.GetGeneral<SET_PAGE>("M_PAGE03=@ModelID", new Dictionary<string, string>() { { "ModelID", ModelID.ToString() } });
        }
        #region Grid
        /// <summary>
        /// 圖文編輯模組
        /// </summary>
        /// <returns></returns>
        [AuthFilter(_FuncionID = "Model/Index")]
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = SET_PAGE.M_PAGE01;
            return View("~/Areas/webadmin/Views/PageEdit/Index.cshtml");
        }

        /// <summary>
        /// Grid Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PagingMain(SearchModelBase model)
        {
            model.LangId = this.LanguageID;
            return Json(_service.Paging(model, _ModelID));
        }

        /// <summary>
        /// 修改單元名稱
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUnit(string id, string name)
        {
            var str = "";
            if (id == "-1")
            {
                str = _menuService.AddUnit<ModelPageEditMain>(name, this.LanguageID, this.Account, _ModelID) ? "新增成功" : "新增失敗";
            }
            else
            {
                str = _menuService.UpdateUnit<ModelPageEditMain>(name, id, this.Account, _ModelID.ToString()) ? "修改成功" : "修改失敗";

                ModelPageEditMain modelAMLMain = _service.GetModelPageEditMainByID(int.Parse(id));
            }

            return Json(str);
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
            List<PageIndexItem> pageIndexItems = _service.GetPageIndexItemsByModelID(idlist);

            string result = _service.Delete(idlist, delaccount, this.LanguageID, this.Account, this.UserName);
            if (result == "刪除成功")
            {
                var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\{_Path}\\";

                pageIndexItems.ForEach(t =>
                {
                    //刪除File
                    if (t.UploadFileName.IsNullOrEmpty() == false)
                    {
                        t.UploadFilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadFile" + t.UploadFilePath;

                        if (System.IO.File.Exists(t.UploadFilePath))
                        {
                            System.IO.File.Delete(t.UploadFilePath);
                        }
                    }
                    if (t.ImageFileName.IsNullOrEmpty() == false)
                    {

                        if (System.IO.File.Exists(oldroot + "\\" + t.ImageFileName))
                        {
                            System.IO.File.Delete(oldroot + "\\" + t.ImageFileName);
                        }
                    }

                    //刪除File
                    if (t.UploadFileName.IsNullOrEmpty() == false)
                    {
                        t.UploadFilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadFile" + t.UploadFilePath;

                        if (System.IO.File.Exists(t.UploadFilePath))
                        {
                            System.IO.File.Delete(t.UploadFilePath);
                        }
                    }
                    if (t.ImageFileName.IsNullOrEmpty() == false)
                    {

                        if (System.IO.File.Exists(oldroot + "\\" + t.ImageFileName))
                        {
                            System.IO.File.Delete(oldroot + "\\" + t.ImageFileName);
                        }
                    }
                });

            }
            return Content(result);

        }

        /// <summary>
        /// 變更Seq
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seq"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSeq(int? id, int seq, string type)
        {
            ModelPageEditMain modelAMLMain = _service.GetModelPageEditMainByID(id.Value);

            string result = _service.UpdateSeq(id.Value, seq, this.LanguageID, this.Account, this.UserName, _ModelID);



            return Json(result);
        }
        #endregion

        #region Item Modify
        /// <summary>
        /// Model Edit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public ActionResult ModelItem(string id, string itemid)
        {
            ViewBag.isview = Request["isview"] == null ? "" : Request["isview"].ToString().AntiXss();
            if (string.IsNullOrWhiteSpace(id)) { return RedirectToAction("Index"); }

            ViewBag.ModelItemList = _service.GetSelectList(id);
            ViewBag.Title = "圖文編輯";
            PageEditItemModel model = null;
            if (itemid.IsNullOrEmpty())
            {
                model = _service.GetFirstModel(id);
            }
            else
            {
                model = _service.GetModelByID(id, itemid.ToString());
            }

            model.HtmlContent = HttpUtility.HtmlDecode(model.HtmlContent);
            model.SET_BASE = _commonService.GetGeneral<SET_BASE>();
            model.SET_PAGE = SET_PAGE;
            return View("~/Areas/webadmin/Views/PageEdit/ModelItem.cshtml", model);
        }

        /// <summary>
        /// CKEditor Upload
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
                var last = upload.FileName.Split('.').Last();
                filename = DateTime.Now.Ticks + "." + last;
                var root = Request.PhysicalApplicationPath + $"/UploadImage/{_Path}/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + $"/UploadImage/{_Path}/" + filename);
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
                var root = Request.PhysicalApplicationPath + $"/UploadFile/{_Path}/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + $"/UploadFile/{_Path}/" + filename);
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

        /// <summary>
        /// Item 存檔
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveItem(PageEditItemModel model)
        {
            var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];

            if (uploadfilepath.IsNullOrEmpty())
            {
                uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile";
            }


            if (model.UploadFile != null)
            {
                model.UploadFileName = model.UploadFile.FileName.Split('\\').Last();
                
                var newpath = uploadfilepath + $"\\{_Path}\\";
                if (System.IO.Directory.Exists(newpath) == false)
                {
                    System.IO.Directory.CreateDirectory(newpath);
                }
                var guid = Guid.NewGuid();
                var filename = DateTime.Now.Ticks + "." + model.UploadFile.FileName.Split('.').Last();
                var path = newpath + filename;
                model.UploadFilePath = $"\\{_Path}\\" + filename;
                model.UploadFile.SaveAs(path);
            }

            if (model.ImageFile != null)
            {
                var root = Request.PhysicalApplicationPath;
                model.ImageFileOrgName = model.ImageFile.FileName.Split('\\').Last();
                var newfilename = DateTime.Now.Ticks + "_" + model.ImageFileOrgName;
                var path = root + $"\\UploadImage\\{_Path}\\" + newfilename;
                if (System.IO.Directory.Exists(root + $"\\UploadImage\\{_Path}\\") == false)
                {
                    System.IO.Directory.CreateDirectory(root + $"\\UploadImage\\{_Path}\\");
                }
                model.ImageFile.SaveAs(path);
                model.ImageFileName = newfilename;

            }


            if (model.PageImages != null)
            {
                int i = 0;
                model.PageImages.ForEach(t =>
                {
                    if (t.UploadFile != null)
                    {
                        t.UploadFileName = t.UploadFile.FileName.Split('\\').Last();

                        var newpath = uploadfilepath + $"\\{SET_PAGE.M_PAGE02}\\";
                        if (System.IO.Directory.Exists(newpath) == false)
                        {
                            System.IO.Directory.CreateDirectory(newpath);
                        }

                        var guid = Guid.NewGuid();
                        var filename = (DateTime.Now.Ticks + i) + "." + t.UploadFile.FileName.Split('.').Last();

                        var path = newpath + filename;

                        t.UploadFilePath = $"\\{SET_PAGE.M_PAGE02}\\" + filename;


                        t.UploadFile.SaveAs(path);
                    }

                    t.ModelID = model.ModelID.Value;
                    t.ItemID = model.ItemID.Value;

                    i++;
                });
            }

            model.HtmlContent = HttpUtility.UrlDecode(model.HtmlContent);
            model.LangID = LanguageID;
            if (model.ItemID == -1)
            {

                string result = _service.CreatePageEdit(model, this.LanguageID, this.Account);


                return Content(result);
            }
            else
            {
                PageEditItemModel Premodel = _service.GetModelByID(model.ModelID.ToString(), model.ItemID.ToString());

                PageIndexItem olddata = _service.GetPageIndexItemByItemID(model.ItemID.ToString());

                model.ImageFileOrgName = model.ImageFile != null ? model.ImageFileOrgName  //新上傳
                                                         : (model.ImageFileName.IsNullOrEmpty()
                                                            ? ""  //砍掉
                                                            : olddata.ImageFileOrgName); //不變

                string result = _service.EditPageItem(model, this.Account);

                var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\{_Path}\\";

                //刪除舊檔案
                if (model.UploadFileName.IsNullOrEmpty())
                {

                    if (System.IO.File.Exists(olddata.UploadFilePath) && olddata.UploadFilePath != olddata.UploadFilePath)
                    {
                        System.IO.File.Delete(olddata.UploadFilePath);
                    }
                    model.UploadFilePath = "";
                    model.UploadFileName = "";
                }
                else
                {
                    if (olddata.UploadFileName != model.UploadFileName && olddata.UploadFilePath != olddata.UploadFilePath)
                    {
                        if (System.IO.File.Exists(olddata.UploadFilePath))
                        {
                            System.IO.File.Delete(olddata.UploadFilePath);
                        }
                    }

                }

                if (model.ImageFileName.IsNullOrEmpty() && olddata.ImageFileName != olddata.ImageFileName)
                {

                    if (System.IO.File.Exists(oldroot + "\\" + olddata.ImageFileName) && model.ImageFileName != olddata.ImageFileName)
                    {
                        System.IO.File.Delete(oldroot + "\\" + olddata.ImageFileName);
                    }
                    model.ImageFileOrgName = "";
                    model.ImageFileName = "";
                }
                else
                {

                    if (olddata.ImageFileName != model.ImageFileName && olddata.ImageFileName != olddata.ImageFileName)
                    {
                        if (System.IO.File.Exists(oldroot + "\\" + olddata.ImageFileName))
                        {
                            System.IO.File.Delete(oldroot + "\\" + olddata.ImageFileName);
                        }
                    }
                }


                return Content(result);
            }
        }
        #endregion

        #region 單元設定
        /// <summary>
        /// 單元設定
        /// </summary>
        /// <param name="modelid"></param>
        /// <returns></returns>
        public ActionResult UnitSetting(string modelid)
        {
            modelid = modelid.AntiXss();

            if (modelid.IsNullOrEmpty()) { return RedirectToAction("Index"); }

            ViewBag.modelid = modelid;
            var model = _service.GetUnitModel(modelid);
            return View("~/Areas/webadmin/Views/PageEdit/UnitSetting.cshtml", model);
        }

        public ActionResult SaveUnit(PageUnitSettingModel model)
        {
            string result = _service.SetUnitModel(model, this.Account);

            return Content(result);
        }

        #endregion

        public ActionResult ImageDownLoad(string FID)
        {

            PageImage fileDownloadFile = _service.GetPageImage(FID);
            string filepath = fileDownloadFile.UploadFilePath;
            string oldfilename = fileDownloadFile.UploadFileName;


            var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];
            if (uploadfilepath.IsNullOrEmpty())
            {
                uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile\\";
            }
            if (filepath != "")
            {
                string filename = System.IO.Path.GetFileName(filepath);
                if (string.IsNullOrEmpty(oldfilename)) { oldfilename = filename; }
                Stream iStream = new FileStream(uploadfilepath + filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return File(iStream, "application/octet-stream", oldfilename);
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        public ActionResult FileDownLoad2(string modelid, string itemid)
        {
            var model = _service.GetModelByID(modelid, itemid);
            string filepath = model.UploadFilePath;
            string oldfilename = model.UploadFileName;
            var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];
            if (uploadfilepath.IsNullOrEmpty())
            {
                uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile";
            }
            if (filepath != "")
            {
                string filename = System.IO.Path.GetFileName(filepath);
                if (string.IsNullOrEmpty(oldfilename)) { oldfilename = filename; }
                Stream iStream = new FileStream(uploadfilepath + filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return File(iStream, "application/octet-stream", oldfilename);
            }
            else
            {
                return RedirectToAction("Error");
            }

        }
    }
}