using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.ViewModel;
using Oaww.Entity;
using System.Data.SqlClient;
using Oaww.Utility;

namespace Oaww.Business
{
    public class DictionaryService : Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();

        public Paging<Dictionary> Paging(SearchModelBase model)
        {
            //todo
            string sql = string.Empty;

            var Paging = new Paging<Dictionary>();
            var onepagecnt = model.Limit;

            if (model.Search.IsNullOrEmpty())
            {
                return Paging;
            }

            sql = @"select t.*  from Dictionary  t                    
                    where t.Category = @Category";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Category", model.Search));

            if (model.Key.IsNullOrEmpty() == false)
            {
                sql += " and Group_Name like @Group_Name";
                base.Parameter.Add(new SqlParameter("@Group_Name", "%" + model.Key + "%"));
            }

            Paging.rows = base.SearchListPage<Dictionary>(sql, model.Offset, model.Limit, " order by " + model.Sort);
            Paging.total = base.SearchCount(sql);

            return Paging;
        }

        public string Delete(string[] idlist, string Category)
        {
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    string sql = string.Empty;
                    string rstr = string.Empty;

                    try
                    {
                        sql = "delete Dictionary where ID in (select * from dbo.Split(@IDs,',')) ";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@IDs", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //update sort order
                        sql = @"with cte as
                                    (
                                    select 
                                     ROW_NUMBER()OVER(order by sort) as ROW,t.ID
                                     from Dictionary t where Category =@Category
                                    )
                                    update s set s.Sort = t.ROW from cte  t
                                    left join Dictionary s on t.ID = s.ID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Category", Category));
                        base.ExeNonQuery(sql, tran);

                        rstr = "刪除成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "刪除辭庫異常,error:" + ex.ToString());

                        tran.Rollback();
                        rstr = "刪除失敗";
                    }

                    return rstr;
                }
            }
        }

        public string UpdateSeq(int id, int seq, string Category)
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                var oldmodel = _commonService.GetHisEntity<Dictionary>("ID", id.ToString());
                if (oldmodel.ID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<Dictionary> mtseqdata = null;

                    mtseqdata = _commonService.GetGeneralList<Dictionary>("Sort>@Sort and Category=@Category"
                                                                                  , new Dictionary<string, string>() { { "Sort", seq.ToString() }, { "Category", Category } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        sql = @"select * from Dictionary where Category=@Category";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Category", Category));

                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<Dictionary> ltseqdata = null;
                    ltseqdata = _commonService.GetGeneralList<Dictionary>("Sort<=@Sort and Category=@Category"
                                                                                  , new Dictionary<string, string>() { { "Sort", qseq.ToString() }, { "Category", Category } })
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
                logger.Error(ex, "更新辭庫排序失敗,error:" + ex.ToString());
                return "更新辭庫排序失敗:" + " error:" + ex.Message;
            }
        }

        public string SaveItem(Dictionary model, string account)
        {
            string sql = string.Empty;

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        if (model.IsInsert == "A")
                        {
                            model.Readonly = false;
                            model.Enabled = true;
                            model.Sort = 1;
                            model.UpdateUser = account;
                            model.UpdateDatetime = DateTime.Now;

                            sql = "update Dictionary set Sort = Sort +1 where Category =@Category";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@Category", model.Category));
                            base.ExeNonQuery(sql, tran);

                            base.InsertObject(model, tran);
                        }
                        else
                        {

                            sql = "update Dictionary set Group_Name=@Group_Name where ID=@ID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ID", model.ID));
                            base.Parameter.Add(new SqlParameter("@Group_Name", model.Group_Name));


                            base.ExeNonQuery(sql, tran);
                        }

                        tran.Commit();

                        return "存檔成功";
                    }
                    catch (Exception ex)
                    {

                        logger.Debug(ex, "辭庫存檔異常，Error:" + ex.ToString());

                        tran.Rollback();

                        return "存檔失敗";
                    }
                }
            }

        }
    }
}
