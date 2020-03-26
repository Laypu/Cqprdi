using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Entity;
using Oaww.ViewModel;
using System.Configuration;
using System.IO;
using Oaww.Utility;

namespace Aofeng.Controllers
{
    public class GalleryController : BaseController
    {
        CommonService _commonService = new CommonService();
        MessageService _service = new MessageService(7);
        public ActionResult Index(string itemid, string mid, int nowpage = 1, int jumpPage = 0, int? group = null)
        {
            if (string.IsNullOrEmpty(itemid))
            {
                return RedirectToAction("Index", "Home");
            }

            MessageViewModel model = new MessageViewModel() { mid = mid, itemid = itemid, group = group};

            model.ListGroupMessage = _service.GetVaildGroupMessages(itemid);

            MessageUnitSetting messageUnitSetting = (MessageUnitSetting)ViewBag.UnitSetting;
            int ShowCount = messageUnitSetting.ShowCount.HasValue ? messageUnitSetting.ShowCount.Value : 10; //沒有預設10
            ViewBag.UnitSetting = new MessageUnitSetting();

            Menu menu = _commonService.GetMenuByMid(mid);

            #region Grid

            var paging = _service.GetPaging(itemid, group, "", nowpage, ShowCount);

            model.ListMessageItem = paging.rows;

            //Grid一定要有
            ViewBag.Total = paging.total;
            ViewBag.ShowCount = ShowCount;
            ViewBag.NowPage = nowpage;
            #endregion

            ViewBag.BoxClass = "news_box pho";

            return View(model);
        }

        public ActionResult MessageView(string itemid, string mid, string GroupName, bool isPrint = false)
        {
            ViewBag.print = isPrint;

            MessageViewModel model = new MessageViewModel() { mid = mid };

            model.MessageItem = _service.GetMessageItemByID(itemid);

            model.GroupName = _service.GetGroupName(model.MessageItem.GroupID.Value);

           
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

        public ActionResult FileDownLoad2(string modelid, string itemid)
        {
            var model = _service.GetMessageItemByID(itemid);
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