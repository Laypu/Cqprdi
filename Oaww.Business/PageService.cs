using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.Entity.SET;
using Oaww.ViewModel;
using System.Data.SqlClient;
using Oaww.Business;
using System.Web.Mvc;
using Oaww.Utility;

namespace Oaww.Business
{
    public class PageService : Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();
        protected int _ModelID = 1;

        public PageService(int ModelID)
        {
            _ModelID = ModelID;
        }

        public PageIndexItem GetVaildPageIndexItemByItemID(string ItemID)
        {
            return _commonService.GetGeneral<PageIndexItem>("ModelID=@ModelID and Enabled=1 Order By Sort", new Dictionary<string, string>() { { "ModelID", ItemID } });
        }

        public PageIndexItem GetVaildPageIndexItemByItemID(string ItemID, int Lang)
        {
            return _commonService.GetGeneral<PageIndexItem>("ModelID=@ModelID and Enabled=1 and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "ModelID", ItemID }, { "Lang_ID", Lang.ToString() } });
        }

        public PageIndexItem GetPageIndexItemByItemID(string ItemID)
        {
            return _commonService.GetGeneral<PageIndexItem>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", ItemID } });
        }

        public List<PageIndexItem> GetPageIndexItemsByModelID(string[] idlist)
        {
            string sql = @"select * from PageIndexItem where ModelID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
            return base.SearchList<PageIndexItem>(sql);
        }

        public ModelPageEditMain GetModelPageEditMainByID(int ID)
        {
            string sql = @"select * from ModelPageEditMain where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));
            return base.GetObject<ModelPageEditMain>(sql);
        }

        public List<PageIndexItem> GetPageIndexItemByID(bool Enable, string ModelID)
        {
            string sql = @"select * from PageIndexItem where Enabled=@Enabled and ModelID=@ModelID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Enabled", Enable));
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            return base.SearchList<PageIndexItem>(sql);
        }

        /// <summary>
        /// Grid Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Paging<ModelPageEditMain> Paging(SearchModelBase model, int ModelID)
        {
            var Paging = new Paging<ModelPageEditMain>();
            var onepagecnt = model.Limit;

            string sql = @"select * from ModelPageEditMain t where t.ModelID=@ModelID and t.Lang_ID=@Lang and Status = 1 ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang", model.LangId));
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            Paging.rows = base.SearchList<ModelPageEditMain>(sql, model.Offset, model.Limit, " order by " + model.Sort);
            Paging.total = base.SearchCount(sql);
            return Paging;
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="delaccount"></param>
        /// <param name="langid"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string Delete(string[] idlist, string delaccount, string langid, string account, string username)
        {
            try
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
                            //delete  ModelPageEditMain
                            sql = @"delete ModelPageEditMain where ID in (select ListItem from dbo.SplitList(',',@idlist))";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                            base.ExeNonQuery(sql, tran);

                            //delete VerifyData
                            sql = @"delete VerifyData where ModelID=@ModelID and ModelMainID in (select ListItem from dbo.SplitList(',',@idlist))";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                            base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                            base.ExeNonQuery(sql, tran);

                            //刪除PageUnitSetting
                            sql = @"delete PageUnitSetting where MainID in (select ListItem from dbo.SplitList(',',@idlist))";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                            base.ExeNonQuery(sql, tran);

                            //刪除SEO
                            sql = @"delete SEO where TypeName='PageIndexItem' and TypeID in (select ListItem from dbo.SplitList(',',@idlist))";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                            base.ExeNonQuery(sql, tran);
                            // _seosqlrepository.DelDataUseWhere("TypeName='PageIndexItem' and TypeID=@1", new object[] { items.ItemID });

                            for (var idx = 0; idx < idlist.Length; idx++)
                            {
                                //刪除PageIndexItem
                                List<PageIndexItem> olditem = _commonService.GetGeneralList<PageIndexItem>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", idlist[idx] } }, tran)
                                                               .ToList();

                                sql = @"delete PageIndexItem where ModelID=@ModelID";
                                base.Parameter.Clear();
                                base.Parameter.Add(new SqlParameter("@ModelID", idlist[idx]));
                                base.ExeNonQuery(sql, tran);



                                foreach (var items in olditem)
                                {

                                    //刪除SEO
                                    sql = @"delete SEO where TypeName='PageIndexItem' and TypeID=@TypeID";
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
                                     from ModelPageEditMain t where t.Lang_ID=@langid and ModelID=@ModelID
                                    )
                                    update s set s.Sort = t.ROW from cte  t
                                    left join ModelPageEditMain s on t.ID = s.ID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@langid", langid));
                            base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                            base.ExeNonQuery(sql, tran);

                            tran.Commit();
                            rstr = "刪除成功";

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "刪除圖文模組異常,error:" + ex.ToString().NewLineReplace());

                            tran.Rollback();
                            rstr = "刪除失敗";
                        }

                        return rstr;
                    }
                }






            }
            catch (Exception ex)
            {
                logger.Error(ex, "刪除圖文模組異常,error:" + ex.ToString().NewLineReplace());
                return "刪除失敗";
            }
        }



        /// <summary>
        /// 更新sort
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seq"></param>
        /// <param name="langid"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string UpdateSeq(int id, int seq, string langid, string account, string username, int ModelID)
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                var oldmodel = GetModelPageEditMainByID(id);
                if (oldmodel.ID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<ModelPageEditMain> mtseqdata = null;

                    mtseqdata = _commonService.GetGeneralList<ModelPageEditMain>("Sort>@Sort and Lang_ID=@Lang_ID and ModelID=@ModelID "
                                                                                  , new Dictionary<string, string>() { { "Sort", seq.ToString() }, { "Lang_ID", langid }, { "ModelID", ModelID.ToString() } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        sql = @"select * from ModelPageEditMain where Lang_ID=@Lang_ID  and ModelID=@ModelID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Lang_ID", langid));
                        base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<ModelPageEditMain> ltseqdata = null;
                    ltseqdata = _commonService.GetGeneralList<ModelPageEditMain>("Sort<=@Sort and Lang_ID=@Lang_ID  and ModelID=@ModelID "
                                                                                  , new Dictionary<string, string>() { { "Sort", qseq.ToString() }, { "Lang_ID", langid }, { "ModelID", ModelID.ToString() } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList(); //_sqlrepository.GetByWhere("Sort<=@1  and Lang_ID=@2", new object[] { qseq, langid }).OrderBy(v => v.Sort).ToArray();
                    //更新seq+1
                    var sidx = 0;
                    for (var idx = 1; idx <= ltseqdata.Count(); idx++)
                    {
                        if (idx == seq && seq < oldmodel.Sort)
                        {
                            sidx += 1;
                        }
                        var tempmodel = ltseqdata[idx - 1];
                        if (tempmodel.ID == id)
                        {
                            continue;
                        }
                        else
                        {
                            sidx += 1;
                        }
                        tempmodel.Sort = sidx;
                        base.UpdateObject(tempmodel);
                    }
                }
                //先取出大於目前seq的資料

                oldmodel.Sort = seq;
                r = base.UpdateObject(oldmodel);
                if (r)
                {
                    return "更新成功";
                }
                else
                {
                    return "更新失敗";
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新圖文編輯排序失敗,error:" + ex.ToString().NewLineReplace());
                return "更新圖文編輯排序失敗:" + " error:" + ex.Message;
            }
        }

        public IList<SelectListItem> GetSelectList(string id)
        {
            var list = _commonService.GetGeneralList<PageIndexItem>("ModelID=@ModelID and Enabled=1 Order By Sort"
                                                                    , new Dictionary<string, string>() { { "ModelID", id.ToString() } }).ToList();
            IList<SelectListItem> item = new List<SelectListItem>();
            if (list.Count() == 0)
            {
                var model = GetModelPageEditMainByID(int.Parse(id));
                base.InsertObject(new PageIndexItem()
                {
                    ModelID = model.ID,
                    ItemName = model.Name,
                    Lang_ID = model.Lang_ID,
                    Sort = 1,
                    Enabled = true,
                    UpdateUser = "system",
                    UpdateDatetime = DateTime.Now
                });
                list = GetPageIndexItemByID(true, id);
            }
            foreach (var l in list)
            {
                item.Add(new SelectListItem() { Text = l.ItemName, Value = l.ItemID.ToString() });
            }
            return item;
        }

        /// <summary>
        /// 取得Defualt Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PageEditItemModel GetFirstModel(string id)
        {
            string sql = @"select Top(1) * from PageIndexItem  where ModelID=@ModelID Order By Sort";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", id));
            var data = base.GetObject<PageIndexItem>(sql);

            if (data.ItemID != 0)
            {
                var fdata = data;
                var seodata = _commonService.GetGeneralList<SEO>("TypeName='PageIndexItem' and TypeID=@TypeID and Lang_ID=@Lang_ID",
                                                                new Dictionary<string, string>() { { "TypeID", fdata.ItemID.ToString() }, { "Lang_ID", data.Lang_ID.ToString() } })
                                            .ToList();

                var hasvdata = _commonService.GetGeneralList<VerifyData>("ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID",
                                                                new Dictionary<string, string>() { { "ModelID", _ModelID.ToString() },
                                                                                                   { "ModelMainID", fdata.ModelID.Value.ToString() },
                                                                                                   { "ModelItemID", fdata.ItemID.ToString() } })
                                            .ToList();

                var model = new PageEditItemModel()
                {
                    ItemID = fdata.ItemID,
                    ModelName = fdata.ItemName,
                    Description = seodata.Count() > 0 ? seodata.First().Description : "",
                    ImageFileName = fdata.ImageFileName,
                    ImageFileOrgName = fdata.ImageFileOrgName,
                    UploadFileDesc = fdata.UploadFileDesc,
                    UploadFileName = fdata.UploadFileName,
                    UploadFilePath = fdata.UploadFilePath,
                    ActiveID = fdata.ActiveID == null ? "" : fdata.ActiveID.Value.ToString(),
                    ActiveItemID = fdata.ActiveItemID == null ? "" : fdata.ActiveItemID.Value.ToString(),
                    WebsiteTitle = seodata.Count() > 0 ? seodata.First().Title : "",
                    Keywords = seodata.Count() == 0 ? new string[10] : new string[] {
                        seodata.First().Keywords1, seodata.First().Keywords2, seodata.First().Keywords3, seodata.First().Keywords4, seodata.First().Keywords5
                    , seodata.First().Keywords6, seodata.First().Keywords7, seodata.First().Keywords8, seodata.First().Keywords9, seodata.First().Keywords10 },
                    HtmlContent = fdata.HtmlContent.IsNullOrEmpty() ? "" : fdata.HtmlContent,
                    ImageFileDesc = fdata.ImageFileDesc,
                    ImageFileLocation = fdata.ImageFileLocation.IsNullOrEmpty() ? "1" : fdata.ImageFileLocation,
                    LinkUrl = fdata.LinkUrl,
                    ModelID = int.Parse(id),
                    ImageUrl = "~/UploadImage/PageEdit/" + fdata.ImageFileName,
                    CreateDatetime = fdata.CreateDatetime == null ? "" : fdata.CreateDatetime.Value.ToString("yyyy/MM/dd HH:mm:ss"),
                    CreateUser = fdata.CreateName,
                    UpdateDatetime = fdata.UpdateDatetime == null ? "" : fdata.UpdateDatetime.Value.ToString("yyyy/MM/dd HH:mm:ss"),
                    UpdateUser = fdata.UpdateName,
                    LinkUrlDesc = fdata.LinkUrlDesc,
                    PageImages = _commonService.GetGeneralList<PageImage>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", fdata.ItemID.ToString() } }).ToList()
                };
                if (hasvdata.Count() > 0)
                {
                    model.VerifyStatus = hasvdata.First().VerifyStatus == 0 ? "審核中" : (hasvdata.First().VerifyStatus == 1 ? "已通過" : "未通過");
                    model.VerifyDateTime = hasvdata.First().VerifyDateTime == null ? "" : hasvdata.First().VerifyDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                    model.VerifyUser = hasvdata.First().VerifyName;

                }
                return model;
            }
            else
            {
                PageEditItemModel model = new PageEditItemModel();
                model.Keywords = new string[10];
                model.ModelID = int.Parse(id);
                model.PageImages = new List<PageImage>();
                return model;
            }

        }

        /// <summary>
        /// 取得Model By ID
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public PageEditItemModel GetModelByID(string modelid, string itemid)
        {
            string sql = @"select * from PageIndexItem  where ModelID=@ModelID and ItemID=@ItemID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", modelid));
            base.Parameter.Add(new SqlParameter("@ItemID", itemid));
            var data = base.GetObject<PageIndexItem>(sql);

            if (data.ItemID != 0)
            {
                var fdata = data;
                var seodata = _commonService.GetGeneralList<SEO>("TypeName='PageIndexItem' and TypeID=@TypeID and Lang_ID=@Lang_ID",
                                                                new Dictionary<string, string>() { { "TypeID", itemid }, { "Lang_ID", data.Lang_ID.ToString() } })
                                            .ToList();

                var model = new PageEditItemModel()
                {
                    ItemID = fdata.ItemID,
                    ModelName = fdata.ItemName,
                    Description = seodata.Count() > 0 ? seodata.First().Description : "",
                    ImageFileName = fdata.ImageFileName,
                    ImageFileOrgName = fdata.ImageFileOrgName,

                    UploadFileDesc = fdata.UploadFileDesc,
                    UploadFileName = fdata.UploadFileName,
                    UploadFilePath = fdata.UploadFilePath,

                    WebsiteTitle = seodata.Count() > 0 ? seodata.First().Title : "",
                    Keywords = seodata.Count() == 0 ? new string[10] : new string[] {
                        seodata.First().Keywords1,seodata.First().Keywords2,seodata.First().Keywords3,seodata.First().Keywords4,seodata.First().Keywords5
                    ,seodata.First().Keywords6,seodata.First().Keywords7,seodata.First().Keywords8,seodata.First().Keywords9,seodata.First().Keywords10},
                    HtmlContent = fdata.HtmlContent.IsNullOrEmpty() ? "" : fdata.HtmlContent,
                    ImageFileDesc = fdata.ImageFileDesc,
                    ImageFileLocation = fdata.ImageFileLocation.IsNullOrEmpty() ? "1" : fdata.ImageFileLocation,
                    LinkUrl = fdata.LinkUrl,
                    LinkUrlDesc = fdata.LinkUrlDesc,
                    ModelID = int.Parse(modelid),
                    ImageUrl = "~/UploadImage/PageEdit/" + fdata.ImageFileName,
                    VerifyStatus = "",
                    CreateDatetime = fdata.CreateDatetime == null ? "" : fdata.CreateDatetime.Value.ToString("yyyy/MM/dd HH:mm:ss"),
                    CreateUser = fdata.CreateName,
                    UpdateDatetime = fdata.UpdateDatetime == null ? "" : fdata.UpdateDatetime.Value.ToString("yyyy/MM/dd HH:mm:ss"),
                    VerifyDateTime = "",
                    UpdateUser = fdata.UpdateName,
                    VerifyUser = "",
                    PageImages = _commonService.GetGeneralList<PageImage>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", fdata.ItemID.ToString() } }).ToList()
                };


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
                return model;
            }
            else
            {
                PageEditItemModel model = new PageEditItemModel();
                model.Keywords = new string[10];
                model.ModelID = int.Parse(modelid);
                model.PageImages = new List<PageImage>();
                return model;
            }

        }

        /// <summary>
        /// 新增PageIndexItem
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LangId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public string CreatePageEdit(PageEditItemModel model, string LangId, string account)
        {
            var iswriteseo = false;

            SET_BASE SET_BASE = _commonService.GetGeneral<SET_BASE>();

            if (model.WebsiteTitle.IsNullOrEmpty() == false || model.Description.IsNullOrEmpty() == false
                   || model.Keywords.Any(v => v.IsNullOrEmpty() == false))
            {
                iswriteseo = true;
            }
            var mexcoount = _commonService.GetMaxID("PageIndexItem", "ModelID", model.ModelID.ToString());

            var admin = _commonService.GetUsersByID(account);

            var PageIndexItem = new PageIndexItem()
            {
                Lang_ID = int.Parse(LangId),
                Enabled = true,
                HtmlContent = model.HtmlContent,
                ImageFileDesc = model.ImageFileDesc,
                ImageFileLocation = model.ImageFileLocation,
                UpdateDatetime = DateTime.Now,
                ModelID = model.ModelID,
                UpdateUser = account,
                ImageFileOrgName = model.ImageFileOrgName,
                LinkUrl = model.LinkUrl,
                UploadFileDesc = model.UploadFileDesc,
                UploadFileName = model.UploadFileName,
                UploadFilePath = model.UploadFilePath,
                ImageFileName = model.ImageFileName,
                Sort = mexcoount + 1,
                CreateDatetime = DateTime.Now,
                CreateUser = account,
                CreateName = admin.User_Name,
                LinkUrlDesc = model.LinkUrlDesc,
                IsVerift = !SET_BASE.M_Base02
            };
            if (model.ActiveID.IsNullOrEmpty() == false)
            {
                PageIndexItem.ActiveID = int.Parse(model.ActiveID);
            }
            if (model.ActiveItemID.IsNullOrEmpty() == false)
            {
                PageIndexItem.ActiveItemID = int.Parse(model.ActiveItemID);
            }

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {

                        var r = base.InsertObject(PageIndexItem, tran);

                        model.PageImages.ForEach(t =>
                        {
                            base.InsertObject(t, tran);
                        });

                        if (r > 0)
                        {
                            base.InsertObject(new VerifyData()
                            {
                                ModelID = _ModelID,
                                ModelItemID = PageIndexItem.ItemID,
                                ModelName = PageIndexItem.ItemName,
                                ModelMainID = PageIndexItem.ModelID.Value,
                                VerifyStatus = SET_BASE.M_Base02 ? 0 : 1,
                                ModelStatus = 1,
                                UpdateDateTime = DateTime.Now,
                                UpdateUser = admin.User_Name,
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
                                    TypeName = "PageIndexItem",
                                    TypeID = PageIndexItem.ItemID,
                                    Lang_ID = int.Parse(LangId)
                                }, tran);
                            }
                            return "新增成功";
                        }
                        else
                        {
                            return "新增失敗";
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "新增圖文編輯Item失敗,error:" + ex.ToString().NewLineReplace());

                        return "新增失敗";
                    }
                }
            }
        }

        /// <summary>
        /// 修改PageIndexItem
        /// </summary>
        /// <param name="model"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public string EditPageItem(PageEditItemModel model, string account)
        {

            SET_BASE SET_BASE = _commonService.GetGeneral<SET_BASE>();
            string sql = string.Empty;

            var iswriteseo = false;

            var olddata = _commonService.GetGeneral<PageIndexItem>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", model.ItemID.ToString() } });

            var admin = _commonService.GetUsersByID(account);

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        //刪除seo
                        sql = "delete SEO where TypeName='PageIndexItem' and TypeID=@TypeID and Lang_ID=@Lang_ID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@TypeID", model.ItemID));
                        base.Parameter.Add(new SqlParameter("@Lang_ID", olddata.Lang_ID));
                        base.ExeNonQuery(sql, tran);

                        var PageIndexItem = olddata;

                        PageIndexItem.ItemID = model.ItemID.Value;
                        PageIndexItem.HtmlContent = model.HtmlContent == null ? "" : model.HtmlContent;
                        PageIndexItem.ImageFileDesc = model.ImageFileDesc == null ? "" : model.ImageFileDesc;
                        PageIndexItem.ImageFileLocation = model.ImageFileLocation == null ? "" : model.ImageFileLocation;
                        PageIndexItem.UpdateDatetime = DateTime.Now;
                        PageIndexItem.ModelID = model.ModelID;
                        PageIndexItem.UpdateUser = account;
                        PageIndexItem.ImageFileOrgName = model.ImageFileOrgName;
                        PageIndexItem.LinkUrl = model.LinkUrl == null ? "" : model.LinkUrl;
                        PageIndexItem.LinkUrlDesc = model.LinkUrlDesc == null ? "" : model.LinkUrlDesc;
                        PageIndexItem.UploadFileDesc = model.UploadFileDesc == null ? "" : model.UploadFileDesc;
                        PageIndexItem.UploadFileName = model.UploadFileName;
                        PageIndexItem.UploadFilePath = model.UploadFilePath == null ? "" : model.UploadFilePath;
                        PageIndexItem.ImageFileName = model.ImageFileName;
                        PageIndexItem.UpdateName = admin.User_Name;
                        PageIndexItem.IsVerift = !SET_BASE.M_Base02;

                        var ModelStatus = 2;
                        if (olddata.ItemID == 0 || olddata.CreateDatetime == null)
                        {
                            PageIndexItem.CreateDatetime = PageIndexItem.UpdateDatetime;
                            PageIndexItem.CreateName = PageIndexItem.UpdateName;
                            PageIndexItem.CreateUser = PageIndexItem.UpdateUser;
                            ModelStatus = 1;
                        }
                        if (model.ActiveID.IsNullOrEmpty() == false)
                        {
                            PageIndexItem.ActiveID = int.Parse(model.ActiveID);
                        }
                        if (model.ActiveItemID.IsNullOrEmpty() == false)
                        {
                            PageIndexItem.ActiveItemID = int.Parse(model.ActiveItemID);
                        }

                        var r = base.UpdateObject(PageIndexItem, tran);

                        sql = @"delete  PageImage where ItemID=@ItemID and ModelID=@ModelID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ItemID", model.ItemID));
                        base.Parameter.Add(new SqlParameter("@ModelID", model.ModelID));
                        base.ExeNonQuery(sql, tran);

                        if (model.PageImages != null)
                        {
                            model.PageImages.ForEach(t =>
                            {
                                base.InsertObject(t, tran);
                            });
                        }

                        sql = @"select * from VerifyData where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                        base.Parameter.Add(new SqlParameter("@ModelMainID", PageIndexItem.ModelID.Value));
                        base.Parameter.Add(new SqlParameter("@ModelItemID", PageIndexItem.ItemID));

                        List<VerifyData> hasvdata = base.SearchList<VerifyData>(sql, tran);

                        if (hasvdata.Count() == 0)
                        {
                            base.InsertObject(new VerifyData()
                            {
                                ModelID = _ModelID,
                                ModelItemID = PageIndexItem.ItemID,
                                ModelName = olddata.ItemName,
                                ModelMainID = PageIndexItem.ModelID.Value,
                                VerifyStatus = SET_BASE.M_Base02 ? 0 : 1,
                                ModelStatus = ModelStatus,
                                UpdateDateTime = DateTime.Now,
                                UpdateUser = admin.User_Name,
                                UpdateAccount = account,
                                LangID = olddata.Lang_ID.Value
                            }, tran);
                        }
                        else
                        {

                            sql = $@"update VerifyData set VerifyStatus={(SET_BASE.M_Base02 ? 0 : 1)},ModelStatus=@ModelStatus,ModelName =@ModelName,VerifyDateTime=Null,VerifyUser=''
                                                        ,VerifyName='',UpdateDateTime=GetDate(),UpdateUser=@UpdateUser,UpdateAccount=@UpdateAccount
                                          where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ModelID", _ModelID));
                            base.Parameter.Add(new SqlParameter("@ModelStatus", ModelStatus));
                            base.Parameter.Add(new SqlParameter("@ModelName", olddata.ItemName));
                            base.Parameter.Add(new SqlParameter("@UpdateUser", admin.User_Name));
                            base.Parameter.Add(new SqlParameter("@UpdateAccount", account));
                            base.Parameter.Add(new SqlParameter("@ModelMainID", PageIndexItem.ModelID.Value));
                            base.Parameter.Add(new SqlParameter("@ModelItemID", PageIndexItem.ItemID));
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
                                TypeName = "PageIndexItem",
                                TypeID = PageIndexItem.ItemID,
                                Lang_ID = olddata.Lang_ID
                            }, tran);
                        }



                        tran.Commit();
                        return "修改成功";

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "修改圖文編輯Item失敗,error:" + ex.ToString().NewLineReplace());
                        tran.Rollback();
                        return "修改失敗";
                    }
                }
            }
        }

        /// <summary>
        /// 取得Unit Setting Data
        /// </summary>
        /// <param name="modelid"></param>
        /// <returns></returns>
        public PageUnitSettingModel GetUnitModel(string modelid)
        {
            var model = new PageUnitSettingModel();
            model.MainID = int.Parse(modelid);
            var data = _commonService.GetGeneral<PageUnitSetting>("MainID=@MainID", new Dictionary<string, string>() { { "MainID", modelid } });
            var maindata = GetModelPageEditMainByID(int.Parse(modelid));
            if (maindata.ID == 0) { model.Title = maindata.Name; }
            if (data.ID != 0)
            {
                model.ID = data.ID;
                //model.MainID = data.MainID;
                model.IsForward = data.IsForward;
                model.IsPrint = data.IsPrint;
                model.IsShare = data.IsShare;
                model.MemberAuth = data.MemberAuth;
                model.EMailAuth = data.EMailAuth;
                model.VIPAuth = data.VIPAuth;
                model.EnterpriceStudentAuth = data.EnterpriceStudentAuth;
                model.GeneralStudentAuth = data.GeneralStudentAuth;
                model.Column1 = data.Column1;
                model.Column2 = data.Column2;
                model.Column3 = data.Column3;
                model.Column4 = data.Column4;
                model.Column5 = data.Column5;
            }
            model.ColumnNameMapping = new Dictionary<string, string>();
            model.ColumnNameMapping.Add("頁面", model.Column1.IsNullOrEmpty() ? "頁面" : model.Column1);
            model.ColumnNameMapping.Add("相關連結", model.Column2.IsNullOrEmpty() ? "相關連結" : model.Column2);
            model.ColumnNameMapping.Add("檔案下載", model.Column3.IsNullOrEmpty() ? "檔案下載" : model.Column3);
            model.ColumnNameMapping.Add("相關圖片", model.Column4.IsNullOrEmpty() ? "相關圖片" : model.Column4);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public string SetUnitModel(PageUnitSettingModel model, string account)
        {
            var newmodel = new PageUnitSetting();
            newmodel.UpdateDatetime = DateTime.Now;
            newmodel.UpdateUser = account;
            int r = 0;

            try
            {

                if (model.ID == -1 || model.ID == 0)
                {
                    newmodel.MainID = model.MainID.Value;
                    newmodel.IsForward = model.IsForward;
                    newmodel.IsPrint = model.IsPrint;
                    newmodel.IsShare = model.IsShare;
                    newmodel.MemberAuth = model.MemberAuth;
                    newmodel.VIPAuth = model.VIPAuth;
                    newmodel.EMailAuth = model.EMailAuth;
                    newmodel.EnterpriceStudentAuth = model.EnterpriceStudentAuth;
                    newmodel.GeneralStudentAuth = model.GeneralStudentAuth;
                    newmodel.Column1 = model.Column1 == null ? "" : model.Column1;
                    newmodel.Column2 = model.Column2 == null ? "" : model.Column2;
                    newmodel.Column3 = model.Column3 == null ? "" : model.Column3;
                    newmodel.Column4 = model.Column4 == null ? "" : model.Column4;
                    newmodel.Column5 = model.Column5 == null ? "" : model.Column5;
                    r = (int)base.InsertObject(newmodel);
                }
                else
                {
                    newmodel.ID = model.ID.Value;
                    newmodel.MainID = model.MainID.Value;
                    newmodel.IsForward = model.IsForward;
                    newmodel.IsPrint = model.IsPrint;
                    newmodel.IsShare = model.IsShare;
                    newmodel.MemberAuth = model.MemberAuth;
                    newmodel.VIPAuth = model.VIPAuth;
                    newmodel.EMailAuth = model.EMailAuth;
                    newmodel.EnterpriceStudentAuth = model.EnterpriceStudentAuth;
                    newmodel.GeneralStudentAuth = model.GeneralStudentAuth;
                    newmodel.Column1 = model.Column1 == null ? "" : model.Column1;
                    newmodel.Column2 = model.Column2 == null ? "" : model.Column2;
                    newmodel.Column3 = model.Column3 == null ? "" : model.Column3;
                    newmodel.Column4 = model.Column4 == null ? "" : model.Column4;
                    newmodel.Column5 = model.Column5 == null ? "" : model.Column5;
                    r = base.UpdateObject(newmodel) ? 1 : 0;
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
                logger.Error(ex, "修改圖文編輯-單元設定失敗,error:" + ex.ToString().NewLineReplace());

                return "修改失敗";
            }


        }
        public PageImage GetPageImage(string id)
        {
            return _commonService.GetGeneral<PageImage>("FID=@FID", new Dictionary<string, string>() { { "FID", id } });
        }

    }
}
