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
        public List<GroupEPaper> GetVaildGroupEPapers(string Main_ID)
        {
            string sql = @"with cte as
                            (
                            select distinct GroupID from EPaperItem s where ModelID = @Main_ID
                            and s.IsVerift = 1 and s.Enabled = 1
                            and isnull(s.StDate,'1999/1/1') <= convert(date, GetDate())
                            and isnull(s.EdDate,'9999/12/31') >= convert(date, GetDate())
                            )
                            select t.GroupID as ID,isnull(s.Group_Name,'無分類') as Group_Name from cte  t
                            left join GroupEPaper s on t.GroupID = s.ID";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Main_ID", Main_ID));

            return base.SearchList<GroupEPaper>(sql);
        }

        public List<EPaperItem> GetEPaperItemsByModelID(string[] idlist)
        {
            string sql = @"select * from EPaperItem where ModelID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
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
            return _commonService.GetHisEntity<GroupEPaper>("ID", ID); ;
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
            if (model.GroupId != null)
            {
                sql += " and GroupID=@GroupID";
                base.Parameter.Add(new SqlParameter("@GroupID", model.GroupId.Value));
            }
            sql = "select * from EPaperItem where LangID=@Lang_ID and ModelID=@ModelID";
            base.Parameter.Clear();
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
                    PublishStr = data.PublishStr
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

            sql += @" and t.Enabled =1
                             and  isnull(t.StDate,'1999/1/1') <= convert(date,GetDate()) 
                             and isnull(t.EdDate,'9999/12/31') >= convert(date,GetDate()) 
                            ";

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

        




        //#region GetUnitModel
        //public EPaperUnitSettingModel GetUnitModel(string langid)
        //{
        //    var data = .GetByWhere("LangID=@1", new object[] { langid });
        //    var model = new EPaperUnitSettingModel();
        //    model.ID = -1;
        //    if (data.Count() > 0)
        //    {
        //        model = new EPaperUnitSettingModel()
        //        {
        //            FrontPagePath = data.First().FrontPagePath,
        //            Column1 = data.First().Column1,
        //            Column2 = data.First().Column2,
        //            Column3 = data.First().Column3,
        //            Column4 = data.First().Column4,
        //            Column5 = data.First().Column5,
        //            Column6 = data.First().Column6,
        //            Column7 = data.First().Column7,
        //            Column8 = data.First().Column8,
        //            Column9 = data.First().Column9,
        //            Column10 = data.First().Column10,
        //            Column11 = data.First().Column11,
        //            Column12 = data.First().Column12,
        //            Column13 = data.First().Column13,
        //            Column14 = data.First().Column14,
        //            Column15 = data.First().Column15,
        //            Column16 = data.First().Column16,
        //            Column17 = data.First().Column17,
        //            Column18 = data.First().Column18,
        //            Column19 = data.First().Column19,
        //            Column20 = data.First().Column20,
        //            IsRSS = data.First().IsRSS,
        //            ShowCount = data.First().ShowCount,
        //            ID = data.First().ID,
        //            Summary = data.First().Summary
        //        };
        //        var cs = data.First().ColumnSetting;
        //        if (cs.IsNullorEmpty() == false)
        //        {
        //            var csarr = cs.Split('@');
        //            var cname = csarr[0].Split(',');
        //            var cuse = csarr[1].Split(',');
        //            for (var v = 0; v < cname.Length; v++)
        //            {
        //                model.UnitSettingColumnList.Add(new UnitSettingColumn()
        //                {
        //                    Name = cname[v],
        //                    Sellected = int.Parse(cuse[v])
        //                });
        //            }
        //        }
        //    }
        //    if (model.UnitSettingColumnList.Count() == 0)
        //    {
        //        var columnlist = _columnsqlrepository.GetByWhere("Type='EPaper'", null).OrderBy(v => v.Sort);
        //        foreach (var c in columnlist)
        //        {
        //            model.UnitSettingColumnList.Add(new UnitSettingColumn()
        //            {
        //                Name = c.ColumnName,
        //                Sellected = 1
        //            });
        //        }
        //    }
        //    model.ColumnNameMapping = new Dictionary<string, string>();
        //    model.ColumnNameMapping.Add("序號", model.Column1.IsNullorEmpty() ? "序號" : model.Column1);
        //    model.ColumnNameMapping.Add("發佈日期", model.Column2.IsNullorEmpty() ? "發佈日期" : model.Column2);
        //    model.ColumnNameMapping.Add("電子報名稱", model.Column3.IsNullorEmpty() ? "電子報名稱" : model.Column3);
        //    model.ColumnNameMapping.Add("年份", model.Column4.IsNullorEmpty() ? "年份" : model.Column4);
        //    model.ColumnNameMapping.Add("電子報訂閱", model.Column5.IsNullorEmpty() ? "電子報訂閱" : model.Column5);
        //    model.ColumnNameMapping.Add("訂閱", model.Column6.IsNullorEmpty() ? "訂閱" : model.Column6);
        //    model.ColumnNameMapping.Add("取消訂閱", model.Column7.IsNullorEmpty() ? "取消訂閱" : model.Column7);
        //    model.ColumnNameMapping.Add("查閱電子報", model.Column8.IsNullorEmpty() ? "查閱電子報" : model.Column8);
        //    model.ColumnNameMapping.Add("Email", model.Column9.IsNullorEmpty() ? "Email" : model.Column9);
        //    model.ColumnNameMapping.Add("Email 格式有誤", model.Column10.IsNullorEmpty() ? "Email 格式有誤" : model.Column10);
        //    model.ColumnNameMapping.Add("此 Email 已有訂閱電子報!", model.Column11.IsNullorEmpty() ? "此 Email 已有訂閱電子報!" : model.Column11);
        //    model.ColumnNameMapping.Add("電子報訂閱成功!", model.Column12.IsNullorEmpty() ? "電子報訂閱成功!" : model.Column12);
        //    model.ColumnNameMapping.Add("此 Email 無訂閱電子報!", model.Column13.IsNullorEmpty() ? "此 Email 無訂閱電子報!" : model.Column13);
        //    model.ColumnNameMapping.Add("電子報取消訂閱成功!", model.Column14.IsNullorEmpty() ? "電子報取消訂閱成功!" : model.Column14);
        //    return model;
        //}
        //#endregion

    }
} 
