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
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Oaww.ViewModel.Search;

namespace Aofeng.Controllers
{

    public class EPaperController : BaseController
    {
        CommonService _commonService = new CommonService();
        //2訊息模組
        MessageService _Mservice = new MessageService(2);
        //12電子報模組
        EPaperService _service = new EPaperService(12);
        
        
        //itemid = ModelEpaperMain的ID(Model_id)
        public ActionResult Index(string itemid, string mid, int nowpage = 0, int jumpPage = 0, int? group = -1, string classType = "large")
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


            EPaperUnitSettingModel unitmodel = new EPaperUnitSettingModel();
            EPaperViewModel model = new EPaperViewModel() { mid = mid, itemid = itemid, group = group, classType = classType };
            if (group == -1)
            {
                group = null;
                model.ListEPaperItem = _service.GetEPaperItemsByModelID(itemid).OrderBy(t => t.Sort).ToList();
            }
            else
            {
                model.ListEPaperItem = _service.GetEPaperItemsByModelID(itemid).OrderBy(t => t.Sort).Where(t => t.GroupID == group).ToList();
            }

            
            
            model.ListGroupEPaper = _service.GetVaildGroupEPapers(itemid,this.LanguageID.ToString());
            model.ListGroupEPaper.Insert(0, new GroupEPaper() { ID = -1, Group_Name = "全部" });
            model.columnSettings = _commonService.GetColumnSettings("EPaper", itemid);
            ViewBag.UnitSetting = new EPaperUnitSetting();
            EPaperUnitSetting EPaperUnitSetting = (EPaperUnitSetting)ViewBag.UnitSetting;
            
            //int ShowCount = EPaperUnitSetting.ShowCount.HasValue ? EPaperUnitSetting.ShowCount.Value : 10;
            #region Grid

            //var paging = _service.GetPaging(itemid, group, "", nowpage, ShowCount);

            //model.ListEPaperItem = paging.rows;

            //Grid一定要有
            //ViewBag.Total = paging.total;
            //ViewBag.ShowCount = ShowCount;
            ViewBag.NowPage = nowpage;
            #endregion

            return View(model);
        }

        public ActionResult PagingItem(EPaperSearchModel model)
        {
            model.LangId = this.LanguageID.ToString();
            return Json(_service.PagingItem(model, model.ModelID.ToString()));
        }

        public ActionResult EPaperReview(string id)
        {
            var model = new EPaperEditModel();
            if (id != "-1")
            {
                model = _service.GetModel(id);
                model.EPaperItemEdit = _service.GetEPaperItemEdit(id);
            }
            return View(model);
        }



    }
}