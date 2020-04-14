using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Filter;
using Oaww.Business;
using Oaww.Utility;
using Oaww.ViewModel;
using Oaww.Entity.SET;
using System.Configuration;
using System.IO;


namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class MenuController : BaseController
    {
        MenuService _service = new MenuService();
        CommonService _commonService = new CommonService();
        ExclusiveLayoutService _exclusiveLayoutService = new ExclusiveLayoutService();
        FormService _Fservice = new FormService();
        /// <summary>
        /// 網站選單
        /// </summary>
        /// <param name="menutype">
        /// 1.主要選單
        /// 2.上方選單
        /// 3.下方選單
        /// </param>
        /// <returns></returns>
        [AuthFilter(_FuncionID = "Menu/MainMenu", _paramter = "menutype")]
        public ActionResult MainMenu(string menutype)
        {
            menutype = menutype.AntiXss();

            Session["menuNowID"] = 0;

            if (menutype == "1")
            {
                ViewBag.onlylevle1 = "N";
                ViewBag.Title = "主要選單";
            }
            else if (menutype == "2")
            {
                ViewBag.onlylevle1 = "Y";
                ViewBag.Title = "上方選單";
            }
            else if (menutype == "3")
            {
                ViewBag.onlylevle1 = "Y";
                ViewBag.Title = "下方選單";
            }
            else
            {
                ViewBag.onlylevle1 = "Y";
                ViewBag.Title = _exclusiveLayoutService.GetLayoutName(int.Parse(menutype) - 3);
            }

            ViewBag.menutype = menutype;

            return View(_service.GetMenu(this.LanguageID, menutype));
        }

        /// <summary>
        /// 網站選單編輯
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="menutype"></param>
        /// <param name="level"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public ActionResult MenuEdit(string menuid, string menutype, string level, string parentid)
        {
            if (menutype == "1")
            {
                ViewBag.Title = "主要選單";
            }
            else if (menutype == "2")
            {
                ViewBag.Title = "上方選單";
            }
            else if (menutype == "3")
            {
                ViewBag.Title = "下方選單";
            }
            else
            {
                ViewBag.Title = _exclusiveLayoutService.GetLayoutName(int.Parse(menutype) - 3);
            }


            var model = new MenuEditModel();

            if (menuid == "-1")
            {
                model.MenuType = int.Parse(menutype);
                model.MenuLevel = int.Parse(level);
                model.ParentID = int.Parse(parentid);
                model.FrontDisplay = true;
            }
            else
            {
                model = _service.GetModel(menuid);
                model.FrontDisplay = model.FrontDisplay.HasValue ? model.FrontDisplay.Value : true;
            }

            List<SelectListItem> ListModel = new List<SelectListItem>();

            if (menutype == "1" || menutype == "2" || menutype == "3")
            {
                ListModel = _commonService.GetGeneralList<SET_MENU>("M_Menu03=1")
                                                           .Select(t => new SelectListItem() { Value = t.M_Menu01.ToString(), Text = t.M_Menu02 })
                                                           .ToList();
            }
            else
            {
                ListModel = _exclusiveLayoutService.GetLayoutModule()
                                                  .Select(t => new SelectListItem() { Value = t.M_Menu01.ToString(), Text = t.M_Menu02 })
                                                  .ToList();
            }

            ViewData["ListModel"] = ListModel;

            return View(model);
        }

        /// <summary>
        /// 取得模組的List
        /// </summary>
        /// <param name="modelid"></param>
        /// <returns></returns>
        public ActionResult GetModelItem(string modelid, string menutype)
        {
            if (menutype == "1" || menutype == "2" || menutype == "3")
            {
                return Json(_service.GetModelItem(modelid, LanguageID));
            }
            else
            {
                return Json(_service.GetModelItem(modelid, LanguageID, menutype));
            }

        }

        /// <summary>
        /// 模組存檔
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveMenu(MenuEditModel model)
        {
            if (model.OpenMode == 3)
            {
                if (model.WindowWidth == null) { return Content("視窗寬度必須為整數"); }
                if (model.WindowHeight == null) { return Content("內頁風格高度必須為整數"); }
            }
            if (model.LinkMode == 2)
            {
                if (model.ModelID == 0) { return Content("請選擇程式模組"); }
                if (model.ModelItemID == 0) { return Content("請選擇程式模組項目"); }
            }
            if (model.LinkMode != 3)
            {
                model.LinkUrl = "";
            }
            if (model.ImageHeight == null) { model.ImageHeight = 219; }
            //model.MenuName = HttpUtility.UrlDecode(model.MenuName);

            //刪除原本檔案
            if (model.ImageFile != null)
            {
                var fileformat = model.ImageFile.FileName.Split('.');
                var fullfilename = model.ImageFile.FileName.Split('\\').Last();
                var orgfilename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));
                long ticks = DateTime.Now.Ticks;
                var root = Request.PhysicalApplicationPath;
                var filename = ticks + "." + fileformat.Last();

                var checkpath = root + "\\UploadImage\\MenuImage\\";
                if (System.IO.Directory.Exists(checkpath) == false)
                {
                    System.IO.Directory.CreateDirectory(checkpath);
                }
                model.ImgNameOri = ticks + "_" + fullfilename;
                var path = root + "\\UploadImage\\MenuImage\\" + model.ImgNameOri;
                model.ImageFile.SaveAs(path);
                model.ImgShowName = fullfilename;
                var thumbpath = root + "\\UploadImage\\MenuImage\\" + ticks + "_thumb." + fileformat.Last();
                model.ImgNameThumb = ticks + "_thumb." + fileformat.Last();

                int width = 170;
                var chartwidth = 170;
                if (model.ImageHeight != null)
                {
                    chartwidth = model.ImageHeight.Value;
                }

                if (chartwidth > width) { chartwidth = width; };
                //var haspath = UploadImg.uploadImgThumb(path, thumbpath, chartwidth);
                //if (haspath == "") { model.ImgNameThumb = ""; }
                //表示有舊資料
            }


                          


            if (model.LinkUploadFile != null)
            {
                model.LinkUploadFileName = model.LinkUploadFile.FileName.Split('\\').Last();
                var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];
                if (uploadfilepath.IsNullOrEmpty())
                {
                    uploadfilepath = Request.PhysicalApplicationPath + "\\UploadFile";
                }
                var newpath = uploadfilepath + "\\MenuFile\\";
                if (System.IO.Directory.Exists(newpath) == false)
                {
                    System.IO.Directory.CreateDirectory(newpath);
                }
                var guid = Guid.NewGuid();
                var filename = guid + "." + model.LinkUploadFile.FileName.Split('.').Last();
                var path = newpath + filename;
                model.LinkUploadFilePath = path;
                model.LinkUploadFile.SaveAs(path);
            }
            model.LangID = int.Parse(this.LanguageID);


            
            if (model.ID <= 0)
            {
                int itemold =0;
                if (model.ModelID == 4 && model.ModelItemID == -1)
                {
                    itemold = model.ModelItemID;

                }
                
                
                string result = _service.Create(model, this.Account, this.UserName);
                                
                
                var itemNew = model.ModelItemID;
                if (itemold  == -1)
                {
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
                    _Fservice.EditSelItem(Fmodel);
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
                    _Fservice.EditSelItem(FmodelEmail);
                }

                return Content(result);
            }
            else
            {
                string result = _service.Update(model, this.Account, this.UserName);
                return Content(result);
            }
        }

        /// <summary>
        /// 刪除模組
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public ActionResult DeleteMenu(int menuid)
        {
            string result = _service.DeleteMenu(menuid);

            return Content(result);
        }

        /// <summary>
        /// 排序變更
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public ActionResult SortUp(string menuid)
        {
            string result = _service.UpdateSort(int.Parse(menuid), "up", this.Account, this.UserName);

            return Content(result);
        }

        /// <summary>
        /// 排序變更
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public ActionResult SortNext(string menuid)
        {
            string result = _service.UpdateSort(int.Parse(menuid), "next", this.Account, this.UserName);

            return Content(result);
        }

        /// <summary>
        /// Menu Enabled
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public ActionResult Menueabled(string menuid)
        {
            string result = _service.MenueStatusUpdate(menuid, true);

            return Content(result);
        }

        /// <summary>
        /// Menu Disabled
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public ActionResult Menudisabled(string menuid)
        {
            string result = _service.MenueStatusUpdate(menuid, false);

            return Content(result);
        }


        /// <summary>
        /// 檔案下載
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult FileDownLoad(string id)
        {
            var model = _service.GetModel(id);
            string filepath = model.LinkUploadFilePath;
            string oldfilename = model.LinkUploadFileName;
            if (filepath != "")
            {
                string filename = System.IO.Path.GetFileName(filepath);
                if (string.IsNullOrEmpty(oldfilename)) { oldfilename = filename; }
                Stream iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                TempData["model"] = model;
                return File(iStream, "application/octet-stream", oldfilename);
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        /// <summary>
        /// Menu Edit View
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public ActionResult MenuLinkEdit(int menuid)
        {
            var model = _service.GetMenuById(menuid);
            return View(model);
        }

        /// <summary>
        /// Save Menu Link
        /// </summary>
        /// <param name="linkurl"></param>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public ActionResult SaveMenuLinkEdit(string linkurl, string menuid)
        {

            string result = _service.UpdateMenuLink(linkurl, menuid, this.Account, this.UserName);

            return Content(result);
        }

        /// <summary>
        /// 取得Link
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GotoMenu(int id = 0)
        {
            //記住現在click的menu id
            Session["menuNowID"] = id;
            string menuType = Session["menutype"] == null ? "1" : Session["menutype"].ToString();
            if (!this.Roles.Contains("administrators"))
            {
                if (menuType == "1" || menuType =="2" || menuType == "3")
                {
                    //直接在這裡確認Auth
                    if (!_commonService.CheckMenuAuth(this.Roles, "1", id.ToString()))
                    {
                        return Json(new string[] { "99" });
                    }
                }
                else
                {
                    //直接在這裡確認Auth
                    if (!_commonService.CheckMenuAuth(menuType, id.ToString()))
                    {
                        return Json(new string[] { "99" });
                    }
                }
                    
            }

            return Json(this.GetMenuUrl(id));
        }


        /// <summary>
        /// 取得Menu的URL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string[] GetMenuUrl(int id)
        {
            if (id == 0) { return new string[] { "", "" }; }
            var menu = _service.GetMenuById(id);

            if (menu.LinkMode == 2)
            {
                SET_MENU SET_MENU = _commonService.GetGeneral<SET_MENU>("M_Menu01=@M_Menu01", new Dictionary<string, string>() { { "M_Menu01", menu.ModelID.ToString() } });

                UrlHelper helper = new UrlHelper(Request.RequestContext);
                return new string[] { menu.LinkMode.ToString(), helper.Action(SET_MENU.M_Menu10, SET_MENU.M_Menu09), "id", menu.ModelItemID.ToString(), "mainid", menu.ModelItemID.ToString() };

            }
            else if (menu.LinkMode == 3)
            {
                Session["IsFromClick"] = null;
                UrlHelper helper = new UrlHelper(Request.RequestContext);
                return new string[] { "2", helper.Action("MenuLinkEdit", "Menu"), "menuid", menu.ID.ToString() };
            }

            return new string[] { "", "" };
        }
    }
}