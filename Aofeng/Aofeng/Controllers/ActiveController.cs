using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Utility;
using Oaww.Entity;
using Oaww.ViewModel;
using System.Configuration;
using System.IO;

namespace Aofeng.Controllers
{
    public class ActiveController : BaseController
    {
        CommonService _commonService = new CommonService();
        MessageService _service = new MessageService(11);
        // GET: Active
        public ActionResult Index(string itemid, string mid, int nowpage = 1, int jumpPage = 0, int? group = -1, string classType = "large")
        {

            if (string.IsNullOrEmpty(itemid))
            {
                return RedirectToAction("Index", "Home");
            }

            MessageViewModel model = new MessageViewModel() { mid = mid, itemid = itemid, group = group, classType = classType };
            if (group == -1)
            {
                group = null;
            }
            model.ListGroupMessage = _service.GetVaildGroupMessages(itemid);
            model.ListGroupMessage.Insert(0, new GroupMessage() { ID = -1, Group_Name = "全部" });
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

            ViewBag.BoxClass = "news_box";

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