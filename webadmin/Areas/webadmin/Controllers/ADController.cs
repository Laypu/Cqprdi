using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Filter;
using Oaww.Entity;
using Oaww.Entity.SET;
using Oaww.Business;
using Oaww.Utility;
using Oaww.ViewModel;
using System.Configuration;

namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class ADController : BaseController
    {
        CommonService _commonService = new CommonService();
        ADService _service = new ADService();
        SiteConfigService _siteConfigService = new SiteConfigService();
        #region Grid
        [AuthFilter(_FuncionID = "AD/Index", _paramter = "type")]
        public ActionResult Index(string type = "1")
        {
            #region security
            type = type.AntiXss();

            #endregion

            Session["menuNowID"] = 0;

            Session["type"] = type;

            if (type == null) { return RedirectToAction("Index", "Home"); }

            ViewBag.Type = type;
            ViewBag.Language = this.LanguageID;

            SET_AD SET_AD = _commonService.GetHisEntity<SET_AD>("ID", type);

            ViewBag.Title = SET_AD.M_AD01.safeHtmlFragment();
            SET_ADKanban SET_ADKanban = _commonService.GetHisEntity<SET_ADKanban>("ID", type);
            ViewBag.Kanban = SET_ADKanban != null && SET_ADKanban.M_KANBAN01.IsNullOrEmpty() == false ? true : false;
            return View();
        }

        public ActionResult Paging(ADSearchModel searchModel)
        {
            searchModel.Lang_ID = this.LanguageID;

            return Json(_service.Paging(searchModel));
        }

        /// <summary>
        /// 變更Order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seq"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult EditADSeq(int? id, int seq, string type)
        {
            string result = _service.UpdateSeq(id.Value, seq, type, this.Account, this.UserName);

            return Json(result);
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="delaccount"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult SetADDelete(string[] idlist, string delaccount, string type)
        {
            string result = _service.Delete(idlist, delaccount, this.LanguageID, this.Account, this.UserName);

            if (result == "刪除成功")
            {

                for (var idx = 0; idx < idlist.Length; idx++)
                {

                    var olddate = _service.GetADMain(idlist[idx]);
                    string stype = olddate.SType;
                    var entity = new ADMain();
                    entity.ID = int.Parse(idlist[idx]);
                    var adpath = string.Empty;


                    var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + $"\\UploadImage\\ADMain\\";

                    //刪除舊檔案
                    if (System.IO.File.Exists(oldroot + "\\" + olddate.Img_Name_Ori))
                    {
                        System.IO.File.Delete(oldroot + "\\" + olddate.Img_Name_Ori);
                    }
                    if (System.IO.File.Exists(oldroot + "\\" + olddate.Img_Name_Thumb))
                    {
                        System.IO.File.Delete(oldroot + "\\" + olddate.Img_Name_Thumb);
                    }

                    var uploadfilepath = ConfigurationManager.AppSettings["UploadFile"];
                    //刪除File
                    if (olddate.UploadVideoFilePath.IsNullOrEmpty() == false)
                    {
                        olddate.UploadVideoFilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadFile" + olddate.UploadVideoFilePath;

                        if (System.IO.File.Exists(olddate.UploadVideoFilePath))
                        {
                            System.IO.File.Delete(olddate.UploadVideoFilePath);
                        }
                    }
                }




            }
            return Content(result);
        }

        /// <summary>
        ///變更狀態
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult SetStatus(string id, bool status, string type)
        {
            string result = _service.UpdateStatus(id, status, this.Account, this.UserName);

            return Content(result);
        }
        #endregion

        [HttpPost]
        public ActionResult ADEdit(string type, string id)
        {
            ViewBag.isview = Request["isview"] == null ? "" : Request["isview"].ToString().AntiXss();

            if (type != null) { Session["ADType"] = type; }
            else
            {
                if (Session["ADType"] != null) { type = Session["ADType"].ToString(); }
            }
            if (type == null) { return RedirectToAction("Index", "Home"); }



            SET_AD SET_AD = _commonService.GetHisEntity<SET_AD>("ID", type);

            ViewBag.Title = SET_AD.M_AD01.safeHtmlFragment();



            var model = new ADEditModel() { ADDesc = "", SET_AD = SET_AD };

            model.Type = type;


            if (id.IsNullOrEmpty())
            {

                return View(model);
            }


            model = _service.GetModel(id);
            model.SET_AD = SET_AD;
            model.ImageUrl = Url.Content("~/UploadImage/ADMain/" + model.Img_Name_Thumb);
            model.ImageUrl2 = Url.Content("~/UploadImage/ADMain/" + model.Img_Name_Thumb2);
            if (model.UploadVideoFilePath.IsNullOrEmpty() == false)
            {
                model.UploadVideoFilePath = Url.Content("~/" + model.UploadVideoFilePath);
            }

            model.Type = type;


            ViewData["CreateUser"] = model.VerifyData == null ? "" : model.VerifyData.UpdateUser;


            return View(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult Save(ADEditModel model)
        {
            var adpath = "ADMain";
            model.SType = "P";
            SiteConfig ListConfig = _siteConfigService.GetALLSiteConfig("1");
            //刪除原本檔案
            if (model.ImageFile != null)
            {
                var fileformat = model.ImageFile.FileName.Split('.');
                var fullfilename = model.ImageFile.FileName.Split('\\').Last();
                var orgfilename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));
                long ticks = DateTime.Now.Ticks;
                var root = Request.PhysicalApplicationPath;
                var filename = ticks + "." + fileformat.Last();

                model.AD_Width = 0;
                model.AD_Height = 600;


                var checkpath = root + "\\UploadImage\\" + adpath + "\\";
                if (System.IO.Directory.Exists(checkpath) == false)
                {
                    System.IO.Directory.CreateDirectory(checkpath);
                }
                model.Img_Name_Ori = ticks + "_" + fullfilename;
                var path = root + "\\UploadImage\\" + adpath + "\\" + model.Img_Name_Ori;
                model.ImageFile.SaveAs(path);
                model.Img_Show_Name = fullfilename;
                var thumbpath = root + "\\UploadImage\\" + adpath + "\\" + ticks + "_thumb." + fileformat.Last();
                model.Img_Name_Thumb = ticks + "_thumb." + fileformat.Last();


                var haspath = UploadImg.uploadImgThumbMaxHeight(path, thumbpath, model.AD_Height.Value, fileformat.Last(), adjustImageEnum.limitMaxImageHeight);
                if (haspath == "") { model.Img_Name_Thumb = ""; }

            }
            if (model.ImageFile2 != null)
            {
                var fileformat = model.ImageFile2.FileName.Split('.');
                var fullfilename = model.ImageFile2.FileName.Split('\\').Last();
                var orgfilename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));
                long ticks = DateTime.Now.Ticks;
                ticks = ticks + 1;
                var root = Request.PhysicalApplicationPath;
                var filename = ticks + "." + fileformat.Last();

                model.AD_Width = 0;
                model.AD_Height = 600;

                var checkpath = root + "\\UploadImage\\" + adpath + "\\";
                if (System.IO.Directory.Exists(checkpath) == false)
                {
                    System.IO.Directory.CreateDirectory(checkpath);
                }
                model.Img_Name_Ori2 = ticks + "_" + fullfilename;
                var path = root + "\\UploadImage\\" + adpath + "\\" + model.Img_Name_Ori2;
                model.ImageFile2.SaveAs(path);
                model.Img_Show_Name2 = fullfilename;
                var thumbpath = root + "\\UploadImage\\" + adpath + "\\" + ticks + "_thumb." + fileformat.Last();
                model.Img_Name_Thumb2 = ticks + "_thumb." + fileformat.Last();


                var haspath = UploadImg.uploadImgThumbMaxHeight(path, thumbpath, model.AD_Height.Value, fileformat.Last(), adjustImageEnum.limitMaxImageHeight);
                if (haspath == "") { model.Img_Name_Thumb = ""; }

            }
            if (model.VideoFile != null)
            {
                model.UploadVideoFileName = model.VideoFile.FileName.Split('\\').Last();
                var uploadfilepath = Request.PhysicalApplicationPath;
                var newpath = uploadfilepath + "\\UploadVideo\\ADEdit\\";
                if (System.IO.Directory.Exists(newpath) == false)
                {
                    System.IO.Directory.CreateDirectory(newpath);
                }
                var guid = Guid.NewGuid();
                var filename = DateTime.Now.Ticks + "." + model.VideoFile.FileName.Split('.').Last();
                var path = newpath + filename;
                model.UploadVideoFilePath = "\\UploadVideo\\ADEdit\\" + filename;
                model.VideoFile.SaveAs(path);
            }


            model.Lang_ID = int.Parse(this.LanguageID);

            if (model.EdDateStr.IsNullOrEmpty() == false)
            {
                model.EdDate = DateTime.Parse(model.EdDateStr);
            }
            if (model.StDateStr.IsNullOrEmpty() == false)
            {
                model.StDate = DateTime.Parse(model.StDateStr);
            }




            if (model.ID >= 0)
            {

                if (model.Type == "main")
                {
                    adpath = "ADMain";
                }



                var olddata = _service.GetADMain(model.ID.ToString());

                var oldfilename = olddata.Img_Name_Thumb;
                var oldfileoriname = olddata.Img_Name_Ori;
                var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                    $"\\UploadImage\\{adpath}\\" + oldfilename;
                var oldoriroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                  $"\\UploadImage\\{adpath}\\" + oldfileoriname;
                var oldroot2 = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                   $"\\UploadImage\\{adpath}\\" + olddata.Img_Name_Thumb2;
                var oldoriroot2 = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                  $"\\UploadImage\\{adpath}\\" + olddata.Img_Name_Ori2;

                if (model.Img_Name_Ori.IsNullOrEmpty())
                {
                    olddata.Img_Name_Thumb = "";
                    olddata.Img_Name_Ori = "";
                    olddata.Img_Show_Name = "";
                }
                else
                {
                    olddata.Img_Name_Ori = model.Img_Name_Ori;
                    olddata.Img_Name_Thumb = model.Img_Name_Thumb;
                    olddata.Img_Show_Name = model.Img_Show_Name;
                }

                if (model.Img_Name_Ori2.IsNullOrEmpty())
                {
                    olddata.Img_Name_Thumb2 = "";
                    olddata.Img_Name_Ori2 = "";
                    olddata.Img_Show_Name2 = "";
                }
                else
                {
                    olddata.Img_Name_Ori2 = model.Img_Name_Ori2;
                    olddata.Img_Name_Thumb2 = model.Img_Name_Thumb2;
                    olddata.Img_Show_Name2 = model.Img_Show_Name2;
                }

                //1.create message
                var datetime = DateTime.Now;

                olddata.AD_Height = model.AD_Height == null ? 420 : model.AD_Height;
                olddata.AD_Width = model.AD_Width == null ? 600 : model.AD_Width;
                olddata.ADDesc = model.ADDesc;
                olddata.Icon = model.Icon;

                #region 影片
                var oldvideofilename = olddata.UploadVideoFilePath;
                var uploadfilepath = Request.PhysicalApplicationPath;

                if (model.UploadVideoFilePath.IsNullOrEmpty())
                {
                    olddata.UploadVideoFileDesc = "";
                    olddata.UploadVideoFileName = "";
                    olddata.UploadVideoFilePath = "";
                }
                else
                {
                    olddata.UploadVideoFileDesc = model.UploadVideoFileDesc;
                    olddata.UploadVideoFileName = model.UploadVideoFileName;
                    olddata.UploadVideoFilePath = model.UploadVideoFilePath;
                }
                #endregion

                olddata.Link_Mode = model.Link_Mode == null ? "" : model.Link_Mode;
                olddata.UpdateDatetime = datetime;
                olddata.UpdateUser = this.Account;
                olddata.StDate = model.StDate;
                olddata.EdDate = model.EdDate;
                olddata.AD_Name = model.AD_Name;
                olddata.Link_Href = model.Link_Href == null ? "" : model.Link_Href;
                olddata.Color = model.Color;

                string result = _service.Update(olddata, this.Account, this.UserName);

                if (result == "修改成功")
                {


                    try
                    {
                        if (model.Img_Name_Ori.IsNullOrEmpty() || model.Img_Name_Thumb != oldfilename)
                        {
                            //刪除舊檔案
                            if (System.IO.File.Exists(oldroot))
                            {
                                System.IO.File.Delete(oldroot);
                            }
                            if (System.IO.File.Exists(oldoriroot))
                            {
                                System.IO.File.Delete(oldoriroot);
                            }
                        }

                        if (model.Img_Name_Ori2.IsNullOrEmpty() || model.Img_Name_Thumb2 != oldfilename)
                        {
                            //刪除舊檔案
                            if (System.IO.File.Exists(oldroot2))
                            {
                                System.IO.File.Delete(oldroot2);
                            }
                            if (System.IO.File.Exists(oldoriroot2))
                            {
                                System.IO.File.Delete(oldoriroot2);
                            }
                        }

                        if (model.UploadVideoFileName.IsNullOrEmpty() || model.UploadVideoFilePath != oldvideofilename)
                        {

                            var newpath = uploadfilepath;
                            //刪除舊檔案
                            if (System.IO.File.Exists(newpath + oldvideofilename))
                            {
                                System.IO.File.Delete(newpath + oldvideofilename);
                            }

                        }
                        ListConfig.LastUpdateDate = DateTime.Now.ToString("yyy/MM/dd");
                        _siteConfigService.Update(ListConfig);
                    }
                    catch (Exception ex)
                    {

                        return Content("修改失敗");
                    }

                }

                return Content(result);
            }
            else
            {
                string result = _service.Create(model, this.Account, this.UserName);

                ListConfig.LastUpdateDate = DateTime.Now.ToString("yyy/MM/dd");
                _siteConfigService.Update(ListConfig);
                return Content(result);
            }


        }

        [AuthFilter(_FuncionID = "AD/Index", _paramter = "type")]
        public ActionResult ADKanbanEdit(string type)
        {
            ViewBag.isview = Request["isview"] == null ? "" : Request["isview"].ToString().AntiXss();

            if (type != null) { Session["ADType"] = type; }
            else
            {
                if (Session["ADType"] != null) { type = Session["ADType"].ToString(); }
            }
            if (type == null) { return RedirectToAction("Index", "Home"); }

            SET_AD SET_AD = _commonService.GetHisEntity<SET_AD>("ID", type);
            SET_ADKanban SET_ADKanban = _commonService.GetHisEntity<SET_ADKanban>("ID", type);

            ViewBag.Title = SET_AD.M_AD01.safeHtmlFragment();

            var model = new ADEditModel() { ADDesc = "" };

            model.Type = type;

            if (id.IsNullOrEmpty())
            {
                return View(model);
            }

            model = _service.GetKanbanModel(type,this.LanguageID);
            if (model == null)
            {
                model = new ADEditModel() {  Type_ID = type };
            }

            model.SET_AD = SET_AD;
            model.SET_ADKanban = SET_ADKanban;
            model.ImageUrl = Url.Content("~/UploadImage/ADMain/" + model.Img_Name_Thumb);

            return View(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SaveKanban(ADEditModel model)
        {
            var adpath = "ADMain";
            model.SType = "P";

            //刪除原本檔案
            if (model.ImageFile != null)
            {
                var fileformat = model.ImageFile.FileName.Split('.');
                var fullfilename = model.ImageFile.FileName.Split('\\').Last();
                var orgfilename = fullfilename.Substring(0, fullfilename.LastIndexOf("."));
                long ticks = DateTime.Now.Ticks;
                var root = Request.PhysicalApplicationPath;
                var filename = ticks + "." + fileformat.Last();

                model.AD_Width = 0;
                model.AD_Height = 600;


                var checkpath = root + "\\UploadImage\\" + adpath + "\\";
                if (System.IO.Directory.Exists(checkpath) == false)
                {
                    System.IO.Directory.CreateDirectory(checkpath);
                }
                model.Img_Name_Ori = ticks + "_" + fullfilename;
                var path = root + "\\UploadImage\\" + adpath + "\\" + model.Img_Name_Ori;
                model.ImageFile.SaveAs(path);
                model.Img_Show_Name = fullfilename;
                var thumbpath = root + "\\UploadImage\\" + adpath + "\\" + ticks + "_thumb." + fileformat.Last();
                model.Img_Name_Thumb = ticks + "_thumb." + fileformat.Last();


                var haspath = UploadImg.uploadImgThumbMaxHeight(path, thumbpath, model.AD_Height.Value, fileformat.Last(), adjustImageEnum.limitMaxImageHeight);
                if (haspath == "") { model.Img_Name_Thumb = ""; }

            }
            if (model.VideoFile != null)
            {
                model.UploadVideoFileName = model.VideoFile.FileName.Split('\\').Last();
                var uploadfilepath = Request.PhysicalApplicationPath;
                var newpath = uploadfilepath + "\\UploadVideo\\ADEdit\\";
                if (System.IO.Directory.Exists(newpath) == false)
                {
                    System.IO.Directory.CreateDirectory(newpath);
                }
                var guid = Guid.NewGuid();
                var filename = DateTime.Now.Ticks + "." + model.VideoFile.FileName.Split('.').Last();
                var path = newpath + filename;
                model.UploadVideoFilePath = "\\UploadVideo\\ADEdit\\" + filename;
                model.VideoFile.SaveAs(path);
            }


            model.Lang_ID = int.Parse(this.LanguageID);


            if (model.ID >= 0)
            {

                var olddata = _service.GetADKanban(model.ID.ToString());
                olddata.Lang_ID = model.Lang_ID;
                var oldfilename = olddata.Img_Name_Thumb;
                var oldfileoriname = olddata.Img_Name_Ori;
                var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                    $"\\UploadImage\\{adpath}\\" + oldfilename;
                var oldoriroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                  $"\\UploadImage\\{adpath}\\" + oldfileoriname;
                if (model.Img_Name_Ori.IsNullOrEmpty())
                {
                    olddata.Img_Name_Thumb = "";
                    olddata.Img_Name_Ori = "";
                    olddata.Img_Show_Name = "";
                }
                else
                {
                    olddata.Img_Name_Ori = model.Img_Name_Ori;
                    olddata.Img_Name_Thumb = model.Img_Name_Thumb;
                    olddata.Img_Show_Name = model.Img_Show_Name;
                }



                //1.create message
                var datetime = DateTime.Now;


                olddata.ADDesc = model.ADDesc;



                olddata.Link_Mode = model.Link_Mode == null ? "" : model.Link_Mode;
                olddata.UpdateDatetime = datetime;
                olddata.UpdateUser = this.Account;

                olddata.AD_Name = model.AD_Name;
                olddata.Link_Href = model.Link_Href == null ? "" : model.Link_Href;

                if (model.UploadVideoFilePath.IsNullOrEmpty())
                {
                    olddata.UploadVideoFileDesc = "";
                    olddata.UploadVideoFileName = "";
                    olddata.UploadVideoFilePath = "";
                }
                else
                {
                    olddata.UploadVideoFileDesc = model.UploadVideoFileDesc;
                    olddata.UploadVideoFileName = model.UploadVideoFileName;
                    olddata.UploadVideoFilePath = model.UploadVideoFilePath;
                }
                string result = _service.UpdateKanban(olddata, this.Account, this.UserName);

                if (result == "修改成功")
                {
                    var oldvideofilename = olddata.UploadVideoFilePath;
                    var uploadfilepath = Request.PhysicalApplicationPath;
                    try
                    {
                        if (model.Img_Name_Ori.IsNullOrEmpty() || model.Img_Name_Thumb != oldfilename)
                        {
                            //刪除舊檔案
                            if (System.IO.File.Exists(oldroot))
                            {
                                System.IO.File.Delete(oldroot);
                            }
                            if (System.IO.File.Exists(oldoriroot))
                            {
                                System.IO.File.Delete(oldoriroot);
                            }
                        }

                        if (model.UploadVideoFileName.IsNullOrEmpty() || model.UploadVideoFilePath != oldvideofilename)
                        {

                            var newpath = uploadfilepath;
                            //刪除舊檔案
                            if (System.IO.File.Exists(newpath + oldvideofilename))
                            {
                                System.IO.File.Delete(newpath + oldvideofilename);
                            }

                        }
                    }
                    catch (Exception ex)
                    {

                        return Content("修改失敗");
                    }

                }

                return Content(result);
            }
            else
            {
                string result = _service.CreateKanba(model, this.Account, this.UserName);


                return Content(result);
            }


        }
    }
}