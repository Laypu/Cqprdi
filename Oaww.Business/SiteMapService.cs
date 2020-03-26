using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.Entity.SiteMap;
using Oaww.ViewModel;
using Oaww.ViewModel.SiteMap;
using System.Data.SqlClient;
using Oaww.Utility;

namespace Oaww.Business
{
   public  class SiteMapService:Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();
        public List<SiteMapKey> GetListSiteMapKeyByModelID(string ModelID)
        {
            return _commonService.GetGeneralList<SiteMapKey>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", ModelID } }).ToList();
        }

        public SiteMapItem GetSiteMapItemByModelID(string ModelID)
        {
            return _commonService.GetGeneral<SiteMapItem>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", ModelID } });
        }

        public ModelSiteMapMain GetModelSiteMapMain(string ID)
        {
            string sql = @"select * from ModelSiteMapMain where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));

            return base.GetObject<ModelSiteMapMain>(sql);
        }

        public Paging<ModelSiteMapMain> Paging(SearchModelBase model)
        {
            string sql = string.Empty;

            var Paging = new Paging<ModelSiteMapMain>();
            var onepagecnt = model.Limit;

            sql = $"select * from ModelSiteMapMain where Lang_ID=@Lang_ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", model.LangId));
            Paging.rows = base.SearchListPage<ModelSiteMapMain>(sql, model.Offset, model.Limit, " order by " + model.Sort);
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

            sql = "select * from Menu where ModelID=8 and ModelItemID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));

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
                        sql = @"delete ModelSiteMapMain where ID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                      

                        //刪除SEO
                        sql = @"delete SEO where TypeName='SiteMapItem' and TypeID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);
                        // _seosqlrepository.DelDataUseWhere("TypeName='PageIndexItem' and TypeID=@1", new object[] { items.ItemID });

                        for (var idx = 0; idx < idlist.Length; idx++)
                        {

                            sql = @"delete SiteMapItem where ModelID=@ModelID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ModelID", idlist[idx]));
                            base.ExeNonQuery(sql, tran);


                            //刪除SEO
                            sql = @"delete SEO where TypeName='SiteMapItem' and TypeID=@TypeID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@TypeID", idlist[idx]));
                            base.ExeNonQuery(sql, tran);


                            sql = @"delete SiteMapKey where ModelID=@ModelID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ModelID", idlist[idx]));
                            base.ExeNonQuery(sql, tran);
                        }

                        //update sort order
                        sql = @"with cte as
                                    (
                                    select 
                                     ROW_NUMBER()OVER(order by sort) as ROW,t.ID
                                     from ModelAMLMain t where t.Lang_ID=1
                                    )
                                    update s set s.Sort = t.ROW from cte  t
                                    left join ModelAMLMain s on t.ID = s.ID";
                        base.Parameter.Clear();
                        base.ExeNonQuery(sql, tran);

                        tran.Commit();
                        rstr = "刪除成功";

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "刪除網站導覽模組異常,error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        rstr = "刪除失敗";
                    }

                    return rstr;
                }
            }
        }

        public SiteMapEditModel GetModelByID(string ModelID)
        {
            SiteMapEditModel model = new SiteMapEditModel();

            model.SiteMapItem = _commonService.GetGeneral<SiteMapItem>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", ModelID } });

            model.siteMapKeys = _commonService.GetGeneralList<SiteMapKey>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", ModelID } })
                                                         .ToList();

            ModelSiteMapMain modelSiteMapMain = _commonService.GetGeneral<ModelSiteMapMain>("ID=@ID", new Dictionary<string, string>() { { "ID", ModelID } });

            if (model.SiteMapItem.ItemID > 0)
            {
 
            }
            else
            {
                model.SiteMapItem = new SiteMapItem()
                {
                    ModelID = int.Parse(ModelID),
                    ModelName = modelSiteMapMain.Name,
                    CreateDatetime = DateTime.Now,
                };
                model.siteMapKeys = new List<SiteMapKey>();
               
            }
            return model;
        }

        /// <summary>
        /// 新增Item
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LangId"></param>
        /// <param name="account"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CreateItem(SiteMapEditModel model, string LangId, string account, string userName)
        {

            string sql = string.Empty;

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        model.SiteMapItem.Sort = 1;
                        model.SiteMapItem.Lang_ID = int.Parse(LangId);
                        model.SiteMapItem.CreateDatetime = DateTime.Now;
                        model.SiteMapItem.CreateUser = account;
                        model.SiteMapItem.Enabled = true;
                        model.SiteMapItem.CreateName = userName;
                       


                        var r = base.InsertObject(model.SiteMapItem, tran);

                        if (model.siteMapKeys != null)
                        {
                            model.siteMapKeys.ForEach(t =>
                            {
                                base.InsertObject(t, tran);
                            });
                        }



                        


                        tran.Commit();
                        return "新增成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "新增網站導覽Item異常,error:" + ex.ToString().NewLineReplace());

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
        public string UpdateItem(SiteMapEditModel model, string LangId, string account, string userName)
        {
            string sql = string.Empty;

            SiteMapItem oldData = _commonService.GetGeneral<SiteMapItem>("ModelID=@ModelID"
                                                                                    , new Dictionary<string, string>() { { "ModelID", model.SiteMapItem.ModelID.ToString() } });

            //舊的資料
            List<SiteMapKey> siteMapKeys = _commonService.GetGeneralList<SiteMapKey>("ModelID=@ModelID"
                                                                                                        , new Dictionary<string, string>() { { "ModelID", model.SiteMapItem.ModelID.Value.ToString() } })
                                                                                                        .ToList();





            oldData.UpdateDatetime = DateTime.Now;
            oldData.UpdateUser = account;
            oldData.UpdateName = userName;
            oldData.HtmlContent = model.SiteMapItem.HtmlContent;
            

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        base.UpdateObject(oldData, tran);

                        sql = @"delete  SiteMapKey where ModelID=@ModelID";
                        base.Parameter.Clear();

                        base.Parameter.Add(new SqlParameter("@ModelID", model.SiteMapItem.ModelID));
                        base.ExeNonQuery(sql, tran);

                        if (model.siteMapKeys != null)
                        {
                            model.siteMapKeys.ForEach(t =>
                            {
                                base.InsertObject(t, tran);
                            });
                        }




                        tran.Commit();
                        return "修改成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "更新網站導覽Item異常,error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        return "修改失敗";
                    }
                }
            }


        }
    }
}
