using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.ViewModel;
using Oaww.ViewModel.Lang;
using Oaww.Utility;
using System.Data.SqlClient;

namespace Oaww.Business
{
    public class SiteService : Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();

        public SiteLayout GetSiteLayoutByID(string ID)
        {
            return _commonService.GetGeneral<SiteLayout>("ID=@ID", new Dictionary<string, string>() { { "ID", ID } });
        }

        /// <summary>
        /// 取得SiteLayou Model
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="langid"></param>
        /// <returns></returns>
        public SiteLayoutModel GetSiteLayout(string stype, string langid)
        {
            SiteLayout data = _commonService.GetGeneral<SiteLayout>("SType=@SType and LangID=@LangID", new Dictionary<string, string>() { { "SType", stype }, { "LangID", langid } });



            if (data.ID != 0)
            {
                var model = new SiteLayoutModel()
                {
                    FirstPageImageUrl = "~/UploadImage/SiteLayout/" + data.FirstPageImgNameOri,
                    FirstPageImgNameThumb = data.FirstPageImgNameThumb,
                    FirstPageImgNameOri = data.FirstPageImgNameOri,
                    FirstPageImgShowName = data.FirstPageImgShowName,
                    HtmlContent = data.HtmlContent,
                    ID = data.ID,
                    InsidePageImageUrl = "~/UploadImage/SiteLayout/" + data.InsidePageImgNameOri,
                    InsidePageImgNameOri = data.InsidePageImgNameOri,
                    InsidePageImgNameThumb = data.InsidePageImgNameThumb,
                    InsidePageImgShowName = data.InsidePageImgShowName,
                    LogoImageUrl = "~/UploadImage/SiteLayout/" + data.LogoImgNameOri,
                    LogoImageUrlThumb = "~/UploadImage/SiteLayout/" + data.LogoImgNameThumb,

                    LogoImgNameOri = data.LogoImgNameOri,
                    LogoImgNameThumb = data.LogoImgNameThumb,
                    LogoImgShowName = data.LogoImgShowName,

                    FowardImgNameOri = data.FowardImgNameOri,
                    FowardImgNameThumb = data.FowardImgNameThumb,
                    FowardImgShowName = data.FowardImgShowName,
                    FowardImageUrl = "~/UploadImage/SiteLayout/" + data.FowardImgNameOri,
                    FowardHtmlContent = data.FowardHtmlContent,

                    PrintImgNameOri = data.PrintImgNameOri,
                    PrintImgNameThumb = data.PrintImgNameThumb,
                    PrintImgShowName = data.PrintImgShowName,
                    PrintImageUrl = data.PrintImgNameOri.IsNullOrEmpty() ? "" : "~/UploadImage/SiteLayout/" + data.PrintImgNameOri,
                    PrintHtmlContent = data.PrintHtmlContent,
                    Page404HtmlContent = data.Page404HtmlContent,
                    Page404Title = data.Page404Title,
                    SType = stype,
                    PublishContent = data.PublishContent,
                    InnerLogoImageUrl = "~/UploadImage/SiteLayout/" + data.InnerLogoImgNameOri,
                    InnerLogoImageUrlThumb = "~/UploadImage/SiteLayout/" + data.InnerLogoImgNameThumb,
                    InnerLogoImgNameOri = data.InnerLogoImgNameOri,
                    InnerLogoImgNameThumb = data.InnerLogoImgNameThumb,
                    InnerLogoImgShowName = data.InnerLogoImgShowName,
                    Recommand = data.Recommand,
                    RecommandImgNameOri = data.RecommandImgNameOri,
                    RecommandImgShowName = data.RecommandImgShowName,
                    RecommandImageUrl = "~/UploadImage/SiteLayout/" + data.RecommandImgNameOri,
                };
                return model;
            }
            else
            {
                return new SiteLayoutModel() { SType = stype };
            }
        }

        /// <summary>
        /// 編輯SiteLayout
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string EditSiteLayout(SiteLayoutModel model)
        {


            var olddata = _commonService.GetGeneral<SiteLayout>("ID=@ID and LangID=@LangID", new Dictionary<string, string>() { { "ID", model.ID.ToString() }, { "LangID", model.LangID } });

            olddata.ID = model.ID.Value;
            olddata.LangID = int.Parse(model.LangID);

            olddata.FirstPageImgNameOri = model.FirstPageImgNameOri == null ? olddata.FirstPageImgNameOri : model.FirstPageImgNameOri;
            olddata.FirstPageImgNameThumb = model.FirstPageImgNameThumb == null ? olddata.FirstPageImgNameThumb : model.FirstPageImgNameThumb;
            olddata.FirstPageImgShowName = model.FirstPageImgShowName == null ? olddata.FirstPageImgShowName : model.FirstPageImgShowName;

            olddata.InsidePageImgNameThumb = model.InsidePageImgNameThumb == null ? olddata.InsidePageImgNameThumb : model.InsidePageImgNameThumb;
            olddata.InsidePageImgShowName = model.InsidePageImgShowName == null ? olddata.InsidePageImgShowName : model.InsidePageImgShowName;
            olddata.InsidePageImgNameOri = model.InsidePageImgNameOri == null ? olddata.InsidePageImgNameOri : model.InsidePageImgNameOri;

            olddata.InnerLogoImgNameOri = model.InnerLogoImgNameOri == null ? "" : model.InnerLogoImgNameOri;
            olddata.InnerLogoImgShowName = model.InnerLogoImgShowName == null ? "" : model.InnerLogoImgShowName;
            olddata.InnerLogoImgNameThumb = model.InnerLogoImgNameThumb == null ? "" : model.InnerLogoImgNameThumb;

            olddata.HtmlContent = model.HtmlContent == null ? olddata.HtmlContent : model.HtmlContent;
            olddata.LogoImgNameOri = model.LogoImgNameOri == null ? "" : model.LogoImgNameOri;
            olddata.LogoImgShowName = model.LogoImgShowName == null ? "" : model.LogoImgShowName;
            olddata.LogoImgNameThumb = model.LogoImgNameThumb == null ? "" : model.LogoImgNameThumb;           

            olddata.PublishContent = model.PublishContent == null ? olddata.PublishContent : model.PublishContent;


            olddata.SType = model.SType;
            olddata.Recommand = model.Recommand;
            olddata.RecommandImgNameOri = model.RecommandImgNameOri;
            olddata.RecommandImgShowName = model.RecommandImgShowName;

            bool r = false;

            try
            {
                if (olddata.ID > 0)
                {

                    olddata.LogoImgShowName = model.LogoImageFile != null ? model.LogoImgShowName
                                                : (model.LogoImgNameOri.IsNullOrEmpty()
                                                                ? ""  //砍掉
                                                                : olddata.LogoImgShowName); //不變

                    olddata.LogoImgNameThumb = model.LogoImageFile != null ? model.LogoImgNameThumb
                                                : (model.LogoImgNameOri.IsNullOrEmpty()
                                                                ? ""  //砍掉
                                                                : olddata.LogoImgNameThumb); //不變

                    olddata.InnerLogoImgShowName = model.InnerLogoImageFile != null ? model.InnerLogoImgShowName
                                                : (model.InnerLogoImgNameOri.IsNullOrEmpty()
                                                                ? ""  //砍掉
                                                                : olddata.InnerLogoImgShowName); //不變

                    olddata.InnerLogoImgNameThumb = model.InnerLogoImageFile != null ? model.InnerLogoImgNameThumb
                                               : (model.InnerLogoImgNameOri.IsNullOrEmpty()
                                                               ? ""  //砍掉
                                                               : olddata.InnerLogoImgNameThumb); //不變

                    r = base.UpdateObject(olddata);

                }
                else
                {
                    r = base.InsertObject(olddata) > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新網站導覽異常,error:" + ex.ToString().NewLineReplace());
            }

            if (r)
            {
                return "設定成功";
            }
            else
            {
                return "設定失敗";
            }
        }

        /// <summary>
        /// 轉寄好友
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string EditFowardSiteLayout(SiteLayoutModel model)
        {

            var r = 0;

            try
            {
                if (model.ID != -1)
                {
                    var olddata = _commonService.GetGeneral<SiteLayout>("ID=@ID", new Dictionary<string, string>() { { "ID", model.ID.ToString() } });
                    var updatedata = olddata;
                    var oldfilename = updatedata.FowardImgNameThumb;
                    var oldfileoriname = updatedata.FowardImgNameOri;
                    updatedata.FowardHtmlContent = model.FowardHtmlContent == null ? "" : model.FowardHtmlContent;
                    if (string.IsNullOrEmpty(model.FowardImgNameOri))
                    {
                        updatedata.FowardImgNameThumb = "";
                        updatedata.FowardImgShowName = "";
                        updatedata.FowardImgNameOri = "";
                    }
                    else
                    {
                        if (updatedata.FowardImgNameOri != model.FowardImgNameOri)
                        {
                            updatedata.FowardImgNameOri = model.FowardImgNameOri;
                            updatedata.FowardImgNameThumb = model.FowardImgNameThumb;
                            updatedata.FowardImgShowName = model.FowardImgShowName;
                        }
                    }
                    r = base.UpdateObject(updatedata) ? 1 : 0;

                }
                else
                {
                    var SiteLayout = new SiteLayout()
                    {
                        FowardHtmlContent = model.FowardHtmlContent,
                        FowardImgNameOri = model.FowardImgNameOri,
                        FowardImgNameThumb = model.FowardImgNameThumb,
                        FowardImgShowName = model.FowardImgShowName,
                        SType = model.SType
                    };

                    r = base.InsertObject(SiteLayout);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新網站導覽異常,error:" + ex.ToString().NewLineReplace());
            }

            if (r > 0)
            {
                return "設定成功";
            }
            else
            {
                return "設定失敗";
            }
        }

        /// <summary>
        /// 友善列印
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string EditPrintSiteLayout(SiteLayoutModel model)
        {
            var r = 0;

            try
            {
                if (model.ID != -1)
                {
                    var olddata = _commonService.GetGeneral<SiteLayout>("ID=@ID", new Dictionary<string, string>() { { "ID", model.ID.ToString() } });
                    var updatedata = olddata;
                    var oldfilename = updatedata.PrintImgNameThumb;
                    var oldfileoriname = updatedata.PrintImgNameOri;
                    updatedata.PrintHtmlContent = model.PrintHtmlContent == null ? "" : model.PrintHtmlContent;
                    if (string.IsNullOrEmpty(model.PrintImgNameOri))
                    {
                        updatedata.PrintImgNameThumb = "";
                        updatedata.PrintImgShowName = "";
                        updatedata.PrintImgNameOri = "";
                    }
                    else
                    {
                        if (updatedata.PrintImgNameOri != model.PrintImgNameOri)
                        {
                            updatedata.PrintImgNameOri = model.PrintImgNameOri;
                            updatedata.PrintImgNameThumb = model.PrintImgNameThumb;
                            updatedata.PrintImgShowName = model.PrintImgShowName;
                        }
                    }
                    r = base.UpdateObject(updatedata) ? 1 : 0;

                }
                else
                {
                    var SiteLayout = new SiteLayout()
                    {
                        ID = model.ID.Value,
                        PrintHtmlContent = model.PrintHtmlContent,
                        PrintImgNameOri = model.PrintImgNameOri,
                        PrintImgNameThumb = model.PrintImgNameThumb,
                        PrintImgShowName = model.PrintImgShowName
                    };
                    r = base.InsertObject(SiteLayout);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新網站導覽異常，error:" + ex.ToString().NewLineReplace());
            }


            if (r > 0)
            {
                return "儲存成功";
            }
            else
            {
                return "儲存失敗";
            }
        }

        /// <summary>
        /// 404
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string EditPage404SiteLayout(SiteLayoutModel model)
        {
            var r = 0;

            try
            {
                if (model.ID != -1)
                {
                    var olddata = _commonService.GetGeneral<SiteLayout>("ID=@ID", new Dictionary<string, string>() { { "ID", model.ID.ToString() } });
                    var updatedata = olddata;
                    updatedata.Page404Title = model.Page404Title == null ? "" : model.Page404Title;
                    updatedata.Page404HtmlContent = model.Page404HtmlContent == null ? "" : model.Page404HtmlContent;
                    r = base.UpdateObject(updatedata) ? 1 : 0;

                }
                else
                {
                    var SiteLayout = new SiteLayout()
                    {
                        ID = model.ID.Value,
                        Page404HtmlContent = model.Page404HtmlContent,
                        Page404Title = model.Page404Title
                    };
                    r = base.InsertObject(SiteLayout);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新網站導覽異常,error:" + ex.ToString().NewLineReplace());
            }

            if (r > 0)
            {
                return "儲存成功";
            }
            else
            {
                return "儲存失敗";
            }
        }

        public SiteLangTextModel GetSiteLangText(string langid)
        {
            var input = _commonService.GetGeneralList<LangInputText>("LangID=@LangID", new Dictionary<string, string>() { { "LangID", langid } }).ToDictionary(v => v.LangTextID, v => v.Text);
            var data = _commonService.GetGeneralList<LangKey>("GroupName='BasicSetting' and Used=1");
            if (data.Count() > 0)
            {
                var model = new SiteLangTextModel();
                model.GroupKey1 = data.Where(v => v.SubGroup == 1).OrderBy(v => v.Sort).ToDictionary(k => k.ID.ToString(), i => i.Item);
                foreach (var key in data.Where(v => v.SubGroup == 1).OrderBy(v => v.Sort).Select(v => v.ID).ToArray())
                {
                    if (input.ContainsKey(key))
                    {
                        model.Group1.Add(key.ToString(), input[key]);
                    }
                    else
                    {
                        model.Group1.Add(key.ToString(), "");
                    }
                }

                model.GroupKey2 = data.Where(v => v.SubGroup == 2).OrderBy(v => v.Sort).ToDictionary(k => k.ID.ToString(), i => i.Item);
                foreach (var key in data.Where(v => v.SubGroup == 2).OrderBy(v => v.Sort).Select(v => v.ID).ToArray())
                {
                    if (input.ContainsKey(key))
                    {
                        model.Group2.Add(key.ToString(), input[key]);
                    }
                    else
                    {
                        model.Group2.Add(key.ToString(), "");
                    }
                }

                model.GroupKey3 = data.Where(v => v.SubGroup == 3).OrderBy(v => v.Sort).ToDictionary(k => k.ID.ToString(), i => i.Item);
                foreach (var key in data.Where(v => v.SubGroup == 3).OrderBy(v => v.Sort).Select(v => v.ID).ToArray())
                {
                    if (input.ContainsKey(key))
                    {
                        model.Group3.Add(key.ToString(), input[key]);
                    }
                    else
                    {
                        model.Group3.Add(key.ToString(), "");
                    }
                }

                model.GroupKey4 = data.Where(v => v.SubGroup == 4).OrderBy(v => v.Sort).ToDictionary(k => k.ID.ToString(), i => i.Item);
                foreach (var key in data.Where(v => v.SubGroup == 4).OrderBy(v => v.Sort).Select(v => v.ID).ToArray())
                {
                    if (input.ContainsKey(key))
                    {
                        model.Group4.Add(key.ToString(), input[key]);
                    }
                    else
                    {
                        model.Group4.Add(key.ToString(), "");
                    }
                }

                model.GroupKey5 = data.Where(v => v.SubGroup == 5).OrderBy(v => v.Sort).ToDictionary(k => k.ID.ToString(), i => i.Item);
                foreach (var key in data.Where(v => v.SubGroup == 5).OrderBy(v => v.Sort).Select(v => v.ID).ToArray())
                {
                    if (input.ContainsKey(key))
                    {
                        model.Group5.Add(key.ToString(), input[key]);
                    }
                    else
                    {
                        model.Group5.Add(key.ToString(), "");
                    }
                }
                model.GroupKey6 = data.Where(v => v.SubGroup == 6).OrderBy(v => v.Sort).ToDictionary(k => k.ID.ToString(), i => i.Item);
                foreach (var key in data.Where(v => v.SubGroup == 6).OrderBy(v => v.Sort).Select(v => v.ID).ToArray())
                {
                    if (input.ContainsKey(key))
                    {
                        model.Group6.Add(key.ToString(), input[key]);
                    }
                    else
                    {
                        model.Group6.Add(key.ToString(), "");
                    }
                }
                return model;
            }
            else
            {
                return new SiteLangTextModel();
            }

        }

        public int CreateLang(LangInputText LangInputText)
        {
            return (int)base.InsertObject(LangInputText);
        }

        public int UpdateLang(string Text,string LangID,string LangTextID)
        {
            string sql = "update LangInputText set Text=@Text where LangID=@LangID and LangTextID=@LangTextID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Text", Text));
            base.Parameter.Add(new SqlParameter("@LangID", LangID));
            base.Parameter.Add(new SqlParameter("@LangTextID", LangTextID));

            return base.ExeNonQuery(sql);
        }
    }
}
