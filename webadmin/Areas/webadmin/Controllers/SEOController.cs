using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Filter;
using Oaww.Business;
using Oaww.ViewModel;
using Oaww.Entity;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class SEOController : BaseController
    {
        CommonService _commonService = new CommonService();
        SEOService _service = new SEOService();
        /// <summary>
        /// SEO設定
        /// </summary>
        /// <returns></returns>
        [AuthFilter(_FuncionID = "SEO/Index")]
        public ActionResult Index(string type = "Main")
        {
            Session["menuNowID"] = 0;

            var seodata = _service.GetSEO(type, this.LanguageID);

            var model = new SEOViewModel() { TypeName = type };
            if (seodata.ID > 0)
            {
                model.ID = seodata.ID;
                model.Description = seodata.Description;
                model.WebsiteTitle = seodata.Title;
                model.Keywords = new string[] {
                        seodata.Keywords1,seodata.Keywords2,seodata.Keywords3,seodata.Keywords4,seodata.Keywords5
                    ,seodata.Keywords6,seodata.Keywords7,seodata.Keywords8,seodata.Keywords9,seodata.Keywords10};
            }
            else
            {
                model.Keywords = new string[10];
            }



            return View(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult Save(SEOViewModel model)
        {
            model.Description = HttpUtility.UrlDecode(model.Description);
            var seomodel = new SEO()
            {
                Description = model.Description == null ? "" : model.Description,
                Keywords1 = model.Keywords[0],
                Keywords2 = model.Keywords[1],
                Keywords3 = model.Keywords[2],
                Keywords4 = model.Keywords[3],
                Keywords5 = model.Keywords[4],
                Keywords6 = model.Keywords[5],
                Keywords7 = model.Keywords[6],
                Keywords8 = model.Keywords[7],
                Keywords9 = model.Keywords[8],
                Keywords10 = model.Keywords[9],
                Title = model.WebsiteTitle == null ? "" : model.WebsiteTitle,
                TypeName = model.TypeName,
                Lang_ID = int.Parse(this.LanguageID)
            };
            var r = 0;
            if (model.ID == -1)
            {
                r = _service.Create(seomodel);
            }
            else
            {
                seomodel.ID = model.ID;
                r = _service.Update(seomodel) ? 1 : 0;
            }
            if (r > 0)
            {

                return Content("儲存成功");
            }
            else
            {
                return Content("儲存失敗");
            }

        }
    }
}