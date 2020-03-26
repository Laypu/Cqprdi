using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Filter;
using Oaww.ViewModel;
using Oaww.Business;
using Oaww.Utility;
using Oaww.Entity;
using Oaww.ViewModel.Lang;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class SiteController : BaseController
    {
        SiteService _service = new SiteService();
        CommonService _commonService = new CommonService();
        #region 網站版面
        /// <summary>
        /// 網站版面資訊設定
        /// </summary>
        /// <param name="stype"></param>
        /// <returns></returns>
        [AuthFilter(_FuncionID = "Config/SiteLayout")]
        public ActionResult SiteLayout(string stype = "P")
        {

            var model = _service.GetSiteLayout(stype, this.LanguageID);
            if (stype == "M")
            {
                ViewBag.Title = "版面管理(手機板)";
            }
            else
            {
                ViewBag.Title = "網站版面資訊設定";
            }

            return View(model);
        }


        /// <summary>
        /// 網站版面資訊存檔
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SaveSiteLayout(SiteLayoutModel model)
        {

            if (model.LogoImageFile != null)
            {
                var fileformat = model.LogoImageFile.FileName.Split('.');
                var fullfilename = model.LogoImageFile.FileName.Split('\\').Last();
                var orgfilename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));
                long ticks = DateTime.Now.Ticks;
                var root = Request.PhysicalApplicationPath;
                var filename = ticks + "." + fileformat.Last();
                var checkpath = root + "\\UploadImage\\SiteLayout\\";
                if (System.IO.Directory.Exists(checkpath) == false)
                {
                    System.IO.Directory.CreateDirectory(checkpath);
                }
                model.LogoImgNameOri = ticks + "_" + fullfilename;
                var path = root + "\\UploadImage\\SiteLayout\\" + model.LogoImgNameOri;
                model.LogoImageFile.SaveAs(path);
                model.LogoImgShowName = fullfilename;
                var thumbpath = root + "\\UploadImage\\SiteLayout\\" + ticks + "_thumb." + fileformat.Last();
                model.LogoImgNameThumb = ticks + "_thumb." + fileformat.Last();

                int height = 80;
                var chartheight = 80;

                if (chartheight > height) { chartheight = height; };
                var haspath = UploadImg.uploadImgThumbMaxHeight(path, thumbpath, chartheight, fileformat.Last());
                if (haspath == "") { model.LogoImgNameThumb = ""; }
            }

            if (model.InnerLogoImageFile != null)
            {
                var fileformat = model.InnerLogoImageFile.FileName.Split('.');
                var fullfilename = model.InnerLogoImageFile.FileName.Split('\\').Last();
                var orgfilename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));
                long ticks = DateTime.Now.Ticks;
                var root = Request.PhysicalApplicationPath;
                var filename = ticks + "." + fileformat.Last();
                var checkpath = root + "\\UploadImage\\SiteLayout\\";
                if (System.IO.Directory.Exists(checkpath) == false)
                {
                    System.IO.Directory.CreateDirectory(checkpath);
                }
                model.InnerLogoImgNameOri = ticks + "_" + fullfilename;
                var path = root + "\\UploadImage\\SiteLayout\\" + model.InnerLogoImgNameOri;
                model.InnerLogoImageFile.SaveAs(path);
                model.InnerLogoImgShowName = fullfilename;
                var thumbpath = root + "\\UploadImage\\SiteLayout\\" + ticks + "_thumb." + fileformat.Last();
                model.InnerLogoImgNameThumb = ticks + "_thumb." + fileformat.Last();

                int height = 80;
                var chartheight = 80;

                if (chartheight > height) { chartheight = height; };
                var haspath = UploadImg.uploadImgThumbMaxHeight(path, thumbpath, chartheight, fileformat.Last());
                if (haspath == "") { model.InnerLogoImgNameThumb = ""; }
            }
            model.PublishContent = HttpUtility.UrlDecode(model.PublishContent);
            model.HtmlContent = HttpUtility.UrlDecode(model.HtmlContent);
            model.LangID = LanguageID;

            string result = _service.EditSiteLayout(model);

            if (result == "設定成功")
            {
                if (model.ID.HasValue && model.ID.Value > 0)
                {
                    model.LogoImgShowName = model.LogoImageFile != null ? model.LogoImgShowName
                                                : (model.LogoImgNameOri.IsNullOrEmpty()
                                                                ? ""  //砍掉
                                                                : model.LogoImgShowName); //不變

                    model.LogoImgNameThumb = model.LogoImageFile != null ? model.LogoImgNameThumb
                                                : (model.LogoImgNameOri.IsNullOrEmpty()
                                                                ? ""  //砍掉
                                                                : model.LogoImgNameThumb); //不變

                    model.InnerLogoImgShowName = model.InnerLogoImageFile != null ? model.InnerLogoImgShowName
                                                : (model.InnerLogoImgNameOri.IsNullOrEmpty()
                                                                ? ""  //砍掉
                                                                : model.InnerLogoImgShowName); //不變

                    model.InnerLogoImgNameThumb = model.InnerLogoImageFile != null ? model.InnerLogoImgNameThumb
                                               : (model.InnerLogoImgNameOri.IsNullOrEmpty()
                                                               ? ""  //砍掉
                                                               : model.InnerLogoImgNameThumb); //不變


                    var oldfilename = model.LogoImgNameThumb;
                    var oldfileoriname = model.LogoImgNameOri;
                    var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                        "\\UploadImage\\SiteLayout\\" + oldfilename;
                    var oldoriroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                      "\\UploadImage\\SiteLayout\\" + oldfileoriname;
                    if (model.LogoImgNameOri.IsNullOrEmpty() || model.LogoImgNameOri != oldfileoriname)
                    {
                        //刪除舊檔案
                        if (System.IO.File.Exists(oldroot))
                        {
                            System.IO.File.Delete(oldroot);
                        }
                        if (System.IO.File.Exists(oldoriroot))
                        {
                            System.IO.File.Delete(oldoriroot);
                        }
                    }


                    oldfilename = model.InnerLogoImgNameThumb;
                    oldfileoriname = model.InnerLogoImgNameOri;
                    oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                       "\\UploadImage\\SiteLayout\\" + oldfilename;
                    oldoriroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                     "\\UploadImage\\SiteLayout\\" + oldfileoriname;
                    if (model.InnerLogoImgNameOri.IsNullOrEmpty() || model.InnerLogoImgNameOri != oldfileoriname)
                    {
                        //刪除舊檔案
                        if (System.IO.File.Exists(oldroot))
                        {
                            System.IO.File.Delete(oldroot);
                        }
                        if (System.IO.File.Exists(oldoriroot))
                        {
                            System.IO.File.Delete(oldoriroot);
                        }
                    }
                }

            }

            return Content(result);

        }

        #endregion

        #region 轉寄好友
        [AuthFilter(_FuncionID = "Config/SiteLayout")]
        public ActionResult FowardSetting(string stype = "P")
        {
            //ViewBag.Title = "轉寄好友";

            var model = _service.GetSiteLayout(stype, LanguageID);
            return View(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SaveFowardSiteLayout(SiteLayoutModel model)
        {

            if (model.FowardImageFile != null)
            {
                var fileformat = model.FowardImageFile.FileName.Split('.');
                var fullfilename = model.FowardImageFile.FileName.Split('\\').Last();
                var orgfilename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));
                long ticks = DateTime.Now.Ticks;
                var root = Request.PhysicalApplicationPath;
                var filename = ticks + "." + fileformat.Last();
                var checkpath = root + "\\UploadImage\\SiteLayout\\";
                if (System.IO.Directory.Exists(checkpath) == false)
                {
                    System.IO.Directory.CreateDirectory(checkpath);
                }
                model.FowardImgNameOri = ticks + "_" + fullfilename;
                var path = root + "\\UploadImage\\SiteLayout\\" + model.FowardImgNameOri;
                model.FowardImageFile.SaveAs(path);
                model.FowardImgShowName = fullfilename;
                var thumbpath = root + "\\UploadImage\\SiteLayout\\" + ticks + "_thumb." + fileformat.Last();
                model.FowardImgNameThumb = ticks + "_thumb." + fileformat.Last();

                int height = 150;
                var chartheight = 150;

                if (chartheight > height) { chartheight = height; };

                var haspath = UploadImg.uploadImgThumbMaxHeight(path, thumbpath, chartheight, fileformat.Last());
                if (haspath == "") { model.FowardImgNameThumb = ""; }
            }

            model.FowardHtmlContent = HttpUtility.UrlDecode(model.FowardHtmlContent);

            var olddata = _service.GetSiteLayoutByID(model.ID.ToString());

            string result = _service.EditFowardSiteLayout(model);

            if (result == "設定成功")
            {
                if (model.ID != -1)
                {
                    var oldfilename = olddata.FowardImgNameThumb;
                    var oldfileoriname = olddata.FowardImgNameOri;

                    var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                            "\\UploadImage\\SiteLayout\\" + oldfilename;
                    var oldoriroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                      "\\UploadImage\\SiteLayout\\" + oldfileoriname;
                    if (model.FowardImgNameOri.IsNullOrEmpty() || model.FowardImgNameOri != oldfileoriname)
                    {
                        //刪除舊檔案
                        if (System.IO.File.Exists(oldroot))
                        {
                            System.IO.File.Delete(oldroot);
                        }
                        if (System.IO.File.Exists(oldoriroot))
                        {
                            System.IO.File.Delete(oldoriroot);
                        }
                    }
                }

            }

            return Content(result);


        }
        #endregion

        #region 友善列印
        [AuthFilter(_FuncionID = "Config/SiteLayout")]
        public ActionResult PrintSetting(string stype)
        {
            var model = _service.GetSiteLayout(stype, LanguageID);
            return View(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SavePrintSiteLayout(SiteLayoutModel model)
        {


            if (model.PrintImageFile != null)
            {
                var fileformat = model.PrintImageFile.FileName.Split('.');
                var fullfilename = model.PrintImageFile.FileName.Split('\\').Last();
                var orgfilename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));
                long ticks = DateTime.Now.Ticks;
                var root = Request.PhysicalApplicationPath;
                var filename = ticks + "." + fileformat.Last();
                var checkpath = root + "\\UploadImage\\SiteLayout\\";
                if (System.IO.Directory.Exists(checkpath) == false)
                {
                    System.IO.Directory.CreateDirectory(checkpath);
                }
                model.PrintImgNameOri = ticks + "_" + fullfilename;
                var path = root + "\\UploadImage\\SiteLayout\\" + model.PrintImgNameOri;
                model.PrintImageFile.SaveAs(path);
                model.PrintImgShowName = fullfilename;
                var thumbpath = root + "\\UploadImage\\SiteLayout\\" + ticks + "_thumb." + fileformat.Last();
                model.PrintImgNameThumb = ticks + "_thumb." + fileformat.Last();

                int height = 150;
                var chartheight = 150;

                if (chartheight > height) { chartheight = height; };

                var haspath = UploadImg.uploadImgThumbMaxHeight(path, thumbpath, chartheight, fileformat.Last());
                if (haspath == "") { model.PrintImgNameThumb = ""; }
            }

            var olddata = _service.GetSiteLayoutByID(model.ID.ToString());

            model.PrintHtmlContent = HttpUtility.UrlDecode(model.PrintHtmlContent);

            string result = _service.EditPrintSiteLayout(model);

            if (result == "設定成功")
            {
                if (model.ID != -1)
                {
                    var oldfilename = olddata.PrintImgNameThumb;
                    var oldfileoriname = olddata.PrintImgNameOri;

                    var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                           "\\UploadImage\\SiteLayout\\" + oldfilename;
                    var oldoriroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                      "\\UploadImage\\SiteLayout\\" + oldfileoriname;
                    if (model.PrintImgNameOri.IsNullOrEmpty() || model.PrintImgNameOri != oldfileoriname)
                    {
                        //刪除舊檔案
                        if (System.IO.File.Exists(oldroot))
                        {
                            System.IO.File.Delete(oldroot);
                        }
                        if (System.IO.File.Exists(oldoriroot))
                        {
                            System.IO.File.Delete(oldoriroot);
                        }
                    }
                }

            }

            return Content(result);

        }
        #endregion

        #region 404
        [AuthFilter(_FuncionID = "Config/SiteLayout")]
        public ActionResult Page404Setting(string stype)
        {
            var model = _service.GetSiteLayout(stype, LanguageID);
            return View(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult Save404SiteLayout(SiteLayoutModel model)
        {

            model.Page404HtmlContent = HttpUtility.UrlDecode(model.Page404HtmlContent);

            string result = _service.EditPage404SiteLayout(model);



            return Content(result);

        }

        #endregion

        #region 多語系
        [AuthFilter(_FuncionID = "Config/SiteLangText")]
        public ActionResult SiteLangText()
        {            
            var model = _service.GetSiteLangText(this.LanguageID);
            return View(model);
        }

        public string SaveSiteLangText(SiteLangTextModel model)
        {
            var input = _commonService.GetGeneralList<LangInputText>("LangID=@LangID", new Dictionary<string, string>() { { "LangID", this.LanguageID } }).ToDictionary(v => v.LangTextID.ToString(), v => v.Text);
            var r = 0;
            foreach (var g in model.Group1.Keys)
            {
                if (input.ContainsKey(g.ToString()))
                {
                    r = _service.UpdateLang( model.Group1[g], this.LanguageID.ToString(), g );
                }
                else
                {
                    r = _service.CreateLang(new LangInputText()
                    {
                        LangID = int.Parse(this.LanguageID),
                        LangTextID = int.Parse(g),
                        Text = model.Group1[g]
                    });
                }
            }

            foreach (var g in model.Group2.Keys)
            {
                if (input.ContainsKey(g.ToString()))
                {
                    r = _service.UpdateLang(model.Group2[g], this.LanguageID, g);
                }
                else
                {
                    r = _service.CreateLang(new LangInputText()
                    {
                        LangID = int.Parse(this.LanguageID),
                        LangTextID = int.Parse(g),
                        Text = model.Group2[g]
                    });
                }
            }

            foreach (var g in model.Group3.Keys)
            {
                if (input.ContainsKey(g.ToString()))
                {
                    r = _service.UpdateLang(model.Group3[g], this.LanguageID, g);
                }
                else
                {
                    r = _service.CreateLang(new LangInputText()
                    {
                        LangID = int.Parse(this.LanguageID),
                        LangTextID = int.Parse(g),
                        Text = model.Group3[g]
                    });
                }
            }

            foreach (var g in model.Group4.Keys)
            {
                if (input.ContainsKey(g.ToString()))
                {
                    r = _service.UpdateLang(model.Group4[g], this.LanguageID, g);
                }
                else
                {
                    r = _service.CreateLang(new LangInputText()
                    {
                        LangID = int.Parse(this.LanguageID),
                        LangTextID = int.Parse(g),
                        Text = model.Group4[g]
                    });
                }
            }
            foreach (var g in model.Group5.Keys)
            {
                if (input.ContainsKey(g.ToString()))
                {
                    r = _service.UpdateLang(model.Group5[g], this.LanguageID, g);
                }
                else
                {
                    r = _service.CreateLang(new LangInputText()
                    {
                        LangID = int.Parse(this.LanguageID),
                        LangTextID = int.Parse(g),
                        Text = model.Group5[g]
                    });
                }
            }
            foreach (var g in model.Group6.Keys)
            {
                if (input.ContainsKey(g.ToString()))
                {
                    r = _service.UpdateLang(model.Group6[g], this.LanguageID, g);
                }
                else
                {
                    r = _service.CreateLang(new LangInputText()
                    {
                        LangID = int.Parse(this.LanguageID),
                        LangTextID = int.Parse(g),
                        Text = model.Group6[g]
                    });
                }
            }
           
            return "";
        }
        #endregion

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
                var root = Request.PhysicalApplicationPath + "/UploadImage/SiteLayout/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/UploadImage/SiteLayout/" + filename);
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
                var root = Request.PhysicalApplicationPath + "/UploadFile/SiteLayout/";
                if (System.IO.Directory.Exists(root) == false)
                {
                    System.IO.Directory.CreateDirectory(root);
                }
                upload.SaveAs(root + filename);
                imageUrl = Url.Content((Request.ApplicationPath == "/" ? "" : Request.ApplicationPath) + "/UploadFile/SiteLayout/" + filename);
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