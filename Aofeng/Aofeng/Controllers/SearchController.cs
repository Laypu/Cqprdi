using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Entity;
using Oaww.ViewModel.Search;
using Oaww.ViewModel;

namespace Aofeng.Controllers
{
    public class SearchController : BaseController
    {
        CommonService _commonService = new CommonService();
        SearchService _service = new SearchService();
        public ActionResult Search(string key, string key2 = "", string key3 = "", string sel1 = "", string sel2 = ""
    , string searchtype = "1", string menutype = "", string menu1 = "", string menu2 = "", string menu3 = "", int nowpage = 0, int jumpPage = 0)
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

            if (string.IsNullOrEmpty(key))
            {
                return RedirectToAction("Index", "Home");
            }

            PageIndexSetting pageIndexSetting = _service.GetPageIndexSetting();
            int ShowCount = pageIndexSetting.ShowCount.HasValue ? pageIndexSetting.ShowCount.Value : 10;

            AdvanceSearchModel model = new AdvanceSearchModel()
            {
                Key = key,
                Key2 = key2,
                Key3 = key3,
                Sel1 = sel1,
                Sel2 = sel2,
                SearchType = searchtype,
                MenuType = menutype,
                Menu1 = menu1,
                Menu2 = menu2,
                Menu3 = menu3,
                Offset = (nowpage - 1) * ShowCount,
                Limit = ShowCount
            };



            Paging<SearchViewModel> paging = _service.GetPaging(model,this.LanguageID.ToString());
            model.paging = paging;

            //Grid一定要有
            ViewBag.Total = paging.total;
            ViewBag.ShowCount = pageIndexSetting.ShowCount.HasValue ? pageIndexSetting.ShowCount.Value : 10;
            ViewBag.NowPage = nowpage;

            ViewBag.modelName = $"搜尋結果 - \"{key}\"";
            ViewBag.mid = "Search";

            return View(model);
        }
    }
}