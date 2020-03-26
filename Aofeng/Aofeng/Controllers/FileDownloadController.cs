using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Entity;
using Oaww.ViewModel;

namespace Aofeng.Controllers
{
    public class FileDownloadController : BaseController
    {
        CommonService _commonService = new CommonService();
        MessageService _service = new MessageService(3);
        public ActionResult Index(string itemid, string mid, int nowpage = 1, int jumpPage = 0, int? group = -1, bool print = false)
        {
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

            MessageViewModel model = new MessageViewModel() { mid = mid, itemid = itemid, group = group };



            if (string.IsNullOrEmpty(itemid))
            {
                return RedirectToAction("Index", "Home");
            }
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
            //model.GroupName = menu.MenuName;
            //Grid一定要有
            ViewBag.Total = paging.total;
            ViewBag.ShowCount = ShowCount;
            ViewBag.NowPage = nowpage;
            #endregion

            ViewBag.BoxClass = "news_box page";

            return View(model);
        }
    }
}