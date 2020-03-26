﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Entity;
using Oaww.Utility;
using Oaww.ViewModel;

namespace Aofeng.Controllers
{
   
    public class HistoryController : BaseController
    {
        CommonService _commonService = new CommonService();
        MessageService _service = new MessageService(10);
        public ActionResult Index(string itemid, string mid, bool print = false)
        {
            ViewBag.print = print;

            MessageViewModel model = new MessageViewModel() { mid = mid, itemid = itemid };

            if (string.IsNullOrEmpty(itemid))
            {
                return RedirectToAction("Index", "Home");
            }
            model.ListGroupMessage = _service.GetVaildGroupMessages(itemid);

            //MessageUnitSetting messageUnitSetting = (MessageUnitSetting)ViewBag.UnitSetting;
            int ShowCount = 9999; //沒有預設10
            ViewBag.UnitSetting = new MessageUnitSetting();

            Menu menu = _commonService.GetMenuByMid(mid);

            #region Grid

            var paging = _service.GetPaging(itemid, "", "", "", 1, ShowCount);

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

            MessageViewModel model = new MessageViewModel() { mid = mid};

            model.MessageItem = _service.GetMessageItemByID(itemid);

            model.GroupName = _service.GetGroupName(model.MessageItem.GroupID.Value);

            model.ListMessageFile = _service.GetFileDownloadFiles(itemid);
            model.ListMessageImage = _service.GetFileDownloadImages(itemid);
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
    }
}