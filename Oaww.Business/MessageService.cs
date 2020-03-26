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

namespace Oaww.Business
{
    public class MessageService : Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();
        protected int _ModelID = 2;

        public MessageService(int ModelID)
        {
            _ModelID = ModelID;
        }
        public List<GroupMessage> GetVaildGroupMessages(string Main_ID)
        {
            string sql = @"with cte as
                            (
                            select distinct GroupID from MessageItem s where ModelID = @Main_ID
                            and s.IsVerift = 1 and s.Enabled = 1
                            and isnull(s.StDate,'1999/1/1') <= convert(date, GetDate())
                            and isnull(s.EdDate,'9999/12/31') >= convert(date, GetDate())
                            )
                            select t.GroupID as ID,isnull(s.Group_Name,'無分類') as Group_Name from cte  t
                            left join GroupMessage s on t.GroupID = s.ID";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Main_ID", Main_ID));

            return base.SearchList<GroupMessage>(sql);
        }

        public List<MessageItem> GetMessageItemsByModelID(string[] idlist)
        {
            string sql = @"select * from MessageItem where ModelID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
            return base.SearchList<MessageItem>(sql);
        }

        public List<MessageItem> GetMessageItems(string[] idlist)
        {
            string sql = @"select * from MessageItem where ItemID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
            return base.SearchList<MessageItem>(sql);
        }

        public MessageItem GetMessageItemByID(string ItemID)
        {
            return _commonService.GetHisEntity<MessageItem>("ItemID", ItemID);
        }

        public GroupMessage GetGroupMessageByID(string ID)
        {
            return _commonService.GetHisEntity<GroupMessage>("ID", ID); ;
        }

        public ModelMessageMain GetModelMessageMain(string ID,string lang)
        {
            string sql = @"select * from ModelMessageMain where ID=@ID and Lang_ID=@Lang_ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));
            base.Parameter.Add(new SqlParameter("@Lang_ID", lang));

            return base.GetObject<ModelMessageMain>(sql);
        }

        /// <summary>
        /// Grid資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Paging<ModelMessageMain> Paging(SearchModelBase model, int ModelID)
        {
            string sql = string.Empty;

            var Paging = new Paging<ModelMessageMain>();
            var onepagecnt = model.Limit;

            sql = "select * from ModelMessageMain where Lang_ID=@Lang_ID and ModelID=@ModelID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", model.LangId));
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            Paging.rows = base.SearchListPage<ModelMessageMain>(sql, model.Offset, model.Limit, " order by " + model.Sort);
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
                        //delete  ModelMessageMain _OK
                        sql = @"delete ModelMessageMain where ID in (select ListItem from dbo.SplitList(',',@idlist))";
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
                        sql = @"delete MessageUnitSetting where MainID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //刪除SEO
                        sql = @"delete SEO where TypeName='MessageItem' and TypeID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);
                        // _seosqlrepository.DelDataUseWhere("TypeName='PageIndexItem' and TypeID=@1", new object[] { items.ItemID });

                        for (var idx = 0; idx < idlist.Length; idx++)
                        {
                            //刪除PageIndexItem
                            List<MessageItem> olditem = _commonService.GetGeneralList<MessageItem>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", idlist[idx] } }, tran)
                                                           .ToList();

                            sql = @"delete MessageItem where ModelID=@ModelID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ModelID", idlist[idx]));
                            base.ExeNonQuery(sql, tran);



                            foreach (var items in olditem)
                            {

                                //刪除SEO
                                sql = @"delete SEO where TypeName='MessageItem' and TypeID=@TypeID";
                                base.Parameter.Clear();
                                base.Parameter.Add(new SqlParameter("@TypeID", items.ItemID));
                                base.ExeNonQuery(sql, tran);


                            }

                        }

                        //update sort order
                        sql = @"with cte as
                                    (
                                    select 
                                     ROW_NUMBER()OVER(order by sort) as ROW,t.ID
                                     from ModelMessageMain t where t.Lang_ID=@Lang and t.ModelID=@ModelID
                                    )
                                    update s set s.Sort = t.ROW from cte  t
                                    left join ModelMessageMain s on t.ID = s.ID and s.ModelID=@ModelID";
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

        public Paging<MessageItemResult> PagingItem(string modelid, MessageSearchModel model,string lang)
        {
            var Paging = new Paging<MessageItemResult>();
            Paging.rows = new List<MessageItemResult>();
            var data = new List<MessageItem>();


            string sql = @"select * from MessageItem t where ModelID=@ModelID  and Lang_ID =@Lang_ID";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", modelid));
            base.Parameter.Add(new SqlParameter("@Lang_ID", lang));
            if (model.GroupId != null)
            {
                sql += " and GroupID=@GroupID";
                base.Parameter.Add(new SqlParameter("@GroupID", model.GroupId.Value));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql += " and (Title like @Title or HtmlContent like @Title)";
                base.Parameter.Add(new SqlParameter("@Title", "%" + model.Title + "%"));
            }

            if (model.Enabled != null)
            {
                sql += " and Enabled=@Enabled";
                base.Parameter.Add(new SqlParameter("@Enabled", model.Enabled));
            }

            if (!model.PublicshDateFrom.IsNullOrEmpty())
            {
                sql += " and (PublicshDate >=@PublicshDateFrom or PublicshDate is Null)";
                base.Parameter.Add(new SqlParameter("@PublicshDateFrom", model.PublicshDateFrom));
            }

            if (!model.PublicshDateTo.IsNullOrEmpty())
            {
                sql += " and (PublicshDate <=@PublicshDateTo or PublicshDate is Null)";
                base.Parameter.Add(new SqlParameter("@PublicshDateTo", model.PublicshDateTo));
            }

            if (!model.DisplayFrom.IsNullOrEmpty())
            {
                sql += " and (EdDate >=@DisplayFrom or EdDate is Null)";
                base.Parameter.Add(new SqlParameter("@DisplayFrom", model.DisplayFrom));
            }

            if (!model.DisplayTo.IsNullOrEmpty())
            {
                sql += " and (StDate <=@DisplayTo or StDate is Null)";
                base.Parameter.Add(new SqlParameter("@DisplayTo", model.DisplayTo));
            }
            if (!string.IsNullOrEmpty(model.IsRange))
            {
                if (model.IsRange == "1")
                {
                    sql += " and (StDate <=@IsRange or StDate is null or StDate='') and (EdDate >=@IsRange or EdDate is null or EdDate='')";
                    base.Parameter.Add(new SqlParameter("@IsRange", DateTime.Now.Date));
                }
                else
                {
                    sql += " and (StDate >@IsRange ) or (EdDate <@IsRange )";
                    base.Parameter.Add(new SqlParameter("@IsRange", DateTime.Now.Date));
                }
            }

            data = base.SearchListPage<MessageItem>(sql, model.Offset, model.Limit, " order by " + model.Sort).ToList();

            Paging.total = base.SearchCount(sql);


            var ALLGroup = _commonService.GetGeneralList<GroupMessage>();
            var modelverifydata = _commonService.GetGeneralList<VerifyData>("ModelID = @ModelID and ModelMainID =@ModelMainID"
                                                                , new Dictionary<string, string>() { { "ModelID", _ModelID.ToString() }, { "ModelMainID", modelid } });
            foreach (var d in data)
            {
                var isrange = false;
                if (d.StDate == null && d.EdDate == null) { isrange = true; }
                else if (d.StDate != null && d.EdDate == null)
                {
                    if (DateTime.Now >= d.StDate.Value)
                    {
                        isrange = true;
                    }
                }
                else if (d.StDate == null && d.EdDate != null)
                {
                    if (DateTime.Now <= d.EdDate.Value.AddDays(1))
                    {
                        isrange = true;
                    }
                }
                else if (d.StDate != null && d.EdDate != null)
                {
                    if (DateTime.Now >= d.StDate.Value && DateTime.Now <= d.EdDate.Value.AddDays(1))
                    {
                        isrange = true;
                    }
                }
                var gname = ALLGroup.Where(v => v.ID == d.GroupID);
                var vdata = modelverifydata.Where(v => v.ModelItemID == d.ItemID);
                Paging.rows.Add(new MessageItemResult()
                {
                    ItemID = d.ItemID,
                    Title = d.Title,
                    ClickCount = d.ClickCnt == null ? "0" : d.ClickCnt.Value.ToString(),
                    Enabled = d.Enabled,
                    IsRange = isrange == true ? "是" : "否",
                    GroupName = gname.Count() > 0 ? gname.First().Group_Name : "無分類",
                    PublicshDate = d.PublicshDate == null ? "" : d.PublicshDate.Value.ToString("yyyy/MM/dd"),
                    Sort = d.Sort.Value,
                    Home = d.Home,
                    VerifyStr = vdata.Count() == 0 ? "審核中" : vdata.First().VerifyStatus == 0 ? "審核中" : (vdata.First().VerifyStatus == 1 ? "已通過" : "未通過"),
                });

            }
            return Paging;
        }

        public MessageEditModel GetModelByID(string modelid, string itemid, string folder)
        {
            var maindata = _commonService.GetGeneral<ModelMessageMain>("ID=@ID", new Dictionary<string, string>() { { "ID", modelid } });
            var data = _commonService.GetGeneral<MessageItem>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", itemid } });
            if (data.ItemID > 0)
            {
                var fdata = data;
                var seodata = _commonService.GetGeneral<SEO>("TypeName='MessageItem' and TypeID=@TypeID", new Dictionary<string, string>() { { "TypeID", itemid } });
                var model = new MessageEditModel()
                {
                    ItemID = fdata.ItemID,
                    Description = seodata.ID > 0 ? seodata.Description : "",
                    ImageFileName = fdata.ImageFileName,
                    ImageFileOrgName = fdata.ImageFileOrgName,
                    UploadFileDesc = fdata.UploadFileDesc,
                    UploadFileName = fdata.UploadFileName,
                    UploadFilePath = fdata.UploadFilePath,
                    UploadFileSize = fdata.UploadFileSize,
                    UploadFileType = fdata.UploadFileType,
                    WebsiteTitle = seodata.ID > 0 ? seodata.Title : "",
                    Keywords = seodata.ID == 0 ? new string[10] : new string[] {
                        seodata.Keywords1, seodata.Keywords2, seodata.Keywords3, seodata.Keywords4, seodata.Keywords5
                    , seodata.Keywords6, seodata.Keywords7, seodata.Keywords8, seodata.Keywords9, seodata.Keywords10 },
                    HtmlContent = fdata.HtmlContent,
                    ImageFileDesc = fdata.ImageFileDesc,
                    ImageFileLocation = fdata.ImageFileLocation,
                    LinkUrl = fdata.LinkUrl,
                    ModelID = fdata.ModelID.Value,
                    ImageUrl = $"~/UploadImage/{folder}/" + fdata.ImageFileName,
                    ActiveID = fdata.ActiveID == null ? "" : fdata.ActiveID.ToString(),
                    ActiveItemID = fdata.ActiveItemID == null ? "" : fdata.ActiveItemID.ToString(),
                    EdDate = fdata.EdDate,
                    EdDateStr = fdata.EdDate == null ? "" : fdata.EdDate.Value.ToString("yyyy/MM/dd"),
                    Group_ID = fdata.GroupID == null ? -1 : fdata.GroupID.Value,
                    Link_Mode = fdata.Link_Mode,
                    PublicshStr = fdata.PublicshDate == null ? "" : fdata.PublicshDate.Value.ToString("yyyy/MM/dd"),
                    UnPublicshStr = fdata.UnPublishDate.HasValue ? fdata.UnPublishDate.Value.ToString("yyyy/MM/dd") : "",
                    StDate = fdata.StDate,
                    StDateStr = fdata.StDate == null ? "" : fdata.StDate.Value.ToString("yyyy/MM/dd"),
                    Title = fdata.Title,
                    RelateImageFileOrgName = fdata.RelateImageFileOrgName,
                    RelateImageName = fdata.RelateImageFileName,
                    RelateImagelUrl = $"~/UploadImage/{folder}/" + fdata.RelateImageFileName,
                    Introduction = fdata.Introduction,
                    CreateDatetime = fdata.CreateDatetime == null ? "" : fdata.CreateDatetime.Value.ToString("yyyy/MM/dd HH:mm:ss"),
                    CreateUser = fdata.CreateName,
                    OpenMode = fdata.OpenMode,
                    UpdateDatetime = fdata.UpdateDatetime == null ? "" : fdata.UpdateDatetime.Value.ToString("yyyy/MM/dd HH:mm:ss"),
                    UpdateUser = fdata.UpdateName,
                    LinkUrlDesc = fdata.LinkUrlDesc,
                    Home = fdata.Home,
                    ComnonHtml1 = fdata.ComnonHtml1,
                    ComnonHtml2 = fdata.ComnonHtml2
                };


                model.fileDownloadFiles = _commonService.GetGeneralList<MessageFile>("ItemID=@ItemID and ModelID=@ModelID",
                                                                                         new Dictionary<string, string>() { { "ItemID", itemid }, { "ModelID", modelid } }).ToList();

                model.messageImages = _commonService.GetGeneralList<MessageImage>("ItemID=@ItemID and ModelID=@ModelID",
                                                                                         new Dictionary<string, string>() { { "ItemID", itemid }, { "ModelID", modelid } }).ToList();

                var hasvdata = _commonService.GetGeneralList<VerifyData>("ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID",
                                                                new Dictionary<string, string>() { { "ModelID", _ModelID.ToString() },
                                                                                                   { "ModelMainID", fdata.ModelID.Value.ToString() },
                                                                                                   { "ModelItemID", fdata.ItemID.ToString() } })
                                            .ToList();

                if (hasvdata.Count() > 0)
                {
                    model.VerifyStatus = hasvdata.First().VerifyStatus == 0 ? "審核中" : (hasvdata.First().VerifyStatus == 1 ? "已通過" : "未通過");
                    model.VerifyDateTime = hasvdata.First().VerifyDateTime == null ? "" : hasvdata.First().VerifyDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                    model.VerifyUser = hasvdata.First().VerifyName;

                }
                model.ModelName = maindata.ID == 0 ? "" : maindata.Name;
                return model;
            }
            else
            {
                MessageEditModel model = new MessageEditModel();
                model.Home = false;
                model.Keywords = new string[10];
                model.ModelID = int.Parse(modelid);
                model.ImageFileLocation = "1";
                model.ModelName = maindata.ID == 0 ? "" : maindata.Name;
                model.UnPublicshStr = "";
                model.fileDownloadFiles = new List<MessageFile>();
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
        public string CreateItem(MessageEditModel model, string LangId, string account, string userName)
        {
            string sql = string.Empty;

            SET_BASE SET_BASE = _commonService.GetGeneral<SET_BASE>();

            var iswriteseo = false;
            if (model.WebsiteTitle.IsNullOrEmpty() == false || model.Description.IsNullOrEmpty() == false
                 || (model.Keywords != null && model.Keywords.Any(v => v.IsNullOrEmpty() == false)))
            {
                iswriteseo = true;
            }

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        var olddata = _commonService.GetGeneralList<MessageItem>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", model.ModelID.ToString() } }, tran);

                        var savemodel = new MessageItem()
                        {
                            HtmlContent = model.HtmlContent,
                            ImageFileDesc = model.ImageFileDesc,
                            ImageFileLocation = model.ImageFileLocation,
                            ModelID = model.ModelID,
                            ImageFileOrgName = model.ImageFileOrgName,
                            LinkUrl = model.LinkUrl,
                            UploadFileDesc = model.UploadFileDesc,
                            UploadFileName = model.UploadFileName,
                            UploadFilePath = model.UploadFilePath,
                            UploadFileSize = model.UploadFileSize,
                            UploadFileType = model.UploadFileType,
                            ImageFileName = model.ImageFileName,
                            Sort = 1,
                            ClickCnt = 0,
                            GroupID = model.Group_ID,
                            Lang_ID = int.Parse(LangId),
                            Title = model.Title,
                            CreateDatetime = DateTime.Now,
                            CreateUser = account,
                            Enabled = true,
                            Link_Mode = model.Link_Mode,
                            RelateImageFileName = model.RelateImageName,
                            RelateImageFileOrgName = model.RelateImageFileOrgName,
                            Introduction = model.Introduction == null ? "" : model.Introduction,
                            CreateName = userName,
                            IsVerift = !SET_BASE.M_Base02,
                            LinkUrlDesc = model.LinkUrlDesc,
                            OpenMode = model.OpenMode,
                            Home = model.Home,
                            ComnonHtml1 = model.ComnonHtml1,
                            ComnonHtml2 = model.ComnonHtml2
                        };
                        if (model.ActiveID.IsNullOrEmpty() == false)
                        {
                            savemodel.ActiveID = int.Parse(model.ActiveID);
                        }
                        if (model.ActiveItemID.IsNullOrEmpty() == false)
                        {
                            savemodel.ActiveItemID = int.Parse(model.ActiveItemID);
                        }
                        if (model.PublicshStr.IsNullOrEmpty() == false)
                        {
                            savemodel.PublicshDate = DateTime.Parse(model.PublicshStr);
                        }
                        if (model.EdDateStr.IsNullOrEmpty() == false)
                        {
                            savemodel.EdDate = DateTime.Parse(model.EdDateStr);
                        }
                        if (model.EdDateStr.IsNullOrEmpty() == false)
                        {
                            savemodel.EdDate = DateTime.Parse(model.EdDateStr);
                        }
                        if (model.StDateStr.IsNullOrEmpty() == false)
                        {
                            savemodel.StDate = DateTime.Parse(model.StDateStr);
                        }

                        if (model.UnPublicshStr.IsNullOrEmpty() == false)
                        {
                            savemodel.UnPublishDate = DateTime.Parse(model.UnPublicshStr);
                        }




                        //先更新sort
                        sql = "update MessageItem set Sort=Sort+1 where ModelID =@ModelID ";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelID", model.ModelID.ToString()));
                        base.ExeNonQuery(sql, tran);

                        var r = base.InsertObject(savemodel, tran);

                        if (model.fileDownloadFiles != null)
                        {
                            //新增files
                            model.fileDownloadFiles.ForEach(t =>
                            {
                                t.ItemID = (int)r;
                                t.ModelID = model.ModelID;

                                base.InsertObject(t, tran);
                            });
                        }

                        if (model.messageImages != null)
                        {
                            //新增files
                            model.messageImages.ForEach(t =>
                            {
                                t.ItemID = (int)r;
                                t.ModelID = model.ModelID;

                                base.InsertObject(t, tran);
                            });
                        }

                        sql = "delete VerifyData where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                        base.Parameter.Add(new SqlParameter("@ModelMainID", savemodel.ModelID.Value));
                        base.Parameter.Add(new SqlParameter("@ModelItemID", savemodel.ItemID));
                        base.ExeNonQuery(sql, tran);

                        base.InsertObject(new VerifyData()
                        {
                            ModelID = _ModelID,
                            ModelItemID = savemodel.ItemID,
                            ModelName = savemodel.Title,
                            ModelMainID = savemodel.ModelID.Value,
                            VerifyStatus = SET_BASE.M_Base02 ? 0 : 1,
                            ModelStatus = 1,
                            UpdateDateTime = DateTime.Now,
                            UpdateUser = userName,
                            UpdateAccount = account,
                            LangID = int.Parse(LangId)
                        }, tran);

                        if (iswriteseo)
                        {
                            r = base.InsertObject(new SEO()
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
                                TypeName = "MessageItem",
                                TypeID = savemodel.ItemID,
                                Lang_ID = int.Parse(LangId)
                            }, tran);
                        }





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
        public string UpdateItem(MessageEditModel model, string LangId, string account, string userName)
        {
            SET_BASE SET_BASE = _commonService.GetGeneral<SET_BASE>();

            var iswriteseo = false;
            if (model.WebsiteTitle.IsNullOrEmpty() == false || model.Description.IsNullOrEmpty() == false
                   || (model.Keywords != null && model.Keywords.Any(v => v.IsNullOrEmpty() == false)))
            {
                iswriteseo = true;
            }
            var olddata = _commonService.GetGeneral<MessageItem>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", model.ItemID.ToString() } });

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        string sql = @"delete SEO where TypeName='MessageItem' and TypeID=@TypeID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@TypeID", model.ItemID));
                        base.ExeNonQuery(sql, tran);

                        olddata.HtmlContent = model.HtmlContent == null ? "" : model.HtmlContent;
                        olddata.ImageFileDesc = model.ImageFileDesc == null ? "" : model.ImageFileDesc;
                        olddata.ImageFileLocation = model.ImageFileLocation == null ? "" : model.ImageFileLocation;
                        olddata.RelateImageFileName = model.RelateImageName;
                        olddata.RelateImageFileOrgName = model.RelateImageFileOrgName;
                        olddata.ImageFileOrgName = model.ImageFileOrgName;
                        olddata.LinkUrl = model.LinkUrl == null ? "" : model.LinkUrl == null ? "" : model.LinkUrl;
                        olddata.LinkUrlDesc = model.LinkUrlDesc == null ? "" : model.LinkUrlDesc == null ? "" : model.LinkUrlDesc;
                        olddata.UploadFileDesc = model.UploadFileDesc == null ? "" : model.UploadFileDesc;
                        olddata.UploadFileName = model.UploadFileName;
                        olddata.UploadFilePath = model.UploadFilePath == null ? "" : model.UploadFilePath;
                        olddata.UploadFileSize = model.UploadFileSize == null ? null : model.UploadFileSize;
                        olddata.UploadFileType = model.UploadFileType == null ? "" : model.UploadFileType;
                        olddata.ImageFileName = model.ImageFileName;
                        olddata.GroupID = model.Group_ID;
                        olddata.Title = model.Title == null ? "" : model.Title;
                        olddata.UpdateDatetime = DateTime.Now;
                        olddata.UpdateUser = account;
                        olddata.Enabled = true;
                        olddata.Link_Mode = model.Link_Mode;
                        olddata.Introduction = model.Introduction == null ? "" : model.Introduction;
                        olddata.UpdateName = userName;
                        olddata.IsVerift = !SET_BASE.M_Base02;
                        olddata.OpenMode = model.OpenMode;
                        olddata.Home = model.Home;
                        olddata.ComnonHtml1 = model.ComnonHtml1;
                        olddata.ComnonHtml2 = model.ComnonHtml2;

                        if (model.ActiveID.IsNullOrEmpty() == false)
                        {
                            olddata.ActiveID = int.Parse(model.ActiveID);
                        }
                        if (model.ActiveItemID.IsNullOrEmpty() == false)
                        {
                            olddata.ActiveItemID = int.Parse(model.ActiveItemID);
                        }
                        if (model.PublicshStr.IsNullOrEmpty() == false)
                        {
                            olddata.PublicshDate = DateTime.Parse(model.PublicshStr);
                        }
                        if (model.EdDateStr.IsNullOrEmpty() == false)
                        {
                            olddata.EdDate = DateTime.Parse(model.EdDateStr);
                        }
                        else
                        {
                            olddata.EdDate = null;
                        }

                        if (model.StDateStr.IsNullOrEmpty() == false)
                        {
                            olddata.StDate = DateTime.Parse(model.StDateStr);
                        }
                        else
                        {
                            olddata.StDate = null;
                        }

                        if (model.UnPublicshStr.IsNullOrEmpty() == false)
                        {
                            olddata.UnPublishDate = DateTime.Parse(model.UnPublicshStr);
                        }

                        var r = base.UpdateObject(olddata, tran);

                        sql = @"delete  MessageFile where ItemID=@ItemID and ModelID=@ModelID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ItemID", model.ItemID));
                        base.Parameter.Add(new SqlParameter("@ModelID", model.ModelID));
                        base.ExeNonQuery(sql, tran);

                        if (model.fileDownloadFiles != null)
                        {
                            model.fileDownloadFiles.ForEach(t =>
                            {
                                base.InsertObject(t, tran);
                            });
                        }

                        sql = @"delete  MessageImage where ItemID=@ItemID and ModelID=@ModelID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ItemID", model.ItemID));
                        base.Parameter.Add(new SqlParameter("@ModelID", model.ModelID));
                        base.ExeNonQuery(sql, tran);

                        if (model.messageImages != null)
                        {
                            //新增files
                            model.messageImages.ForEach(t =>
                            {
                                base.InsertObject(t, tran);
                            });
                        }

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

                            if (iswriteseo)
                            {
                                base.InsertObject(new SEO()
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
                                    TypeName = "MessageItem",
                                    TypeID = olddata.ItemID,
                                    Lang_ID = int.Parse(LangId)
                                }, tran);
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

        public MessageFile GetFileDownloadFile(string id)
        {
            return _commonService.GetGeneral<MessageFile>("FID=@FID", new Dictionary<string, string>() { { "FID", id } });
        }

        public MessageImage GetFileDownloadImage(string id)
        {
            return _commonService.GetGeneral<MessageImage>("FID=@FID", new Dictionary<string, string>() { { "FID", id } });
        }
        #region 前台

        public Paging<MessageItem> GetPaging(string itemid, int? group, string title, int nowpage, int showCount)
        {
            string sql = @"select  ROW_NUMBER() OVER(ORDER BY t.Sort) AS No,t.*,isnull(s.Group_Name,'無分類') as GroupName 
                           from MessageItem t
                           left join GroupMessage s on t.GroupID = s.ID where ModelID=@itemid";
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

            var Paging = new Paging<MessageItem>();

            Paging.total = base.SearchCount(sql);

            Paging.rows = base.SearchListPage<MessageItem>(sql, (nowpage - 1) * showCount, showCount, " order by t.Sort");

            return Paging;
        }
        public Paging<MessageItem> GetPaging(string itemid, string fromDate, string toDate, string title, int nowpage, int showCount)
        {
            string sql = "select  ROW_NUMBER() OVER(ORDER BY Sort) AS No,t.* from MessageItem t where ModelID=@itemid";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@itemid", itemid));

            if (fromDate.IsNullOrEmpty() == false)
            {
                sql += " and PublicshDate >= @fromDate";
                base.Parameter.Add(new SqlParameter("@fromDate", fromDate));
            }

            if (toDate.IsNullOrEmpty() == false)
            {
                sql += " and PublicshDate <= @toDate";
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

            var Paging = new Paging<MessageItem>();

            Paging.total = base.SearchCount(sql);

            Paging.rows = base.SearchListPage<MessageItem>(sql, (nowpage - 1) * showCount, showCount, " order by t.Sort");

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

            string sql = @"select t.Group_Name from GroupMessage t where t.ID=@ID ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", GroupID));

            return base.ExecuteScalar(sql, "").ToString();
        }

        public List<MessageFile> GetFileDownloadFiles(string ItemID)
        {
            //舊的資料
            return _commonService.GetGeneralList<MessageFile>("ItemID=@ItemID "
                                                                    , new Dictionary<string, string>() { { "ItemID", ItemID }
                                                                                                        }).ToList();

        }

        public List<MessageImage> GetFileDownloadImages(string ItemID)
        {
            //舊的資料
            return _commonService.GetGeneralList<MessageImage>("ItemID=@ItemID "
                                                                    , new Dictionary<string, string>() { { "ItemID", ItemID }
                                                                                                        }).ToList();

        }
        #endregion

        public void UpdateClickcnt(string ItemID)
        {
            string sql = "update MessageItem set ClickCnt= ClickCnt+1 where ItemID=@ItemID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ItemID", ItemID));
            base.ExeNonQuery(sql);
        }
    }
}
