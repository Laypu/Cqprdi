using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using System.Data.SqlClient;
using Oaww.ViewModel;
using Oaww.ViewModel.Search;
using Oaww.Utility;
using System.Web;

namespace Oaww.Business
{
    public class SearchService : Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();
        public List<Menu> GetMenus(string MenuLevel, string MenuType)
        {
            string sql = "select * from Menu where MenuLevel=@MenuLevel and MenuType=@MenuType";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@MenuLevel", MenuLevel));
            base.Parameter.Add(new SqlParameter("@MenuType", MenuType));

            return base.SearchList<Menu>(sql);
        }

        public List<Menu> GetMenusByParent(string MenuLevel, string ParentID)
        {
            string sql = "select * from Menu where MenuLevel=@MenuLevel and ParentID=@ParentID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@MenuLevel", MenuLevel));
            base.Parameter.Add(new SqlParameter("@ParentID", ParentID));

            return base.SearchList<Menu>(sql);
        }

        public PageIndexSetting GetPageIndexSetting()
        {
            return _commonService.GetGeneral<PageIndexSetting>();
        }

        public Paging<SearchViewModel> GetPaging(AdvanceSearchModel model,string Lang)
        {
            string sql = string.Empty;

            sql = @"select ID from   menu  where LinkMode = 2 and MenuType= 1";

            if (model.MenuType.IsNullOrEmpty() == false)
            {
                sql += " and MenuType=@MenuType";
                base.Parameter.Add(new SqlParameter("@MenuType", model.MenuType));
            }

            if (model.Menu1.IsNullOrEmpty() == false && model.Menu2.IsNullOrEmpty() && model.Menu3.IsNullOrEmpty())
            {
                sql += @" and (ID=@ID 
                              or ID in (
                                        select t.ID from Menu t 
                                        left join Menu s on t.ParentID = s.ID
                                        where (s.ParentID=@ID or t.ParentID=@ID )
                                       )
                           )";
                base.Parameter.Add(new SqlParameter("@ID", model.Menu1));
            }

            if (model.Menu3.IsNullOrEmpty() && model.Menu2.IsNullOrEmpty() == false && model.Menu1.IsNullOrEmpty() == false)
            {
                sql += @" and (ParentID=@ID or ID=@ID )";
                base.Parameter.Add(new SqlParameter("@ID", model.Menu2));
            }

            if (model.Menu3.IsNullOrEmpty() == false && model.Menu2.IsNullOrEmpty() == false && model.Menu1.IsNullOrEmpty() == false)
            {
                sql += @" and ID=@ID ";
                base.Parameter.Add(new SqlParameter("@ID", model.Menu3));
            }

            List<string> listID = base.SearchList<string>(sql);


            sql = @"with cte as
                            (
                            select   t.ModelID,t.ItemID,'Page/Index' as URL,1 as ModuleID,t.ItemName as Title,t.HtmlContent as Content,t.CreateDatetime 
							from PageIndexItem t
                            where t.IsVerift =1 and t.Enabled =1   and t.Lang_ID=@Lang_ID
                            union all
                            select  t.ModelID,t.ItemID,a.M_MESSAGE06+'/'+ a.M_MESSAGE14,s.ModelID,t.Title,t.HtmlContent,t.CreateDatetime 
							from MessageItem t
							left join ModelMessageMain s on s.ID = t.ModelID
							left join SET_MESSAGE a on s.ModelID = a.M_MESSAGE01
                            where t.IsVerift = 1 and t.Enabled =1 and t.Lang_ID=@Lang_ID
                                and  isnull(t.StDate,'1999/1/1') <= convert(date,GetDate()) 
                                and isnull(t.EdDate,'9999/12/31') >= convert(date,GetDate()) 
                                and a.M_MESSAGE16 = 1
                            union all
                            select  t.ModelID,t.ItemID,'Video/VideoView',9,t.Title,t.HtmlContent ,t.CreateDatetime
							from VideoItem t
                            where t.IsVerift = 1 and t.Enabled =1 and t.Lang_ID=@Lang_ID
                                and  isnull(t.StDate,'1999/1/1') <= convert(date,GetDate()) 
                                and isnull(t.EdDate,'9999/12/31') >= convert(date,GetDate())                             
                            )
                            select a.M_Menu02 as ModuleName, s.*,t.MenuType,t.MenuLevel,t.ID,t.ParentID from Menu t
                            inner  join cte  s on t.ModelID  = s.ModuleID and t.ModelItemID = s.ModelID		
							left join SET_MENU a on t.ModelID = a.M_Menu01					
                            where t.Status = 1";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", Lang));
            if (listID.Count() > 0)
            {
                sql += " and t.ID in (select ListItem from dbo.SplitList(',',@listID) )";
                base.Parameter.Add(new SqlParameter("@listID", string.Join(",", listID.ToArray())));
            }

            if (model.Key.IsNullOrEmpty() == false && model.Key2.IsNullOrEmpty() == false && model.Key3.IsNullOrEmpty() == false)
            {
                if (model.SearchType == "1")
                {
                    sql += $@" and (( Title like @Key {model.Sel1} Title like @Key2 {model.Sel2} Title like @Key3 )
                               or 
                                ( Content like @Key {model.Sel1} Content like @Key2 {model.Sel2} Content like @Key3 ))";
                }
                else if (model.SearchType == "2")
                {
                    sql += $@" and ( Title like @Key {model.Sel1} Title like @Key2 {model.Sel2} Title like @Key3 )";
                }
                else if (model.SearchType == "3")
                {
                    sql += $@" and ( Content like @Key {model.Sel1} Content like @Key2 {model.Sel2} Content like @Key3 )";
                }

                base.Parameter.Add(new SqlParameter("@Key", "%" + model.Key.Replace("%", "[%]") + "%"));
                base.Parameter.Add(new SqlParameter("@Key2", "%" + model.Key2.Replace("%", "[%]") + "%"));
                base.Parameter.Add(new SqlParameter("@Key3", "%" + model.Key3.Replace("%", "[%]") + "%"));
            }

            if (model.Key.IsNullOrEmpty() == false && model.Key2.IsNullOrEmpty() == false && model.Key3.IsNullOrEmpty())
            {
                if (model.SearchType == "1")
                {
                    sql += $@" and (( Title like @Key {model.Sel1} Title like @Key2  )
                               or 
                                ( Content like @Key {model.Sel1} Content like @Key2  ))";
                }
                else if (model.SearchType == "2")
                {
                    sql += $@" and ( Title like @Key {model.Sel1} Title like @Key2  )";
                }
                else if (model.SearchType == "3")
                {
                    sql += $@" and ( Content like @Key {model.Sel1} Content like @Key2  )";
                }

                base.Parameter.Add(new SqlParameter("@Key", "%" + model.Key.Replace("%", "[%]") + "%"));
                base.Parameter.Add(new SqlParameter("@Key2", "%" + model.Key2.Replace("%", "[%]") + "%"));
            }

            if (model.Key.IsNullOrEmpty() == false && model.Key2.IsNullOrEmpty() && model.Key3.IsNullOrEmpty())
            {
                if (model.SearchType == "1")
                {
                    sql += $@" and (( Title like @Key   )
                               or 
                                ( Content like @Key   ))";
                }
                else if (model.SearchType == "2")
                {
                    sql += $@" and ( Title like @Key   )";
                }
                else if (model.SearchType == "3")
                {
                    sql += $@" and ( Content like @Key )";
                }

                base.Parameter.Add(new SqlParameter("@Key", "%" + model.Key.Replace("%", "[%]") + "%"));
            }

            Paging<SearchViewModel> paging = new Paging<SearchViewModel>();
            paging.total = base.Search(sql).Rows.Count;
            paging.rows = base.SearchListPage<SearchViewModel>(sql, model.Offset, model.Limit, "order by CreateDatetime");

            paging.rows.ForEach(t =>
            {
                t.Title = HttpUtility.HtmlDecode(t.Title);
                t.Content = HttpUtility.HtmlDecode(t.Content);
            });

            return paging;
        }
    }
}
