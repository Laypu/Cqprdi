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
    public class EPaperController : MenuController
    {
        protected CommonService _commonService = new CommonService();
        protected EPaperService _service;
        protected int _ModelID = 12;
        protected SET_EPAPER SET_EPAPER = new SET_EPAPER();


        private SET_BASE SET_BASE;
        private SET_MENU SET_MENU;
        //增加電子報模組
        MenuService _menuService = new MenuService();

        public EPaperController()
        {
            SET_EPAPER = _commonService.GetGeneral<SET_EPAPER>("M_EPAPER01=@ID", new Dictionary<string, string>() { { "ID", _ModelID.ToString() } });
            _service = new EPaperService(_ModelID);
            SET_BASE = _commonService.GetGeneral<SET_BASE>();
            SET_MENU = _commonService.GetGeneral<SET_MENU>("M_Menu01=@ID", new Dictionary<string, string>() { { "ID", _ModelID.ToString() } });

        }
        public EPaperController(int ModelID = 12)
        {
            SET_EPAPER = _commonService.GetGeneral<SET_EPAPER>("M_EPAPER01=@ID", new Dictionary<string, string>() { { "ID", ModelID.ToString() } });
            _service = new EPaperService(ModelID);
            _ModelID = ModelID;
            SET_BASE = _commonService.GetGeneral<SET_BASE>();
            SET_MENU = _commonService.GetGeneral<SET_MENU>("M_Menu01=@ID", new Dictionary<string, string>() { { "ID", _ModelID.ToString() } });

        }

        [AuthFilter(_FuncionID = "Model/Index")]
        public ActionResult Index()
        {
            ViewBag.Title = SET_EPAPER.M_EPAPER05;
            ViewBag.Link = SET_EPAPER.M_EPAPER06;

            return View();
        }


        [HttpPost]
        public ActionResult SetMainDelete(string[] idlist, string delaccount)
        {
            
                List<EPaperItem> EPaperItem = _service.GetEPaperItems(idlist);

                string result = _service.Delete(idlist, delaccount, this.LanguageID, this.Account, this.UserName);

                //if (result == "刪除成功")
                //{
                //    var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\{SET_EPAPER.M_EPAPER02}\\";

                //    foreach (var items in EPaperItem)
                //    {
                //        //刪除File
                //        if (items.UploadFileName.IsNullOrEmpty() == false)
                //        {
                //            items.UploadFilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadFile" + items.UploadFilePath;

                //            if (System.IO.File.Exists(items.UploadFilePath))
                //            {
                //                System.IO.File.Delete(items.UploadFilePath);
                //            }
                //        }
                //        if (items.ImageFileName.IsNullOrEmpty() == false)
                //        {

                //            if (System.IO.File.Exists(oldroot + "\\" + items.ImageFileName))
                //            {
                //                System.IO.File.Delete(oldroot + "\\" + items.ImageFileName);
                //            }
                //        }


                //    }


                //}

                return Content(result);
           
        }


        //電子報模組呈現
        public ActionResult PagingMain(SearchModelBase model)
        {
            model.LangId = this.LanguageID;
            return Json(_service.Paging(model, _ModelID));
        }





        //增加電子報模組
        public ActionResult EditUnit(string mainid, string name)
        {

            var str = "";
            if (mainid == "-1" || string.IsNullOrEmpty(mainid))
            {


                str = _menuService.AddUnit<ModelEPaperMain>(name, this.LanguageID, this.Account, _ModelID) && _service.SaveToColumnSetting(_ModelID) ? "新增成功" : "新增失敗";


            }
            else
            {

                str = _menuService.UpdateUnit<ModelEPaperMain>(name, mainid, this.Account, _ModelID.ToString()) ? "修改成功" : "修改失敗";

            }
            return Json(str);

        }

        public ActionResult ModelItem(string menuindex, string mainid, bool admin = false)
        {
            mainid = mainid.AntiXss();

            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }


            var grouplist = _commonService.GetAllGroupSelectList<GroupEPaper>(mainid, int.Parse(this.LanguageID));
            grouplist.Insert(0, new System.Web.Mvc.SelectListItem() { Text = "全部", Value = "" });
            ViewBag.grouplist = grouplist;
            ViewBag.mainid = mainid.AntiXss();
            ViewBag.admin = admin;
            var maindata = _service.GetModelEPaperMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.Title = maindata.Name; }

            ViewBag.SET_BASE = SET_BASE;
            ViewBag.SET_MENU = SET_MENU;
            ViewBag.SET_EPAPER = SET_EPAPER;


            return View("~/Areas/webadmin/Views/EPaper/ModelItem.cshtml");
        }




        //GROUP 在DATATABLE顯示
        public ActionResult PagingGroup(SearchModelBase model)
        {
            return Json(_commonService.PagingGroup<GroupEPaper>(model, int.Parse(this.LanguageID)));
        }

        public ActionResult GroupEdit(string mainid)
        {
            mainid = mainid.AntiXss();

            ViewBag.mainid = mainid;

            var maindata = _service.GetModelEPaperMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.ModelItemTitle = maindata.Name; }


            return View("~/Areas/webadmin/Views/EPaper/GroupEdit.cshtml");
        }


        //排序GROUP(SORT)
        public ActionResult EditGroupSeq(int? id, int seq, string mainid)
        {
            GroupEPaper modelAMLMain = _service.GetGroupEPaperByID(id.ToString());

            string result = _commonService.UpdateGroupSeq<GroupEPaper>(id.Value, seq, mainid, this.Account, this.UserName);



            return Json(result);
        }

        //刪除GROUP
        public ActionResult SetGroupDelete(string[] idlist, string delaccount, string type)
        {
            string result = _commonService.DeleteGroup<GroupEPaper, EPaperItem>(idlist, delaccount, this.Account, this.UserName);

            return Content(result);
        }


        public ActionResult EditGroup(string name, string id, string mainid)
        {
            if (id == "-1" || id.IsNullOrEmpty())
            {
                string result = _commonService.EditGroupEPaper<GroupEPaper>(name, id, mainid, this.Account, int.Parse(this.LanguageID));

                return Json(result);
            }
            else
            {
                GroupEPaper groupAML = _service.GetGroupEPaperByID(id);

                string result = _commonService.EditGroupEPaper<GroupEPaper>(name, id, mainid, this.Account, int.Parse(this.LanguageID));

                return Json(result);
            }

        }

        public ActionResult PagingItem(EPaperSearchModel model)
        {
            model.LangId = this.LanguageID;
            return Json(_service.PagingItem(model, model.ModelID.ToString()));
        }


        public ActionResult EPaperEdit(string mainid, string itemid = "-1")
        {
            ViewBag.isview = Request["isview"] == null ? "" : Request["isview"].ToString().AntiXss();
            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }
            EPaperEditModel model = null;
            ViewBag.grouplist = _commonService.GetAllGroupSelectList<GroupEPaper>(mainid, int.Parse(this.LanguageID));
            ViewBag.GroupID = new SelectList(ViewBag.grouplist, "GroupID", "Group_Name");
            ViewBag.mainid = mainid;
            var EPaperItemdata = _service.GetEPaperItemByID(itemid);

            if (EPaperItemdata.ItemID > 0) { ViewBag.Title = EPaperItemdata.Title; }

            var maindata = _service.GetModelEPaperMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.ModelItemTitle = maindata.Name; }

            model = _service.GetModelByID(mainid, itemid, SET_EPAPER.M_EPAPER02);
            model.SET_EPAPER = SET_EPAPER;
            model.SET_BASE = SET_BASE;
            //model = _service.GetEPaperItemByID(itemid);
            return View("~/Areas/webadmin/Views/EPaper/EpaperEdit.cshtml", model);


        }

        //#region SaveItem
        public ActionResult SaveItem(EPaperEditModel model)
        {
            if (model.TopBannerImg != null)
            {
                model.TopBannerImgOrgName = model.TopBannerImg.FileName.Split('\\').Last();
                var root = Request.PhysicalApplicationPath;
                var newpath = root + "\\UploadImage\\EPaper\\";
                if (System.IO.Directory.Exists(newpath) == false)
                {
                    System.IO.Directory.CreateDirectory(newpath);
                }
                var guid = Guid.NewGuid();
                var filename = DateTime.Now.Ticks + "." + model.TopBannerImg.FileName.Split('.').Last();
                var path = newpath + filename;
                model.TopBannerImgName = filename;
                model.TopBannerImgPath = path;
                model.TopBannerImg.SaveAs(path);
            }
            model.BottomHtmlContent = HttpUtility.UrlDecode(model.BottomHtmlContent);
            model.CenterHtmlContent = HttpUtility.UrlDecode(model.CenterHtmlContent);
            model.LeftHtmlContent = HttpUtility.UrlDecode(model.LeftHtmlContent);
            model.PageEndHtmlContent = HttpUtility.UrlDecode(model.PageEndHtmlContent);
            model.TopHtmlContent = HttpUtility.UrlDecode(model.TopHtmlContent);
            if (model.ItemID == -1)
            {

                string result = _service.CreateItem(model, this.LanguageID, this.Account, this.UserName);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string result = _service.UpdateItem(model, this.LanguageID, this.Account, this.UserName);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateItemSeq(int modelid, int id, int seq, string type)
        {
            EPaperItem AMLItem = _service.GetEPaperItemByID(id.ToString());

            string result = _commonService.UpdateItemSeq<EPaperItem>(id, seq, modelid.ToString(), this.Account, this.UserName, this.LanguageID);


            return Json(result);
        }
        public ActionResult SetItemDelete(string[] idlist, string delaccount, string type)
        {
            List<EPaperItem> EPaperItem = _service.GetEPaperItems(idlist);

            string result = _commonService.DeleteItem<EPaperItem>(idlist, delaccount, this.Account, this.UserName, SET_EPAPER.M_EPAPER02, _ModelID);

            if (result == "刪除成功")
            {
                var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\{SET_EPAPER.M_EPAPER02}\\";
                foreach (var items in EPaperItem)
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


                }


            }

            return Content(result);
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
            string result = _commonService.SetItemStatus<EPaperItem>(id, status, this.Account, this.UserName);


            return Content(result);
        }

        public ActionResult SetHomeStatus(string id, bool status, string type)
        {
            string result = _commonService.SetHomeStatus<EPaperItem>(id, status, this.Account, this.UserName);


            return Content(result);
        }

        #region UnitSetting
        
        public ActionResult UnitSetting(string mainid)
        {
            
            mainid = mainid.AntiXss();
            ViewBag.SET_EPAPER = SET_EPAPER;
            if (mainid.IsNullOrEmpty()) { return RedirectToAction("Index"); }
            ViewBag.mainid = mainid;
            ViewBag.modelid = mainid;
            var model = _commonService.GetUnitModel<EPaperUnitSettingModel, EPaperUnitSetting>(mainid,"EPaper");
            var maindata = _service.GetModelEPaperMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.Title = maindata.Name; }

            if (model.columnSettings.Count == 0)
            {
                model.columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = maindata.ID, ColumnKey = "No", ColumnName = "序號", Used = true, Sort = 1 });
                model.columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = maindata.ID, ColumnKey = "PublicshDate", ColumnName = "發佈日期", Used = true, Sort = 2 });
                model.columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = maindata.ID, ColumnKey = "Title", ColumnName = "電子報名稱", Used = true, Sort = 3 });
                model.columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = maindata.ID, ColumnKey = "GroupName", ColumnName = "類別", Used = true, Sort = 4 });
                //model.columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = maindata.ID, ColumnKey = "LinkUrl", ColumnName = "相關連結", Used = true, Sort = 5 });
                //model.columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = maindata.ID, ColumnKey = "UploadFileDesc", ColumnName = "檔案下載", Used = true, Sort = 6 });
                //model.columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = maindata.ID, ColumnKey = "UnPublishDate", ColumnName = "下架時間", Used = true, Sort = 7 });
            }

            return View("~/Areas/webadmin/Views/EPaper/UnitSetting.cshtml", model);
        }

        public ActionResult SaveUnit(EPaperUnitSettingModel model)
        {
            //var Premodel = _commonService.GetUnitModel<EPaperUnitSettingModel, EPaperUnitSetting>(model.MainID.ToString());
            model.IntroductionHtml = HttpUtility.UrlDecode(model.IntroductionHtml);
            model.Summary = HttpUtility.UrlDecode(model.Summary);
            
            string result = _service.SetUnitModel(model, this.Account,this.LanguageID);

            return Content(result);
        }


        #endregion

        #region Subscriber
        public ActionResult Subscriber(string mainid)
        {
            mainid = mainid.AntiXss();
            var maindata = _service.GetModelEPaperMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.Title = maindata.Name; }
            ViewBag.mainid = mainid;
            ViewBag.LangID = this.LanguageID;
            return View("~/Areas/webadmin/Views/EPaper/Subscriber.cshtml");
        }

        public ActionResult PagingEpaperOrder(SubscriberSearchModel model)
        {
            model.LangId = this.LanguageID;
            var result =  _service.PagingEpaperOrder(model);
            
            return Json(result);
        
        }

        public ActionResult AddSubscriber(string sid, string name) 
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(name);
                }
                catch
                {
                    return Json("Email格式錯誤");
                }
                var str = "";
                if (sid == "-1" || string.IsNullOrEmpty(sid))
                {
                    
                    str = _service.AddSubscriber(name, this.Account,int.Parse(this.LanguageID));
                }
                //else
                //{
                //    Common.SetLogs(this.UserID, this.Account, "修改訊息管理單元名稱 ID=" + mainid + " 改為:" + name);
                //    str = _IMessageManager.UpdateUnit(name, mainid, this.Account);
                //}
                return Json(str);
            }
            else { return Json("請先登入"); }
        }

        #region DelSubscriber
        public ActionResult DelSubscriber(string[] idlist, string delaccount, string type)
        {
            //List<EPaperSubscriber> EPaperSubscriber = _service.GetEPaperSubscribers(idlist).ToList();

            string result = _service.DeleteItem<EPaperSubscriber>(idlist, delaccount);

            return Content(result);
        }
        #endregion

        public ActionResult SetSubscriberStatus(string id, bool status, string type)
        {
            string result = _service.SetItemStatus(id, status, this.Account, this.UserName);


            return Content(result);
        }
        #endregion
        #region Export
        public ActionResult Export(SubscriberSearchModel searchModel, string name)
        {
            try
            {
                if (name.IsNullOrEmpty()) { name = "資料下載"; }
                string _fname = System.Web.HttpUtility.UrlEncode(name + ".xlsx", System.Text.Encoding.UTF8);
                Response.AddHeader("Content-Disposition", "attachment; filename='" + _fname + "';filename*=utf-8''" + _fname);
                return File(_service.GetExport(searchModel,this.LanguageID ), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                return Json("匯出訂閱者管理列表失敗=" + ex.Message);
            }
            
        }
        #endregion

        //public ActionResult EPaperReview(string id)
        //{
        //    var model = new EPaperEditModel();
        //    if (id != "-1")
        //    {
        //        model = _service.GetModel(id);
        //        model.EPaperItemEdit = _service.GetEPaperItemEdit(id);
        //    }
        //    return View(model);
        //}


        #region 手動
        public ActionResult EPaperManuallyContent(string id, string mainid)
        {
            var maindata = _service.GetModelEPaperMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.ModelItemTitle = maindata.Name; }


            ViewBag.mainid = mainid;
            ViewBag.ID = id;
            ViewBag.HtmlContent = _commonService.GetGeneral<EPaperContent>("EPaperID=@EPaperID ",new Dictionary<string, string> { { "EPaperID", id } }).EPaperHtmlContent;
            var itemdata = _service.GetModel(id);
            ViewBag.Title = itemdata.Title;
            
            return View();
        }
        #endregion

        #region SaveManuallyContent
        public ActionResult SaveManuallyContent(string htmlcontent, string ID)
        {
            htmlcontent = HttpUtility.UrlDecode(htmlcontent);
            var result = _service.SavePaperManuallyContent(ID, htmlcontent);
            return Json(result);
            
        }
        #endregion


        #region 自動
        public ActionResult EPaperContentMenu(string id, string mainid)
        {
            

            ViewBag.mainid = mainid;

            ViewBag.ID = id;
            //"1"表示主選單
            var model = _commonService.GetMenu(this.LanguageID, "1");
            var itemdata = _service.GetModel(id);
            //過濾model


            var maindata = _service.GetModelEPaperMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.ModelItemTitle = maindata.Name; }


            var l3model = model.Where(v => v.MenuLevel == 3 && v.LinkMode == 2 && v.ModelID == 2 ).OrderBy(v => v.Sort);
            
            var l2model = model.Where(v => v.MenuLevel == 2 && v.LinkMode == 2 && v.ModelID == 2 || (l3model.Any(X => X.ParentID == v.ID))).OrderBy(v => v.Sort);
            
            var l1model = model.Where(v => v.MenuLevel == 1 && v.LinkMode == 2 && v.ModelID == 2 || (l2model.Any(X => X.ParentID == v.ID))).OrderBy(v => v.Sort);
            var allmember = l3model.Union(l2model).Union(l1model);
            ViewBag.Title = itemdata.Title;
            ViewBag.ItemList = _commonService.GetGeneralList<EPaperAutoItem> ("EPaperID=@EPaperID ", new Dictionary<string, string>() { { "EPaperID", id } }).OrderBy(v => v.Sort);
            return View(allmember);
        }
        #endregion

        #region EPaperItemSelect的view
        public ActionResult EPaperItemSelect(string menuid, string id ,string mainid)
        {
            //EpaperMain 的ID
            ViewBag.mainid = mainid;
            
            //Menu的 ID
            ViewBag.MenuId = menuid;

            //EPaperItem 的ID
            ViewBag.ID = id;

            var menudata = _menuService.GetModel(menuid);
            //模組ID(訊息為2)
            ViewBag.ModelID = menudata.ModelID;
            
            //訊息 的ID
            ViewBag.ModelItemID = menudata.ModelItemID;
            var itemdata = _service.GetModel(id);
            ViewBag.Title = itemdata.Title;
            return View();
        }
        #endregion
        #region PagingMenuItem的table資料
        public ActionResult PagingMenuItem(SearchModelBase model)
        {
            model.LangId = LanguageID;
            var result = _service.PagingMenuItem(model);
            return Json(result);
        }
        #endregion


        #region SetEpaperItem 勾選存入資料庫
        public ActionResult SetEpaperItem(bool issel, string itemid, string menuid, string id, string modelid, string mainid)
        {
            return Json(_service.SetEpaperItem(issel, id, itemid, menuid, modelid, mainid));
        }
        #endregion

        #region EPaperItemSort  電子報內容排序
        public ActionResult EPaperItemSort(string id ,string mainid)
        {
            var maindata = _service.GetModelEPaperMain(mainid, this.LanguageID);

            if (maindata.ID > 0) { ViewBag.ModelItemTitle = maindata.Name; }
            ViewBag.mainid = mainid;
            ViewBag.ID = id;
            ViewBag.table = _service.GetSortTable(id);
            var Epapermodel = _service.GetEPaperItemEdit(id);
            var itemdata = _service.GetModel(id);
            ViewBag.Title = itemdata.Title;
            return View("~/Areas/webadmin/Views/EPaper/EPaperItemSort.cshtml",Epapermodel);
        }
        #endregion

        

        #region SaveEPaperItemSort
        public ActionResult SaveEPaperItemSort(Dictionary<string, string> allitemkey, string eid, string iseditsub)
        {
            string result = _service.SaveEPaperItemSort(allitemkey, eid, iseditsub);
            return Json(result);
        }
        #endregion



        #region DeleteEPaperItemSort 
        public ActionResult DeleteEPaperItemSort(string[] delarrid, string eid)
        {
            string result = _service.DeleteEPaperItemSort(delarrid, eid);
            return Json(result);
        }
        #endregion


        #region EPaperReview
        public ActionResult EPaperReview(string id)
        {
            
            var model = new EPaperEditModel();
            if (id != "-1")
            {
                model = _service.GetModel(id);
                model.EPaperHtmlContent = _service.GetEPaperContent(id);
                model.EPaperItemEdit = _service.GetEPaperItemEdit(id);
            }
            return View(model);
        }
        #endregion

        #region SetIsEdit 發布
        public ActionResult SetIsEdit(string id, bool status)
        {
            return Json(_service.SetIsEdit(id, status, this.Account, this.UserName));
            
        }
        #endregion

    }


}