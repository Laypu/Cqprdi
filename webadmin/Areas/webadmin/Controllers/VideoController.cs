using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Filter;
using Oaww.ViewModel;
using Oaww.ViewModel.Search;
using Oaww.Entity;
using Oaww.Entity.SET;
using Oaww.Utility;
using System.Configuration;
using System.IO;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class VideoController : BaseController
    {
        CommonService _commonService = new CommonService();
        MenuService _menuService = new MenuService();
        VideoService _service = new VideoService();
        private SET_BASE SET_BASE;

        public VideoController()
        {
           
            SET_BASE = _commonService.GetGeneral<SET_BASE>();
        }
        #region Grid
        [AuthFilter(_FuncionID = "Model/Index")]
        public ActionResult Index()
        {
            ViewBag.Title = "影音管理";
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
            ModelVideoMain modelAMLMain = _service.GetModelVideoMain(id.ToString());

            string result = _commonService.UpdateSeq<ModelVideoMain>(id.Value, seq, this.LanguageID, this.Account, this.UserName);



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
            List<VideoItem> videoItems = _service.GetVideoByModelID(idlist);

            string result = _service.Delete(idlist, delaccount, this.LanguageID, this.Account, this.UserName);

            if (result == "刪除成功")
            {
                var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadImage\\VideoItem\\";

                foreach (var items in videoItems)
                {
                    //刪除File
                    if (items.UploadFileName.IsNullOrEmpty() == false)
                    {
                        items.UploadFilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadFile" + items.UploadFilePath;

                        if (System.IO.File.Exists(items.UploadFilePath))
                        {
                            System.IO.File.Delete(items.UploadFilePath);
                        }
                    }
                    if (items.ImageFileName.IsNullOrEmpty() == false)
                    {

                        if (System.IO.File.Exists(oldroot + "\\" + items.ImageFileName))
                        {
                            System.IO.File.Delete(oldroot + "\\" + items.ImageFileName);
                        }
                    }

                    if (items.RelateImageFileName.IsNullOrEmpty() == false)
                    {

                        if (System.IO.File.Exists(oldroot + "\\" + items.RelateImageFileName))
                        {
                            System.IO.File.Delete(oldroot + "\\" + items.RelateImageFileName);
                        }
                    }
                }


            }

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


                str = _menuService.AddUnit<ModelVideoMain>(name, this.LanguageID, this.Account, 9) ? "新增成功" : "新增失敗";



            }
            else
            {

                str = _menuService.UpdateUnit<ModelVideoMain>(name, mainid, this.Account, "9") ? "修改成功" : "修改失敗";


            }
            return Json(str);

        }

        #endregion

        #region Item Page
        public ActionResult ModelItem(string mainid, string menuindex)
        {
            mainid = mainid.AntiXss();

            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }


            var grouplist = _commonService.GetAllGroupSelectList<GroupVideo>(mainid);
            grouplist.Insert(0, new System.Web.Mvc.SelectListItem() { Text = "全部", Value = "" });
            ViewBag.grouplist = grouplist;
            ViewBag.mainid = mainid;
            var maindata = _service.GetModelVideoMain(mainid);

            if (maindata.ID > 0) { ViewBag.Title = maindata.Name; }
            ViewBag.SET_BASE = SET_BASE;

            return View();
        }

        public ActionResult PagingItem(MessageSearchModel model)
        {
            return Json(_service.PagingItem(model.ModelID.ToString(), model));
        }

        public ActionResult VideoEdit(string mainid, string itemid = "-1")
        {
            mainid = mainid.AntiXss();

            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }
            ViewBag.isview = Request["isview"] == null ? "" : Request["isview"].ToString().AntiXss();

            ViewBag.grouplist = _commonService.GetAllGroupSelectList<GroupVideo>(mainid);
            VideoEditModel model = null;
            model = _service.GetModelByID(mainid, itemid);

            model.Introduction = HttpUtility.HtmlDecode(model.Introduction);

            return View(model);
        }

        public ActionResult SaveItem(VideoEditModel model)
        {
            if (model.UploadFile != null)
            {
                model.UploadFileName = model.UploadFile.FileName.Split('\\').Last();
                var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];
                if (uploadfilepath.IsNullOrEmpty())
                {
                    uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile";
                }
                var newpath = uploadfilepath + "\\VideoItem\\";
                if (System.IO.Directory.Exists(newpath) == false)
                {
                    System.IO.Directory.CreateDirectory(newpath);
                }
                var guid = Guid.NewGuid();
                var filename = DateTime.Now.Ticks + "." + model.UploadFile.FileName.Split('.').Last();
                var path = newpath + filename;
                model.UploadFilePath = "\\VideoItem\\" + filename;
                model.UploadFile.SaveAs(path);
            }

            if (model.ImageFile != null)
            {
                var root = Request.PhysicalApplicationPath;
                model.ImageFileOrgName = model.ImageFile.FileName.Split('\\').Last();
                var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];
                if (uploadfilepath.IsNullOrEmpty())
                {
                    uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile";
                }
                var newfilename = DateTime.Now.Ticks + "_" + model.ImageFileOrgName;
                var path = root + "\\UploadImage\\VideoItem\\" + newfilename;
                if (System.IO.Directory.Exists(root + "\\UploadImage\\VideoItem\\") == false)
                {
                    System.IO.Directory.CreateDirectory(root + "\\UploadImage\\VideoItem\\");
                }
                model.ImageFile.SaveAs(path);
                model.ImageFileName = newfilename;
            }

            if (model.RelateImageFile != null)
            {
                var root = Request.PhysicalApplicationPath;
                model.RelateImageFileOrgName = model.RelateImageFile.FileName.Split('\\').Last();
                var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];
                if (uploadfilepath.IsNullOrEmpty())
                {
                    uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile";
                }
                var newfilename = DateTime.Now.Ticks + "_" + model.RelateImageFileOrgName;
                var path = root + "\\UploadImage\\VideoItem\\" + newfilename;
                if (System.IO.Directory.Exists(root + "\\UploadImage\\VideoItem\\") == false)
                {
                    System.IO.Directory.CreateDirectory(root + "\\UploadImage\\VideoItem\\");
                }
                model.RelateImageFile.SaveAs(path);
                model.RelateImageName = newfilename;
            }

            model.Introduction = HttpUtility.UrlDecode(model.Introduction);
            if (model.ItemID == -1)
            {

                string result = _service.CreateItem(model, this.LanguageID, this.Account, this.UserName);


                return Content(result);
            }
            else
            {
                VideoEditModel Premodel = _service.GetModelByID(model.ModelID.ToString(), model.ItemID.ToString());

                VideoItem olddata = _service.GetVideoItemByItemID(model.ItemID.ToString());

                string result = _service.UpdateItem(model, this.LanguageID, this.Account, this.UserName);

                if (result == "修改成功")
                {
                    var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadImage\\VideoItem\\";
                    var fileroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadFile";


                    //刪除舊檔案
                    if (model.UploadFileName.IsNullOrEmpty())
                    {
                        if (System.IO.File.Exists(fileroot + olddata.UploadFilePath))
                        {
                            System.IO.File.Delete(fileroot + olddata.UploadFilePath);
                        }
                        model.UploadFilePath = "";
                        model.UploadFileName = "";
                    }
                    else
                    {
                        if (olddata.UploadFileName != model.UploadFileName)
                        {
                            if (System.IO.File.Exists(fileroot + olddata.UploadFilePath))
                            {
                                System.IO.File.Delete(fileroot + olddata.UploadFilePath);
                            }
                        }
                    }
                    if (model.ImageFileName.IsNullOrEmpty())
                    {

                        if (System.IO.File.Exists(oldroot + "\\" + olddata.ImageFileName))
                        {
                            System.IO.File.Delete(oldroot + "\\" + olddata.ImageFileName);
                        }
                        model.ImageFileOrgName = "";
                        model.ImageFileName = "";
                    }
                    else
                    {

                        if (olddata.ImageFileName != model.ImageFileName)
                        {
                            if (System.IO.File.Exists(oldroot + "\\" + olddata.ImageFileName))
                            {
                                System.IO.File.Delete(oldroot + "\\" + olddata.ImageFileName);
                            }
                        }
                    }

                    if (model.RelateImageName.IsNullOrEmpty())
                    {

                        if (System.IO.File.Exists(oldroot + "\\" + olddata.RelateImageFileName))
                        {
                            System.IO.File.Delete(oldroot + "\\" + olddata.RelateImageFileName);
                        }
                        model.RelateImageFileOrgName = "";
                        model.RelateImageName = "";
                    }
                    else
                    {

                        if (olddata.RelateImageFileName != model.RelateImageName)
                        {
                            if (System.IO.File.Exists(oldroot + "\\" + olddata.RelateImageFileName))
                            {
                                System.IO.File.Delete(oldroot + "\\" + olddata.RelateImageFileName);
                            }
                        }
                    }


                }

                return Content(result);
            }
        }

        public ActionResult FileDownLoad(string modelid, string itemid)
        {
            VideoItem model = _service.GetVideoItemByItemID(itemid);

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

        /// <summary>
        /// 更新item Seq
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="id"></param>
        /// <param name="seq"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult UpdateItemSeq(int modelid, int id, int seq, string type)
        {
            VideoItem AMLItem = _service.GetVideoItemByItemID(id.ToString());

            string result = _commonService.UpdateItemSeq<VideoItem>(id, seq, modelid.ToString(), this.Account, this.UserName,this.LanguageID);


            return Json(result);
        }

        /// <summary>
        /// 更新Item 有效/無效
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult SetItemStatus(string id, bool status, string type)
        {
            string result = _commonService.SetItemStatusWithNoUnPublish<VideoItem>(id, status, this.Account, this.UserName);


            return Content(result);
        }

        /// <summary>
        /// 刪除Item
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="delaccount"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult SetItemDelete(string[] idlist, string delaccount, string type)
        {

            List<VideoItem> videoItems = _service.GetVideo(idlist);

            string result = _commonService.DeleteItem<VideoItem>(idlist, delaccount, this.Account, this.UserName, "VideoItem", 9);

            var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\VideoItem\\";

            if (result == "刪除成功")
            {
                videoItems.ForEach(t =>
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

                    if (t.RelateImageFileName.IsNullOrEmpty() == false)
                    {

                        if (System.IO.File.Exists(oldroot + "\\" + t.RelateImageFileName))
                        {
                            System.IO.File.Delete(oldroot + "\\" + t.RelateImageFileName);
                        }
                    }
                });
            }
            return Content(result);
        }

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
                var root = Request.PhysicalApplicationPath + "/UploadImage/VideoItem/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }

                upload.SaveAs(root + filename);

                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/UploadImage/VideoItem/" + filename);
                var vMessage = string.Empty;
                result = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + imageUrl + "\", \"" + vMessage + "\");</script></body></html>";
            }
            return Json(new
            {
                uploaded = 1,
                fileName = filename,
                url = imageUrl
            });
            //return Content(result);

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
                var root = Request.PhysicalApplicationPath + "/UploadFile/VideoItem/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/UploadFile/VideoItem/" + filename);
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

        #endregion

        #region 類別管理
        public ActionResult GroupEdit(string mainid)
        {
            mainid = mainid.AntiXss();

            ViewBag.mainid = mainid;
            return View();
        }

        /// <summary>
        /// 編輯類別
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="mainid"></param>
        /// <returns></returns>
        public ActionResult EditGroup(string name, string id, string mainid)
        {
            if (id == "-1" || id.IsNullOrEmpty())
            {
                string result = _commonService.EditGroup<GroupVideo>(name, id, mainid, this.Account);

                return Json(result);
            }
            else
            {
                GroupVideo groupAML = _service.GetGroupVideoByID(id);

                string result = _commonService.EditGroup<GroupVideo>(name, id, mainid, this.Account);

                return Json(result);
            }

        }

        /// <summary>
        /// Grid Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult PagingGroup(SearchModelBase model)
        {
            return Json(_commonService.PagingGroup<GroupVideo>(model));
        }

        /// <summary>
        /// 編輯Group Seq
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seq"></param>
        /// <param name="mainid"></param>
        /// <returns></returns>
        public ActionResult EditGroupSeq(int? id, int seq, string mainid)
        {
            GroupVideo modelAMLMain = _service.GetGroupVideoByID(id.ToString());

            string result = _commonService.UpdateGroupSeq<GroupVideo>(id.Value, seq, mainid, this.Account, this.UserName);

            return Json(result);
        }

        /// <summary>
        /// 刪除Group
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="delaccount"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult SetGroupDelete(string[] idlist, string delaccount, string type)
        {
            string result = _commonService.DeleteGroup<GroupVideo, VideoItem>(idlist, delaccount, this.Account, this.UserName);

            return Content(result);
        }

        public ActionResult SetGroupStatus(string id, bool status, string account, string username)
        {
            string result = _commonService.UpdateGroupStatus<GroupVideo>(id, status, this.Account, this.UserName);

            return Content(result);
        }
        #endregion

        #region UnitSetting
        /// <summary>
        /// 單元設定
        /// </summary>
        /// <param name="mainid"></param>
        /// <returns></returns>
        public ActionResult UnitSetting(string mainid)
        {
            mainid = mainid.AntiXss();

            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }

            ViewBag.modelid = mainid;
            var model = _commonService.GetUnitModel<VideoUnitSettingModel, VideoUnitSetting>(mainid);
            var maindata = _service.GetModelVideoMain(mainid);

            if (maindata.ID > 0) { ViewBag.Title = maindata.Name; }
            return View(model);
        }

        /// <summary>
        /// Unit Setting 存檔
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveUnit(VideoUnitSettingModel model)
        {
            var Premodel = _commonService.GetUnitModel<VideoUnitSettingModel, VideoUnitSetting>(model.MainID.ToString());

            string result = _commonService.SetUnitModel<VideoUnitSettingModel, VideoUnitSetting>(model, this.Account);


            return Content(result);
        }

        #endregion
    }
}