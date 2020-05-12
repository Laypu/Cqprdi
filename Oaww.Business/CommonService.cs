using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Oaww.Entity;
using Oaww.Utility;
using System.Web.Mvc;
using Oaww.ViewModel;
using System.IO;


namespace Oaww.Business
{
    public class CommonService : BaseBusiness.BaseBusiness
    {
        #region 泛型
        public T GetHisEntity<T>(string column, string value) where T : class, new()
        {
            string sql = "select top 1 * from " + typeof(T).Name + $" where   {column}=@column ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@column", value));

            return base.GetObject<T>(sql) == null ? new T() : base.GetObject<T>(sql);
        }
        public T GetHisEntity<T>(string column, string value, string language) where T : class, new()
        {
            string sql = "select top 1 * from " + typeof(T).Name + $" where   {column}=@column and Lang_ID=@Lang_ID ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@column", value));
            base.Parameter.Add(new SqlParameter("@Lang_ID", language));

            return base.GetObject<T>(sql) == null ? new T() : base.GetObject<T>(sql);
        }
        public IList<T> GetGeneralList<T>() where T : class
        {
            string sql = "select * from " + typeof(T).Name;

            return base.SearchList<T>(sql) == null ? new List<T>() : base.SearchList<T>(sql);
        }

        public IList<T> GetGeneralList<T>(SqlTransaction tran) where T : class
        {
            string sql = "select * from " + typeof(T).Name;

            return base.SearchList<T>(sql, tran) == null ? new List<T>() : base.SearchList<T>(sql, tran);
        }

        public IList<T> GetGeneralList<T>(string constrain) where T : class
        {
            string sql = "select * from " + typeof(T).Name + " where " + constrain;

            return base.SearchList<T>(sql) == null ? new List<T>() : base.SearchList<T>(sql);
        }

        public IList<T> GetGeneralListByID<T>(string[] idlist) where T : class
        {
            string sql = "select * from " + typeof(T).Name + " where ID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));

            return base.SearchList<T>(sql) == null ? new List<T>() : base.SearchList<T>(sql);
        }

        public IList<T> GetGeneralListByItemID<T>(string[] idlist) where T : class
        {
            string sql = "select * from " + typeof(T).Name + " where ItemID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));

            return base.SearchList<T>(sql) == null ? new List<T>() : base.SearchList<T>(sql);
        }

        public IList<T> GetGeneralList<T>(string constrain, Dictionary<string, string> ListParas) where T : class
        {
            string sql = "select * from " + typeof(T).Name + " where " + constrain;

            base.Parameter.Clear();

            foreach (KeyValuePair<string, string> kvp in ListParas)
            {
                base.Parameter.Add(new SqlParameter("@" + kvp.Key, kvp.Value));
            }

            return base.SearchList<T>(sql) == null ? new List<T>() : base.SearchList<T>(sql);
        }

        public IList<T> GetGeneralList<T>(string constrain, Dictionary<string, string> ListParas, SqlTransaction tran) where T : class
        {
            string sql = "select * from " + typeof(T).Name + " where " + constrain;

            base.Parameter.Clear();

            foreach (KeyValuePair<string, string> kvp in ListParas)
            {
                base.Parameter.Add(new SqlParameter("@" + kvp.Key, kvp.Value));
            }

            return base.SearchList<T>(sql, tran) == null ? new List<T>() : base.SearchList<T>(sql, tran);
        }

        public T GetGeneral<T>() where T : class, new()
        {

            string sql = "select top 1 * from " + typeof(T).Name;

            return base.GetObject<T>(sql) == null ? new T() : base.GetObject<T>(sql);
        }

        public T GetGeneral<T>(string constrain) where T : class, new()
        {

            string sql = "select top 1 * from " + typeof(T).Name + " where " + constrain;

            return base.GetObject<T>(sql) == null ? new T() : base.GetObject<T>(sql);
        }

        public T GetGeneral<T>(string constrain, Dictionary<string, string> ListParas) where T : class, new()
        {


            string sql = "select * from " + typeof(T).Name + " where " + constrain;

            base.Parameter.Clear();

            foreach (KeyValuePair<string, string> kvp in ListParas)
            {
                base.Parameter.Add(new SqlParameter("@" + kvp.Key, kvp.Value));
            }

            return base.GetObject<T>(sql) == null ? new T() : base.GetObject<T>(sql);
        }

        public T GetGeneral<T>(string constrain, Dictionary<string, string> ListParas, SqlTransaction tran) where T : class, new()
        {


            string sql = "select * from " + typeof(T).Name + " where " + constrain;

            base.Parameter.Clear();

            foreach (KeyValuePair<string, string> kvp in ListParas)
            {
                base.Parameter.Add(new SqlParameter("@" + kvp.Key, kvp.Value));
            }

            return base.GetObject<T>(sql, tran) == null ? new T() : base.GetObject<T>(sql, tran);
        }

        public string DeleteGeneralList<T>(string[] idlist) where T : BaseItem, new()
        {
            string sql = $"delete { typeof(T).Name } where ID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));

            try
            {
                base.ExeNonQuery(sql);
                return "刪除成功";
            }
            catch (Exception ex)
            {
                logger.Debug(ex, $"刪除{typeof(T).Name},error:" + ex.ToString().NewLineReplace());
                return "刪除失敗";
            }
        }
        #endregion

        #region SiteMap

        /// <summary>
        /// 取得上方Menu
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public List<AdminFunctionAuth> GetSiteMapMenuByUser(string[] roles)
        {
            string sql = @"select * from AdminFunctionAuth t where t.GroupID in (select * from dbo.SplitList(',',@roles))";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@roles", string.Join(",", roles)));

            return base.SearchList<AdminFunctionAuth>(sql);
        }

        /// <summary>
        /// 取得左邊Menu
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="langID"></param>
        /// <param name="IsAdmin"></param>
        /// <param name="menutype"></param>
        /// <returns></returns>
        public List<Menu> GetMenuByRoles(string[] roles, string langID, bool IsAdmin, string menutype, string account)
        {
            string sql = string.Empty;
            base.Parameter.Clear();

            if (IsAdmin)
            {
                sql = @"select * from Menu t where Status=1 and MenuType=@MenuType and t.LangID=@langID";
            }
            else if (menutype == "1" || menutype == "2" || menutype == "3")
            {
                sql = @"select * from Menu t
                            where t.ID in (
                            select t.ItemID from AdminFunctionAuth t where t.GroupID in (select * from dbo.SplitList(',',@roles))
                            and t.Type='1' and t.LangID=@langID  and MenuType=@MenuType
                            ) and Status=1
                            order by t.MenuLevel,t.ParentID,t.Sort";

                base.Parameter.Add(new SqlParameter("@roles", string.Join(",", roles)));

            }
            else
            {
                sql = @"select * from Menu t where Status=1 and MenuType=@MenuType and t.LangID=@langID
                        and MenuType in
                        (
                            select 
                            s.ID+3
                            from Users t
                            left join ExclusiveLayout s on t.ID = s.Manager
                            where t.Account =@account
                        )";
                base.Parameter.Add(new SqlParameter("@account", account));
            }

            base.Parameter.Add(new SqlParameter("@langID", langID));
            base.Parameter.Add(new SqlParameter("@MenuType", menutype));

            return base.SearchList<Menu>(sql);
        }

        public bool CheckMenuAuth(string menuType, string ItemID)
        {
            string sql = @"select * from Menu t
                            where t.MenuType = @menuType and t.ID=@ItemID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@menuType", menuType));
            base.Parameter.Add(new SqlParameter("@ItemID", ItemID));

            return base.SearchCount(sql) > 0;
        }

        /// <summary>
        /// 確認角色是否有權限
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="Type"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public bool CheckMenuAuth(string[] RoleID, string Type, string ItemID)
        {
            string sql = @"select * from AdminFunctionAuth t 
                            where t.GroupID in (select ListItem from dbo.SplitList(',',@RoleID))
                                  and t.Type=@Type and t.ItemID=@ItemID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Type", Type));
            base.Parameter.Add(new SqlParameter("@ItemID", ItemID));
            base.Parameter.Add(new SqlParameter("@RoleID", string.Join(",", RoleID)));

            return base.SearchCount(sql) > 0;
        }

        public bool CheckMenuAuth(string[] RoleID, string Type, string URL, string paramter)
        {
            string sql = @"select t.GroupID from AdminFunctionAuth t 
                            left join AdminFunction s on t.ItemID= s.ID 
                            where t.GroupID in (select ListItem from dbo.SplitList(',',@RoleID))
                                  and t.Type=@Type and s.Url=@URL and s.Parameter=@paramter";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Type", Type));
            base.Parameter.Add(new SqlParameter("@URL", URL));
            base.Parameter.Add(new SqlParameter("@paramter", paramter));
            base.Parameter.Add(new SqlParameter("@RoleID", string.Join(",", RoleID)));

            return base.SearchCount(sql) > 0;
        }

        public bool CheckMenuAuthPara(string[] RoleID, string Type, string URL, string paramter, string account)
        {
            string sql = @"select t.GroupID from AdminFunctionAuth t 
                            left join AdminFunction s on t.ItemID= s.ID 
                            where t.GroupID in (select ListItem from dbo.SplitList(',',@RoleID))
                                  and t.Type=@Type and s.Url=@URL and isnull(s.Parameter,'')=@paramter";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Type", Type));
            base.Parameter.Add(new SqlParameter("@URL", URL));
            base.Parameter.Add(new SqlParameter("@paramter", paramter));
            base.Parameter.Add(new SqlParameter("@RoleID", string.Join(",", RoleID)));

            int rowCnt = base.SearchCount(sql);
            if (rowCnt > 0)
                return true;
            else
            {
                sql = @"select 
                                33+s.ID as ID,
                                3 as GroupID,
                                s.Name as Name,
                                'Menu/MainMenu' as Url,
                                'Menu' as Controller,
                                'MainMenu' as Action,
                                'menutype='+convert(varchar,s.ID+3) as Parameter,
                                -1 as ParentGroupID,
                                null as Area
                                from Users t
                                left join ExclusiveLayout s on t.ID = s.Manager
                                where t.Account =@account 
                                      and @URL='Menu/MainMenu'
                                      and 'menutype='+convert(varchar,s.ID+3)=@paramter ";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@account", account));
                base.Parameter.Add(new SqlParameter("@URL", URL));
                base.Parameter.Add(new SqlParameter("@paramter", paramter));

                return base.SearchCount(sql) > 0;
            }
        }

        public List<Menu> GetMenu(string langID, string menuType)
        {
            string sql = string.Empty;
            base.Parameter.Clear();

            sql = @"select * from Menu t where Status=1 and LangID=@langID  and MenuType=@MenuType";
            base.Parameter.Add(new SqlParameter("@MenuType", menuType));
            base.Parameter.Add(new SqlParameter("@langID", langID));

            return base.SearchList<Menu>(sql);
        }

        #endregion

        #region Main Grid
        public Paging<ModelCommonMain> Paging(SearchModelBase model, int ModelID)
        {
            string sql = string.Empty;

            var Paging = new Paging<ModelCommonMain>();
            var onepagecnt = model.Limit;

            sql = "select * from ModelCommonMain where Lang_ID=@Lang_ID and ModelID=@ModelID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", model.LangId));
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            Paging.rows = base.SearchListPage<ModelCommonMain>(sql, model.Offset, model.Limit, " order by " + model.Sort);
            Paging.total = base.SearchCount(sql);

            return Paging;
        }

        public string DeleteMainGrid<T>(string[] idlist, string delaccount, string langid, string account, string username, int ModelID) where T : ItemBase
        {
            string sql = string.Empty;
            string rstr = string.Empty;
            var r = 0;
            //檢查是否在使用中
            sql = "select * from Menu where ModelID=@ModelID and langid=@langid and ModelItemID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
            base.Parameter.Add(new SqlParameter("@langid", langid));
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

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
                        sql = @"delete ModelCommonMain where ID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //delete VerifyData_OK
                        sql = @"delete VerifyData where ModelID=@ModelID and ModelMainID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.Parameter.Add(new SqlParameter("@@ModelID", ModelID));
                        base.ExeNonQuery(sql, tran);

                        //刪除PageUnitSetting
                        sql = @"delete CommonUnitSetting where MainID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);


                        for (var idx = 0; idx < idlist.Length; idx++)
                        {
                            //刪除PageIndexItem
                            List<T> olditem = GetGeneralList<T>("ModelID=@ModelID", new Dictionary<string, string>() { { "ModelID", idlist[idx] } }, tran)
                                                           .ToList();

                            sql = $@"delete {typeof(T).Name} where ModelID=@ModelID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ModelID", idlist[idx]));
                            base.ExeNonQuery(sql, tran);
                        }

                        //update sort order
                        sql = @"with cte as
                                    (
                                    select 
                                     ROW_NUMBER()OVER(order by sort) as ROW,t.ID
                                     from ModelCommonMain t where t.Lang_ID=@Lang and ModelID=@ModelID
                                    )
                                    update s set s.Sort = t.ROW from cte  t
                                    left join ModelCommonMain s on t.ID = s.ID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Lang", langid));
                        base.Parameter.Add(new SqlParameter("@ModelID", ModelID));
                        base.ExeNonQuery(sql, tran);

                        tran.Commit();
                        rstr = "刪除成功";

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Common模組刪除失敗，error:" + ex.ToString().NewLineReplace());
                        tran.Rollback();
                        rstr = "刪除失敗";
                    }

                    return rstr;
                }
            }
        }

        public ModelCommonMain GetModelCommonMain(string ID, string lang)
        {
            string sql = @"select * from ModelCommonMain where ID=@ID and Lang_ID=@Lang_ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));
            base.Parameter.Add(new SqlParameter("@Lang_ID", lang));

            return base.GetObject<ModelCommonMain>(sql);
        }
        #endregion

        #region 群組設定

        public IList<SelectListItem> GetAllGroupSelectList<T>(string mainid) where T : GroupBase
        {
            string sql = string.Empty;

            sql = $@"select * from { typeof(T).Name} where Main_ID=@Main_ID Order By Sort";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Main_ID", mainid));
            var list = base.SearchList<T>(sql);

            IList<SelectListItem> item = new List<SelectListItem>();
            item.Add(new SelectListItem() { Text = "無分類", Value = "0" });

            foreach (var l in list)
            {
                item.Add(new SelectListItem() { Text = l.Group_Name, Value = l.ID.ToString() });
            }
            return item;
        }

        public IList<SelectListItem> GetAllGroupSelectList<T>(string mainid,int Lang_ID) where T : GroupBase
        {
            string sql = string.Empty;
            base.Parameter.Clear();
            sql = $@"select * from { typeof(T).Name} where Main_ID=@Main_ID";
            if (Lang_ID == 0)
            {
                sql += " and Lang_ID is null Order By Sort";
            }
            else
            {
                sql += " and Lang_ID=@Lang_ID Order By Sort";
                base.Parameter.Add(new SqlParameter("@Lang_ID", Lang_ID));
            }
            
            
            base.Parameter.Add(new SqlParameter("@Main_ID", mainid));
            var list = base.SearchList<T>(sql);

            IList<SelectListItem> item = new List<SelectListItem>();
            item.Add(new SelectListItem() { Text = "無分類", Value = "0" });

            foreach (var l in list)
            {
                item.Add(new SelectListItem() { Text = l.Group_Name, Value = l.ID.ToString() });
            }
            return item;
        }

        public Paging<T> PagingGroup<T>(SearchModelBase model) where T : class
        {
            var Paging = new Paging<T>();

            string sql = $@"select * from { typeof(T).Name} where Main_ID=@Main_ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Main_ID", model.ModelID));

            if (model.Search != "")
            {
                sql += " and Group_Name like @Group_Name";
                base.Parameter.Add(new SqlParameter("@Group_Name", "%" + model.Search + "%"));
            }

            Paging.rows = base.SearchListPage<T>(sql, model.Offset, model.Limit, " order by " + model.Sort);

            Paging.total = base.SearchCount(sql);
            return Paging;
        }

        public Paging<T> PagingGroup<T>(SearchModelBase model,int Lang_ID) where T : class
        {
            var Paging = new Paging<T>();

            string sql = $@"select * from { typeof(T).Name} where Main_ID=@Main_ID";

            base.Parameter.Clear();

              if (Lang_ID == 0)
            {
                sql += " and Lang_ID is null";
            }
            else
            {
                sql += " and Lang_ID=@Lang_ID";
                base.Parameter.Add(new SqlParameter("@Lang_ID", Lang_ID));
            }

            base.Parameter.Add(new SqlParameter("@Main_ID", model.ModelID));

            if (model.Search != "")
            {
                sql += " and Group_Name like @Group_Name";
                base.Parameter.Add(new SqlParameter("@Group_Name", "%" + model.Search + "%"));
            }

            Paging.rows = base.SearchListPage<T>(sql, model.Offset, model.Limit, " order by " + model.Sort);

            Paging.total = base.SearchCount(sql);
            return Paging;
        }

        public string UpdateGroupSeq<T>(int id, int seq, string mainid, string account, string username) where T : GroupBase, new()
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                T oldmodel = GetGeneral<T>("ID=@ID", new Dictionary<string, string>() { { "ID", id.ToString() } });
                if (oldmodel.ID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<T> mtseqdata = null;

                    mtseqdata = GetGeneralList<T>("Sort>@Sort and Main_ID=@Main_ID"
                                                                                  , new Dictionary<string, string>() { { "Sort", seq.ToString() }, { "Main_ID", mainid } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        sql = $@"select * from {typeof(T).Name} where Main_ID=@Main_ID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Main_ID", mainid));

                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<T> ltseqdata = null;
                    ltseqdata = GetGeneralList<T>("Sort<=@Sort and Main_ID=@Main_ID"
                                                                                  , new Dictionary<string, string>() { { "Sort", qseq.ToString() }, { "Main_ID", mainid } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();
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
                logger.Error(ex, "更新排序異常，error:" + ex.ToString().NewLineReplace());
                return "更新排序失敗:" + " error:" + ex.ToString();
            }
        }

        public string UpdateGroupSeq<T>(int id, int seq, string mainid, string account, string username, int parent) where T : GroupBase, new()
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                T oldmodel = GetGeneral<T>("ID=@ID", new Dictionary<string, string>() { { "ID", id.ToString() } });
                if (oldmodel.ID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<T> mtseqdata = null;

                    mtseqdata = GetGeneralList<T>($"Sort>@Sort and Main_ID=@Main_ID and {(parent == 0 ? " ParentID is null" : " ParentID=@ParentID")}"
                                                                                  , new Dictionary<string, string>() { { "Sort", seq.ToString() }, { "Main_ID", mainid }, { "ParentID", parent.ToString() } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        base.Parameter.Clear();

                        sql = $@"select * from {typeof(T).Name} where Main_ID=@Main_ID";
                        if (parent == 0)
                        {
                            sql += " and ParentID is null";
                        }
                        else
                        {
                            sql += " and ParentID=@ParentID";
                            base.Parameter.Add(new SqlParameter("@ParentID", parent));
                        }

                        base.Parameter.Add(new SqlParameter("@Main_ID", mainid));

                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<T> ltseqdata = null;
                    ltseqdata = GetGeneralList<T>($"Sort<=@Sort and Main_ID=@Main_ID and {(parent == 0 ? " ParentID is null" : " ParentID = @ParentID")}"
                                                                                  , new Dictionary<string, string>() { { "Sort", qseq.ToString() }, { "Main_ID", mainid }, { "ParentID", parent.ToString() } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();
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
                logger.Error(ex, "更新排序異常，error:" + ex.ToString().NewLineReplace());
                return "更新排序失敗:" + " error:" + ex.ToString();
            }
        }
        public string DeleteGroup<T, S>(string[] idlist, string delaccount, string account, string username) where T : GroupBase, new()
                                                                                                            where S : ItemBase
        {

            string sql = string.Empty;
            try
            {
                var r = 0;
                for (var idx = 0; idx < idlist.Length; idx++)
                {
                    var users = GetGeneralList<S>("GroupID=@GroupID", new Dictionary<string, string>() { { "GroupID", idlist[idx] } });
                    if (users.Count() > 0)
                    {
                        var gname = GetGeneral<T>("ID=@ID", new Dictionary<string, string>() { { "ID", idlist[idx] } }).Group_Name;
                        return "群組名稱:" + gname + " 目前已被使用,無法刪除";
                    }

                    sql = $@"delete  { typeof(T).Name} where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@ID", idlist[idx]));
                    r = base.ExeNonQuery(sql);
                }
                var rstr = "";
                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("刪除群組:" + delaccount);
                    rstr = "刪除成功";
                }
                else
                {
                    rstr = "刪除失敗";
                }

                //update sort order
                sql = $@"with cte as
                                    (
                                    select 
                                     ROW_NUMBER()OVER(PARTITION by Main_ID  order by sort) as ROW,t.ID
                                     from {typeof(T).Name} t 
                                    )
                                    update s set s.Sort = t.ROW from cte  t
                                    left join {typeof(T).Name} s on t.ID = s.ID";
                base.Parameter.Clear();
                base.ExeNonQuery(sql);


                return rstr;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "刪除群組失敗:" + ex.ToString().NewLineReplace());
                return "刪除失敗";
            }
        }

        public string DeleteGroup<T, S>(string[] idlist, string delaccount, string account, string username, int parent) where T : GroupBase, new()
                                                                                                            where S : ItemBase
        {

            string sql = string.Empty;
            try
            {
                var r = 0;
                for (var idx = 0; idx < idlist.Length; idx++)
                {
                    var users = GetGeneralList<S>("GroupID=@GroupID", new Dictionary<string, string>() { { "GroupID", idlist[idx] } });
                    if (users.Count() > 0)
                    {
                        var gname = GetGeneral<T>("ID=@ID", new Dictionary<string, string>() { { "ID", idlist[idx] } }).Group_Name;
                        return "群組名稱:" + gname + " 目前已被使用,無法刪除";
                    }

                    sql = $@"delete  { typeof(T).Name} where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@ID", idlist[idx]));
                    r = base.ExeNonQuery(sql);
                }
                var rstr = "";
                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("刪除群組:" + delaccount);
                    rstr = "刪除成功";
                }
                else
                {
                    rstr = "刪除失敗";
                }

                //update sort order
                sql = $@"with cte as
                                    (
                                    select 
                                     ROW_NUMBER()OVER(PARTITION by Main_ID,ParentID  order by sort) as ROW,t.ID,isnull(t.ParentID,0) as ParentID
                                     from {typeof(T).Name} t 
                                    )
                                    update s set s.Sort = t.ROW from cte  t
                                    left join {typeof(T).Name} s on t.ID = s.ID and t.ParentID = isnull(s.ParentID,0)                                    
                                    ";
                base.Parameter.Clear();
                base.ExeNonQuery(sql);


                return rstr;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "刪除群組失敗:" + ex.ToString().NewLineReplace());

                return "刪除失敗";
            }
        }
        public string EditGroup<T>(string name, string id, string mainid, string account) where T : GroupBase, new()
        {
            string sql = string.Empty;
            var r = 0;

            try
            {
                if (id == "-1" || id.IsNullOrEmpty())
                {
                    var alldata = GetGeneralList<T>("Main_ID=@Main_ID", new Dictionary<string, string>() { { "Main_ID", mainid } });
                    if (alldata.Any(v => v.Group_Name == name))
                    {
                        return "類別名稱已經存在";
                    }

                    sql = $"select isnull(Max(Sort),0) from  { typeof(T).Name} where Main_ID=@Main_ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Main_ID", mainid));


                    int maxsort = int.Parse(base.ExecuteScalar(sql, "0").ToString());
                    var group = new T();
                    group.Group_Name = name;
                    group.Enabled = true;
                    group.Readonly = false;
                    group.Seo_Manage = false;
                    group.Main_ID = int.Parse(mainid);
                    group.Sort = maxsort + 1;
                    group.UpdateDatetime = DateTime.Now;
                    group.UpdateUser = account;



                    r = (int)base.InsertObject(group);
                    if (r > 0)
                    {
                        return "新增成功";
                    }
                    else
                    {
                        return "新增失敗";
                    }
                }
                else
                {
                    var checkdata = GetGeneralList<T>("Group_Name=@Group_Name and ID!=@ID AND Main_ID=@Main_ID",
                                                      new Dictionary<string, string>() { { "Group_Name", name }, { "ID", id }, { "Main_ID", mainid } });
                    if (checkdata.Count() > 0)
                    {
                        return "類別名稱已經存在";
                    }

                    sql = $@"update {typeof(T).Name} set Group_Name=@Group_Name,UpdateDatetime=GetDate(),UpdateUser=@UpdateUser
                        where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Group_Name", name));
                    base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                    base.Parameter.Add(new SqlParameter("@ID", id));

                    r = base.ExeNonQuery(sql);
                    if (r > 0)
                    {
                        return "修改成功";
                    }
                    else
                    {
                        return "修改失敗";
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "修改群組異常,error:" + ex.ToString().NewLineReplace());

                return "系統異常，請通知資訊人員！";
            }
        }
        public string EditGroup<T>(string name, string id, string mainid, string account, int parent) where T : GroupFileDownload, new()
        {
            string sql = string.Empty;
            var r = 0;

            try
            {
                if (id == "-1" || id.IsNullOrEmpty())
                {
                    var alldata = GetGeneralList<T>("Main_ID=@Main_ID", new Dictionary<string, string>() { { "Main_ID", mainid } });
                    if (alldata.Any(v => v.Group_Name == name))
                    {
                        return "類別名稱已經存在";
                    }

                    sql = $"select isnull(Max(Sort),0) from  { typeof(T).Name} where Main_ID=@Main_ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Main_ID", mainid));

                    if (parent == 0)
                    {
                        sql += " and ParentID is null";
                    }
                    else
                    {
                        sql += " and ParentID =@ParentID";
                        base.Parameter.Add(new SqlParameter("@ParentID", parent));
                    }

                    int maxsort = int.Parse(base.ExecuteScalar(sql, "0").ToString());
                    var group = new T();
                    group.Group_Name = name;
                    group.Enabled = true;
                    group.Readonly = false;
                    group.Seo_Manage = false;
                    group.Main_ID = int.Parse(mainid);
                    group.Sort = maxsort + 1;
                    group.UpdateDatetime = DateTime.Now;
                    group.UpdateUser = account;

                    if (parent != 0)
                    {
                        group.ParentID = parent;
                    }

                    r = (int)base.InsertObject(group);
                    if (r > 0)
                    {
                        return "新增成功";
                    }
                    else
                    {
                        return "新增失敗";
                    }
                }
                else
                {
                    var checkdata = GetGeneralList<T>("Group_Name=@Group_Name and ID!=@ID AND Main_ID=@Main_ID",
                                                      new Dictionary<string, string>() { { "Group_Name", name }, { "ID", id }, { "Main_ID", mainid } });
                    if (checkdata.Count() > 0)
                    {
                        return "類別名稱已經存在";
                    }

                    sql = $@"update {typeof(T).Name} set Group_Name=@Group_Name,UpdateDatetime=GetDate(),UpdateUser=@UpdateUser
                        where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Group_Name", name));
                    base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                    base.Parameter.Add(new SqlParameter("@ID", id));

                    r = base.ExeNonQuery(sql);
                    if (r > 0)
                    {
                        return "修改成功";
                    }
                    else
                    {
                        return "修改失敗";
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "修改群組異常,error:" + ex.ToString().NewLineReplace());

                return "系統異常，請通知資訊人員！";
            }
        }

        public string EditGroupEPaper<T>(string name, string id, string mainid, string account, int Lang_ID) where T : GroupEPaper, new()
        {
            string sql = string.Empty;
            var r = 0;

            try
            {
                if (id == "-1" || id.IsNullOrEmpty())
                {
                    var alldata = GetGeneralList<T>("Main_ID=@Main_ID", new Dictionary<string, string>() { { "Main_ID", mainid } });
                    if (alldata.Any(v => v.Group_Name == name))
                    {
                        return "類別名稱已經存在";
                    }

                    sql = $"select isnull(Max(Sort),0) from  { typeof(T).Name} where Main_ID=@Main_ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Main_ID", mainid));

                    if (Lang_ID == 0)
                    {
                        sql += " and ParentID is null";
                    }
                    else
                    {
                        sql += " and Lang_ID =@Lang_ID";
                        base.Parameter.Add(new SqlParameter("@Lang_ID", Lang_ID));
                    }

                    int maxsort = int.Parse(base.ExecuteScalar(sql, "0").ToString());
                    var group = new T();
                    group.Group_Name = name;
                    group.Enabled = true;
                    group.Readonly = false;
                    group.Seo_Manage = false;
                    group.Main_ID = int.Parse(mainid);
                    group.Sort = maxsort + 1;
                    group.UpdateDatetime = DateTime.Now;
                    group.UpdateUser = account;

                    if (Lang_ID != 0)
                    {
                        group.Lang_ID = Lang_ID;
                    }

                    r = (int)base.InsertObject(group);
                    if (r > 0)
                    {
                        return "新增成功";
                    }
                    else
                    {
                        return "新增失敗";
                    }
                }
                else
                {
                    var checkdata = GetGeneralList<T>("Group_Name=@Group_Name and ID!=@ID AND Main_ID=@Main_ID",
                                                      new Dictionary<string, string>() { { "Group_Name", name }, { "ID", id }, { "Main_ID", mainid } });
                    if (checkdata.Count() > 0)
                    {
                        return "類別名稱已經存在";
                    }

                    sql = $@"update {typeof(T).Name} set Group_Name=@Group_Name,UpdateDatetime=GetDate(),UpdateUser=@UpdateUser
                        where ID=@ID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Group_Name", name));
                    base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                    base.Parameter.Add(new SqlParameter("@ID", id));

                    r = base.ExeNonQuery(sql);
                    if (r > 0)
                    {
                        return "修改成功";
                    }
                    else
                    {
                        return "修改失敗";
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "修改群組異常,error:" + ex.ToString().NewLineReplace());

                return "系統異常，請通知資訊人員！";
            }
        }
        public string UpdateGroupStatus<T>(string id, bool status, string account, string username) where T : GroupBase
        {
            try
            {
                string sql = $@"update {typeof(T).Name} set Enabled=@Enabled,UpdateDatetime=GetDate(),UpdateUser=@UpdateUser
                              where  ID=@ID";

                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@Enabled", status));
                base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                base.Parameter.Add(new SqlParameter("@ID", id));


                var r = base.ExeNonQuery(sql);
                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("修改GroupMessage顯示狀態 id=" + id + " 為:" + status);
                    return "更新成功";
                }
                else
                {
                    return "更新失敗";
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, $"修改 {typeof(T).Name}顯示狀態失敗: " + ex.ToString().NewLineReplace());

                return "更新失敗";
            }
        }

        #endregion

        #region Item設定

        public string UpdateItemSeq<T>(int id, int seq, string modelid, string account, string username, string Lang) where T : ItemBase, new()
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                T oldmodel = GetGeneral<T>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", id.ToString() } });
                if (oldmodel.ItemID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<T> mtseqdata = null;

                    mtseqdata = GetGeneralList<T>("Sort>@Sort and ModelID=@ModelID and Lang_ID=@Lang_ID"
                                                                                  , new Dictionary<string, string>() { { "Sort", seq.ToString() }, { "ModelID", modelid }, { "Lang_ID", Lang } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        sql = $@"select * from {typeof(T).Name} where ModelID=@ModelID and Lang_ID=@Lang_ID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelID", modelid));
                        base.Parameter.Add(new SqlParameter("@Lang_ID", Lang));

                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<T> ltseqdata = null;
                    ltseqdata = GetGeneralList<T>("Sort<=@Sort and ModelID=@ModelID and Lang_ID=@Lang_ID"
                                                                                  , new Dictionary<string, string>() { { "Sort", qseq.ToString() }, { "ModelID", modelid }, { "Lang_ID", Lang } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();
                    //更新seq+1
                    var sidx = 0;
                    for (var idx = 1; idx <= ltseqdata.Count(); idx++)
                    {
                        if (idx == seq && seq < oldmodel.Sort)
                        {
                            sidx += 1;
                        }
                        var tempmodel = ltseqdata[idx - 1];
                        if (tempmodel.ItemID == id)
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
                base.Parameter.Clear();
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
                logger.Error(ex, $"更新{typeof(T).Name}排序失敗:" + " error:" + ex.ToString().NewLineReplace());
                return "更新排序失敗:" + " error:" + ex.Message;
            }
        }

        public string SetItemStatus<T>(string id, bool status, string account, string username) where T : class
        {
            try
            {
                string sql = $@"update {typeof(T).Name} set Enabled=@Enabled
                                                           ,UnPublishDate = case when @Enabled =0 then GetDate() else null end 
                                where ItemID=@ItemID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@Enabled", status ? 1 : 0));
                base.Parameter.Add(new SqlParameter("@ItemID", int.Parse(id)));

                var r = base.ExeNonQuery(sql);

                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("修改MessageItem顯示狀態 id=" + id + " 為:" + status);
                    return "更新成功";
                }
                else
                {
                    return "更新失敗";
                }

            }
            catch (Exception ex)
            {
                logger.Error($"修改{typeof(T).Name}顯示狀態失敗:" + ex.ToString().NewLineReplace());
                return "更新失敗";
            }
        }
        public string SetHomeStatus<T>(string id, bool status, string account, string username) where T : class
        {
            try
            {
                string sql = $@"update {typeof(T).Name} set Home=@Home                                                           
                                where ItemID=@ItemID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@Home", status ? 1 : 0));
                base.Parameter.Add(new SqlParameter("@ItemID", int.Parse(id)));

                var r = base.ExeNonQuery(sql);

                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("修改MessageItem顯示狀態 id=" + id + " 為:" + status);
                    return "更新成功";
                }
                else
                {
                    return "更新失敗";
                }

            }
            catch (Exception ex)
            {
                logger.Error($"修改{typeof(T).Name}Home顯示狀態失敗:" + ex.ToString().NewLineReplace());
                return "更新失敗";
            }
        }
        public string SetStatus<T>(string id, bool status) where T : class
        {
            try
            {
                string sql = $@"update {typeof(T).Name} set Enabled=@Enabled
                                                           
                                where ID=@ID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@Enabled", status ? 1 : 0));
                base.Parameter.Add(new SqlParameter("@ID", int.Parse(id)));

                var r = base.ExeNonQuery(sql);

                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("修改MessageItem顯示狀態 id=" + id + " 為:" + status);
                    return "更新成功";
                }
                else
                {
                    return "更新失敗";
                }

            }
            catch (Exception ex)
            {
                logger.Error($"修改{typeof(T).Name}顯示狀態失敗:" + ex.ToString().NewLineReplace());
                return "更新失敗";
            }
        }

        public string SetItemStatusWithNoUnPublish<T>(string id, bool status, string account, string username) where T : class
        {
            try
            {
                string sql = $@"update {typeof(T).Name} set Enabled=@Enabled                                                           
                                where ItemID=@ItemID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@Enabled", status ? 1 : 0));
                base.Parameter.Add(new SqlParameter("@ItemID", int.Parse(id)));

                var r = base.ExeNonQuery(sql);

                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("修改MessageItem顯示狀態 id=" + id + " 為:" + status);
                    return "更新成功";
                }
                else
                {
                    return "更新失敗";
                }

            }
            catch (Exception ex)
            {
                logger.Error($"修改{typeof(T).Name}顯示狀態失敗:" + ex.ToString().NewLineReplace());
                return "更新失敗";
            }
        }


        public string DeleteItem<T>(string[] idlist, string delaccount, string account, string username
                                    , string folder, int ModelID) where T : ItemBase, new()
        {
            try
            {
                string sql = string.Empty;

                var r = 0;

                var modelid = -1;
                for (var idx = 0; idx < idlist.Length; idx++)
                {
                    var entity = new T();
                    var olditem = GetGeneral<T>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", idlist[idx] } });
                    modelid = olditem.ModelID.Value;
                    entity.ItemID = int.Parse(idlist[idx]);

                    sql = $@"delete {typeof(T).Name} where ItemID=@ItemID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@ItemID", idlist[idx]));
                    r = base.ExeNonQuery(sql);

                    if (r <= 0)
                    {
                        logger.Error($"刪除{typeof(T).Name}項目失敗:ID=" + idlist[idx].NewLineReplace());
                    }
                    else
                    {
                        sql = "delete VerifyData where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelID", ModelID));
                        base.Parameter.Add(new SqlParameter("@ModelMainID", modelid));
                        base.Parameter.Add(new SqlParameter("@ModelItemID", idlist[idx]));
                        base.ExeNonQuery(sql);

                        sql = $@"delete SEO where TypeName='{folder}' and TypeID=@TypeID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@TypeID", idlist[idx]));
                        base.ExeNonQuery(sql);

                        sql = @"delete  EPaperAutoItem where EPaperID=@EPaperID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@EPaperID", idlist[idx]));
                        base.ExeNonQuery(sql);
                    }
                }
                var rstr = "";
                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("刪除訊息項目:" + delaccount);
                    rstr = "刪除成功";
                    
                }
                else
                {
                    rstr = "刪除失敗";
                }

                //update sort order
                sql = $@"with cte as
                        (
                        select 
                            ROW_NUMBER()OVER(order by sort) as ROW,t.ItemID
                            from {typeof(T).Name} t where t.ModelId=@ModelId
                        )
                        update s set s.Sort = t.ROW from cte  t
                        left join {typeof(T).Name} s on t.ItemID = s.ItemID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@ModelId", modelid));
                base.ExeNonQuery(sql);

                return rstr;
            }
            catch (Exception ex)
            {
                logger.Error($"刪除{typeof(T).Name}群組失敗:" + ex.ToString().NewLineReplace());
                return "刪除失敗";
            }
        }

        public string DeleteItem<T>(string[] idlist, string ModelID, SqlTransaction tran) where T : ItemBase, new()
        {
            try
            {
                string sql = string.Empty;

                var r = 0;

                var modelid = -1;
                for (var idx = 0; idx < idlist.Length; idx++)
                {
                    var entity = new T();
                    var olditem = GetGeneral<T>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", idlist[idx] } });
                    modelid = olditem.ModelID.Value;
                    entity.ItemID = int.Parse(idlist[idx]);

                    sql = $@"delete {typeof(T).Name} where ItemID=@ItemID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@ItemID", idlist[idx]));
                    r = base.ExeNonQuery(sql, tran);

                    if (r <= 0)
                    {
                        logger.Error($"刪除{typeof(T).Name}項目失敗:ID=" + idlist[idx].NewLineReplace());
                    }
                    //else
                    //{
                    //    sql = "delete VerifyData where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                    //    base.Parameter.Clear();
                    //    base.Parameter.Add(new SqlParameter("@ModelID", ModelID));
                    //    base.Parameter.Add(new SqlParameter("@ModelMainID", modelid));
                    //    base.Parameter.Add(new SqlParameter("@ModelItemID", idlist[idx]));
                    //    base.ExeNonQuery(sql,tran);

                    //}
                }
                var rstr = "";
                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("刪除訊息項目:" + delaccount);
                    rstr = "刪除成功";
                }
                else
                {
                    rstr = "刪除失敗";
                }

                //update sort order
                sql = $@"with cte as
                        (
                        select 
                            ROW_NUMBER()OVER(order by sort) as ROW,t.ItemID
                            from {typeof(T).Name} t where t.ModelId=@ModelId
                        )
                        update s set s.Sort = t.ROW from cte  t
                        left join {typeof(T).Name} s on t.ItemID = s.ItemID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@ModelId", modelid));
                base.ExeNonQuery(sql, tran);

                return rstr;
            }
            catch (Exception ex)
            {
                logger.Error($"刪除{typeof(T).Name}群組失敗:" + ex.ToString().NewLineReplace());
                throw ex;
            }
        }

        public string InsertDeleteData<T>(string[] idlist, string delaccount, string account, string username
                                   , string folder, int ModelID) where T : ItemBase, new()
        {
            try
            {
                string sql = string.Empty;
                string title = string.Empty;
                var r = 0;


                var modelid = -1;
                for (var idx = 0; idx < idlist.Length; idx++)
                {

                    var entity = new T();
                    var olditem = GetGeneral<T>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", idlist[idx] } });
                    //ModelID = GetModelID(ModelID, olditem.ModelID.Value);
                    modelid = olditem.ModelID.Value;
                    entity.ItemID = int.Parse(idlist[idx]);


                    sql = @"select * from VerifyData where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@ModelID", ModelID));
                    base.Parameter.Add(new SqlParameter("@ModelMainID", olditem.ModelID.Value));
                    base.Parameter.Add(new SqlParameter("@ModelItemID", olditem.ItemID));

                    List<VerifyData> hasvdata = base.SearchList<VerifyData>(sql);

                    if (hasvdata.Count() == 0)
                    {
                        title = GetTitle(idlist[idx], ModelID);

                        base.InsertObject(new VerifyData()
                        {
                            ModelID = ModelID,
                            ModelItemID = olditem.ItemID,
                            ModelName = title,
                            ModelMainID = olditem.ModelID.Value,
                            VerifyStatus = 0,
                            ModelStatus = 3,
                            UpdateDateTime = DateTime.Now,
                            UpdateUser = username,
                            UpdateAccount = account,
                            LangID = 1
                        });
                    }
                    else
                    {
                        sql = @"update VerifyData set VerifyStatus=0,ModelStatus=@ModelStatus,VerifyDateTime=Null,VerifyUser=''
                                                        ,VerifyName='',UpdateDateTime=GetDate(),UpdateUser=@UpdateUser,UpdateAccount=@UpdateAccount
                                          where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelStatus", 3));
                        base.Parameter.Add(new SqlParameter("@UpdateUser", username));
                        base.Parameter.Add(new SqlParameter("@ModelID", ModelID));
                        base.Parameter.Add(new SqlParameter("@UpdateAccount", account));
                        base.Parameter.Add(new SqlParameter("@ModelMainID", olditem.ModelID.Value));
                        base.Parameter.Add(new SqlParameter("@ModelItemID", olditem.ItemID));
                        base.ExeNonQuery(sql);
                    }

                    if (typeof(T).Name == "EMP_RECRUIT")
                    {
                        sql = $@"update { typeof(T).Name } set Del=1,Update_Name=@UpdateName
                                                          ,Update_UserID=@UpdateUser,Update_DateTime=GetDate()
                                                          
                             where ItemID=@ItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ItemID", idlist[idx]));
                        base.Parameter.Add(new SqlParameter("@UpdateName", username));
                        base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                        base.ExeNonQuery(sql);
                    }
                    else if (typeof(T).Name == "ORDER_INFO")
                    {
                        sql = $@"update { typeof(T).Name } set Del=1,Update_Name=@UpdateName
                                                          ,Update_UserID=@UpdateUser,Update_DateTime=GetDate()
                                                          
                             where ItemID=@ItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ItemID", idlist[idx]));
                        base.Parameter.Add(new SqlParameter("@UpdateName", username));
                        base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                        base.ExeNonQuery(sql);
                    }
                    else
                    {
                        sql = $@"update { typeof(T).Name } set Del=1,UpdateName=@UpdateName
                                                          ,UpdateUser=@UpdateUser,UpdateDatetime=GetDate()
                                                          
                             where ItemID=@ItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ItemID", idlist[idx]));
                        base.Parameter.Add(new SqlParameter("@UpdateName", username));
                        base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                        base.ExeNonQuery(sql);
                    }



                }
                var rstr = "";
                if (r >= 0)
                {
                    //NLogManagement.SystemLogInfo("刪除訊息項目:" + delaccount);
                    rstr = "刪除成功";
                }
                else
                {
                    rstr = "刪除失敗";
                }



                return rstr;
            }
            catch (Exception ex)
            {
                logger.Error($"刪除{typeof(T).Name}群組失敗:" + ex.ToString().NewLineReplace());
                return "刪除失敗";
            }
        }

        #endregion

        #region 單元設定
        /// <summary>
        /// T:
        /// </summary>
        /// <typeparam name="T">MessageUnitSetting</typeparam>
        /// <typeparam name="S">MessageUnitSettingModel</typeparam>
        /// <param name="modelid"></param>
        /// <returns></returns>
        public T GetUnitModel<T, S>(string modelid, string type = "Message") where T : UnitSettingModelBase, new()
                                                   where S : UnitSetting
        {
            var data = GetGeneralList<S>("MainID=@MainID", new Dictionary<string, string>() { { "MainID", modelid } }).ToList();
            var model = new T();
            model.MainID = int.Parse(modelid);

            if (data.Count() > 0)
            {
                model = new T()
                {
                    ClassOverview = data.First().ClassOverview,
                    Column1 = data.First().Column1,
                    Column2 = data.First().Column2,
                    Column3 = data.First().Column3,
                    Column4 = data.First().Column4,
                    Column5 = data.First().Column5,
                    Column6 = data.First().Column6,
                    Column7 = data.First().Column7,
                    Column8 = data.First().Column8,
                    Column9 = data.First().Column9,
                    Column10 = data.First().Column10,
                    Column11 = data.First().Column11,
                    Column12 = data.First().Column12,
                    Column13 = data.First().Column13,
                    Column14 = data.First().Column14,
                    Column15 = data.First().Column15,
                    Column16 = data.First().Column16,
                    Column17 = data.First().Column17,
                    Column18 = data.First().Column18,
                    Column19 = data.First().Column19,
                    Column20 = data.First().Column20,
                    Summary = data.First().Summary,
                    IsForward = data.First().IsForward,
                    IsPrint = data.First().IsPrint,
                    IsRSS = data.First().IsRSS,
                    IsShare = data.First().IsShare,
                    MemberAuth = data.First().MemberAuth,
                    MainID = data.First().MainID,
                    ShowCount = data.First().ShowCount,
                    ID = data.First().ID,
                    EMailAuth = data.First().EMailAuth,
                    VIPAuth = data.First().VIPAuth,
                    EnterpriceStudentAuth = data.First().EnterpriceStudentAuth,
                    GeneralStudentAuth = data.First().GeneralStudentAuth,
                    IntroductionHtml = data.First().IntroductionHtml,
                };
            }

            model.columnSettings = GetGeneralList<ColumnSetting>("Type=@Type and MainID=@MainID", new Dictionary<string, string>() { { "Type", type }, { "MainID", modelid } }).OrderBy(v => v.Sort).ToList();

            return model;
        }

        public string SetUnitModel<T, S>(T model, string account, string Type = "Message", string MainID = "1") where T : UnitSettingModelBase, new()
                                                                where S : UnitSetting, new()
        {
            S newmodel = new S();
            newmodel.UpdateDatetime = DateTime.Now;
            newmodel.UpdateUser = account;
            var r = 0;
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        if (model.ID == -1)
                        {
                            newmodel.MainID = model.MainID.Value;
                            newmodel.ClassOverview = model.ClassOverview;
                            newmodel.Column1 = model.Column1;
                            newmodel.Column2 = model.Column2;
                            newmodel.Column3 = model.Column3;
                            newmodel.Column4 = model.Column4;
                            newmodel.Column5 = model.Column5;
                            newmodel.Column6 = model.Column6;
                            newmodel.Column7 = model.Column7;
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
                            newmodel.ColumnSetting = "";
                            newmodel.IntroductionHtml = model.IntroductionHtml == null ? "" : model.IntroductionHtml;
                            r = (int)base.InsertObject(newmodel, tran);
                        }
                        else
                        {
                            //newmodel.ID = model.ID;
                            //newmodel.MainID = model.MainID.Value;
                            //newmodel.ClassOverview = model.ClassOverview;
                            //newmodel.Column1 = model.Column1 == null ? "" : model.Column1;
                            //newmodel.Column2 = model.Column2 == null ? "" : model.Column2;
                            //newmodel.Column3 = model.Column3 == null ? "" : model.Column3;
                            //newmodel.Column4 = model.Column4 == null ? "" : model.Column4;
                            //newmodel.Column5 = model.Column5 == null ? "" : model.Column5;
                            //newmodel.Column6 = model.Column6 == null ? "" : model.Column6;
                            //newmodel.Column7 = model.Column7 == null ? "" : model.Column7;
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
                            //newmodel.ColumnSetting = "";
                            //newmodel.IntroductionHtml = model.IntroductionHtml == null ? "" : model.IntroductionHtml;
                            model.IntroductionHtml = model.IntroductionHtml == null ? "" : model.IntroductionHtml;
                            string sql2 = $@"update {typeof(S).Name} set  ClassOverview=@ClassOverview,IsRSS=@IsRSS,IsShare=@IsShare
                                                                         ,IsPrint=@IsPrint,IsForward=@IsForward,ShowCount=@ShowCount
                                                                         ,IntroductionHtml=@IntroductionHtml
                                                                         ,UpdateDatetime=GetDate(),UpdateUser=@UpdateUser
                                            where ID=@ID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ClassOverview", model.ClassOverview));
                            base.Parameter.Add(new SqlParameter("@IsRSS", model.IsRSS));
                            base.Parameter.Add(new SqlParameter("@ID", model.ID));
                            base.Parameter.Add(new SqlParameter("@IsShare", model.IsShare));
                            base.Parameter.Add(new SqlParameter("@IsPrint", model.IsPrint));
                            base.Parameter.Add(new SqlParameter("@IntroductionHtml", model.IntroductionHtml));
                            base.Parameter.Add(new SqlParameter("@IsForward", model.IsForward));
                            base.Parameter.Add(new SqlParameter("@ShowCount", model.ShowCount));
                            base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                            r = base.ExeNonQuery(sql2, tran);

                            //r = base.UpdateObject(newmodel, tran) ? 1 : 0;
                        }


                        string sql = "delete ColumnSetting where Type=@Type and MainID=@MainID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Type", Type));
                        base.Parameter.Add(new SqlParameter("@MainID", MainID));
                        base.ExeNonQuery(sql, tran);

                        if (model.columnSettings != null)
                        {
                            model.columnSettings.ForEach(t =>
                            {
                                base.InsertObject(t, tran);
                            });
                        }

                        tran.Commit();
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
                        tran.Rollback();
                        logger.Error(ex, "修改單元設定異常，ex:" + ex.ToString().NewLineReplace());
                        return "系統異常，請通知資訊人員！";
                    }
                }
            }


        }

        public List<ColumnSetting> GetColumnSettings(string type, string MainID)
        {
            string sql = @"select * from ColumnSetting where Type=@Type and  MainID=@MainID and Used = 1 order by Sort";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Type", type));
            base.Parameter.Add(new SqlParameter("@MainID", MainID));
            return base.SearchList<ColumnSetting>(sql);
        }

        #endregion

        #region 前台

        public List<ADMain> GetAdMainByID(string ID)
        {
            string sql = @"select * from ADMain where Type_ID=@ID 
                        and  isnull(StDate,'1999/1/1') <= convert(date,GetDate()) 
                             and isnull(EdDate,'9999/12/31') >= convert(date, GetDate())
                             and Enabled = 1
                            order by Sort";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));

            return base.SearchList<ADMain>(sql);
        }

        public List<ADMain> GetAdMainByID(string ID, int lang)
        {
            string sql = @"select * from ADMain where Type_ID=@ID 
                        and  isnull(StDate,'1999/1/1') <= convert(date,GetDate()) 
                             and isnull(EdDate,'9999/12/31') >= convert(date, GetDate())
                             and Enabled = 1 and Lang_ID=@Lang_ID
                            order by Sort";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));
            base.Parameter.Add(new SqlParameter("@Lang_ID", lang));

            return base.SearchList<ADMain>(sql);
        }

        public ADKanban GetKanbanByID(string ID)
        {
            string sql = "select * from ADKanban where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));

            return base.GetObject<ADKanban>(sql) == null ? new ADKanban() : base.GetObject<ADKanban>(sql);
        }

        public ADKanban GetKanbanByID(string ID, string Lang)
        {
            string sql = "select * from ADKanban where Type_ID=@ID and Lang_ID=@Lang_ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));
            base.Parameter.Add(new SqlParameter("@Lang_ID", Lang));

            return base.GetObject<ADKanban>(sql) == null ? new ADKanban() : base.GetObject<ADKanban>(sql);
        }

        public ADMain GetOneAdMainByIDWithRnd(string ID)
        {
            string sql = @"
                            select * from 
                            (
                            select  *,NEWID() as RND from ADMain t
                            where t.Type_ID=@ID
                            ) s order by s.RND";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));

            return base.GetObject<ADMain>(sql);
        }

        public SEO GetSEOByTypeNameAndID(string typeName, string typeID = "")
        {
            string sql = @"select * from SEO where TypeName=@typeName";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@typeName", typeName));
            if (!typeID.IsNullOrEmpty())
            {
                sql += " and TypeID=@typeID";
                base.Parameter.Add(new SqlParameter("@typeID", typeID));
            }
            return base.GetObject<SEO>(sql);
        }
        public SiteConfig GetALLSiteConfig(string ID)
        {
            string sql = @"select * from SiteConfig where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));

            return base.GetObject<SiteConfig>(sql);
        }
        public SEO GetSEOByTypeNameAndID(string typeName, string Lang, string typeID = "")
        {
            string sql = @"select * from SEO where TypeName=@typeName and Lang_ID=@Lang_ID ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@typeName", typeName));
            base.Parameter.Add(new SqlParameter("@Lang_ID", Lang));
            if (!typeID.IsNullOrEmpty())
            {
                sql += " and TypeID=@typeID";
                base.Parameter.Add(new SqlParameter("@typeID", typeID));
            }
            return base.GetObject<SEO>(sql);
        }

        public string GetConnectionNetworkMid()
        {
            string sql = @"select t.ID from Menu t where t.ModelID =10";

            return base.ExecuteScalar(sql, "").ToString();
        }

        #endregion

        public int GetMaxID(string table, string columnName, string column2 = "", string columnValue = "", string column3 = "", string column3Value = "")
        {
            base.Parameter.Clear();
            string sql = $@"SELECT 
                            ISNULL( MAX(B.{columnName}),0)
                        FROM 
                        (SELECT 
                            ISNUMERIC({columnName})AS XX,convert(int,{columnName})  as {columnName}
                        FROM 
                            {table} 
                        WHERE                                 	
                            ISNUMERIC({columnName}) = 1 ";
            if (!string.IsNullOrEmpty(column2))
            {
                if (columnValue == "null")
                {
                    sql += $" and {column2} is null";
                }
                else
                {
                    sql += $" and isnull({column2},'') = @columnValue";
                    base.Parameter.Add(new SqlParameter("@columnValue", columnValue));
                }
            }
            if (!string.IsNullOrEmpty(column3))
            {
                if (column3Value == "null")
                {
                    sql += $" and isnull({column3},'') is null ";
                }
                else
                {
                    sql += $" and isnull({column3},'') = @column3Value";
                    base.Parameter.Add(new SqlParameter("@column3Value", column3Value));
                }
            }

            sql += @")
                        AS B";

            return int.Parse(base.ExecuteScalar(sql).ToString()) + 1;
        }

        public int GetMaxID(SqlTransaction tran, string table, string columnName, string column2 = "", string columnValue = "", string column3 = "", string column3Value = "")
        {
            string sql = $@"SELECT 
                            ISNULL( MAX(B.{columnName}),0)
                        FROM 
                        (SELECT 
                            ISNUMERIC({columnName})AS XX,convert(int,{columnName})  as {columnName}
                        FROM 
                            {table} 
                        WHERE                                 	
                            ISNUMERIC({columnName}) = 1 ";
            if (!string.IsNullOrEmpty(column2))
            {
                if (columnValue == "null")
                {
                    sql += $" and {column2} is null";
                }
                else
                {
                    sql += $" and isnull({column2},'') = '{columnValue}'";
                }


            }


            if (!string.IsNullOrEmpty(column3))
            {
                if (column3Value == "null")
                {
                    sql += $" and isnull({column3},'') is null ";
                }
                else
                {
                    sql += $" and isnull({column3},'') = '{column3Value}'";
                }
            }

            sql += @")
                        AS B";

            base.Parameter.Clear();

            return int.Parse(base.ExecuteScalar(sql).ToString()) + 1;
        }

        /// <summary>
        /// 新增時update Sort
        /// </summary>
        /// <param name="table"></param>
        /// <param name="langid"></param>
        /// <param name="tran"></param>
        public void UpdateSortByTableAndLang(string table, string langid, SqlTransaction tran)
        {
            string sql = $@"update {table} set Sort=Sort+1 where Lang_ID=@Lang_ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", langid));

            try
            {
                base.ExeNonQuery(sql, tran);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新sort異常，error:" + ex.ToString().NewLineReplace());
                throw ex;
            }
        }
        public void UpdateSortByTableAndLang(string table, string langid, int ModelID, SqlTransaction tran)
        {
            string sql = $@"update {table} set Sort=Sort+1 where Lang_ID=@Lang_ID and ModelID=@ModelID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", langid));
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            try
            {
                base.ExeNonQuery(sql, tran);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新sort異常，error:" + ex.ToString().NewLineReplace());
                throw ex;
            }
        }
        /// <summary>
        /// 新增時update Sort
        /// </summary>
        /// <param name="table"></param>
        /// <param name="langid"></param>
        /// <param name="tran"></param>
        public void UpdateSortByTableAndLang(SqlTransaction tran, string table, string langid, int Sort, string ParentID = "", string MenuLevel = "")
        {
            string sql = $@"update {table} set Sort=Sort-1 where LangID=@Lang_ID and Sort>@Sort";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", langid));
            base.Parameter.Add(new SqlParameter("@Sort", Sort));

            if (!ParentID.IsNullOrEmpty())
            {
                sql += " and ParentID=@ParentID";
                base.Parameter.Add(new SqlParameter("@ParentID", ParentID));
            }


            if (!MenuLevel.IsNullOrEmpty())
            {
                sql += " and MenuLevel=@MenuLevel";
                base.Parameter.Add(new SqlParameter("@MenuLevel", MenuLevel));
            }

            try
            {
                base.ExeNonQuery(sql, tran);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新sort異常，error:" + ex.ToString().NewLineReplace());
                throw ex;
            }
        }

        public Users GetUsersByID(string account)
        {
            string sql = "select * from Users where Account=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", account));

            return base.GetObject<Users>(sql);
        }
        public string UpdateItemSeq<T>(int id, int seq) where T : BaseItem, new()
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                T oldmodel = GetGeneral<T>("ID=@ID", new Dictionary<string, string>() { { "ID", id.ToString() } });
                if (oldmodel.ID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<T> mtseqdata = null;

                    mtseqdata = GetGeneralList<T>("Sort>@Sort"
                                                            , new Dictionary<string, string>() { { "Sort", seq.ToString() } })
                                                            .OrderBy(v => v.Sort)
                                                            .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        sql = $@"select * from {typeof(T).Name} ";


                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<T> ltseqdata = null;
                    ltseqdata = GetGeneralList<T>("Sort<=@Sort "
                                                                , new Dictionary<string, string>() { { "Sort", qseq.ToString() } })
                                                                .OrderBy(v => v.Sort)
                                                                .ToList();
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
                logger.Error(ex, $"更新{typeof(T).Name}排序失敗:" + ex.ToString().NewLineReplace());

                return "更新排序失敗:" + " error:" + ex.Message;
            }
        }
        public string UpdateSeq<T>(int id, int seq, string langid, string account, string username) where T : BaseModel, new()
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                T oldmodel = GetGeneral<T>("ID=@ID", new Dictionary<string, string>() { { "ID", id.ToString() } });
                if (oldmodel.ID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<T> mtseqdata = null;

                    mtseqdata = GetGeneralList<T>("Sort>@Sort and Lang_ID=@Lang_ID"
                                                                                  , new Dictionary<string, string>() { { "Sort", seq.ToString() }, { "Lang_ID", langid } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        sql = $@"select * from {typeof(T).Name} where Lang_ID=@Lang_ID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Lang_ID", langid));

                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<T> ltseqdata = null;
                    ltseqdata = GetGeneralList<T>("Sort<=@Sort and Lang_ID=@Lang_ID"
                                                                                  , new Dictionary<string, string>() { { "Sort", qseq.ToString() }, { "Lang_ID", langid } })
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
                logger.Error(ex, $"更新{typeof(T).Name}排序失敗:" + ex.ToString().NewLineReplace());

                return "更新排序失敗:" + " error:" + ex.Message;
            }
        }
        public string UpdateSeq<T>(int id, int seq, string langid, string account, string username, int ModelID) where T : BaseModel, new()
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                T oldmodel = GetGeneral<T>("ID=@ID", new Dictionary<string, string>() { { "ID", id.ToString() } });
                if (oldmodel.ID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<T> mtseqdata = null;

                    mtseqdata = GetGeneralList<T>("Sort>@Sort and Lang_ID=@Lang_ID and ModelID=@ModelID"
                                                                                  , new Dictionary<string, string>() { { "Sort", seq.ToString() }, { "Lang_ID", langid }, { "ModelID", ModelID.ToString() } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        sql = $@"select * from {typeof(T).Name} where Lang_ID=@Lang_ID and ModelID=@ModelID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Lang_ID", langid));
                        base.Parameter.Add(new SqlParameter("@ModelID", ModelID.ToString()));

                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<T> ltseqdata = null;
                    ltseqdata = GetGeneralList<T>("Sort<=@Sort and Lang_ID=@Lang_ID and ModelID=@ModelID "
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
                logger.Error(ex, $"更新{typeof(T).Name}排序失敗:" + ex.ToString().NewLineReplace());

                return "更新排序失敗:" + " error:" + ex.Message;
            }
        }
        public string GetTitle(string ItemID, int ModelID)
        {
            string sql = string.Empty;

            switch (ModelID)
            {
                case 2:
                    sql = @"select Title from MessageItem where ItemID=@ItemID ";
                    break;
                case 3:
                    sql = @"select Title from FileDownloadItem where ItemID=@ItemID ";
                    break;
                case 9:
                    sql = @"select Title from VideoItem where ItemID=@ItemID ";
                    break;
            }

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ItemID", ItemID));
            return base.ExecuteScalar(sql, "").ToString();
        }

        public VerifyData GetVerifyData(string ModelID, string ModelMainID, string ModelItemID)
        {
            string sql = @"select * from VerifyData where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));
            base.Parameter.Add(new SqlParameter("@ModelMainID", ModelMainID));
            base.Parameter.Add(new SqlParameter("@ModelItemID", ModelItemID));

            return base.GetObject<VerifyData>(sql);
        }

        public List<SelectListItem> GetAllGroupSelectListByParent(string Table, string mainid, int parent)
        {
            string sql = string.Empty;

            sql = $@"select * from { Table } where Main_ID=@Main_ID and isnull(ParentID,0) = @parent  Order By Sort";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Main_ID", mainid));
            base.Parameter.Add(new SqlParameter("@parent", parent));
            var list = base.SearchList<GroupBase>(sql);

            List<SelectListItem> item = new List<SelectListItem>();

            if (parent == 0)
            {
                item.Add(new SelectListItem() { Text = "無分類", Value = "0" });
            }

            foreach (var l in list)
            {
                item.Add(new SelectListItem() { Text = l.Group_Name, Value = l.ID.ToString() });
            }
            return item;
        }

        public SiteLayout GetSiteLayout()
        {
            return GetGeneral<SiteLayout>();
        }

        public SiteLayout GetSiteLayout(string Lang)
        {
            return GetGeneral<SiteLayout>("LangID=@LangID", new Dictionary<string, string>() { { "LangID", Lang } });
        }

        public Menu GetMenuByMid(string mid)
        {
            return GetGeneral<Menu>("ID=@ID", new Dictionary<string, string>() { { "ID", mid } });
        }

        public IList<Menu> GetMenu(string langID, string menuType, bool FrontDisplay)
        {
            string sql = string.Empty;
            base.Parameter.Clear();

            sql = @"select * from Menu t 
                    where Status=1 and LangID=@langID  
                    and MenuType=@MenuType and FrontDisplay=@FrontDisplay 
                    order by t.Sort";
            base.Parameter.Add(new SqlParameter("@MenuType", menuType));
            base.Parameter.Add(new SqlParameter("@langID", langID));
            base.Parameter.Add(new SqlParameter("@FrontDisplay", FrontDisplay ? 1 : 0));

            return base.SearchList<Menu>(sql);
        }

        public SiteFlow GetSiteFlow()
        {
            return GetGeneral<SiteFlow>();
        }

        public UnitSetting GetUnitSetting(string Table, string MainID)
        {
            switch (Table)
            {
                case "PageUnitSetting":
                    return GetGeneral<PageUnitSetting>("MainID=@MainID", new Dictionary<string, string>() { { "MainID", MainID } });
                case "MessageUnitSetting":
                    return GetGeneral<MessageUnitSetting>("MainID=@MainID", new Dictionary<string, string>() { { "MainID", MainID } });
                //case "FileDownloadUnitSetting":
                //    return GetGeneral<FileDownloadUnitSetting>("MainID=@MainID", new Dictionary<string, string>() { { "MainID", MainID } });
                case "VideoUnitSetting":
                    return GetGeneral<VideoUnitSetting>("MainID=@MainID", new Dictionary<string, string>() { { "MainID", MainID } });
                case "ProductUnitSetting":
                    return GetGeneral<ProductUnitSetting>("MainID=@MainID", new Dictionary<string, string>() { { "MainID", MainID } });
                default:
                    return GetGeneral<CommonUnitSetting>("MainID=@MainID", new Dictionary<string, string>() { { "MainID", MainID } });
            }

        }

        public void UpdateSeqAdd<T>(int Lang_ID, string SType, SqlTransaction tran, string Type_ID = "") where T : class
        {
            string sql = $"update {typeof(T).Name} set Sort = Sort +1 where Lang_ID=@Lang_ID and SType=@SType";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", Lang_ID));
            base.Parameter.Add(new SqlParameter("@SType", SType));
            if (!Type_ID.IsNullOrEmpty())
            {
                sql += " and Type_ID=@Type_ID ";
                base.Parameter.Add(new SqlParameter("@Type_ID", Type_ID));
            }

            try
            {
                base.ExeNonQuery(sql, tran);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"更新 {typeof(T).Name} sort異常，error:" + ex.ToString().NewLineReplace());
                throw ex;
            }
        }

        public List<Dictionary> GetDic(string dic, string parent = "")
        {
            string sql = "select * from Dictionary where Category=@dic";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@dic", dic));
            if (parent.IsNullOrEmpty() == false)
            {
                sql += " and ParentID=@parent";
                base.Parameter.Add(new SqlParameter("@parent", parent));
            }

            return base.SearchList<Dictionary>(sql);
        }

        public List<Dictionary> GetDicWithWatch(string dic, string column)
        {
            string sql = $@"select t.ID,t.Group_Name,t.Sort  
                            from Dictionary t where t.Category=@dic
                            and t.Enabled=1
                            union 
                            select {column},{column},99 from Product_Watch t
                            left join Dictionary s on t.{column} = s.ID
                            where s.ID is null
                            order by t.Sort";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@dic", dic));

            return base.SearchList<Dictionary>(sql);
        }

        public string GetDicValue(string dic, string value)
        {
            if (value.IsNullOrEmpty())
                return "";

            string sql = "select Group_Name from Dictionary where Category=@dic and ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@dic", dic));
            base.Parameter.Add(new SqlParameter("@ID", value));

            return base.ExecuteScalar(sql, value).ToString();
        }

        #region 多語系
        public string GetMulti(string LKey, string LangID)
        {
            string sql = @"select  t.Text from LangInputText t where 
                            t.LangTextID in
                            (select top 1 t.ID from LangKey t where t.LKey=@LKey)
                            and t.LangID=@LangID
                            ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@LKey", LKey));
            base.Parameter.Add(new SqlParameter("@LangID", LangID));

            return base.ExecuteScalar(sql, LKey).ToString();
        }
        public bool chkExist(string Key)
        {
            string sql = "select t.ID from LangKey t where t.LKey=@LKey";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@LKey", Key));

            return base.SearchCount(sql) > 0;
        }
        public void AddMultiKey(string Key)
        {
            LangKey LangKey = new LangKey();
            LangKey.GroupName = "BasicSetting";
            LangKey.SubGroup = 1;
            LangKey.Item = Key;
            LangKey.Sort = this.GetMaxID("LangKey", "Sort", "SubGroup", "1");
            LangKey.LKey = Key;
            LangKey.Used = true;

            base.InsertObject(LangKey);
        }
        #endregion

        #region Html Help
        public string GetFrontUrl(int ModelID)
        {
            string sql = "select isnull(M_Menu08,M_Menu04) from SET_MENU where M_Menu01=@ModelID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            return base.ExecuteScalar(sql, "").ToString();
        }
        #endregion
    }
}
