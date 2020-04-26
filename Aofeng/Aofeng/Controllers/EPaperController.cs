﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Entity;
using Oaww.Utility;
using Oaww.ViewModel;

namespace Aofeng.Controllers
{

    public class EPaperController : BaseController
    {
        CommonService _commonService = new CommonService();
        MessageService _Mservice = new MessageService(10);
        PageService _Pservice = new PageService(10);
        public ActionResult Index(string itemid, string mid,string mainid, bool print = false)
        {
            
            

            ViewBag.print = print;
            MessageViewModel model = new MessageViewModel() { mid = mid, itemid = itemid };

            if (string.IsNullOrEmpty(itemid))
            {
                return RedirectToAction("Index", "Home");
            }
            model.ListGroupMessage = _Mservice.GetVaildGroupMessages(itemid);

            //MessageUnitSetting messageUnitSetting = (MessageUnitSetting)ViewBag.UnitSetting;
            int ShowCount = 9999; //沒有預設10
            ViewBag.UnitSetting = new MessageUnitSetting();

            Menu menu = _commonService.GetMenuByMid(mid);

            #region Grid

            var paging = _Mservice.GetPaging(itemid, "", "", "", 1, ShowCount);

            model.ListMessageItem = paging.rows;

            //Grid一定要有
            ViewBag.Total = paging.total;
            ViewBag.ShowCount = ShowCount;

            #endregion

            ViewBag.BoxClass = "inner_box";

            ViewBag.modelName = menu.MenuName;

            return View(model);
        }

        public ActionResult MessageView(string itemid, string mid, string GroupName, bool isPrint = false)
        {
            ViewBag.print = isPrint;

            MessageViewModel model = new MessageViewModel() { mid = mid };

            model.MessageItem = _Mservice.GetMessageItemByID(itemid);

            model.GroupName = _Mservice.GetGroupName(model.MessageItem.GroupID.Value);

            model.ListMessageFile = _Mservice.GetFileDownloadFiles(itemid);
            model.ListMessageImage = _Mservice.GetFileDownloadImages(itemid);
            ViewBag.PublishDate = model.MessageItem.PublicshDate.Value.ToString("yyyy/MM/dd");
            ViewBag.BoxClass = "inner_box";


            ViewBag.modelName = model.MessageItem.Title;

            //FB Image
            UrlHelper helper = new UrlHelper(Request.RequestContext);
            var urlBuilder = new System.UriBuilder(Request.Url.AbsoluteUri) { Path = helper.Content("~/UploadImage/MessageItem/" + model.MessageItem.RelateImageFileName), Query = null, };
            ViewBag.ImageUrl = model.MessageItem.RelateImageFileName.IsNullOrEmpty() ? "" : urlBuilder.ToString();
            ViewBag.FBContent = model.MessageItem.HtmlContent.StripHTML().Replace("\n", "").Replace("\t", "");

            return View(model);
        }
        public ActionResult FileDownLoad2(string modelid, string itemid)
        {
            var model = _Mservice.GetMessageItemByID(itemid);
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