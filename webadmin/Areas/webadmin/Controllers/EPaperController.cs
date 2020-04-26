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


                str = _menuService.AddUnit<ModelEPaperMain>(name, this.LanguageID, this.Account, _ModelID) ? "新增成功" : "新增失敗";


            }
            else
            {

                str = _menuService.UpdateUnit<ModelEPaperMain>(name, mainid, this.Account, _ModelID.ToString()) ? "修改成功" : "修改失敗";

            }
            return Json(str);

        }

        public ActionResult ModelItem(string menuindex, string mainid , bool admin = false  )
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

        public ActionResult GroupEdit()
        {
            //mainid = mainid.AntiXss();

            //ViewBag.mainid = mainid;
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
            model = _service.GetModelByID(mainid, itemid, SET_EPAPER.M_EPAPER02);
            model.SET_EPAPER = SET_EPAPER;
            model.SET_BASE = SET_BASE;
            //model = _service.GetEPaperItemByID(itemid);
            return View("~/Areas/webadmin/Views/EPaper/EpaperEdit.cshtml",model);
            

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

                return Content(result);
            }
            else
            {
                string result = _service.UpdateItem(model, this.LanguageID, this.Account, this.UserName);

                return Content(result);
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


        //#endregion
    }
}