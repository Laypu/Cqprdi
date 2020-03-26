using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Filter;
using Oaww.ViewModel;
using Oaww.Entity;
using Oaww.Entity.SET;
using Oaww.Utility;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class ExclusiveLayoutController : BaseController
    {
        MenuService _menuService = new MenuService();
        CommonService _commonService = new CommonService();
        ExclusiveLayoutService _service = new ExclusiveLayoutService();

        [AuthFilter(_FuncionID = "ExclusiveLayout/Index")]
        public ActionResult Index()
        {
            ViewBag.Title = "專屬版面管理";
            return View();
        }

        public ActionResult PagingMain(SearchModelBase model)
        {
            model.LangId = this.LanguageID;
            return Json(_service.Paging(model));
        }

        public ActionResult MessageEdit(int ID = 0)
        {
            ExclusiveLayoutViewModel model = new ExclusiveLayoutViewModel();
            model.ExclusiveLayout = _commonService.GetGeneral<ExclusiveLayout>("ID=@ID", new Dictionary<string, string>() { { "ID", ID.ToString() } });

            model.SET_LAYOUT_GROUPs = _commonService.GetGeneralList<SET_LAYOUT_GROUP>().ToList();

            model.Users = _service.GetUsers();
            model.ExclusiveLayout.ImagelUrl = $"~/UploadImage/ExclusiveLayout/" + model.ExclusiveLayout.ImageFileName;

            return View(model);
        }

        public ActionResult SaveItem(ExclusiveLayoutViewModel model)
        {
            if (model.ExclusiveLayout.ImageFile != null)
            {
                var root = Request.PhysicalApplicationPath;
                model.ExclusiveLayout.ImageFileOrgName = model.ExclusiveLayout.ImageFile.FileName.Split('\\').Last();


                var newfilename = DateTime.Now.Ticks + "_" + model.ExclusiveLayout.ImageFileOrgName;
                var path = root + $"\\UploadImage\\ExclusiveLayout\\" + newfilename;
                if (System.IO.Directory.Exists(root + $"\\UploadImage\\ExclusiveLayout\\") == false)
                {
                    System.IO.Directory.CreateDirectory(root + $"\\UploadImage\\ExclusiveLayout\\");
                }
                model.ExclusiveLayout.ImageFile.SaveAs(path);
                model.ExclusiveLayout.ImageFileName = newfilename;
            }

            if (model.ExclusiveLayout.ID <= 0)
            {
                model.ExclusiveLayout.Enabled = true;
                model.ExclusiveLayout.Remark = model.ExclusiveLayout.Remark ?? "";
                model.ExclusiveLayout.Lang_ID = int.Parse(this.LanguageID);
            }
            

            string result = _service.SaveItem(model.ExclusiveLayout);

            if (result == "修改成功")
            {
                var olddata = _commonService.GetGeneral<ExclusiveLayout>("ID=@ID", new Dictionary<string, string>() { { "ID", model.ExclusiveLayout.ID.ToString() } });

                var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\ExclusiveLayout\\";


                if (model.ExclusiveLayout.ImageFileName.IsNullOrEmpty())
                {

                    if (System.IO.File.Exists(oldroot + "\\" + olddata.ImageFileName))
                    {
                        System.IO.File.Delete(oldroot + "\\" + olddata.ImageFileName);
                    }
                    model.ExclusiveLayout.ImageFileOrgName = "";
                    model.ExclusiveLayout.ImageFileName = "";
                }
                else
                {

                    if (olddata.ImageFileName != model.ExclusiveLayout.ImageFileName)
                    {
                        if (System.IO.File.Exists(oldroot + "\\" + olddata.ImageFileName))
                        {
                            System.IO.File.Delete(oldroot + "\\" + olddata.ImageFileName);
                        }
                    }
                }
            }

            return Content(result);
        }

        public ActionResult SetItemStatus(string id, bool status, string type)
        {
            string result = _commonService.SetStatus<ExclusiveLayout>(id, status);

            return Content(result);
        }

    }
}