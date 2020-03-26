using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.ViewModel;
using System.Data.SqlClient;
using Oaww.Utility;

namespace Oaww.Business
{
    public class ADService : Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();


        public ADMain GetADMain(string id)
        {
            return _commonService.GetGeneral<ADMain>("ID=@ID", new Dictionary<string, string>() { { "ID", id } });
        }
        public ADKanban GetADKanban(string id)
        {
            return _commonService.GetGeneral<ADKanban>("ID=@ID", new Dictionary<string, string>() { { "ID", id } });
        }

        /// <summary>
        /// 取得Edit ViewModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ADEditModel GetModel(string id)
        {
            //logger.Info("Test");

            var model = new ADEditModel();

            string sql = @"select * from ADMain where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", id));
            model = base.GetObject<ADEditModel>(sql);

            if (model.StDate != null)
            {
                model.StDateStr = model.StDate.Value.ToString("yyyy/MM/dd");
            }
            if (model.EdDate != null)
            {
                model.EdDateStr = model.EdDate.Value.ToString("yyyy/MM/dd");
            }

            return model;
        }

        public ADEditModel GetKanbanModel(string id, string lang)
        {
            //logger.Info("Test");

            var model = new ADEditModel();

            string sql = @"select * from ADKanban where ID=@ID and Lang_ID=@Lang_ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", id));
            base.Parameter.Add(new SqlParameter("@Lang_ID", lang));
            model = base.GetObject<ADEditModel>(sql);



            return model;
        }

        /// <summary>
        /// pageing data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Paging<ADListResult> Paging(ADSearchModel model)
        {
            var Paging = new Paging<ADListResult>();


            string sql = @"select t.*
                            from ADMain t
                             left join VerifyData s on t.ID = s.ModelItemID and s.ModelID in ('33','34','35','36') and s.ModelMainID = 1
                             where Lang_ID=@Lang_ID and isnull(Del,0) = 0  ";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", model.Lang_ID));
            if (!model.SType.IsNullOrEmpty())
            {
                sql += " and t.SType=@SType";
                base.Parameter.Add(new SqlParameter("@SType", model.SType));
            }

            if (!model.ADType.IsNullOrEmpty())
            {
                sql += " and t.Type_ID=@ADType";
                base.Parameter.Add(new SqlParameter("@ADType", model.ADType));
            }

            Paging.total = base.SearchCount(sql);
            Paging.rows = base.SearchListPage<ADListResult>(sql, model.Offset, model.Limit, " order by " + model.Sort);

            Paging.rows.ForEach(t =>
            {
                if (t.StDate == null & t.EdDate == null) { t.IsRange = "是"; }
                else if (t.StDate != null && t.EdDate == null && DateTime.Now.Date >= t.StDate.Value)
                {
                    t.IsRange = "是";
                }
                else if (t.StDate == null && t.EdDate != null && DateTime.Now.Date <= t.EdDate.Value)
                {
                    t.IsRange = "是";
                }
                else if (t.StDate != null && t.EdDate != null && DateTime.Now.Date <= t.EdDate.Value)
                {
                    if (DateTime.Now >= t.StDate.Value && DateTime.Now <= t.EdDate.Value.AddDays(1))
                    {
                        t.IsRange = "是";
                    }
                }
                else
                {
                    t.IsRange = "否";
                }
            });

            return Paging;
        }

        /// <summary>
        /// 更新顯示順序
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seq"></param>
        /// <param name="type"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string UpdateSeq(int id, int seq, string type, string account, string username)
        {
            try
            {
                string sql = string.Empty;
                var r = 0;

                var oldmodel = new List<ADMain>();
                oldmodel = _commonService.GetGeneralList<ADMain>("ID=@ID", new Dictionary<string, string>() { { "ID", id.ToString() } }).ToList();
                var stype = oldmodel.First().SType;
                var langid = oldmodel.First().Lang_ID;

                if (seq == 0 || seq == 1)
                {
                    //reset
                    sql = "update ADMain set Sort=@Sort where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Sort", -1));
                    base.Parameter.Add(new SqlParameter("@ID", oldmodel.First().ID));
                    r = base.ExeNonQuery(sql);

                    //update sort order
                    sql = @"with cte as
                            (
                            select 
                                ROW_NUMBER()OVER(order by sort) as ROW,t.ID
                                from ADMain t where t.Lang_ID=@Lang and t.Type_ID=@TypeID
                            )
                            update s set s.Sort = t.ROW from cte  t
                            left join ADMain s on t.ID = s.ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Lang", langid.ToString())); ;
                    base.Parameter.Add(new SqlParameter("@TypeID", oldmodel.First().Type_ID));
                    base.ExeNonQuery(sql);
                }
                else
                {
                    if (seq <= 0) { seq = 1; }

                    if (oldmodel.Count() == 0) { return "更新失敗"; }


                    if (oldmodel.First().Sort != seq)
                    {
                        IEnumerable<ADMain> mtseqdata = null;
                        mtseqdata = _commonService.GetGeneralList<ADMain>("Sort>@Sort and Lang_ID=@Lang_ID and  Type_ID=@TypeID "
                                                                  , new Dictionary<string, string>() { {"Sort",seq.ToString() },
                                                                                                 { "Lang_ID", langid.ToString() },
                                                                                                 { "TypeID",oldmodel.First().Type_ID} }).OrderBy(v => v.Sort);

                        var updatetime = DateTime.Now;
                        if (mtseqdata.Count() == 0)
                        {
                            var totalcnt = 0;
                            sql = "select * from ADMain where  Type_ID=@TypeID and Lang_ID=@Lang_ID";
                            base.Parameter.Clear();

                            base.Parameter.Add(new SqlParameter("@TypeID", oldmodel.First().Type_ID));
                            base.Parameter.Add(new SqlParameter("@Lang_ID", langid.ToString()));
                            totalcnt = base.SearchCount(sql);
                            seq = totalcnt;
                        }

                        var qseq = (seq > oldmodel.First().Sort) ? seq : oldmodel.First().Sort;
                        List<ADMain> ltseqdata = null;
                        ltseqdata = _commonService.GetGeneralList<ADMain>("Sort<=@Sort and Lang_ID=@Lang_ID and Type_ID=@TypeID"
                                                                  , new Dictionary<string, string>() { { "Sort", qseq.ToString() },
                                                                                                   { "Lang_ID", langid.ToString() },
                                                                                                   { "TypeID",oldmodel.First().Type_ID} }).OrderBy(v => v.Sort)
                                                  .ToList();
                        //更新seq+1
                        var sidx = 0;
                        for (var idx = 1; idx <= ltseqdata.Count(); idx++)
                        {
                            if (idx == seq && seq < oldmodel.First().Sort)
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
                            tempmodel.UpdateDatetime = updatetime;
                            tempmodel.UpdateUser = account;
                            sql = "update ADMain set Sort=@Sort where ID=@ID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@Sort", tempmodel.Sort));
                            base.Parameter.Add(new SqlParameter("@ID", tempmodel.ID));
                            base.ExeNonQuery(sql);
                        }
                    }
                    //先取出大於目前seq的資料

                    oldmodel.First().Sort = seq;

                    sql = "update ADMain set Sort=@Sort where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Sort", oldmodel.First().Sort));
                    base.Parameter.Add(new SqlParameter("@ID", oldmodel.First().ID));
                    r = base.ExeNonQuery(sql);
                }


                if (r > 0)
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
                logger.Error(ex, $"更新群組排序失敗：error:" + ex.ToString().NewLineReplace());
                return "更新群組排序失敗:" + " error:" + ex.Message;
            }
        }

        public string Delete(string[] idlist, SqlTransaction tran)
        {
            try
            {

                string sql = string.Empty;
                string typeID = string.Empty;
                var r = 0;
                var stype = "";
                for (var idx = 0; idx < idlist.Length; idx++)
                {
                    var olddate = _commonService.GetGeneral<ADMain>("ID=@ID", new Dictionary<string, string>() { { "ID", idlist[idx] } });
                    stype = olddate.SType;
                    var entity = new ADMain();
                    entity.ID = int.Parse(idlist[idx]);

                    sql = @"delete ADMain where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@ID", idlist[idx]));
                    r = base.ExeNonQuery(sql, tran);

                    typeID = olddate.Type_ID;
                }
                var rstr = "";
                if (r >= 0)
                {

                    rstr = "刪除成功";
                }
                else
                {
                    //NLogManagement.SystemLogInfo("刪除輪播廣告失敗:" + delaccount);
                    rstr = "刪除失敗";
                }

                //update sort order
                sql = @"with cte as
                        (
                        select 
                            ROW_NUMBER()OVER(order by sort) as ROW,t.ID
                            from ADMain t where t.Lang_ID=@langid and t.Type_ID=@Type_ID
                        )
                        update s set s.Sort = t.ROW from cte  t
                        left join ADMain s on t.ID = s.ID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@langid", 1));
                base.Parameter.Add(new SqlParameter("@Type_ID", typeID));
                base.ExeNonQuery(sql, tran);

                return rstr;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "刪除廣告失敗: " + ex.ToString().NewLineReplace());
                return "刪除失敗";
            }
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
                string typeID = string.Empty;
                var r = 0;
                var stype = "";
                for (var idx = 0; idx < idlist.Length; idx++)
                {
                    var olddate = _commonService.GetGeneral<ADMain>("ID=@ID", new Dictionary<string, string>() { { "ID", idlist[idx] } });
                    stype = olddate.SType;
                    var entity = new ADMain();
                    entity.ID = int.Parse(idlist[idx]);

                    sql = @"delete ADMain where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@ID", idlist[idx]));
                    r = base.ExeNonQuery(sql);

                    typeID = olddate.Type_ID;
                }
                var rstr = "";
                if (r >= 0)
                {

                    rstr = "刪除成功";
                }
                else
                {
                    //NLogManagement.SystemLogInfo("刪除輪播廣告失敗:" + delaccount);
                    rstr = "刪除失敗";
                }

                //update sort order
                sql = @"with cte as
                        (
                        select 
                            ROW_NUMBER()OVER(order by sort) as ROW,t.ID
                            from ADMain t where t.Lang_ID=@langid and t.Type_ID=@Type_ID
                        )
                        update s set s.Sort = t.ROW from cte  t
                        left join ADMain s on t.ID = s.ID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@langid", langid));
                base.Parameter.Add(new SqlParameter("@Type_ID", typeID));
                base.ExeNonQuery(sql);

                return rstr;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "刪除廣告失敗: " + ex.ToString().NewLineReplace());
                return "刪除失敗";
            }
        }



        /// <summary>
        /// 變更狀態
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string UpdateStatus(string id, bool status, string account, string username)
        {
            try
            {
                string sql = @"update ADMain set Enabled=@Enabled,UpdateDatetime=GetDate(),UpdateUser=@UpdateUser where ID=@ID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@Enabled", status));
                base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                base.Parameter.Add(new SqlParameter("@ID", id));
                var r = base.ExeNonQuery(sql);

                if (r >= 0)
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
                logger.Error(ex, "變更狀態失敗:" + ex.ToString().NewLineReplace());
                return "更新失敗";
            }
        }

        /// <summary>
        /// 新增 Entity
        /// </summary>
        /// <param name="model"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string Create(ADEditModel model, string account, string username)
        {



            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        //1.create message
                        var datetime = DateTime.Now;

                        string sql = string.Empty;

                        _commonService.UpdateSeqAdd<ADMain>(model.Lang_ID, "P", tran, model.Type);


                        var ad = new ADMain()
                        {
                            Link_Href = model.Link_Href,
                            Lang_ID = model.Lang_ID,
                            AD_Height = model.AD_Height,
                            AD_Name = model.AD_Name,
                            AD_Width = model.AD_Width,
                            Link_Mode = model.Link_Mode,
                            Create_Date = datetime,
                            StDate = model.StDate,
                            EdDate = model.EdDate,
                            Enabled = true,
                            Img_Name_Ori = model.Img_Name_Ori,
                            Sort = 1,
                            Type_ID = model.Type,
                            UpdateDatetime = datetime,
                            UpdateUser = account,
                            Img_Name_Thumb = model.Img_Name_Thumb,
                            Img_Show_Name = model.Img_Show_Name,
                            SType = model.SType,
                            UploadVideoFileName = model.UploadVideoFileName,
                            UploadVideoFilePath = model.UploadVideoFilePath,
                            UploadVideoFileDesc = model.UploadVideoFileDesc,
                            IsVerift = false,
                            ADDesc = model.ADDesc,
                            Icon = model.Icon,
                            Color = model.Color
                        };

                        var r = base.InsertObject(ad, tran);



                        if (r > 0)
                        {
                            tran.Commit();
                            return "新增成功";
                        }
                        else
                        {
                            tran.Rollback();
                            return "新增失敗";
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();

                        logger.Error(ex, "新增Entity失敗:" + ex.ToString().NewLineReplace());
                        return "更新失敗";
                    }
                }
            }
        }

        public string CreateKanba(ADEditModel model, string account, string username)
        {
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        //1.create message
                        var datetime = DateTime.Now;

                        string sql = string.Empty;



                        var ad = new ADKanban()
                        {
                            ID = int.Parse(model.Type),
                            Link_Href = model.Link_Href,
                            Lang_ID = model.Lang_ID,
                            AD_Name = model.AD_Name,
                            Link_Mode = model.Link_Mode,
                            Create_Date = datetime,
                            Img_Name_Ori = model.Img_Name_Ori,
                            Img_Show_Name = model.Img_Show_Name,
                            Type_ID = model.Type,
                            UpdateDatetime = datetime,
                            UpdateUser = account,
                            Img_Name_Thumb = model.Img_Name_Thumb,
                            ADDesc = model.ADDesc,
                            UploadVideoFileDesc = model.UploadVideoFileDesc,
                            UploadVideoFileName = model.UploadVideoFileName,
                            UploadVideoFilePath = model.UploadVideoFilePath,
                            VideoLink = model.VideoLink
                        };

                        var r = base.InsertObject(ad, tran);




                        tran.Commit();
                        return "新增成功";

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();

                        logger.Error(ex, "新增Entity失敗:" + ex.ToString().NewLineReplace());
                        return "更新失敗";
                    }
                }
            }
        }
        public string Update(ADMain model, string account, string username)
        {

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {

                    try
                    {
                        var r = base.UpdateObject(model, tran);

                        if (r)
                        {

                            tran.Commit();

                            return "修改成功";
                        }
                        else
                        {
                            tran.Rollback();
                            return "修改失敗";
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "修改Entity失敗:" + ex.ToString().NewLineReplace());
                        tran.Rollback();
                        return "修改失敗";
                    }
                }
            }
        }

        public string UpdateKanban(ADKanban model, string account, string username)
        {

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {

                    try
                    {
                        var r = base.UpdateObject(model, tran);

                        if (r)
                        {

                            tran.Commit();

                            return "修改成功";
                        }
                        else
                        {
                            tran.Rollback();
                            return "修改失敗";
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "修改Entity失敗:" + ex.ToString().NewLineReplace());
                        tran.Rollback();
                        return "修改失敗";
                    }
                }
            }
        }
    }
}
