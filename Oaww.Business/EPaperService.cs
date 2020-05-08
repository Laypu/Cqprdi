using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.Entity.SET;
using System.Data.SqlClient;
using Oaww.ViewModel;
using Oaww.ViewModel.Search;
using Oaww.Utility;
using System.Web;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Web.Mvc;

namespace Oaww.Business
{
    public class EPaperService : Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();
        protected int _ModelID = 12;

        public EPaperService(int ModelID)
        {
            _ModelID = ModelID;
        }
        public string GetEPMulti(string Key, int MainID)
        {
            string sql = string.Empty;
            string deval = "";
            switch (Key)
            {
                case "Column1":
                    sql ="select t.Column1 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "序號";
                    break;
                case "Column2":
                    sql = "select t.Column2 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "發佈日期";
                    break;
                case "Column3":
                    sql = "select t.Column3 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "電子報名稱";
                    break;
                case "Column4":
                    sql = "select t.Column4 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "類別";
                    break;
                case "Column5":
                    sql = "select t.Column5 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "訂閱電子報";
                    break;
                case "Column6":
                    sql = "select t.Column6 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "訂閱";
                    break;
                case "Column7":
                    sql = "select t.Column7 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "取消";
                    break;
                case "Column8":
                    sql = "select t.Column8 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "查閱電子報";
                    break;
                case "Column9":
                    sql = "select t.Column9 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "請輸入Email";
                    break;
                case "Column10":
                    sql = "select t.Column10 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "EMail格式錯誤";
                    break;
                case "Column11":
                    sql = "select t.Column11 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "此 Email 已有訂閱電子報!";
                    break;
                case "Column12":
                    sql = "select t.Column12 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "電子報訂閱成功!";
                    break;
                case "Column13":
                    sql = "select t.Column13 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "此 EMail 已經訂閱!";
                    break;
                case "Column14":
                    sql = "select t.Column14 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "電子報取消訂閱成功!";
                    break;
                case "Column15":
                    sql = "select t.Column15 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "EMail請確實輸入";
                    break;
                case "Column16":
                    sql = "select t.Column16 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "電子報訂閱失敗!";
                    break;
                case "Column17":
                    sql = "select t.Column17 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "此 Email 無訂閱電子報!";
                    break;
                case "Column18":
                    sql = "select t.Column18 from EPaperUnitSetting t where t.LangID=@LangID and t.MainID=@MainID";
                    deval = "電子報取消訂閱失敗";
                    break;
            }
            var Lang = HttpContext.Current.Session["LangID"].ToString();
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@LangID", Lang));
            base.Parameter.Add(new SqlParameter("@MainID", MainID));
            var keyval = base.ExecuteScalar(sql).ToString();
            return keyval.IsNullOrEmpty()? deval : base.ExecuteScalar(sql).ToString();
        }
        public List<GroupEPaper> GetVaildGroupEPapers(string Main_ID,string Lang_ID)
        {
            string sql = @"with cte as
                            (
                            select distinct GroupID from EPaperItem s where ModelID = @Main_ID 
                            and  s.Enabled = 1 
                            
                            )
                            select * from cte  t
                            left join GroupEPaper s on t.GroupID = s.ID where Lang_ID=@Lang_ID order by sort";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Main_ID", Main_ID));
            base.Parameter.Add(new SqlParameter("@Lang_ID", Lang_ID));
            
            return base.SearchList<GroupEPaper>(sql);
        }

        public List<EPaperItem> GetEPaperItemsByModelID(string[] idlist)
        {
            string sql = @"select * from EPaperItem where ModelID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
            return base.SearchList<EPaperItem>(sql);
        }

        public List<EPaperItem> GetEPaperItemsByModelID(string model_id)
        {
            string sql = @"select * from EPaperItem where ModelID =@model_id";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@model_id", model_id));
            return base.SearchList<EPaperItem>(sql);
        }

        public List<EPaperItem> GetEPaperItems(string[] idlist)
        {
            string sql = @"select * from EPaperItem where ItemID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
            return base.SearchList<EPaperItem>(sql);
        }

        public EPaperItem GetEPaperItemByID(string ItemID)
        {
            return _commonService.GetHisEntity<EPaperItem>("ItemID", ItemID);
        }

        public GroupEPaper GetGroupEPaperByID(string ID)
        {
            return _commonService.GetHisEntity<GroupEPaper>("ID", ID); 
        }

        public ModelEPaperMain GetModelEPaperMain(string ID, string lang)
        {
            string sql = @"select * from ModelEPaperMain where ID=@ID and Lang_ID=@Lang_ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));
            base.Parameter.Add(new SqlParameter("@Lang_ID", lang));

            return base.GetObject<ModelEPaperMain>(sql);
        }

        /// <summary>
        /// Grid資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Paging<ModelEPaperMain> Paging(SearchModelBase model, int ModelID)
        {
            string sql = string.Empty;

            var Paging = new Paging<ModelEPaperMain>();
            var onepagecnt = model.Limit;

            sql = "select * from ModelEPaperMain where Lang_ID=@Lang_ID and ModelID=@ModelID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", model.LangId));
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            Paging.rows = base.SearchListPage<ModelEPaperMain>(sql, model.Offset, model.Limit, " order by " + model.Sort);
            Paging.total = base.SearchCount(sql);

            return Paging;
        }

        /// <summary>
        /// 刪除Main資料
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="delaccount"></param>
        /// <param name="langid"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string Delete(string[] idlist, string delaccount, string langid, string account, string username)
        {
            string sql = string.Empty;
            string rstr = string.Empty;
            var r = 0;
            //檢查是否在使用中

            sql = "select * from Menu where ModelID=@ModelID and ModelItemID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
            base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));

            if (base.SearchCount(sql) > 0)
            {
                return "刪除失敗,刪除的項目使用中..";
            }

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        //delete  ModelEPaperMain _OK
                        sql = @"delete ModelEPaperMain where ID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //delete VerifyData_OK
                        sql = @"delete VerifyData where ModelID=@ModelID and ModelMainID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                        base.ExeNonQuery(sql, tran);

                        //刪除PageUnitSetting
                        sql = @"delete EPaperUnitSetting where MainID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //刪除SEO
                        sql = @"delete SEO where TypeName='EPaperItem' and TypeID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);
                        // _seosqlrepository.DelDataUseWhere("TypeName='PageIndexItem' and TypeID=@1", new object[] { items.ItemID });

                        for (var idx = 0; idx < idlist.Length; idx++)
                        {
                            //刪除PageIndexItem
                            List<EPaperItem> olditem = _commonService.GetGeneralList<EPaperItem>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", idlist[idx] } }, tran)
                                                           .ToList();

                            sql = @"delete EPaperItem where ModelID=@ModelID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ModelID", idlist[idx]));
                            base.ExeNonQuery(sql, tran);



                            //foreach (var items in olditem)
                            //{

                            //    //刪除SEO
                            //    sql = @"delete SEO where TypeName='EPaperItem' and TypeID=@TypeID";
                            //    base.Parameter.Clear();
                            //    base.Parameter.Add(new SqlParameter("@TypeID",items.ItemID));
                            //    base.ExeNonQuery(sql, tran);


                            //}

                        }

                        //update sort order
                        sql = @"with cte as
                                    (
                                    select 
                                     ROW_NUMBER()OVER(order by sort) as ROW,t.ID
                                     from ModelEPaperMain t where t.Lang_ID=@Lang and t.ModelID=@ModelID
                                    )
                                    update s set s.Sort = t.ROW from cte  t
                                    left join ModelEPaperMain s on t.ID = s.ID and s.ModelID=@ModelID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                        base.Parameter.Add(new SqlParameter("@Lang", langid));
                        base.ExeNonQuery(sql, tran);

                        tran.Commit();
                        rstr = "刪除成功";

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "刪除訊息模組異常,error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        rstr = "刪除失敗";
                    }

                    return rstr;
                }
            }
        }



        public Paging<EPaperItemResult> PagingItem(EPaperSearchModel model, string modelid)
        {
            string sql = string.Empty;

            var Paging = new Paging<EPaperItemResult>();
            Paging.rows = new List<EPaperItemResult>();
            var datas = new List<EPaperItem>();
            var onepagecnt = model.Limit;

            base.Parameter.Clear();
            sql = "select * from EPaperItem where LangID=@Lang_ID and ModelID=@ModelID";
            if (model.GroupId != null)
            {
                sql += " and GroupID=@GroupID";
                base.Parameter.Add(new SqlParameter("@GroupID", model.GroupId.Value));
            }
            
            base.Parameter.Add(new SqlParameter("@Lang_ID", model.LangId));
            base.Parameter.Add(new SqlParameter("@ModelID", modelid));

            var ALLGroup = _commonService.GetGeneralList<GroupEPaper>();
            datas = base.SearchListPage<EPaperItem>(sql, model.Offset, model.Limit, " order by " + model.Sort).ToList();
            //Paging.rows = base.SearchListPage<EPaperItemResult>(sql, model.Offset, model.Limit, " order by " + model.Sort).ToList();

            foreach (var data in datas)
            {
                var GroupName = ALLGroup.Where(v => v.ID == data.GroupID);
                Paging.rows.Add(new EPaperItemResult()
                {
                    ItemID =data.ItemID,
                    GroupName = GroupName.Count() > 0 ? GroupName.First().Group_Name : "無分類",
                    Title = data.Title,
                    IsPublishStr = data.IsPublished == true ? "已發佈" : ("<button class='btn blue isedit' id='editbtn_" + data.ItemID + "' value='" + data.ItemID + "'>發佈</button>"),
                    FormatStr = data.PaperMode == 1 ? "手動" : "自動",
                    Enabled = data.Enabled,
                    Sort = data.Sort.Value,
                    Introduction = data.Introduction,
                    IsPublished = data.IsPublished,
                    PublishStr = data.PublishStr,

                });
            }
            
            

            
            Paging.total = base.SearchCount(sql);

            return Paging;
        }

        public EPaperEditModel GetModelByID(string modelid, string itemid, string folder)
        {
            var maindata = _commonService.GetGeneral<ModelEPaperMain>("ID=@ID", new Dictionary<string, string>() { { "ID", modelid } });
            var data = _commonService.GetGeneral<EPaperItem>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", itemid } });
            if (itemid != "-1")
            {
                var olddata = data;
                var seodata = _commonService.GetGeneral<SEO>("TypeName='EPaperItem' and TypeID=@TypeID", new Dictionary<string, string>() { { "TypeID", itemid } });
                EPaperEditModel model = new EPaperEditModel();
                model.ItemID = olddata.ItemID;
                model.Lang_ID = olddata.LangID;
                model.PaperMode = olddata.PaperMode.Value;
                model.PaperStyle = olddata.PaperStyle.Value;
                model.PublishStr = olddata.PublishStr;
                model.Title = olddata.Title;
                model.Introduction = olddata.Introduction;
                model.TopBannerImgUrl = VirtualPathUtility.ToAbsolute("~/UploadImage/EPaper/" + olddata.TopBannerImgName);
                model.TopBannerImgPath = olddata.TopBannerImgPath;
                model.TopBannerImgOrgName = olddata.TopBannerImgOrgName;
                model.TopBannerImgName = olddata.TopBannerImgName;
                model.PageEndHtmlContent = olddata.PageEndHtmlContent;
                model.Enabled = olddata.Enabled.Value;
                model.TopHtmlContent = olddata.TopHtmlContent;
                model.LeftHtmlContent = olddata.LeftHtmlContent;
                model.CenterHtmlContent = olddata.CenterHtmlContent;
                model.BottomHtmlContent = olddata.BottomHtmlContent;
                model.ModelID = maindata.ID;
                
                return model;
            }
            else
            {
                EPaperEditModel model = new EPaperEditModel();
                model.ItemID = int.Parse(itemid);
                model.ModelID = maindata.ID;
                return model;
            }
                


            
            

        }

        /// <summary>
        /// 新增Item
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LangId"></param>
        /// <param name="account"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CreateItem(EPaperEditModel model, string LangId, string account, string userName)
        {
            string sql = string.Empty;

            SET_BASE SET_BASE = _commonService.GetGeneral<SET_BASE>();

            //var iswriteseo = false;
            //if (model.WebsiteTitle.IsNullOrEmpty() == false || model.Description.IsNullOrEmpty() == false
            //     || (model.Keywords != null && model.Keywords.Any(v => v.IsNullOrEmpty() == false)))
            //{
            //    iswriteseo = true;
            //}

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        var olddata = _commonService.GetGeneralList<EPaperItem>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", model.ModelID.ToString() } }, tran);

                        var savemodel = new EPaperItem()
                        {
                            TopHtmlContent = model.TopHtmlContent == null ? "" : model.TopHtmlContent,
                            BottomHtmlContent = model.BottomHtmlContent == null ? "" : model.BottomHtmlContent,
                            CenterHtmlContent = model.CenterHtmlContent == null ? "" : model.CenterHtmlContent,
                            Introduction = model.Introduction == null ? "" : model.Introduction,
                            LeftHtmlContent = model.LeftHtmlContent == null ? "" : model.LeftHtmlContent,
                            PageEndHtmlContent = model.PageEndHtmlContent == null ? "" : model.PageEndHtmlContent,
                            CreateDatetime = DateTime.Now,
                            Sort = 1,
                            LangID = int.Parse(LangId),
                            ModelID = model.ModelID,
                            GroupID = model.GroupID,
                            PaperMode = model.PaperMode,
                            PaperStyle = model.PaperStyle,
                            PublishStr = model.PublishStr,
                            Title = model.Title == null ? "" : model.Title,
                            TopBannerImgName = model.TopBannerImgName,
                            TopBannerImgPath = model.TopBannerImgPath,
                            TopBannerImgOrgName = model.TopBannerImgOrgName,
                            IsPublished = false,
                            Enabled = true,
                            UpdateUser = account,
                            UpdateDatetime = DateTime.Now,
                            UploadFileName = model.UploadFileName,
                            UploadFilePath = model.UploadFilePath,
                            ImageFileName = model.ImageFileName,
                            
                        };

                        
                        //if (model.ActiveID.IsNullOrEmpty() == false)
                        //{
                        //    savemodel.ActiveID = int.Parse(model.ActiveID);
                        //}
                        //if (model.ActiveItemID.IsNullOrEmpty() == false)
                        //{
                        //    savemodel.ActiveItemID = int.Parse(model.ActiveItemID);
                        //}
                        //if (model.PublicshStr.IsNullOrEmpty() == false)
                        //{
                        //    savemodel.PublicshDate = DateTime.Parse(model.PublicshStr);
                        //}
                        //if (model.EdDateStr.IsNullOrEmpty() == false)
                        //{
                        //    savemodel.EdDate = DateTime.Parse(model.EdDateStr);
                        //}
                        //if (model.EdDateStr.IsNullOrEmpty() == false)
                        //{
                        //    savemodel.EdDate = DateTime.Parse(model.EdDateStr);
                        //}
                        //if (model.StDateStr.IsNullOrEmpty() == false)
                        //{
                        //    savemodel.StDate = DateTime.Parse(model.StDateStr);
                        //}

                        //if (model.UnPublicshStr.IsNullOrEmpty() == false)
                        //{
                        //    savemodel.UnPublishDate = DateTime.Parse(model.UnPublicshStr);
                        //}




                        //先更新sort
                        sql = "update EPaperItem set Sort=Sort+1 where ModelID =@ModelID ";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelID", model.ModelID.ToString()));
                        base.ExeNonQuery(sql, tran);

                        var r = base.InsertObject(savemodel, tran);

                        //if (model.fileDownloadFiles != null)
                        //{
                        //    //新增files
                        //    model.fileDownloadFiles.ForEach(t =>
                        //    {
                        //        t.ItemID = (int)r;
                        //        t.ModelID = model.ModelID;

                        //        base.InsertObject(t, tran);
                        //    });
                        //}
                        //if (model.EPaperDateRange != null)
                        //{
                        //    //新增files
                        //    model.EPaperDateRange.ForEach(t =>
                        //    {
                        //        t.ItemID = (int)r;
                        //        t.ModelID = model.ModelID;
                        //        base.InsertObject(t, tran);
                        //    });
                        //}

                        //if (model.EPaperImages != null)
                        //{
                        //    //新增files
                        //    model.EPaperImages.ForEach(t =>
                        //    {
                        //        t.ItemID = (int)r;
                        //        t.ModelID = model.ModelID;

                        //        base.InsertObject(t, tran);
                        //    });
                        //}

                        sql = "delete VerifyData where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                        base.Parameter.Add(new SqlParameter("@ModelMainID", savemodel.ModelID.Value));
                        base.Parameter.Add(new SqlParameter("@ModelItemID", savemodel.ItemID));
                        base.ExeNonQuery(sql, tran);

                        //base.InsertObject(new VerifyData()
                        //{
                        //    ModelID = _ModelID,
                        //    ModelItemID = savemodel.ItemID,
                        //    ModelName = savemodel.Title,
                        //    ModelMainID = savemodel.ModelID.Value,
                        //    VerifyStatus = SET_BASE.M_Base02 ? 0 : 1,
                        //    ModelStatus = 1,
                        //    UpdateDateTime = DateTime.Now,
                        //    UpdateUser = userName,
                        //    UpdateAccount = account,
                        //    LangID = int.Parse(LangId)
                        //}, tran);

                        //if (iswriteseo)
                        //{
                        //    r = base.InsertObject(new SEO()
                        //    {
                        //        Description = model.Description == null ? "" : model.Description,
                        //        Keywords1 = model.Keywords[0],
                        //        Keywords2 = model.Keywords[1],
                        //        Keywords3 = model.Keywords[2],
                        //        Keywords4 = model.Keywords[3],
                        //        Keywords5 = model.Keywords[4],
                        //        Keywords6 = model.Keywords[5],
                        //        Keywords7 = model.Keywords[6],
                        //        Keywords8 = model.Keywords[7],
                        //        Keywords9 = model.Keywords[8],
                        //        Keywords10 = model.Keywords[9],
                        //        Title = model.WebsiteTitle == null ? "" : model.WebsiteTitle,
                        //        TypeName = "EPaperItem",
                        //        TypeID = savemodel.ItemID,
                        //        Lang_ID = int.Parse(LangId)
                        //    }, tran);
                        //}





                        tran.Commit();
                        return "新增成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "新增訊息Item異常,error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        return "新增失敗";
                    }
                }
            }
        }

        #region GetModel
        public EPaperEditModel GetModel(string id)
        {
            var model = new EPaperEditModel();
            if (id != "-1")
            {
                var olddata = _commonService.GetGeneral<EPaperItem>("ItemID=@ItemID",new Dictionary<string, string> { { "ItemID", id } });
                model.ItemID = olddata.ItemID;
                model.PaperMode = olddata.PaperMode.Value;
                model.PaperStyle = olddata.PaperStyle.Value;
                model.PublishStr = olddata.PublishStr;
                model.Title = olddata.Title;
                model.Introduction = olddata.Introduction;
                model.TopBannerImgUrl = VirtualPathUtility.ToAbsolute("~/UploadImage/EPaper/" + olddata.TopBannerImgName);
                model.TopBannerImgPath = olddata.TopBannerImgPath;
                model.TopBannerImgOrgName = olddata.TopBannerImgOrgName;
                model.TopBannerImgName = olddata.TopBannerImgName;
                model.PageEndHtmlContent = olddata.PageEndHtmlContent;
                model.Enabled = olddata.Enabled.Value;
                model.TopHtmlContent = olddata.TopHtmlContent;
                model.LeftHtmlContent = olddata.LeftHtmlContent;
                model.CenterHtmlContent = olddata.CenterHtmlContent;
                model.BottomHtmlContent = olddata.BottomHtmlContent;
                //var addata = _commonService.GetGeneral<ADMain>(id);
                
                //var ADID = new List<string>();
                //var ADName = new List<string>();
                //var ADLink = new List<string>();
                //var ADFileName = new List<string>();
                //var ADFilePath = new List<string>();
                //foreach (var a in addata)
                //{
                //    ADID.Add(a.ID.ToString());
                //    ADName.Add(a.ADName == null ? "" : a.ADName);
                //    ADLink.Add(a.ADLink == null ? "" : a.ADLink);
                //    ADFileName.Add(a.ADFileName == null ? "" : a.ADFileName);
                //    ADFilePath.Add(a.ADFilePath == null ? "" : a.ADFilePath);
                //}
                //model.ADFileName = ADFileName.ToArray();
                //model.ADID = ADID.ToArray();
                //model.ADLink = ADLink.ToArray();
                //model.ADName = ADName.ToArray();
                //model.ADFilePath = ADFilePath.ToArray();
            //    if (model.PaperMode == 1)
            //    {
            //        var cmodel = _epapercontnetsqlrepository.GetByWhere("EID=@1", new object[] { id });
            //        if (cmodel.Count() > 0)
            //        {
            //            model.EPaperContent = cmodel.First().EPaperHtmlContent;
            //        }
            //    }
            }
            //else
            //{
            //    var addata = _adsqlrepository.GetByWhere("MainID=@1", new object[] { "-1" }).OrderBy(v => v.ADIndex);
            //    var ADID = new List<string>();
            //    var ADName = new List<string>();
            //    var ADLink = new List<string>();
            //    var ADFileName = new List<string>();
            //    var ADFilePath = new List<string>();
            //    foreach (var a in addata)
            //    {
            //        ADID.Add(a.ID.ToString());
            //        ADName.Add(a.ADName == null ? "" : a.ADName);
            //        ADLink.Add(a.ADLink == null ? "" : a.ADLink);
            //        ADFileName.Add(a.ADFileName == null ? "" : a.ADFileName);
            //        ADFilePath.Add(a.ADFilePath == null ? "" : a.ADFilePath);
            //    }
            //    model.ADFileName = ADFileName.ToArray();
            //    model.ADID = ADID.ToArray();
            //    model.ADLink = ADLink.ToArray();
            //    model.ADName = ADName.ToArray();
            //    model.ADFilePath = ADFilePath.ToArray();
            //}

            return model;
        }
        #endregion

        //#region GetEPaperItemEdit
        //public List<EPaperItemEdit> GetEPaperItemEdit(string id)
        //{
        //    var model = new List<EPaperItemEdit>();
        //    UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        //    //var list = _epaperitemsqlrepository.GetByWhere("EPaperID=@1", new object[] { id }).OrderBy(v=>v.MenuLevel1Sort)
        //    //    .ThenBy(v => v.MenuLevel2Sort).ThenBy(v => v.MenuLevel3Sort).ThenBy(v=>v.Sort);
        //    var list = _commonService.GetGeneral<EPaperAutoItem>(id)
        //    var grouplist = list.GroupBy(v => v.MenuID);
        //    foreach (var g1 in grouplist)
        //    {
        //        var g2list = g1.OrderBy(v => v.Sort).GroupBy(x => x.MainID);
        //        foreach (var g2 in g2list)
        //        {
        //            var EPaperItemEdit = new EPaperItemEdit();
        //            if (g1.Count() > 0)
        //            {
        //                EPaperItemEdit.MenuID = g1.First().MenuID.ToString();
        //                EPaperItemEdit.SortID = g1.First().GroupSortID;
        //                EPaperItemEdit.MainID = g1.First().MainID.ToString();
        //            }

        //            EPaperItemEdit.ItemName = new List<string>();
        //            EPaperItemEdit.ItemUrl = new List<string>();
        //            EPaperItemEdit.ItemKey = new List<string>();

        //            var modelid = g2.First().ModelID;
        //            foreach (var item in g2.ToList())
        //            {
        //                EPaperItemEdit.ItemKey.Add(item.ModelID + "_" + item.ItemID + "_" + item.MenuID + "_" + item.MainID);
        //            }
        //            if (modelid == 2)
        //            {
        //                var maindata = _messagemainsqlrepository.GetByWhere("ID=@1", new object[] { g2.Key });
        //                if (maindata.Count() == 0)
        //                {
        //                    continue;
        //                }
        //                EPaperItemEdit.Name = maindata.First().Name;
        //                var itemlist = _messagesqlrepository.GetByWhere("ModelID=@1", new object[] { g2.Key });
        //                foreach (var item in g2.ToList())
        //                {
        //                    var data = itemlist.Where(v => v.ItemID == item.ItemID);
        //                    if (data.Count() > 0)
        //                    {
        //                        EPaperItemEdit.ItemName.Add(data.First().Title);
        //                        EPaperItemEdit.ItemUrl.Add(helper.Action("MessageView", "Message", new { Area = "" }) + "?id=" + item.ItemID + "&mid=" + item.MenuID);
        //                    }
        //                }
        //            }
        //            else if (modelid == 3)
        //            {
        //                var maindata = _activemainsqlrepository.GetByWhere("ID=@1", new object[] { g2.Key });
        //                if (maindata.Count() == 0)
        //                {
        //                    continue;
        //                }
        //                EPaperItemEdit.Name = maindata.First().Name;
        //                var itemlist = _activesqlrepository.GetByWhere("ModelID=@1", new object[] { g2.Key });
        //                foreach (var item in g2.ToList())
        //                {
        //                    var data = itemlist.Where(v => v.ItemID == item.ItemID);
        //                    if (data.Count() > 0)
        //                    {
        //                        EPaperItemEdit.ItemName.Add(data.First().Title);
        //                        EPaperItemEdit.ItemUrl.Add(helper.Action("ActiveView", "Active", new { Area = "" }) + "?id=" + item.ItemID + "&mid=" + item.MenuID);
        //                    }
        //                }
        //            }
        //            else if (modelid == 4)
        //            {
        //                var maindata = _filedownloadsqlrepository.GetByWhere("ID=@1", new object[] { g2.Key });
        //                if (maindata.Count() == 0)
        //                {
        //                    continue;
        //                }
        //                EPaperItemEdit.Name = maindata.First().Name;
        //                var itemlist = _filedownloaditemsqlrepository.GetByWhere("ModelID=@1", new object[] { g2.Key });
        //                foreach (var item in g2.ToList())
        //                {
        //                    var data = itemlist.Where(v => v.ItemID == item.ItemID);
        //                    if (data.Count() > 0)
        //                    {
        //                        EPaperItemEdit.ItemName.Add(data.First().Title);
        //                        EPaperItemEdit.ItemUrl.Add(helper.Action("Index", "Download", new { Area = "" }) + "?id=" + item.ItemID + "&mid=" + item.MenuID);
        //                    }
        //                }
        //            }
        //            else if (modelid == 7)
        //            {
        //                var maindata = _articlemainsqlrepository.GetByWhere("ID=@1", new object[] { g2.Key });
        //                if (maindata.Count() == 0)
        //                {
        //                    continue;
        //                }
        //                EPaperItemEdit.Name = maindata.First().Name;
        //                var itemlist = _articlesqlrepository.GetByWhere("ModelID=@1", new object[] { g2.Key });
        //                foreach (var item in g2.ToList())
        //                {
        //                    var data = itemlist.Where(v => v.ItemID == item.ItemID);
        //                    if (data.Count() > 0)
        //                    {
        //                        EPaperItemEdit.ItemName.Add(data.First().Title);
        //                        EPaperItemEdit.ItemUrl.Add(helper.Action("ArticleView", "Article", new { Area = "" }) + "?id=" + item.ItemID + "&mid=" + item.MenuID);
        //                    }
        //                }
        //            }
        //            model.Add(EPaperItemEdit);
        //        }


        //    }
        //    return model;
        //}
        //#endregion



        /// <summary>
        /// 更新Item
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LangId"></param>
        /// <param name="account"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string UpdateItem(EPaperEditModel model, string LangId, string account, string userName)
        {
            SET_BASE SET_BASE = _commonService.GetGeneral<SET_BASE>();
            string sql = string.Empty;
            //var iswriteseo = false;
            //if (model.WebsiteTitle.IsNullOrEmpty() == false || model.Description.IsNullOrEmpty() == false
            //       || (model.Keywords != null && model.Keywords.Any(v => v.IsNullOrEmpty() == false)))
            //{
            //    iswriteseo = true;
            //}
            var olddata = _commonService.GetGeneral<EPaperItem>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", model.ItemID.ToString() } });

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        olddata.TopHtmlContent = model.TopHtmlContent == null ? "" : model.TopHtmlContent;
                        olddata.BottomHtmlContent = model.BottomHtmlContent == null ? "" : model.BottomHtmlContent;
                        olddata.CenterHtmlContent = model.CenterHtmlContent == null ? "" : model.CenterHtmlContent;
                        olddata.Introduction = model.Introduction == null ? "" : model.Introduction;
                        olddata.LeftHtmlContent = model.LeftHtmlContent == null ? "" : model.LeftHtmlContent;
                        olddata.PageEndHtmlContent = model.PageEndHtmlContent == null ? "" : model.PageEndHtmlContent;
                        olddata.CreateDatetime = DateTime.Now;
                        olddata.GroupID = model.GroupID;
                        olddata.LangID = int.Parse(LangId);
                        olddata.ModelID = model.ModelID;
                        olddata.Sort = 1;
                        olddata.PaperMode = model.PaperMode;
                        olddata.PaperStyle = model.PaperStyle;
                        olddata.PublishStr = model.PublishStr;
                        olddata.Title = model.Title == null ? "" : model.Title;
                        olddata.TopBannerImgName = model.TopBannerImgName;
                        olddata.TopBannerImgPath = model.TopBannerImgPath;
                        olddata.TopBannerImgOrgName = model.TopBannerImgOrgName;
                        olddata.IsPublished = false;
                        olddata.Enabled = true;
                        olddata.UpdateUser = account;
                        olddata.UpdateDatetime = DateTime.Now;

                        

                        var r = base.UpdateObject(olddata, tran);

                        


                        if (r)
                        {
                            sql = "select * from VerifyData where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                            base.Parameter.Add(new SqlParameter("@ModelMainID", olddata.ModelID.Value));
                            base.Parameter.Add(new SqlParameter("@ModelItemID", olddata.ItemID));
                            var hasvdata = base.SearchList<VerifyData>(sql);

                            if (hasvdata.Count() == 0)
                            {
                                base.InsertObject(new VerifyData()
                                {
                                    ModelID = _ModelID,
                                    ModelItemID = olddata.ItemID,
                                    ModelName = olddata.Title,
                                    ModelMainID = olddata.ModelID.Value,
                                    VerifyStatus = SET_BASE.M_Base02 ? 0 : 1,
                                    ModelStatus = 2,
                                    UpdateDateTime = DateTime.Now,
                                    UpdateUser = userName,
                                    UpdateAccount = account,
                                    LangID = int.Parse(LangId)
                                });
                            }
                            else
                            {
                                sql = $@"update VerifyData set VerifyStatus={(SET_BASE.M_Base02 ? 0 : 1)},ModelStatus=2,VerifyDateTime=Null,VerifyUser=''
                                                             ,VerifyName='',ModelName=@ModelName,UpdateDateTime=GetDate(),UpdateUser=@UpdateUser,UpdateAccount=@UpdateAccount
                                        where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID ";
                                base.Parameter.Clear();
                                base.Parameter.Add(new SqlParameter("@ModelName", olddata.Title));
                                base.Parameter.Add(new SqlParameter("@UpdateUser", userName));
                                base.Parameter.Add(new SqlParameter("@UpdateAccount", account));
                                base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                                base.Parameter.Add(new SqlParameter("@ModelMainID", olddata.ModelID.Value));
                                base.Parameter.Add(new SqlParameter("@ModelItemID", olddata.ItemID));
                                base.ExeNonQuery(sql, tran);

                            }

                            

                        }

                        tran.Commit();
                        return "修改成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "更新訊息Item異常,error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        return "修改失敗";
                    }
                }
            }
        }

        public bool SaveToColumnSetting(int MainID ) 
        {
            var i = 0;
            var r = 0;
            List<ColumnSetting> columnSettings = new List<ColumnSetting>();
            columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = MainID, ColumnKey = "No", ColumnName = "序號", Used = true, Sort = 1 });
           columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = MainID, ColumnKey = "PublicshDate", ColumnName = "發佈日期", Used = true, Sort = 2 });
            columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = MainID, ColumnKey = "Title", ColumnName = "電子報名稱", Used = true, Sort = 3 });
            columnSettings.Add(new ColumnSetting() { Type = "EPaper", MainID = MainID, ColumnKey = "GroupName", ColumnName = "類別", Used = true, Sort = 4 });

            try
            {
                columnSettings.ForEach(t =>
            {
                base.Parameter.Clear();
                var sql = "insert into ColumnSetting ([Type],[MainID],[ColumnKey],[ColumnName],[Used],[Sort]) " +
                                          "values(@Type,@MainID,@ColumnKey,@ColumnName,@Used,@Sort)";
                base.Parameter.Add(new SqlParameter("@Type", columnSettings[i].Type));
                base.Parameter.Add(new SqlParameter("@MainID", columnSettings[i].MainID));
                base.Parameter.Add(new SqlParameter("@ColumnKey", columnSettings[i].ColumnKey));
                base.Parameter.Add(new SqlParameter("@ColumnName", columnSettings[i].ColumnName));
                base.Parameter.Add(new SqlParameter("@Used", columnSettings[i].Used));
                base.Parameter.Add(new SqlParameter("@Sort", columnSettings[i].Sort));
                r = base.ExeNonQuery(sql);
                i++;

            });
                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                logger.Error(ex, "新增電子報欄位異常，ex:" + ex.ToString().NewLineReplace());
                return false;
            }
        }


        //public EPaperFile GetFileDownloadFile(string id)
        //{
        //    return _commonService.GetGeneral<EPaperFile>("FID=@FID", new Dictionary<string, string>() { { "FID", id } });
        //}

        //public EPaperImage GetFileDownloadImage(string id)
        //{
        //    return _commonService.GetGeneral<EPaperImage>("FID=@FID", new Dictionary<string, string>() { { "FID", id } });
        //}

        #region 前台
        public Paging<EPaperItem> GetPaging(string itemid, int? group, string title, int nowpage, int showCount)
        {
            string sql = @"select  ROW_NUMBER() OVER(ORDER BY t.Sort) AS No,t.*,isnull(s.Group_Name,'無分類') as GroupName 
                           from EPaperItem t
                           left join GroupEPaper s on t.GroupID = s.ID where ModelID=@itemid";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@itemid", itemid));

            if (group.HasValue)
            {
                sql += " and t.GroupID=@group";
                base.Parameter.Add(new SqlParameter("@group", group));
            }

            if (title.IsNullOrEmpty() == false)
            {
                sql += " and t.Title like @Title";
                base.Parameter.Add(new SqlParameter("@Title", "%" + title + "%"));
            }

            sql += @" and t.Enabled =1";
            sql += @" and t.IsPublished =1";

            var Paging = new Paging<EPaperItem>();

            Paging.total = base.SearchCount(sql);

            Paging.rows = base.SearchListPage<EPaperItem>(sql, (nowpage - 1) * showCount, showCount, " order by t.Sort");

            return Paging;
        }
        public Paging<EPaperItem> GetPaging(string itemid, string fromDate, string toDate, string title, int nowpage, int showCount)
        {
            string sql = "select  ROW_NUMBER() OVER(ORDER BY Sort) AS No,t.* from EPaperItem t where ModelID=@itemid";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@itemid", itemid));

            if (fromDate.IsNullOrEmpty() == false)
            {
                sql += " and PublishDate >= @fromDate";
                base.Parameter.Add(new SqlParameter("@fromDate", fromDate));
            }

            if (toDate.IsNullOrEmpty() == false)
            {
                sql += " and PublishDate <= @toDate";
                base.Parameter.Add(new SqlParameter("@toDate", toDate));
            }

            if (title.IsNullOrEmpty() == false)
            {
                sql += " and Title like @Title";
                base.Parameter.Add(new SqlParameter("@Title", "%" + title + "%"));
            }

            sql += @" and t.Enabled =1 and t.IsVerift = 1
                             and  isnull(t.StDate,'1999/1/1') <= convert(date,GetDate()) 
                             and isnull(t.EdDate,'9999/12/31') >= convert(date,GetDate()) 
                            ";

            var Paging = new Paging<EPaperItem>();

            Paging.total = base.SearchCount(sql);

            Paging.rows = base.SearchListPage<EPaperItem>(sql, (nowpage - 1) * showCount, showCount, " order by t.Sort");

            return Paging;
        }
        /// <summary>
        /// 取得Group Name
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public string GetGroupName(int GroupID)
        {
            if (GroupID == 0)
            {
                return "無分類";
            }

            string sql = @"select t.Group_Name from GroupEPaper t where t.ID=@ID ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", GroupID));

            return base.ExecuteScalar(sql, "").ToString();
        }

        //public List<EPaperFile> GetFileDownloadFiles(string ItemID)
        //{
        //    //舊的資料
        //    return _commonService.GetGeneralList<EPaperFile>("ItemID=@ItemID "
        //                                                            , new Dictionary<string, string>() { { "ItemID", ItemID }
        //                                                                                                }).ToList();

        //}

        //public List<EPaperImage> GetFileDownloadImages(string ItemID)
        //{
        //    //舊的資料
        //    return _commonService.GetGeneralList<EPaperImage>("ItemID=@ItemID "
        //                                                            , new Dictionary<string, string>() { { "ItemID", ItemID }
        //                                                                                                }).ToList();
        //}
        //public List<ActiveDateRange> GetActiveDateRangeALL(string ItemID)
        //{
        //    //新增
        //    return _commonService.GetGeneralList<ActiveDateRange>("ModelID=@ModelID "
        //                                                  , new Dictionary<string, string>() { { "ModelID", ItemID } }).ToList();
        //}

        //public List<ActiveDateRange> GetActiveDateRange(string ItemID)
        //{
        //    //新增
        //    return _commonService.GetGeneralList<ActiveDateRange>("ItemID=@ItemID "
        //                                                            , new Dictionary<string, string>() { { "ItemID", ItemID } }).ToList();
        //}

        #endregion


        public void UpdateClickcnt(string ItemID)
        {
            string sql = "update EPaperItem set ClickCnt= ClickCnt+1 where ItemID=@ItemID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ItemID", ItemID));
            base.ExeNonQuery(sql);
        }

       
        public string SetUnitModel(EPaperUnitSettingModel model, string account,string LangID, string Type = "EPaper")
        {
            var newmodel = new EPaperUnitSetting();
            newmodel.UpdateDatetime = DateTime.Now;
            newmodel.UpdateUser = account;
            var r = 0;

            var columnsetting = new StringBuilder();
            foreach (var i in model.columnSettings)
            {
                columnsetting.Append(i.ColumnName +",");
            }
            columnsetting.Append("@");
            foreach (var i in model.columnSettings)
            {
                columnsetting.Append(i.Used + ",");
            }



                    try
                    {
                        if (model.ID == -1)
                        {

                            newmodel.FrontPagePath = model.FrontPagePath;
                            newmodel.Column1 = model.Column1;
                            newmodel.Column2 = model.Column2;
                            newmodel.Column3 = model.Column3;
                            newmodel.Column4 = model.Column4;
                            newmodel.Column5 = model.Column5;
                            newmodel.Column6 = model.Column6;
                            newmodel.Column7 = model.Column7;
                            newmodel.Column8 = model.Column8;
                            newmodel.Column9 = model.Column9;
                            newmodel.Column10 = model.Column10;
                            newmodel.Column11 = model.Column11;
                            newmodel.Column12 = model.Column12;
                            newmodel.Column13 = model.Column13;
                            newmodel.Column14 = model.Column14;
                            newmodel.Column15 = model.Column15;
                            newmodel.Column16 = model.Column16;
                            newmodel.Column17 = model.Column17;
                            newmodel.Column18 = model.Column18;
                            newmodel.Column19 = model.Column19;
                            newmodel.Column20 = model.Column20;
                            newmodel.MainID = (int)model.MainID;
                            newmodel.LangID = Int32.Parse(LangID);
                            newmodel.IsRSS = model.IsRSS;
                            newmodel.IsShare = model.IsShare;
                            newmodel.IsPrint = model.IsPrint;
                            newmodel.IsForward = model.IsForward;
                            newmodel.MemberAuth = model.MemberAuth;
                            newmodel.ShowCount = model.ShowCount;
                            newmodel.VIPAuth = model.VIPAuth;
                            newmodel.EMailAuth = model.EMailAuth;
                            newmodel.EnterpriceStudentAuth = model.EnterpriceStudentAuth;
                            newmodel.GeneralStudentAuth = model.GeneralStudentAuth;
                            newmodel.IntroductionHtml = model.IntroductionHtml == null ? "" : model.IntroductionHtml;
                            newmodel.ShowCount = model.ShowCount;
                            newmodel.ColumnSetting = columnsetting.ToString();
                            newmodel.Summary = model.Summary == null ? "" : model.Summary;
                            r = (int)base.InsertObject(newmodel);
                        }
                        else
                        {
                            //newmodel.ID = model.ID;
                            //newmodel.FrontPagePath = model.FrontPagePath;
                            //newmodel.Column1 = model.Column1;
                            //newmodel.Column2 = model.Column2;
                            //newmodel.Column3 = model.Column3;
                            //newmodel.Column4 = model.Column4;
                            //newmodel.Column5 = model.Column5;
                            //newmodel.Column6 = model.Column6;
                            //newmodel.Column7 = model.Column7;
                            //newmodel.Column8 = model.Column8;
                            //newmodel.Column9 = model.Column9;
                            //newmodel.Column10 = model.Column10;
                            //newmodel.Column11 = model.Column11;
                            //newmodel.Column12 = model.Column12;
                            //newmodel.Column13 = model.Column13;
                            //newmodel.Column14 = model.Column14;
                            //newmodel.Column15 = model.Column15;
                            //newmodel.Column16 = model.Column16;
                            //newmodel.Column17 = model.Column17;
                            //newmodel.Column18 = model.Column18;
                            //newmodel.Column19 = model.Column19;
                            //newmodel.Column20 = model.Column20;

                            //newmodel.LangID = 1;
                            //newmodel.IsRSS = model.IsRSS;
                            //newmodel.IsShare = model.IsShare;
                            //newmodel.IsPrint = model.IsPrint;
                            //newmodel.IsForward = model.IsForward;
                            //newmodel.MemberAuth = model.MemberAuth;
                            //newmodel.ShowCount = model.ShowCount;
                            //newmodel.VIPAuth = model.VIPAuth;
                            //newmodel.EMailAuth = model.EMailAuth;
                            //newmodel.EnterpriceStudentAuth = model.EnterpriceStudentAuth;
                            //newmodel.GeneralStudentAuth = model.GeneralStudentAuth;
                            //newmodel.IntroductionHtml = model.IntroductionHtml == null ? "" : model.IntroductionHtml;
                            //newmodel.ShowCount = model.ShowCount;
                            //newmodel.ColumnSetting = columnsetting;
                            //newmodel.Summary = model.Summary == null ? "" : model.Summary;
                            string sql2 = $@"update EPaperUnitSetting set ClassOverview=@ClassOverview,IsRSS=@IsRSS,IsShare=@IsShare,IsPrint=@IsPrint,IsForward=@IsForward
                                                                         ,ShowCount=@ShowCount,FrontPagePath = @FrontPagePath,ColumnSetting=@ColumnSetting
                                                                         ,IntroductionHtml=@IntroductionHtml,Summary=@Summary,MainID=@MainID
                                                                         ,UpdateDatetime=@UpdateDatetime,UpdateUser=@UpdateUser
                                                                         ,Column1 = @Column1,Column2 = @Column2,Column3 = @Column3,Column4 = @Column4,Column5 = @Column5
                                                                         ,Column6 = @Column6,Column7 = @Column7,Column8 = @Column8,Column9 = @Column9,Column10 = @Column10
                                                                         ,Column11 = @Column11,Column12 = @Column12,Column13 = @Column13,Column14 = @Column14,Column15 = @Column15
                                                                         ,Column16 = @Column16,Column17 = @Column17,Column18 = @Column18,Column19 = @Column19,Column20 = @Column20  where ID=@ID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ID", model.ID));
                            base.Parameter.Add(new SqlParameter("@Column1", model.Column1 == null ? "" :model.Column1));
                            base.Parameter.Add(new SqlParameter("@Column2", model.Column2 == null ? "" : model.Column2));
                            base.Parameter.Add(new SqlParameter("@Column3", model.Column3 == null ? "" : model.Column3));
                            base.Parameter.Add(new SqlParameter("@Column4", model.Column4 == null ? "" : model.Column4));
                            base.Parameter.Add(new SqlParameter("@Column5", model.Column5 == null ? "" : model.Column5));
                            base.Parameter.Add(new SqlParameter("@Column6", model.Column6 == null ? "" : model.Column6));
                            base.Parameter.Add(new SqlParameter("@Column7", model.Column7 == null ? "" : model.Column7));
                            base.Parameter.Add(new SqlParameter("@Column8", model.Column8 == null ? "" : model.Column8));
                            base.Parameter.Add(new SqlParameter("@Column9", model.Column9 == null ? "" : model.Column9));
                            base.Parameter.Add(new SqlParameter("@Column10", model.Column10 == null ? "" : model.Column10));
                            base.Parameter.Add(new SqlParameter("@Column11", model.Column11 == null ? "" : model.Column11));
                            base.Parameter.Add(new SqlParameter("@Column12", model.Column12 == null ? "" : model.Column12));
                            base.Parameter.Add(new SqlParameter("@Column13", model.Column13 == null ? "" : model.Column13));
                            base.Parameter.Add(new SqlParameter("@Column14", model.Column14 == null ? "" : model.Column14));
                            base.Parameter.Add(new SqlParameter("@Column15", model.Column15 == null ? "" : model.Column15));
                            base.Parameter.Add(new SqlParameter("@Column16", model.Column16 == null ? "" : model.Column16));
                            base.Parameter.Add(new SqlParameter("@Column17", model.Column17 == null ? "" : model.Column17));
                            base.Parameter.Add(new SqlParameter("@Column18", model.Column18 == null ? "" : model.Column18));
                            base.Parameter.Add(new SqlParameter("@Column19", model.Column19 == null ? "" : model.Column19));
                            base.Parameter.Add(new SqlParameter("@Column20", model.Column20 == null ? "" : model.Column20));
                            base.Parameter.Add(new SqlParameter("@FrontPagePath", model.FrontPagePath == null ? "" : model.FrontPagePath));
                            base.Parameter.Add(new SqlParameter("@Summary", model.Summary == null ? "" : model.Summary)); 
                            base.Parameter.Add(new SqlParameter("@ClassOverview", model.ClassOverview));
                            base.Parameter.Add(new SqlParameter("@IsRSS", model.IsRSS ));
                            base.Parameter.Add(new SqlParameter("@IsShare", model.IsShare));
                            base.Parameter.Add(new SqlParameter("@IsPrint", model.IsPrint));
                            base.Parameter.Add(new SqlParameter("@IntroductionHtml", model.IntroductionHtml == null ? "" : model.IntroductionHtml));
                            base.Parameter.Add(new SqlParameter("@IsForward", model.IsForward));
                            base.Parameter.Add(new SqlParameter("@ShowCount", model.ShowCount));
                            base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                            base.Parameter.Add(new SqlParameter("@UpdateDatetime", DateTime.Now));
                            base.Parameter.Add(new SqlParameter("@ColumnSetting", columnsetting.ToString()));
                            base.Parameter.Add(new SqlParameter("@MainID", model.MainID));

                            r = base.ExeNonQuery(sql2);
                        }
                        string sql = "delete ColumnSetting where Type=@Type and MainID=@MainID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Type", Type));
                        base.Parameter.Add(new SqlParameter("@MainID", model.MainID));
                                                
                        base.ExeNonQuery(sql);

                        if (model.columnSettings != null)
                        {
                            var i = 0;
                            
                            model.columnSettings.ForEach(t =>
                            {
                                base.Parameter.Clear();
                                sql = "insert into ColumnSetting ([Type],[MainID],[ColumnKey],[ColumnName],[Used],[Sort]) " +
                                                          "values(@Type,@MainID,@ColumnKey,@ColumnName,@Used,@Sort)";
                                base.Parameter.Add(new SqlParameter("@Type", Type));
                                base.Parameter.Add(new SqlParameter("@MainID", model.MainID));
                                base.Parameter.Add(new SqlParameter("@ColumnKey", model.columnSettings[i].ColumnKey));
                                base.Parameter.Add(new SqlParameter("@ColumnName", model.columnSettings[i].ColumnName));
                                base.Parameter.Add(new SqlParameter("@Used", model.columnSettings[i].Used));
                                base.Parameter.Add(new SqlParameter("@Sort", model.columnSettings[i].Sort));
                                base.ExeNonQuery(sql);
                                i++;

                            });
                        }

                        
                        if (r > 0)
                        {
                            return "修改成功";
                        }
                        else
                        {
                            return "修改失敗";
                        }

                    }
                    catch (Exception ex)
                    {
                        
                        logger.Error(ex, "修改單元設定異常，ex:" + ex.ToString().NewLineReplace());
                        return "系統異常，請通知資訊人員！";
                    }

                
        }

        public Paging<EPaperSubscriber> PagingEpaperOrder(SubscriberSearchModel model)
        {
            var Paging = new Paging<EPaperSubscriber>();
            string sql = @"select * from EPaperSubscriber t where LangID=@LangID";
            var data = new List<EPaperSubscriber>();


            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@LangID", int.Parse(model.LangId)));

            if (model.Name != null)
            {
                sql += " (Name like @Name)";
                base.Parameter.Add(new SqlParameter("@Name", "%" + model.Name + "%"));
            }
            if (string.IsNullOrEmpty(model.EMail) == false)
            {
                sql += " and (EMail like @EMail)";
                base.Parameter.Add(new SqlParameter("@EMail", "%" + model.EMail + "%"));
            }
            if (string.IsNullOrEmpty(model.Status) == false)
            {
                sql += " and Status=@Status";
                base.Parameter.Add(new SqlParameter("@Status", model.Status));
            }
            if (string.IsNullOrEmpty(model.DateFrom) == false)
            {
                sql += " and UpdateDate >= @DateFrom ";
                base.Parameter.Add(new SqlParameter("@DateFrom", DateTime.Parse(model.DateFrom)));
            }
            if (string.IsNullOrEmpty(model.DateTo) == false)
            {
                sql += " and UpdateDate <= @DateTo ";
                base.Parameter.Add(new SqlParameter("@DateTo", DateTime.Parse(model.DateTo)));
            }
            
            data = base.SearchListPage<EPaperSubscriber>(sql, model.Offset, model.Limit, " order by " + model.Sort).ToList();


            Paging.rows = data;
            Paging.total = base.SearchCount(sql);


            return Paging;
        }

        #region AddSubscriber 前台
        public string AddSubscriber(string email, string account,int lang,string MainID)
        {

            string sql = string.Empty;
            


            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    var olddata = _commonService.GetGeneralList<EPaperSubscriber>("EMail=@EMail", new Dictionary<string, string>() {{ "EMail", email } }, tran);

                    if (olddata.Count() > 0)
                    {
                        return GetEPMulti("Column13", int.Parse(MainID));
                    }
                    var nowdate = DateTime.Now;

                    var Model = new EPaperSubscriber()
                    {
                        EMail = email,
                        Status = true,
                        CreateDate = nowdate,
                        CreateUser = account,
                        UpdateDate = nowdate,
                        UpdateUser = account,
                        OPDateStr = nowdate.ToString("yyyy/MM/dd"),
                        LangID=lang
                    };

                    //sql = "update EPaperSubscriber ";
                    //base.ExeNonQuery(sql, tran);
                    

                    var r = (int)base.InsertObject(Model, tran);
                    if (r > 0)
                    {
                        tran.Commit();

                        return GetEPMulti("Column12", int.Parse(MainID));
                    }
                    else
                    {
                        return GetEPMulti("Column16", int.Parse(MainID));
                    }
                }
            }
                   
        }
        #endregion
        #region AddSubscriber 後台
        public string AddSubscriber(string email, string account, int lang)
        {

            string sql = string.Empty;



            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    var olddata = _commonService.GetGeneralList<EPaperSubscriber>("EMail=@EMail", new Dictionary<string, string>() { { "EMail", email } }, tran);

                    if (olddata.Count() > 0)
                    {
                        return "此 EMail 已經訂閱!";
                    }
                    var nowdate = DateTime.Now;

                    var Model = new EPaperSubscriber()
                    {
                        EMail = email,
                        Status = true,
                        CreateDate = nowdate,
                        CreateUser = account,
                        UpdateDate = nowdate,
                        UpdateUser = account,
                        OPDateStr = nowdate.ToString("yyyy/MM/dd"),
                        LangID = lang
                    };

                    //sql = "update EPaperSubscriber ";
                    //base.ExeNonQuery(sql, tran);


                    var r = (int)base.InsertObject(Model, tran);
                    if (r > 0)
                    {
                        tran.Commit();

                        return "電子報訂閱成功!";
                    }
                    else
                    {
                        return "電子報訂閱失敗!";
                    }
                }
            }

        }
#endregion
        #region CancelSubscriber

        public string CancelSubscriber(string email, string delaccount, string MainID)
        {
            try
            {
                var r = 0;
                string sql = string.Empty;
                using (SqlConnection connection = base.OpenConnection())
                {
                    using (SqlTransaction tran = base.GetTransaction(connection))
                    {
                        sql = "delete EPaperSubscriber where UPPER(EMail)=@EMail";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@EMail", email.ToUpper()));
                        var olddata = _commonService.GetGeneralList<EPaperSubscriber>("EMail=@EMail", new Dictionary<string, string>() { { "EMail", email } }, tran);
                        if (olddata.Count() <= 0)
                        {
                            return GetEPMulti("Column17", int.Parse(MainID));
                        }

                        r = base.ExeNonQuery(sql);
                        if (r >= 0)
                        {
                            tran.Commit();
                            return GetEPMulti("Column14", int.Parse(MainID));
                        }
                        else
                        {
                            return GetEPMulti("Column18", int.Parse(MainID));
                        }

                    }
                }
             }
            catch (Exception ex)
            {

                return GetEPMulti("Column18", int.Parse(MainID)) == "" ? "電子報取消訂閱失敗" : GetEPMulti("Column18", int.Parse(MainID));
            }
        }
        #endregion
        




        #region GetExport
        public byte[] GetExport(SubscriberSearchModel model,string LangID)
        {

            var Paging = new Paging<EPaperSubscriber>();
            string sql = @"select * from EPaperSubscriber t where LangID=@LangID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@LangID", int.Parse(LangID
                )));
            var data = new List<EPaperSubscriber>();
            if (model.Name != null)
            {
                sql += " (Name like @Name)";
                base.Parameter.Add(new SqlParameter("@Name", "%" + model.Name + "%"));
            }
            if (string.IsNullOrEmpty(model.EMail) == false)
            {
                sql += " and (EMail like @EMail)";
                base.Parameter.Add(new SqlParameter("@EMail", "%" + model.EMail + "%"));
            }
            if (string.IsNullOrEmpty(model.Status) == false)
            {
                sql += " and Status=@Status";
                base.Parameter.Add(new SqlParameter("@Status", model.Status));
            }
            if (string.IsNullOrEmpty(model.DateFrom) == false)
            {
                sql += " and UpdateDate <= @DateFrom or UpdateDate is Null";
                base.Parameter.Add(new SqlParameter("@DateFrom", DateTime.Parse(model.DateFrom)));
            }
            if (string.IsNullOrEmpty(model.DateTo) == false)
            {
                sql += " and UpdateDate >= @DateTo or UpdateDate is Null";
                base.Parameter.Add(new SqlParameter("@DateTo", DateTime.Parse(model.DateTo)));
            }
            data = base.SearchListPage<EPaperSubscriber>(sql, model.Offset, model.Limit, " order by " + model.Sort).ToList();


            Paging.rows = data;
            Paging.total = base.SearchCount(sql);

            MemoryStream ms = new MemoryStream();
            XSSFWorkbook hssfworkbook = new XSSFWorkbook();
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 9;
            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.WrapText = true;
            style.SetFont(font);

            ISheet sheet = hssfworkbook.CreateSheet("數據資料庫列表");
            IRow row = sheet.CreateRow(0);
            row.HeightInPoints = 16;
            SetValue(sheet, "訂閱/取消日期", 0, 0, style);
            SetValue(sheet, "EMail", 0, 1, style);
            SetValue(sheet, "是否訂閱", 0, 2, style);
            sheet.SetColumnWidth(0, 5000);
            sheet.SetColumnWidth(1, 10000);
            sheet.SetColumnWidth(2, 3000);
            var ridx = 1;
            foreach (var d in Paging.rows)
            {
                SetValue(sheet, d.OPDateStr, ridx, 0, style);
                SetValue(sheet, d.EMail, ridx, 1, style);
                SetValue(sheet, d.Status == true ? "是" : "否", ridx, 2, style);
                ridx += 1;
            }
            hssfworkbook.Write(ms);
            hssfworkbook = null;
            byte[] bytes = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return bytes;
        }
        #endregion

        #region SetValue
        private void SetValue(ISheet sheet, string value, int _r, int _c, ICellStyle style)
        {
            if (sheet.GetRow(_r) == null)
            {
                sheet.CreateRow(_r);
            }
            if (sheet.GetRow(_r).GetCell(_c) == null)
            {
                sheet.GetRow(_r).CreateCell(_c);
            }
            sheet.GetRow(_r).GetCell(_c).CellStyle = style;
            if (value == null) { value = ""; }
            sheet.GetRow(_r).GetCell(_c).SetCellValue(value);
        }
        #endregion

        public List<EPaperSubscriber> GetEPaperSubscribers(string[] idlist)
        {
            string sql = @"select * from EPaperSubscriber where ModelID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
            return base.SearchList<EPaperSubscriber>(sql);
        }

        public string DeleteItem<EPaperSubscriber>(string[] idlist, string delaccount)
        {
            try
            {
                string sql = string.Empty;

                var r = 0;

                var modelid = -1;
                for (var idx = 0; idx < idlist.Length; idx++)
                {
                    
                    sql = "delete EPaperSubscriber where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@ID", idlist[idx]));
                    r = base.ExeNonQuery(sql);

                    
                }
                var str = "";
                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("刪除訊息項目:" + delaccount);
                    str = "刪除成功";
                }
                else
                {
                    str = "刪除失敗";
                }

                //update sort order
                
                return str;
            }
            catch (Exception ex)
            {
                logger.Error("刪除EPaperSubscriber群組失敗:" + ex.ToString().NewLineReplace());
                return "刪除失敗";
            }
        }

        #region PagingMenuItem的資料
        public Paging<EPaperContentItem> PagingMenuItem(SearchModelBase model)
        {

            var menu = _commonService.GetGeneral<Menu>("ID=@ID", new Dictionary<string, string>() { { "ID", model.ModelID.ToString() } });
            
            var clickdata = _commonService.GetGeneralList <EPaperAutoItem>("MenuID=@MenuID and EPaperID=@EPaperID",new Dictionary<string, string>() { { "MenuID", model.ModelID.ToString() },{ "EPaperID",model.Key } });
            var Paging = new Paging<EPaperContentItem>();
            if (menu.ModelID == 2)
            {
                var Messagemodel = _commonService.GetGeneralList<MessageItem>("ModelID=@ModelID Order By Sort", new Dictionary<string, string>(){ {"ModelID", menu.ModelItemID.ToString() } });
                Paging.total = Messagemodel.Count();
                Messagemodel = Messagemodel.Skip(model.Offset).Take(model.Limit).ToList();
                foreach (var m in Messagemodel)
                {
                    Paging.rows.Add(new EPaperContentItem()
                    {
                        Selected = clickdata.Any(v => v.ItemID == m.ItemID),
                        ItemID = m.ItemID,
                        ModelID = m.ModelID.Value,
                        MenuID = model.ModelID,
                        Enabled = m.Enabled.Value,
                        Title = m.Title
                    });
                }

            }
            //else if (menu.ModelID == 3)
            //{
            //    var _active = _activesqlrepository.GetByWhere("ModelID=@1  Order By Sort", new object[] { menu.ModelItemID });
            //    Paging.total = _active.Count();
            //    _active = _active.Skip(model.Offset).Take(model.Limit);
            //    foreach (var m in _active)
            //    {
            //        Paging.rows.Add(new EPaperContentItem()
            //        {
            //            Selected = clickdata.Any(v => v.ItemID == m.ItemID),
            //            ItemID = m.ItemID,
            //            ModelID = m.ModelID.Value,
            //            MenuID = model.ModelID,
            //            Enabled = m.Enabled.Value,
            //            Title = m.Title
            //        });
            //    }

            //}
            //else if (menu.ModelID == 4)
            //{
            //    var _active = _filedownloaditemsqlrepository.GetByWhere("ModelID=@1  Order By Sort", new object[] { menu.ModelItemID });
            //    Paging.total = _active.Count();
            //    _active = _active.Skip(model.Offset).Take(model.Limit);
            //    foreach (var m in _active)
            //    {
            //        Paging.rows.Add(new EPaperContentItem()
            //        {
            //            Selected = clickdata.Any(v => v.ItemID == m.ItemID),
            //            ItemID = m.ItemID,
            //            ModelID = m.ModelID.Value,
            //            MenuID = model.ModelID,
            //            Enabled = m.Enabled.Value,
            //            Title = m.Title
            //        });
            //    }

            //}
            //else if (menu.ModelID == 7)
            //{
            //    var _article = _articlesqlrepository.GetByWhere("ModelID=@1  Order By Sort", new object[] { menu.ModelItemID });
            //    Paging.total = _article.Count();
            //    _article = _article.Skip(model.Offset).Take(model.Limit);
            //    foreach (var m in _article)
            //    {
            //        Paging.rows.Add(new EPaperContentItem()
            //        {
            //            Selected = clickdata.Any(v => v.ItemID == m.ItemID),
            //            ItemID = m.ItemID,
            //            ModelID = m.ModelID.Value,
            //            MenuID = model.ModelID,
            //            Enabled = m.Enabled.Value,
            //            Title = m.Title
            //        });
            //    }
            //}

            return Paging;
        }
        #endregion

        #region SetEpaperItem
        public string SetEpaperItem(bool issel, string id, string itemid, string menuid, string modelid, string mainid)
        {
            if (issel == false)
            {
                var r = 0;
                var sql = "delete From EPaperAutoItem where menuid=@menuid and itemid=@itemid and EPaperID=@EPaperID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@menuid", menuid));
                base.Parameter.Add(new SqlParameter("@itemid", itemid));
                base.Parameter.Add(new SqlParameter("@EPaperID", id));

                r = base.ExeNonQuery(sql);

                var data = _commonService.GetGeneralList<EPaperAutoItem>("menuid=@menuid and modelid=@modelid and EPaperID=@EPaperID Order By Sort", new Dictionary<string, string> { { "menuid",mainid },{ "modelid", modelid },{ "EPaperID", id }});

                sql = "update EPaperAutoItem";
                //idx為新的sort
                for (var idx = 1; idx <= data.Count(); idx++)
                {
                    
                    UpdateSort(idx.ToString(), menuid, data[idx-1].ItemID.ToString(), id);
                
                }
                if (data.Count() == 0)
                {

                    var alldata = _commonService.GetGeneralList<EPaperAutoItem>("EPaperID=@EPaperID Order By GroupSortID", new Dictionary<string, string> {  { " EPaperID", id } });

                    if (alldata.Count() > 0)
                    {
                        var groupdata = alldata.GroupBy(v => v.MenuID).ToArray();
                        for (var idx = 1; idx <= groupdata.Count(); idx++)
                        {
                           UpdateGroupSort(idx.ToString(), menuid, groupdata[idx].First().ItemID.ToString(), id);
                            
                        }

                    }
                }
            }
            else
            {

                using (SqlConnection connection = base.OpenConnection())
                {
                    using (SqlTransaction tran = base.GetTransaction(connection))
                    {
                        var r = 0;
                        var data = _commonService.GetGeneralList<EPaperAutoItem>("menuid=@menuid and modelid=@modelid and EPaperID=@EPaperID order by Sort", new Dictionary<string, string> { { "menuid", mainid }, { "modelid", modelid }, { "EPaperID", id } });
                        var Groupcount = 1;
                        if (data.Count() > 0)
                        {
                            Groupcount = data.First().GroupSortID;
                        }
                        else
                        {
                            var alldata = _commonService.GetGeneralList<EPaperAutoItem>("EPaperID=@EPaperID Order By GroupSortID", new Dictionary<string, string> { { "EPaperID", id } });
                            if (alldata.Count() > 0) { Groupcount = alldata.Last().GroupSortID + 1; }
                        }
                        var menu = _commonService.GetGeneral<Menu>("ID=@ID", new Dictionary<string, string>() { { "ID", menuid } });
                        var edata = new EPaperAutoItem()
                        {
                            EPaperID = int.Parse(id),
                            ItemID = int.Parse(itemid),
                            MenuID = int.Parse(menuid),
                            ModelID = int.Parse(modelid),
                            MainID = int.Parse(mainid),
                            Sort = data.Count() + 1,
                            GroupSortID = Groupcount,
                        };
                        if (menu.MenuLevel == 1)
                        {
                            edata.MenuLevel1Sort = menu.Sort.Value;
                        }
                        else if (menu.MenuLevel == 2)
                        {
                            edata.MenuLevel2Sort = menu.Sort.Value;
                            var tempmenu = _commonService.GetGeneral<Menu>("ID=@ID", new Dictionary<string, string>() { { "ID", menuid } });
                            edata.MenuLevel1Sort = tempmenu.Sort.Value;
                        }
                        else if (menu.MenuLevel == 3)
                        {
                            edata.MenuLevel3Sort = menu.Sort.Value;
                            var tempmenu = _commonService.GetGeneral<Menu>("ID=@ID", new Dictionary<string, string>() { { "ID", menu.ParentID.ToString() } });

                            edata.MenuLevel2Sort = tempmenu.Sort.Value;
                            tempmenu = _commonService.GetGeneral<Menu>("ID=@ID", new Dictionary<string, string>() { { "ID", tempmenu.ParentID.ToString() } });

                            edata.MenuLevel1Sort = tempmenu.Sort.Value;
                        }

                        var sql = "insert into EPaperAutoItem ([EPaperID],[ModelID],[ItemID],[MenuID],[MainID],[Sort]," +
                                                               "[MenuLevel3Sort],[MenuLevel2Sort],[MenuLevel1Sort],[GroupSortID])" +
                                                               " Values(@EPaperID,@ModelID,@ItemID,@MenuID,@MainID,@Sort," +
                                                               "@MenuLevel3Sort,@MenuLevel2Sort,@MenuLevel1Sort,@GroupSortID)";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@EPaperID", edata.EPaperID));
                        base.Parameter.Add(new SqlParameter("@ModelID",edata.ModelID));
                        base.Parameter.Add(new SqlParameter("@ItemID", edata.ItemID));
                        base.Parameter.Add(new SqlParameter("@MenuID", edata.MenuID));
                        base.Parameter.Add(new SqlParameter("@MainID", edata.MainID));
                        base.Parameter.Add(new SqlParameter("@Sort", edata.Sort.ToString()));
                        base.Parameter.Add(new SqlParameter("@MenuLevel3Sort", edata.MenuLevel3Sort.ToString()));
                        base.Parameter.Add(new SqlParameter("@MenuLevel2Sort", edata.MenuLevel2Sort.ToString()));
                        base.Parameter.Add(new SqlParameter("@MenuLevel1Sort", edata.MenuLevel1Sort.ToString()));
                        base.Parameter.Add(new SqlParameter("@GroupSortID", edata.GroupSortID));

                        r = base.ExeNonQuery(sql, tran);
                        if (r > 0)
                        {
                            tran.Commit();



                            return "新增成功";
                        }
                        else
                        {
                            return "新增失敗";
                        }



                    }

                }
            }
            return "";
        }
        #endregion
         
        #region UpdateSort after chose messageitem
        public int UpdateSort(string sort, string menuid, string itemid, string id)
        {
            var r = 0;
            var sql = "";
            try
            {
                sql = "update EPaperAutoItem set sort=@sort where menuid=@menuid and itemid=@itemid and EPaperID=@EPaperID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@menuid", menuid));
                base.Parameter.Add(new SqlParameter("@itemid", itemid));
                base.Parameter.Add(new SqlParameter("@EPaperID", id));
                base.Parameter.Add(new SqlParameter("@sort", sort));

                r = base.ExeNonQuery(sql);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "新增訊息Item異常,error:" + ex.ToString().NewLineReplace());

            }

            return r;
        }
        #endregion
        #region UpdateSort after chose messageitem
        public int UpdateGroupSort(string groupsort, string menuid, string itemid, string id)
        {
            var r = 0;
            var sql = "";
            try
            {
                sql = "update EPaperAutoItem set GroupSortID=@GroupSortID where menuid=@menuid and itemid=@itemid and EPaperID=@EPaperID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@menuid", menuid));
                base.Parameter.Add(new SqlParameter("@itemid", itemid));
                base.Parameter.Add(new SqlParameter("@EPaperID", id));
                base.Parameter.Add(new SqlParameter("@GroupSortID", groupsort));

                r = base.ExeNonQuery(sql);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "新增訊息Item異常,error:" + ex.ToString().NewLineReplace());

            }

            return r;
        }
        #endregion

        //手動的電子報存檔
        #region SavePaperManuallyContent
        public string SavePaperManuallyContent(string id, string content)
        {
            var model = _commonService.GetGeneralList<EPaperContent>("EPaperID=@EPaperID", new Dictionary<string, string>() { { "EPaperID",id } });
            
            var r = 0;
            var sql = "";
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    if (model.Count() !=0)
                    {
                        sql = "update EPaperContent set EPaperHtmlContent=@EPaperHtmlContent where EPaperID =@EPaperID ";
                        
                    }
                    else
                    {
                        sql = "insert into EPaperContent(EPaperID,EPaperHtmlContent) Values(@EPaperID,@EPaperHtmlContent)";
                    }
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@EPaperID", int.Parse(id)));
                    base.Parameter.Add(new SqlParameter("@EPaperHtmlContent", content));
                    r=base.ExeNonQuery(sql, tran);
                    if (r > 0)
                    {
                        tran.Commit();



                        return "設定完成";
                    }
                    else
                    {
                        return "設定失敗";
                    }
                }
            }
            


            //_siteconfigqlrepository.Update("LastUpdateDate=@1", "", new object[] { DateTime.Now.ToString("yyyy/MM/dd") });
            
        }
        #endregion

        #region GetSortTable 電子報排序
        public string GetSortTable(string id)
        {


            var list = _commonService.GetGeneralList<EPaperAutoItem>("EPaperID=@EPaperID Order By GroupSortID", new Dictionary<string, string> { { "EPaperID", id } }).OrderBy(v => v.MenuLevel1Sort)
                .ThenBy(v => v.MenuLevel2Sort).ThenBy(v => v.MenuLevel3Sort).ThenBy(v => v.Sort);



            var grouplist = list.GroupBy(v => v.MenuID);
            var sb = new System.Text.StringBuilder();
            foreach (var g1 in grouplist)
            {
                var menu = _commonService.GetGeneralList<Menu>("ID=@ID", new Dictionary<string, string>() { { "ID", list.First().MenuID.ToString() } });

                var lists = g1.ToList().OrderBy(v => v.Sort);
                if (menu.Count() > 0 && lists.Count() > 0)
                {
                    sb.Append("<div class='table-scrollable'>");
                    sb.Append("<table class='table table-bordered table-hover' border='0' cellspacing='0' cellpadding='0'>");
                    sb.Append("<thead><tr class='bg-grey_1' filed-class='odd gradeX'>");
                    sb.Append("<th class='text-center' width='80px'>刪除</th><th class='text-center' width='100px'>排序</th><th class='text-center'>標題</th></thead><tbody>");

                    //訊息模組
                    if (lists.First().ModelID == 2)
                    {
                        var itemlist = _commonService.GetGeneralList<MessageItem>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", list.First().MainID.ToString() } });

                        foreach (var item in lists)
                        {
                            var citem = itemlist.Where(v => v.ItemID == item.ItemID);
                            if (citem.Count() > 0)
                            {
                                sb.Append("<tr><td class='text-center'><label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'><input type='checkbox' class='checkboxes' id='"
                                    + (item.EPaperID + "_" + item.MenuID + "_" + item.ItemID) + "'/><span></span></label></td>");
                                sb.Append("<td class='text-center'><input type='hidden' value='" + item.Sort + "'/>" +
                                 "<input  type='text'  value='" + item.Sort + "' class='editinput form-control input-xsmall sortedit' idindex='" +
                                (item.EPaperID + "_" + item.MenuID + "_" + item.ItemID) + "'/><span class='required seqerror' style='color:red;display:none;margin-left:5px;font-size:12px'></span></td>");
                                sb.Append("<td class='text-center'>" + citem.First().Title + "</td></tr>");
                            }
                        }
                    }
                    //活動模組
                    //else if (lists.First().ModelID == 3)
                    //{
                    //    var itemlist = _commonService.GetGeneralList<>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", list.First().MainID.ToString() } });

                    //    var itemlist = _activesqlrepository.GetByWhere("ModelID=@1", new object[] { lists.First().MainID });
                    //    foreach (var item in lists)
                    //    {
                    //        var citem = itemlist.Where(v => v.ItemID == item.ItemID);
                    //        if (citem.Count() > 0)
                    //        {
                    //            sb.Append("<tr><td class='text-center'><label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'><input type='checkbox' class='checkboxes' id='"
                    //          + (item.EPaperID + "_" + item.MenuID + "_" + item.ItemID) + "'/><span></span></label></td>");
                    //            sb.Append("<td class='text-center'><input type='hidden' value='" + item.Sort + "'/>" +
                    //             "<input  type='text'  value='" + item.Sort + "' class='editinput form-control input-xsmall sortedit' idindex='" +
                    //            (item.EPaperID + "_" + item.MenuID + "_" + item.ItemID) + "'/><span class='required seqerror' style='color:red;display:none;margin-left:5px;font-size:12px'></span></td>");
                    //            sb.Append("<td class='text-center'>" + citem.First().Title + "</td></tr>");
                    //        }
                    //    }
                    //}
                    //else if (lists.First().ModelID == 4)
                    //{
                    //    var itemlist = _filedownloaditemsqlrepository.GetByWhere("ModelID=@1", new object[] { lists.First().MainID });
                    //    foreach (var item in lists)
                    //    {
                    //        var citem = itemlist.Where(v => v.ItemID == item.ItemID);
                    //        if (citem.Count() > 0)
                    //        {
                    //            sb.Append("<tr><td class='text-center'><label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'><input type='checkbox' class='checkboxes' id='"
                    //          + (item.EPaperID + "_" + item.MenuID + "_" + item.ItemID) + "'/><span></span></label></td>");
                    //            sb.Append("<td class='text-center'><input type='hidden' value='" + item.Sort + "'/>" +
                    //             "<input  type='text'  value='" + item.Sort + "' class='editinput form-control input-xsmall sortedit' idindex='" +
                    //            (item.EPaperID + "_" + item.MenuID + "_" + item.ItemID) + "'/><span class='required seqerror' style='color:red;display:none;margin-left:5px;font-size:12px'></span></td>");
                    //            sb.Append("<td class='text-center'>" + citem.First().Title + "</td></tr>");
                    //        }
                    //    }
                    //}
                    //else if (lists.First().ModelID == 7)
                    //{
                    //    var itemlist = _articlesqlrepository.GetByWhere("ModelID=@1", new object[] { lists.First().MainID });
                    //    foreach (var item in lists)
                    //    {
                    //        var citem = itemlist.Where(v => v.ItemID == item.ItemID);
                    //        if (citem.Count() > 0)
                    //        {
                    //            sb.Append("<tr><td class='text-center'><label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'><input type='checkbox' class='checkboxes' id='"
                    //        + (item.EPaperID + "_" + item.MenuID + "_" + item.ItemID) + "'/><span></span></label></td>");
                    //            sb.Append("<td class='text-center'><input type='hidden' value='" + item.Sort + "'/>" +
                    //             "<input  type='text'  value='" + item.Sort + "' class='editinput form-control input-xsmall sortedit' idindex='" +
                    //            (item.EPaperID + "_" + item.MenuID + "_" + item.ItemID) + "'/><span class='required seqerror' style='color:red;display:none;margin-left:5px;font-size:12px'></span></td>");
                    //            sb.Append("<td class='text-center'>" + citem.First().Title + "</td></tr>");
                    //        }
                    //    }
                    //}

                    sb.Append("</tbody></table></div>");
                }
            }
            return sb.ToString();
        }
        #endregion

        #region GetEPaperItemEdit
        public List<EPaperItemEdit> GetEPaperItemEdit(string id)
        {
            var model = new List<EPaperItemEdit>();
            UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            //var list = _epaperitemsqlrepository.GetByWhere("EPaperID=@1", new object[] { id }).OrderBy(v=>v.MenuLevel1Sort)
            //    .ThenBy(v => v.MenuLevel2Sort).ThenBy(v => v.MenuLevel3Sort).ThenBy(v=>v.Sort);
            var list = _commonService.GetGeneralList<EPaperAutoItem>("EPaperID=@EPaperID Order By GroupSortID", new Dictionary<string, string> { { "EPaperID", id } });
            var grouplist = list.GroupBy(v => v.MenuID);
            foreach (var g1 in grouplist)
            {
                var g2list = g1.OrderBy(v => v.Sort).GroupBy(x => x.MainID);
                foreach (var g2 in g2list)
                {
                    var EPaperItemEdit = new EPaperItemEdit();
                    if (g1.Count() > 0)
                    {
                        EPaperItemEdit.MenuID = g1.First().MenuID.ToString();
                        EPaperItemEdit.SortID = g1.First().GroupSortID;
                        EPaperItemEdit.MainID = g1.First().MainID.ToString();
                    }

                    EPaperItemEdit.ItemName = new List<string>();
                    EPaperItemEdit.ItemUrl = new List<string>();
                    EPaperItemEdit.ItemKey = new List<string>();

                    var modelid = g2.First().ModelID;
                    foreach (var item in g2.ToList())
                    {
                        EPaperItemEdit.ItemKey.Add(item.ModelID + "_" + item.ItemID + "_" + item.MenuID + "_" + item.MainID);
                    }
                    if (modelid == 2)
                    {
                        var maindata = _commonService.GetGeneralList<ModelMessageMain>("ID=@ID", new Dictionary<string, string>() { { "ID", g2.Key.ToString() } });

                        if (maindata.Count() == 0)
                        {
                            continue;
                        }
                        EPaperItemEdit.Name = maindata.First().Name;
                        var itemlist = _commonService.GetGeneralList<MessageItem>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", g2.Key.ToString() } });

                        foreach (var item in g2.ToList())
                        {
                            var data = itemlist.Where(v => v.ItemID == item.ItemID);
                            if (data.Count() > 0)
                            {
                                EPaperItemEdit.ItemName.Add(data.First().Title);
                                EPaperItemEdit.ItemUrl.Add(helper.Action("MessageView", "Message", new { Area = "" }) + "?itemid=" + item.ItemID + "&mid=" + item.MenuID);
                            }
                        }
                    }
                    //else if (modelid == 3)
                    //{
                    //    var maindata = _activemainsqlrepository.GetByWhere("ID=@1", new object[] { g2.Key });
                    //    if (maindata.Count() == 0)
                    //    {
                    //        continue;
                    //    }
                    //    EPaperItemEdit.Name = maindata.First().Name;
                    //    var itemlist = _activesqlrepository.GetByWhere("ModelID=@1", new object[] { g2.Key });
                    //    foreach (var item in g2.ToList())
                    //    {
                    //        var data = itemlist.Where(v => v.ItemID == item.ItemID);
                    //        if (data.Count() > 0)
                    //        {
                    //            EPaperItemEdit.ItemName.Add(data.First().Title);
                    //            EPaperItemEdit.ItemUrl.Add(helper.Action("ActiveView", "Active", new { Area = "" }) + "?id=" + item.ItemID + "&mid=" + item.MenuID);
                    //        }
                    //    }
                    //}
                    //else if (modelid == 4)
                    //{
                    //    var maindata = _filedownloadsqlrepository.GetByWhere("ID=@1", new object[] { g2.Key });
                    //    if (maindata.Count() == 0)
                    //    {
                    //        continue;
                    //    }
                    //    EPaperItemEdit.Name = maindata.First().Name;
                    //    var itemlist = _filedownloaditemsqlrepository.GetByWhere("ModelID=@1", new object[] { g2.Key });
                    //    foreach (var item in g2.ToList())
                    //    {
                    //        var data = itemlist.Where(v => v.ItemID == item.ItemID);
                    //        if (data.Count() > 0)
                    //        {
                    //            EPaperItemEdit.ItemName.Add(data.First().Title);
                    //            EPaperItemEdit.ItemUrl.Add(helper.Action("Index", "Download", new { Area = "" }) + "?id=" + item.ItemID + "&mid=" + item.MenuID);
                    //        }
                    //    }
                    //}
                    //else if (modelid == 7)
                    //{
                    //    var maindata = _articlemainsqlrepository.GetByWhere("ID=@1", new object[] { g2.Key });
                    //    if (maindata.Count() == 0)
                    //    {
                    //        continue;
                    //    }
                    //    EPaperItemEdit.Name = maindata.First().Name;
                    //    var itemlist = _articlesqlrepository.GetByWhere("ModelID=@1", new object[] { g2.Key });
                    //    foreach (var item in g2.ToList())
                    //    {
                    //        var data = itemlist.Where(v => v.ItemID == item.ItemID);
                    //        if (data.Count() > 0)
                    //        {
                    //            EPaperItemEdit.ItemName.Add(data.First().Title);
                    //            EPaperItemEdit.ItemUrl.Add(helper.Action("ArticleView", "Article", new { Area = "" }) + "?id=" + item.ItemID + "&mid=" + item.MenuID);
                    //        }
                    //    }
                    //}
                    model.Add(EPaperItemEdit);
                }


            }
            return model;
        }
        #endregion

        #region DeleteEPaperItemSort 刪除SORT
        public string DeleteEPaperItemSort(string[] delarrid, string EPaperID)
        {//
            
            try
            {
                if (delarrid.Length == 0) { return "更新成功"; }
                var r = 0;
                var sql = "";
                foreach (var delid in delarrid)
                {
                    if (delid.IndexOf("chk_m_") == 0)
                    {
                        var menuid = delid.Replace("chk_m_", "");
                        sql = "delete from EPaperAutoItem where EPaperID=@EPaperID and MenuID = @menuid";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@menuid", menuid));
                        base.Parameter.Add(new SqlParameter("@EPaperID", EPaperID));
                        base.ExeNonQuery(sql);
                    }
                    else
                    {
                        var subitemkey = delid.Replace("chk_s_", "");
                        var keys = subitemkey.Split('_');
                        sql = "delete from EPaperAutoItem where EPaperID=@EPaperID and ModelID=@ModelID and ItemID=@ItemID and MenuID=@MenuID and MainID=@MainID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@EPaperID", EPaperID));
                        base.Parameter.Add(new SqlParameter("@ModelID", keys[0] ));
                        base.Parameter.Add(new SqlParameter("@ItemID", keys[1] ));
                        base.Parameter.Add(new SqlParameter("@MenuID", keys[2]));
                        base.Parameter.Add(new SqlParameter("@MainID", keys[3]));
                        base.ExeNonQuery(sql);
                    }
                }
                var allitem =_commonService.GetGeneralList<EPaperAutoItem>("EPaperID=@EPaperID Order by GroupSortID,Sort", new Dictionary<string, string>() { { "EPaperID", EPaperID } });
                var grouplist = allitem.GroupBy(v => v.MenuID).ToList();
                for (var gidx = 0; gidx < grouplist.Count(); gidx++) {
                    var menuid = grouplist[gidx].Key;
                    sql = "update EPaperAutoItem set GroupSortID=@GroupSortID where EPaperID =@EPaperID and MenuID = @MenuID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@EPaperID", EPaperID));
                    base.Parameter.Add(new SqlParameter("@GroupSortID", gidx + 1));
                    base.Parameter.Add(new SqlParameter("@MenuID", menuid));

                    base.ExeNonQuery(sql);
                    
                    if (r > 0) {
                        var subitem = grouplist[gidx].OrderBy(v=>v.Sort).ToList();
                        for (var sidx = 0; sidx < subitem.Count(); sidx++)
                        {
                            sql = "update EPaperAutoItem set Sort=@Sort where EPaperID =@EPaperID and ModelID=@ModelID and ItemID=@ItemID and MenuID = @MenuID and MainID=@MainID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@Sort", sidx + 1));
                            base.Parameter.Add(new SqlParameter("@EPaperID", EPaperID));
                            base.Parameter.Add(new SqlParameter("@ModelID", subitem[sidx].ModelID));
                            base.Parameter.Add(new SqlParameter("@ItemID", subitem[sidx].ItemID));
                            base.Parameter.Add(new SqlParameter("@MainID", subitem[sidx].MainID));
                            base.Parameter.Add(new SqlParameter("@MenuID", subitem[sidx].MenuID));
                            base.ExeNonQuery(sql);

                        }
                    }
                }
                //_siteconfigqlrepository.Update("LastUpdateDate=@1", "", new object[] { DateTime.Now.ToString("yyyy/MM/dd") });
                return "更新成功";
            }
            catch (Exception ex) {
                return "更新失敗";
            }
        }
        #endregion


        #region SetIsEdit 發佈
        public string SetIsEdit(string id, bool status, string account, string username)
        {
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        var r = 0;
                        var entity = new EPaperItem();
                        entity.IsPublished = status ? true : false;
                        entity.ItemID = int.Parse(id);
                        var sql = "update EPaperItem set IsPublished=@IsPublished where ItemID=@ItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@IsPublished", entity.IsPublished));
                        base.Parameter.Add(new SqlParameter("@ItemID", entity.ItemID));
                        r = base.ExeNonQuery(sql, tran);
                        if (r >= 0)
                        {
                            
                            tran.Commit();
                            return "更新成功";
                        }
                        else
                        {
                            return "更新失敗";
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "刪除訊息模組異常,error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        return "更新失敗";
                    }

                }
            }

                    
        }
        #endregion

    }
} 
