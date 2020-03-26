using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.ViewModel;
using Oaww.Entity;
using Oaww.Entity.SET;
using System.Data.SqlClient;
using Oaww.Utility;

namespace Oaww.Business
{
    public class ExclusiveLayoutService:Oaww.BaseBusiness.BaseBusiness
    {
        public Paging<ExclusiveLayout> Paging(SearchModelBase model)
        {
            var Paging = new Paging<ExclusiveLayout>();
            var onepagecnt = model.Limit;

            string sql = @"select 
                            t.ID,
                            s.GROUP_NAME,
                            a.User_Name as ManagerName,
                            t.Name,
                            t.URL,
                            t.Enabled
                           from ExclusiveLayout t
                           left join SET_LAYOUT_GROUP s on t.GroupID = s.ID
                           left join Users a on t.Manager = a.ID
                           where 1=1 ";
            base.Parameter.Clear();
            
            if(model.Search.IsNullOrEmpty() == false)
            {
                sql += " and Name like @Name";
                base.Parameter.Add(new SqlParameter("@Name", "%" + model.Search + "%"));
            }

            if (model.Key.IsNullOrEmpty() == false)
            {
                sql += " and a.User_Name like @Manager";
                base.Parameter.Add(new SqlParameter("@Manager", "%" + model.Key + "%"));
            }

            Paging.rows = base.SearchList<ExclusiveLayout>(sql, model.Offset, model.Limit, " order by " + model.Sort);
            Paging.total = base.SearchCount(sql);
            return Paging;
        }

        public List<Users> GetUsers()
        {
            string sql = "select GROUP_ID from SET_LAYOUT ";

            string GroupID = base.ExecuteScalar(sql,"").ToString();

            sql = "select * from Users where  1=1";

            base.Parameter.Clear();

            if(GroupID.IsNullOrEmpty() == false)
            {
                sql += " and Group_ID=@Group_ID";
                base.Parameter.Add(new SqlParameter("@Group_ID", GroupID));
            }

            return base.SearchList<Users>(sql).ToList();
        }

        public string SaveItem(ExclusiveLayout model)
        {
            if(model.ID <= 0)
            {
                try
                {
                    base.InsertObject(model);
                    return "新增成功";
                }
                catch(Exception ex)
                {
                    logger.Debug(ex, "專屬版面新增失敗，error:"+ex.ToString());

                    return "新增失敗";
                }
            }
            else
            {
                try
                {
                    base.UpdateObject(model);
                    return "修改成功";
                }
                catch (Exception ex)
                {
                    logger.Debug(ex, "專屬版面修改失敗，error:" + ex.ToString());

                    return "修改失敗";
                }
            }
        }

        public string SaveItemN(ExclusiveLayout model)
        {
            try
            {
                base.UpdateObject(model);
                return "修改成功";
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "專屬版面修改失敗，error:" + ex.ToString());

                return "修改失敗";
            }
        }
        public List<AdminFunction> GetSelfLayout(string account,string Lang)
        {
            string sql = @"select 
                            33+s.ID as ID,
                            3 as GroupID,
                            s.Name as ItemName,
                            'Menu/MainMenu' as Url,
                            'Menu' as Controller,
                            'MainMenu' as Action,
                            'menutype='+convert(varchar,s.ID+3) as Parameter,
                            -1 as ParentGroupID,
                            null as Area
                            from Users t
                            left join ExclusiveLayout s on t.ID = s.Manager
                            where t.Account =@account and Lang_ID=@Lang_ID and s.Enabled=1";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@account", account));
            base.Parameter.Add(new SqlParameter("@Lang_ID", Lang));

            return base.SearchList<AdminFunction>(sql);
        }

        public string GetLayoutName(int ID)
        {
            string sql = "select t.Name from ExclusiveLayout t where t.ID = @ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));

            return base.ExecuteScalar(sql).ToString();
        }

        public List<SET_MENU> GetLayoutModule()
        {
            string sql = @"select * from SET_MENU t where
                            t.M_Menu01 in (select items from dbo.Split((select top 1 t.MODULE_LIST from SET_LAYOUT t),','))";

            return base.SearchList<SET_MENU>(sql);
        }

        public string GetLayoutMenuType(string account)
        {
            string sql = @"select 
                            s.ID+3
                                from Users t
                                left join ExclusiveLayout s on t.ID = s.Manager
                                where t.Account =@account";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@account", account));

            return base.ExecuteScalar(sql, "").ToString();
        }

        public ExclusiveLayout GetLayoutNameByURL(string url)
        {
            string sql = @"select top 1 * from ExclusiveLayout t  where t.URL =@Url";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Url", url));
            return base.GetObject<ExclusiveLayout>(sql);
        }

        public ExclusiveLayout GetLayoutNameByID(int ID)
        {
            string sql = @"select top 1 * from ExclusiveLayout t  where t.ID =@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID-33));
            return base.GetObject<ExclusiveLayout>(sql);
        }

        public ExclusiveLayout GetLayoutNameByAccount(string account)
        {
            string sql = @"select * from ExclusiveLayout t where t.Manager = 
                            (
	                            select t.ID from Users t
	                            where t.Account=@account
                            )";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@account", account));
            return base.GetObject<ExclusiveLayout>(sql);
        }
        public List<Menu> GetLayoutByURL(string url)
        {
            string sql = @"select t.* from Menu t
                            where t.MenuType =
                            (select top 1 t.ID+3 from ExclusiveLayout t  where t.URL =@url)
                            and t.Status =1";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@url", url));
            return base.SearchList<Menu>(sql);
        }
    }
}
