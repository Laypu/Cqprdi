using Oaww.Entity;
using Oaww.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oaww.Business
{
   public class LogService : BaseBusiness.BaseBusiness
    {
        /// <summary>
        /// 交易類別
        /// </summary>
        /// <returns></returns>
        public List<string> GetCategory()
        {
            string sql = @"select distinct t.Name from sys_Operation_Log_Category t";

            base.Parameter.Clear();

            return base.SearchList<string>(sql);
        }
        public List<GroupUser> GetGroup()
        {
            string sql = @"select distinct t.Group_Name,t.ID from GroupUser t";

            base.Parameter.Clear();

            return base.SearchList<GroupUser>(sql);
        }
        public Paging<LogData> Paging(LogSearchModel model)
        {
            Paging<LogData> result = new Paging<LogData>();

            string sql = @"select 
                            t.LM_Time,
                            t.Operation,
                            s.Group_ID + '-' + t.Operator + '-' + s.User_Name as MUser,                           
                            b.Name,
                            t.OperationModelName as ModelName,
                            t.OperationItemName as ItemName,
                            t.Before,
                            t.After
                             from sys_Operation_Log t
                            left join Users s on t.Operator = s.Account
                            left join GroupUser a on s.Group_ID = a.ID
                            left join sys_Operation_Log_Category b on t.TrxType = b.Category
                            where 1=1";



            base.Parameter.Clear();
            if (!string.IsNullOrEmpty(model.Operation))
            {

                if (model.Operation == "1")
                {
                    sql += " and t.Operation in ('Login','Logout','Release')";
                }
                else if (model.Operation == "2")
                {
                    sql += " and t.Operation in ('Insert','Delete','Update')";
                }
                else
                {
                    sql += " and t.Operation=@Operation";
                    base.Parameter.Add(new SqlParameter("@Operation", model.Operation));
                }
            }

            if (!string.IsNullOrEmpty(model.sdate))
            {
                sql += " and convert(date,t.LM_Time) >= @sdate";
                base.Parameter.Add(new SqlParameter("@sdate", model.sdate));
            }

            if (!string.IsNullOrEmpty(model.edate))
            {
                sql += " and convert(date,t.LM_Time) <= @edate";
                base.Parameter.Add(new SqlParameter("@edate", model.edate));
            }

            if (!string.IsNullOrEmpty(model.GroupID))
            {
                sql += "  and   a.ID= @GroupID";
                base.Parameter.Add(new SqlParameter("@GroupID", model.GroupID));
            }


            if (!string.IsNullOrEmpty(model.UserID))
            {
                sql += " and s.Account like @UserID";
                base.Parameter.Add(new SqlParameter("@UserID", "%" + model.UserID + "%"));
            }

            if (!string.IsNullOrEmpty(model.UserName))
            {
                sql += " and s.User_Name like @UserName";
                base.Parameter.Add(new SqlParameter("@UserName", "%" + model.UserName + "%"));
            }

            if (!string.IsNullOrEmpty(model.Trx))
            {
                sql += " and b.Name = @Trx";
                base.Parameter.Add(new SqlParameter("@Trx", model.Trx));
            }

            if (!string.IsNullOrEmpty(model.KeyWord))
            {
                sql += " and (t.Before like @KeyWord or t.After like @KeyWord )";
                base.Parameter.Add(new SqlParameter("@KeyWord", "%" + model.KeyWord + "%"));
            }

            result.total = base.SearchCount(sql);
            var LoD=base.SearchListPage<LogData>(sql, model.Offset, model.Limit, " order by LM_Time desc ");
            //result.rows = base.SearchListPage<LogData>(sql, model.Offset, model.Limit, " order by LM_Time desc ");
            //foreach(var item in LoD)
            //{
            //    if(item.After!="" && item.Before != "")
            //    {
            //        var befor = item.Before.Split(',');
            //        var After = item.After.Split(',');
            //        for(var i=0;i< befor.Length;i++)
            //        {
            //           var brforName = befor[i].Split(':')[0];
            //            //var AfterName = After[i].Split(':')[0];
            //            if(After.Any(o=>o.Contains(brforName)))
            //            {
            //                After.Any(o => o.Contains(brforName))
            //            }
            //        }
            //    }
            //}
            result.rows = LoD;
            return result;
        }
    }
}
