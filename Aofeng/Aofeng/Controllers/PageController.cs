﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Utility;
using Oaww.Entity;
using System.Configuration;
using System.IO;
namespace Aofeng.Controllers
{
    public class PageController : BaseController
    {
        PageService _service = new PageService(1);
        public ActionResult Index(string itemid, string mid, bool print = false)
        {
            ViewBag.print = print;

            if (itemid.IsNullOrEmpty() || mid.IsNullOrEmpty()) { return RedirectToAction("Index", "Home"); }

            PageIndexItem PageIndexItem = _service.GetVaildPageIndexItemByItemID(itemid,this.LanguageID);

            if (PageIndexItem == null || PageIndexItem.HtmlContent == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PageIndexItem.HtmlContent = HttpUtility.HtmlDecode(PageIndexItem.HtmlContent);

            ViewBag.PublishDate = "最近更新日期 :" + (PageIndexItem.UpdateDatetime.HasValue ? PageIndexItem.UpdateDatetime.Value.ToString("yyyy/MM/dd") : PageIndexItem.CreateDatetime.Value.ToString("yyyy/MM/dd"));

            ViewBag.BoxClass = "inner_box";

            //FB Image
            UrlHelper helper = new UrlHelper(Request.RequestContext);
            var urlBuilder = new System.UriBuilder(Request.Url.AbsoluteUri) { Path = helper.Content("~/UploadImage/PageEdit/" + PageIndexItem.ImageFileName), Query = null, };
            ViewBag.ImageUrl = PageIndexItem.ImageFileName.IsNullOrEmpty() ? "" : urlBuilder.ToString();
            ViewBag.FBContent = PageIndexItem.HtmlContent.StripHTML().Replace("\n", "").Replace("\t", "");

            return View(PageIndexItem);
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