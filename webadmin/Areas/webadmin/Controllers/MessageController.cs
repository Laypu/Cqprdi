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
    public class MessageController : BaseController
    {
        protected CommonService _commonService = new CommonService();
        MenuService _menuService = new MenuService();
        protected MessageService _service;
        protected SET_MESSAGE SET_MESSAGE = new SET_MESSAGE();
        protected int _ModelID = 2;
        private SET_BASE SET_BASE;
        private SET_MENU SET_MENU;
        protected List<SET_MESSAGE_ADD> SET_MESSAGE_ADDs;
        SiteConfigService _siteConfigService = new SiteConfigService();
        public MessageController()
        {
            SET_MESSAGE = _commonService.GetGeneral<SET_MESSAGE>("M_MESSAGE01=@ID", new Dictionary<string, string>() { { "ID", _ModelID.ToString() } });
            _service = new MessageService(_ModelID);
            SET_BASE = _commonService.GetGeneral<SET_BASE>();
            SET_MENU = _commonService.GetGeneral<SET_MENU>("M_Menu01=@ID", new Dictionary<string, string>() { { "ID", _ModelID.ToString() } });
            SET_MESSAGE_ADDs = _commonService.GetGeneralList<SET_MESSAGE_ADD>("M_MESSAGE_ADD01=@ID", new Dictionary<string, string>() { { "ID", _ModelID.ToString() } }).ToList();
        }
        public MessageController(int ModelID = 2)
        {
            SET_MESSAGE = _commonService.GetGeneral<SET_MESSAGE>("M_MESSAGE01=@ID", new Dictionary<string, string>() { { "ID", ModelID.ToString() } });
            _service = new MessageService(ModelID);
            _ModelID = ModelID;
            SET_BASE = _commonService.GetGeneral<SET_BASE>();
            SET_MENU = _commonService.GetGeneral<SET_MENU>("M_Menu01=@ID", new Dictionary<string, string>() { { "ID", _ModelID.ToString() } });
            SET_MESSAGE_ADDs = _commonService.GetGeneralList<SET_MESSAGE_ADD>("M_MESSAGE_ADD01=@ID", new Dictionary<string, string>() { { "ID", _ModelID.ToString() } }).ToList();
        }

        #region Grid
        [AuthFilter(_FuncionID = "Model/Index")]
        public ActionResult Index()
        {
            ViewBag.Title = SET_MESSAGE.M_MESSAGE05;
            ViewBag.Link = SET_MESSAGE.M_MESSAGE06;
            return View("~/Areas/webadmin/Views/Message/Index.cshtml");
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
        /// update seq
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seq"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult EditSeq(int? id, int seq, string type)
        {
            ModelMessageMain modelAMLMain = _service.GetModelMessageMain(id.ToString(), this.LanguageID);

            string result = _commonService.UpdateSeq<ModelMessageMain>(id.Value, seq, this.LanguageID, this.Account, this.UserName, _ModelID);


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
            List<MessageItem> messageItems = _service.GetMessageItemsByModelID(idlist);

            string result = _service.Delete(idlist, delaccount, this.LanguageID, this.Account, this.UserName);

            if (result == "刪除成功")
            {
                var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\{SET_MESSAGE.M_MESSAGE02}\\";

                foreach (var items in messageItems)
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


                str = _menuService.AddUnit<ModelMessageMain>(name, this.LanguageID, this.Account, _ModelID) ? "新增成功" : "新增失敗";


            }
            else
            {

                str = _menuService.UpdateUnit<ModelMessageMain>(name, mainid, this.Account, _ModelID.ToString()) ? "修改成功" : "修改失敗";

            }
            return Json(str);

        }

        #endregion

        #region Item Page
        /// <summary>
        /// Model Item List
        /// </summary>
        /// <param name="mainid"></param>
        /// <param name="menuindex"></param>
        /// <returns></returns>
        public ActionResult ModelItem(string mainid, string menuindex, bool admin = false)
        {
            mainid = mainid.AntiXss();

            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }


            var grouplist = _commonService.GetAllGroupSelectList<GroupMessage>(mainid);
            grouplist.Insert(0, new System.Web.Mvc.SelectListItem() { Text = "全部", Value = "" });
            ViewBag.grouplist = grouplist;
            ViewBag.mainid = mainid.AntiXss();
            ViewBag.admin = admin;
            var maindata = _service.GetModelMessageMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.Title = maindata.Name; }

            ViewBag.SET_BASE = SET_BASE;
            ViewBag.SET_MENU = SET_MENU;
            ViewBag.SET_MESSAGE = SET_MESSAGE;
            ViewBag.SET_MESSAGE_ADDs = SET_MESSAGE_ADDs;

            return View("~/Areas/webadmin/Views/Message/ModelItem.cshtml");
        }

        public ActionResult PagingItem(MessageSearchModel model)
        {
            return Json(_service.PagingItem(model.ModelID.ToString(), model, this.LanguageID));
        }

        public ActionResult MessageEdit(string mainid, string itemid = "-1")
        {
            ViewBag.isview = Request["isview"] == null ? "" : Request["isview"].ToString().AntiXss();
            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }

            ViewBag.grouplist = _commonService.GetAllGroupSelectList<GroupMessage>(mainid);
            MessageEditModel model = null;
            model = _service.GetModelByID(mainid, itemid, SET_MESSAGE.M_MESSAGE02);
            model.Link_Mode = model.Link_Mode.HasValue ? model.Link_Mode.Value : 1;
            model.SET_MESSAGE = SET_MESSAGE;
            model.SET_BASE = SET_BASE;

            return View("~/Areas/webadmin/Views/Message/MessageEdit.cshtml", model);
        }

        public ActionResult SaveItem(MessageEditModel model)
        {
            var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];
            SiteConfig ListConfig = _siteConfigService.GetALLSiteConfig("1");
            if (uploadfilepath.IsNullOrEmpty())
            {
                uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile";
            }
            if (model.fileDownloadFiles != null)
            {
                int i = 0;
                model.fileDownloadFiles.ForEach(t =>
                {
                    if (t.UploadFile != null)
                    {
                        t.UploadFileName = t.UploadFile.FileName.Split('\\').Last();


                        var newpath = uploadfilepath + $"\\{SET_MESSAGE.M_MESSAGE02}\\";
                        if (System.IO.Directory.Exists(newpath) == false)
                        {
                            System.IO.Directory.CreateDirectory(newpath);
                        }

                        var guid = Guid.NewGuid();
                        var filename = (DateTime.Now.Ticks + i) + "." + t.UploadFile.FileName.Split('.').Last();

                        var path = newpath + filename;

                        t.UploadFilePath = $"\\{SET_MESSAGE.M_MESSAGE02}\\" + filename;
                        t.UploadFileSize = t.UploadFile.ContentLength / 1024;
                        t.UploadFileType = t.UploadFile.FileName.Split('.').Last().GetFileTypeByExtension();

                        t.UploadFile.SaveAs(path);
                    }

                    t.ModelID = model.ModelID;
                    t.ItemID = model.ItemID;

                    i++;
                });
            }


            if (model.messageImages != null)
            {
                int i = 0;
                model.messageImages.ForEach(t =>
                {
                    if (t.UploadFile != null)
                    {
                        t.UploadFileName = t.UploadFile.FileName.Split('\\').Last();


                        var newpath = uploadfilepath + $"\\{SET_MESSAGE.M_MESSAGE02}\\";
                        if (System.IO.Directory.Exists(newpath) == false)
                        {
                            System.IO.Directory.CreateDirectory(newpath);
                        }

                        var guid = Guid.NewGuid();
                        var filename = (DateTime.Now.Ticks + i) + "." + t.UploadFile.FileName.Split('.').Last();

                        var path = newpath + filename;

                        t.UploadFilePath = $"\\{SET_MESSAGE.M_MESSAGE02}\\" + filename;


                        t.UploadFile.SaveAs(path);
                    }

                    t.ModelID = model.ModelID;
                    t.ItemID = model.ItemID;

                    i++;
                });
            }

            if (model.UploadFile != null)
            {
                model.UploadFileName = model.UploadFile.FileName.Split('\\').Last();

                if (uploadfilepath.IsNullOrEmpty())
                {
                    uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile";
                }
                var newpath = uploadfilepath + $"\\{SET_MESSAGE.M_MESSAGE02}\\";
                if (System.IO.Directory.Exists(newpath) == false)
                {
                    System.IO.Directory.CreateDirectory(newpath);
                }
                var guid = Guid.NewGuid();
                var filename = DateTime.Now.Ticks + "." + model.UploadFile.FileName.Split('.').Last();
                var path = newpath + filename;
                model.UploadFilePath = $"\\{SET_MESSAGE.M_MESSAGE02}\\" + filename;
                model.UploadFile.SaveAs(path);
                model.UploadFileSize = model.UploadFile.ContentLength / 1024;
                model.UploadFileType = model.UploadFile.FileName.Split('.').Last().GetFileTypeByExtension();
            }
            

            if (model.ImageFile != null)
            {
                var root = Request.PhysicalApplicationPath;
                model.ImageFileOrgName = model.ImageFile.FileName.Split('\\').Last();


                var newfilename = DateTime.Now.Ticks + "_" + model.ImageFileOrgName;
                var path = root + $"\\UploadImage\\{SET_MESSAGE.M_MESSAGE02}\\" + newfilename;
                if (System.IO.Directory.Exists(root + $"\\UploadImage\\{SET_MESSAGE.M_MESSAGE02}\\") == false)
                {
                    System.IO.Directory.CreateDirectory(root + $"\\UploadImage\\{SET_MESSAGE.M_MESSAGE02}\\");
                }
                model.ImageFile.SaveAs(path);
                model.ImageFileName = newfilename;
            }

            if (model.RelateImageFile != null)
            {
                var root = Request.PhysicalApplicationPath;
                model.RelateImageFileOrgName = model.RelateImageFile.FileName.Split('\\').Last();


                var newfilename = DateTime.Now.Ticks + "_" + model.RelateImageFileOrgName;
                var path = root + $"\\UploadImage\\{SET_MESSAGE.M_MESSAGE02}\\" + newfilename;
                if (System.IO.Directory.Exists(root + $"\\UploadImage\\{SET_MESSAGE.M_MESSAGE02}\\") == false)
                {
                    System.IO.Directory.CreateDirectory(root + $"\\UploadImage\\{SET_MESSAGE.M_MESSAGE02}\\");
                }
                model.RelateImageFile.SaveAs(path);
                model.RelateImageName = newfilename;
            }

            model.HtmlContent = HttpUtility.UrlDecode(model.HtmlContent);
            model.Title = HttpUtility.UrlDecode(model.Title);
            if (model.ItemID == -1)
            {

                string result = _service.CreateItem(model, this.LanguageID, this.Account, this.UserName);

                ListConfig.LastUpdateDate = DateTime.Now.ToString("yyy/MM/dd");
                _siteConfigService.Update(ListConfig);
                return Content(result);
            }
            else
            {
                MessageEditModel Premodel = _service.GetModelByID(model.ModelID.ToString(), model.ItemID.ToString(), SET_MESSAGE.M_MESSAGE02);

                MessageItem olddata = _service.GetMessageItemByID(model.ItemID.ToString());

                string result = _service.UpdateItem(model, this.LanguageID, this.Account, this.UserName);


                if (result == "修改成功")
                {
                    ListConfig.LastUpdateDate = DateTime.Now.ToString("yyy/MM/dd");
                    _siteConfigService.Update(ListConfig);
                    var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\{SET_MESSAGE.M_MESSAGE02}\\";

                    //刪除舊檔案,如果還在就不刪除
                    Premodel.fileDownloadFiles.ForEach(t =>
                    {
                        if (model.fileDownloadFiles.Where(s => t.UploadFilePath == s.UploadFilePath).Count() == 0)
                        {
                            if (System.IO.File.Exists(uploadfilepath + t.UploadFilePath))
                            {
                                System.IO.File.Delete(uploadfilepath + t.UploadFilePath);
                            }
                        }
                    });

                    //刪除舊檔案
                    if (model.UploadFileName.IsNullOrEmpty())
                    {
                        if (System.IO.File.Exists(olddata.UploadFilePath))
                        {
                            System.IO.File.Delete(olddata.UploadFilePath);
                        }
                        model.UploadFilePath = "";
                        model.UploadFileName = "";
                    }
                    else
                    {
                        if (olddata.UploadFileName != model.UploadFileName)
                        {
                            if (System.IO.File.Exists(olddata.UploadFilePath))
                            {
                                System.IO.File.Delete(olddata.UploadFilePath);
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

        public ActionResult FileDownLoad(string FID)
        {


            string filepath = string.Empty;
            string oldfilename = string.Empty;

            if (SET_MESSAGE.M_MESSAGE04)
            {
                MessageFile fileDownloadFile = _service.GetFileDownloadFile(FID);
                filepath = fileDownloadFile.UploadFilePath;
                oldfilename = fileDownloadFile.UploadFileName;
            }
            else
            {
                MessageItem messageItem = _service.GetMessageItemByID(FID);
                filepath = messageItem.UploadFilePath;
                oldfilename = messageItem.UploadFileName;
            }

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
            var model = _service.GetModelByID(modelid, itemid, SET_MESSAGE.M_MESSAGE02);

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
        public ActionResult ImageDownLoad(string FID)
        {

            MessageImage fileDownloadFile = _service.GetFileDownloadImage(FID);
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
            MessageItem AMLItem = _service.GetMessageItemByID(id.ToString());

            string result = _commonService.UpdateItemSeq<MessageItem>(id, seq, modelid.ToString(), this.Account, this.UserName, this.LanguageID);


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
            string result = _commonService.SetItemStatus<MessageItem>(id, status, this.Account, this.UserName);


            return Content(result);
        }

        public ActionResult SetHomeStatus(string id, bool status, string type)
        {
            string result = _commonService.SetHomeStatus<MessageItem>(id, status, this.Account, this.UserName);


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
            List<MessageItem> messageItems = _service.GetMessageItems(idlist);

            string result = _commonService.DeleteItem<MessageItem>(idlist, delaccount, this.Account, this.UserName, SET_MESSAGE.M_MESSAGE02, _ModelID);

            if (result == "刪除成功")
            {
                var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\{SET_MESSAGE.M_MESSAGE02}\\";

                foreach (var items in messageItems)
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
                var root = Request.PhysicalApplicationPath + $"/UploadImage/{SET_MESSAGE.M_MESSAGE02}/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }

                upload.SaveAs(root + filename);

                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + $"/UploadImage/{SET_MESSAGE.M_MESSAGE02}/" + filename);
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
                var root = Request.PhysicalApplicationPath + $"/UploadFile/{SET_MESSAGE.M_MESSAGE02}/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + $"/UploadFile/{SET_MESSAGE.M_MESSAGE02}/" + filename);
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
            return View("~/Areas/webadmin/Views/Message/GroupEdit.cshtml");
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
                string result = _commonService.EditGroup<GroupMessage>(name, id, mainid, this.Account);

                return Json(result);
            }
            else
            {
                GroupMessage groupAML = _service.GetGroupMessageByID(id);

                string result = _commonService.EditGroup<GroupMessage>(name, id, mainid, this.Account);

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
            return Json(_commonService.PagingGroup<GroupMessage>(model));
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
            GroupMessage modelAMLMain = _service.GetGroupMessageByID(id.ToString());

            string result = _commonService.UpdateGroupSeq<GroupMessage>(id.Value, seq, mainid, this.Account, this.UserName);



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
            string result = _commonService.DeleteGroup<GroupMessage, MessageItem>(idlist, delaccount, this.Account, this.UserName);

            return Content(result);
        }

        public ActionResult SetGroupStatus(string id, bool status, string account, string username)
        {
            string result = _commonService.UpdateGroupStatus<GroupMessage>(id, status, this.Account, this.UserName);


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
            ViewBag.mainid = mainid;
            ViewBag.modelid = mainid;
            var model = _commonService.GetUnitModel<MessageUnitSettingModel, MessageUnitSetting>(mainid);
            var maindata = _service.GetModelMessageMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.Title = maindata.Name; }

            if (model.columnSettings.Count == 0)
            {
                model.columnSettings.Add(new ColumnSetting() { Type = "Message", MainID = maindata.ID, ColumnKey = "No", ColumnName = "序號", Used = true, Sort = 1 });
                model.columnSettings.Add(new ColumnSetting() { Type = "Message", MainID = maindata.ID, ColumnKey = "PublicshDate", ColumnName = "發佈日期", Used = true, Sort = 2 });
                model.columnSettings.Add(new ColumnSetting() { Type = "Message", MainID = maindata.ID, ColumnKey = "Title", ColumnName = "標題", Used = true, Sort = 3 });
                model.columnSettings.Add(new ColumnSetting() { Type = "Message", MainID = maindata.ID, ColumnKey = "GroupName", ColumnName = "類別", Used = true, Sort = 4 });
                model.columnSettings.Add(new ColumnSetting() { Type = "Message", MainID = maindata.ID, ColumnKey = "LinkUrl", ColumnName = "相關連結", Used = true, Sort = 5 });
                model.columnSettings.Add(new ColumnSetting() { Type = "Message", MainID = maindata.ID, ColumnKey = "UploadFileDesc", ColumnName = "檔案下載", Used = true, Sort = 6 });
                model.columnSettings.Add(new ColumnSetting() { Type = "Message", MainID = maindata.ID, ColumnKey = "UnPublishDate", ColumnName = "下架時間", Used = true, Sort = 7 });
            }



            ViewBag.SET_MESSAGE = SET_MESSAGE;
            ViewBag.SET_MESSAGE_ADDs = SET_MESSAGE_ADDs;

            return View("~/Areas/webadmin/Views/Message/UnitSetting.cshtml", model);
        }

        /// <summary>
        /// Unit Setting 存檔
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveUnit(MessageUnitSettingModel model)
        {
            //var Premodel = _commonService.GetUnitModel<MessageUnitSettingModel, MessageUnitSetting>(model.MainID.ToString());
            model.IntroductionHtml =  HttpUtility.UrlDecode(model.IntroductionHtml);
            string result = _commonService.SetUnitModel<MessageUnitSettingModel, MessageUnitSetting>(model, this.Account, "Message", model.MainID.Value.ToString());

            return Content(result);
        }

        #endregion
    }
}