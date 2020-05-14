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
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

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
         
            var Error = string.Empty;
            if (Request["btnorder"] != null)
            {
                
                var input = Request["txtEmailInput"];
                if (input.IsNullOrEmpty())
                {
                    Error=_service.GetEPMulti("Column15", int.Parse(itemid));
                    ViewBag.ErrorInfo = Error;
                    ViewData["message"] = Error;
                }
                else
                {
                    var echeck = new EmailAddressAttribute();
                    if (echeck.IsValid(input) == false)
                    {
                        Error = _service.GetEPMulti("Column10", int.Parse(itemid));
                        ViewBag.ErrorInfo = Error;
                        ViewData["message"] = Error;
                    }
                    else
                    {
                        var mess= _service.AddSubscriber(input, "user", this.LanguageID, itemid);
                        ViewBag.ErrorInfo = mess;
                        ViewData["message"] = mess;
                    }
                }
            }
            else if (Request["btncancel"] != null)
            {
                var input = Request["txtEmailInput"];
                if (input.IsNullOrEmpty())
                {
                    Error = _service.GetEPMulti("Column15", int.Parse(itemid));
                    ViewBag.ErrorInfo = Error;
                    ViewData["message"] =Error;
                }
                else
                {
                    var echeck = new EmailAddressAttribute();
                    if (echeck.IsValid(input) == false)
                    {
                        Error = _service.GetEPMulti("Column10", int.Parse(itemid));
                        ViewBag.ErrorInfo = Error;
                        ViewData["message"] = Error;
                    }
                    else
                    {
                        var mess = _service.CancelSubscriber(input, "user", itemid);
                        ViewBag.ErrorInfo = mess;
                        ViewData["message"] = mess;
                    }
                }
            }




            EPaperUnitSettingModel unitmodel = new EPaperUnitSettingModel();
            EPaperViewModel model = new EPaperViewModel() { mid = mid, itemid = itemid, group = group, classType = classType };
            var EPaperItem = _service.GetEPaperItemsByModelID(itemid);
            model.ListGroupEPaper = _service.GetVaildGroupEPaper(itemid, this.LanguageID.ToString());
            //model.ListGroupEPaper.Insert(0, new GroupEPaper() { ID = -1, Group_Name = "全部" });
            //if (group == -1)
            ////{
            //    group = null;
            //    model.ListEPaperItem = EPaperItem.OrderBy(t => t.Sort).Where(t=> t.IsPublished ==true).ToList();
            //}
            //else
            //{
            //    model.ListEPaperItem = EPaperItem.OrderBy(t => t.Sort).Where(t => t.IsPublished == true).Where(t => t.GroupID == group).ToList();
            //}
          

            //model.ListGroupEPaper = _service.GetVaildGroupEPapers(itemid,this.LanguageID.ToString());
          
            if (group == -1)
            {
                group = null;
            }
            model.columnSettings = _commonService.GetColumnSettings("EPaper", itemid);
            ViewBag.UnitSetting = new EPaperUnitSetting();
            EPaperUnitSetting EPaperUnitSetting = (EPaperUnitSetting)ViewBag.UnitSetting;

            int ShowCount = EPaperUnitSetting.ShowCount.HasValue ? EPaperUnitSetting.ShowCount.Value : 10;
            #region Grid

            var paging = _service.GetPaging(itemid, group, "", nowpage, ShowCount);

            model.ListEPaperItem = paging.rows;
            //List<int> groupidlist = new List<int>();
            //foreach (var i in model.ListEPaperItem)
            //{
            //    if (groupidlist.Contains((int)i.GroupID))
            //    {


            //    }
            //    else
            //    {
            //        groupidlist.Add((int)i.GroupID);
            //    }

            //}
            model.ListGroupEPaper.Insert(0, new GroupEPaper() { ID = -1, Group_Name = "全部" });

            //Grid一定要有
            ViewBag.Total = paging.total;
            ViewBag.ShowCount = ShowCount;
            ViewBag.NowPage = nowpage;
            #endregion

            return View(model);
        }

        public ActionResult PagingItem(EPaperSearchModel model)
        {
            model.LangId = this.LanguageID.ToString();
            return Json(_service.PagingItem(model, model.ModelID.ToString()));
        }

        public ActionResult EPaperDetails(string id)
        {
            var model = new EPaperEditModel();
            if (id != "-1")
            {
                model = _service.GetModel(id);
                model.EPaperItemEdit = _service.GetEPaperItemEdit(id);
                model.EPaperHtmlContent = _service.GetEPaperContent(id);
            }
            return View(model);
        }



    }
}