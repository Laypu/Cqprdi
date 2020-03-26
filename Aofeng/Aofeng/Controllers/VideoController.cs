using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.ViewModel;
using Oaww.Entity;
using Oaww.Utility;

namespace Aofeng.Controllers
{
    public class VideoController : BaseController
    {
        CommonService _commonService = new CommonService();
        VideoService _service = new VideoService();
        public ActionResult Index(string itemid, string mid, int nowpage = 1, int jumpPage = 0, string fromDate = "", string toDate = "", bool print = false, string title = "")
        {
            if (string.IsNullOrEmpty(itemid))
            {
                return RedirectToAction("Index", "Home");
            }

            #region page action計算
            if (nowpage == 0 && jumpPage != 0)
            {
                nowpage = jumpPage;
            }
            else if (nowpage == 0 && jumpPage == 0)
            {
                nowpage = 1;
            }
            #endregion

            ViewBag.print = print;

            VideoViewModel model = new VideoViewModel() { mid = mid, itemid = itemid, fromDate = fromDate, toDate = toDate, title = title }; ;

            model.ListGroupVideo = _service.GetVaildGroupByMainID(itemid);
            VideoUnitSetting messageUnitSetting = (VideoUnitSetting)ViewBag.UnitSetting;
            int ShowCount = messageUnitSetting.ShowCount.HasValue ? messageUnitSetting.ShowCount.Value : 10; //沒有預設10
            ViewBag.UnitSetting = new VideoUnitSetting();

            Menu menu = _commonService.GetMenuByMid(mid);

            #region Grid

            var paging = _service.GetPaging(itemid, fromDate, toDate, title, nowpage, ShowCount);

            model.ListVideoItem = paging.rows;
           
            //Grid一定要有
            ViewBag.Total = paging.total;
            ViewBag.ShowCount = ShowCount;
            ViewBag.NowPage = nowpage;
            #endregion

            ViewBag.BoxClass = "news_box";

            return View(model);
        }

        public ActionResult VideoView(string itemID, string mid, bool print = false)
        {
            ViewBag.print = print;

            VideoItem videoItem = _service.GetVideoItemByItemID(itemID);

            videoItem.GroupName = _service.GetGroupName(videoItem.GroupID.Value);

            ViewBag.modelName = videoItem.Title;

            if (videoItem.VideoLink.IsNullOrEmpty() == false)
            {
                //if (videoItem.VideoLink.IndexOf("embed") > 0)
                //{
                //    var keyindex = videoItem.VideoLink.IndexOf("embed/");
                //    string key = videoItem.VideoLink.Substring(keyindex + 6);
                //    videoItem.VideoLink = "https://www.youtube.com/embed/" + key;
                //}
                //else if (videoItem.VideoLink.IndexOf("watch?v=") > 0)
                //{
                //    var keyindex = videoItem.VideoLink.IndexOf("watch?v=");
                //    var key = "";
                //    if (keyindex == -1) { key = videoItem.VideoLink; }
                //    else
                //    {
                //        key = videoItem.VideoLink.Substring(keyindex + 8);
                //    }
                //    videoItem.VideoLink = "https://www.youtube.com/embed/" + key;
                //}
                //else if (videoItem.VideoLink.IndexOf("https://youtu.be/") >= 0)
                //{
                //    videoItem.VideoLink = videoItem.VideoLink.Replace("https://youtu.be/", "https://www.youtube.com/embed/");
                //}
                videoItem.VideoLink = videoItem.VideoLink.UrlToEmbedCode();
            }
            ViewBag.BoxClass = "inner_box";

            //FB Image
            UrlHelper helper = new UrlHelper(Request.RequestContext);
            var urlBuilder = new System.UriBuilder(Request.Url.AbsoluteUri) { Path = helper.Content("~/UploadImage/MessageItem/" + videoItem.RelateImageFileName), Query = null, };
            ViewBag.ImageUrl = videoItem.RelateImageFileName.IsNullOrEmpty() ? "" : urlBuilder.ToString();
            ViewBag.FBContent = videoItem.Introduction.StripHTML().Replace("\n", "").Replace("\t", "");
            return View(videoItem);
        }
    }
}