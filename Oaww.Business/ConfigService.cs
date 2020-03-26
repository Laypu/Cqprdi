using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.Utility;
using Oaww.ViewModel;
using Oaww.ViewModel.Lang;
using System.Data.SqlClient;
using Oaww.ViewModel.Search;
using System.Security;
using System.Web.Mvc;


namespace Oaww.Business
{
    public class ConfigService : BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();
        MenuService _menuService = new MenuService();

        #region　流量分析
        public SiteFlow GetSiteFlow()
        {
            return _commonService.GetGeneral<SiteFlow>();
        }

        public bool UpdateSiteFlow(string code, string path, string id)
        {
            try
            {
                if (id == "")
                {
                    SiteFlow siteFlow = new SiteFlow()
                    {
                        Siteflow_Code = code,
                        Siteflow_Link = path
                    };

                    return base.InsertObject(siteFlow) > 0;
                }
                else
                {
                    SiteFlow siteFlow = new SiteFlow()
                    {
                        Siteflow_Code = code,
                        Siteflow_Link = path,
                        ID = int.Parse(id)
                    };

                    return base.UpdateObject(siteFlow);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新SiteFlow異常，error:" + ex.ToString().NewLineReplace());

                return false;
            }
        }

        #endregion

        #region UserList 
        public Paging<AdminMemberListResult> UserPaging(AuthoritySearchModel model, string account)
        {
            var Paging = new Paging<AdminMemberListResult>();
            Paging.rows = new List<AdminMemberListResult>();
            var onepagecnt = model.Limit;
            List<Users> data = new List<Users>();
            var grouplist = _commonService.GetGeneralList<GroupUser>();

            string sql = @"select * from Users  where  1=1 ";

            if (account != "admin")
            {
                sql += " and Account != 'admin'";
            }

            if (model.GroupId.IsNullOrEmpty() == false)
            {
                sql += " and Group_ID=@Group_ID";
                base.Parameter.Add(new SqlParameter("@Group_ID", model.GroupId));
            }

            if (model.Name.IsNullOrEmpty() == false)
            {
                sql += " and User_Name like @Name";
                base.Parameter.Add(new SqlParameter("@Name", model.Name));
            }

            if (model.Account.IsNullOrEmpty() == false)
            {
                sql += " and Account like @Account";
                base.Parameter.Add(new SqlParameter("@Account", model.Account));
            }

            if (model.Status.IsNullOrEmpty() == false)
            {
                sql += " and Enabled = @Status";
                base.Parameter.Add(new SqlParameter("@Status", model.Status));
            }

            Paging.total = base.SearchCount(sql);


            data = base.SearchListPage<Users>(sql, model.Offset, model.Limit, " order by " + model.Sort);

            foreach (var d in data)
            {
                var td = grouplist.Where(v => v.ID == d.Group_ID);
                Paging.rows.Add(new AdminMemberListResult()
                {
                    Account = d.Account,
                    GroupId = d.Group_ID,
                    GroupName = td.Count() > 0 ? td.First().Group_Name : "",
                    ID = d.ID.ToString(),
                    Email = d.User_Email,
                    Name = d.User_Name,
                    Status = d.Enabled.Value,
                    Readonly = d.Readonly.Value
                });
            }
            return Paging;
        }

        public string UpdateStatus(string id, bool status, string account, string username)
        {
            try
            {
                string sql = "update Users set Enabled=@Enabled,UpdateDatetime=GetDate(),UpdateUser=@UpdateUser where ID=@ID";
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

                return "更新失敗";
            }
        }

        public string UserDelete(string[] idlist, string delaccount, string account, string username)
        {
            string sql = string.Empty;

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {


                        var r = 0;
                        for (var idx = 0; idx < idlist.Length; idx++)
                        {
                            var users = _commonService.GetGeneralList<Users>("Group_ID=@Group_ID", new Dictionary<string, string>() { { "Group_ID", idlist[idx] } }, tran).ToList();
                            if (users.Count() > 0)
                            {
                                var gname = _commonService.GetGeneral<GroupUser>("ID=@ID", new Dictionary<string, string>() { { "ID", idlist[idx] } }, tran).Group_Name;
                                return "群組名稱:" + gname + " 目前有所屬會員,無法刪除";
                            }

                            sql = "delete Users  where ID=@ID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ID", idlist[idx]));
                            base.ExeNonQuery(sql, tran);
                        }
                        var rstr = "";
                        if (r >= 0)
                        {

                            rstr = "刪除成功";
                        }
                        else
                        {
                            rstr = "刪除失敗";
                        }
                        var alldata = _commonService.GetGeneralList<GroupUser>().OrderBy(v => v.Sort).ToArray();
                        for (var idx = 1; idx <= alldata.Count(); idx++)
                        {
                            var tempmodel = alldata[idx - 1];
                            tempmodel.Sort = idx;
                            base.UpdateObject(tempmodel, tran);
                        }
                        tran.Commit();

                        return rstr;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return "刪除失敗";
                    }
                }
            }
        }

        public AdminMemberModel GetAdminMemberModelByID(string ID)
        {
            var model = new AdminMemberModel();
            var AdminMember = _commonService.GetGeneral<Users>("ID=@ID", new Dictionary<string, string>() { { "ID", ID } });
            if (AdminMember != null)
            {
                model.Account = AdminMember.Account;
                model.GroupId = AdminMember.Group_ID;
                model.ID = AdminMember.ID;
                model.Password = AdminMember.PWD;
                model.Name = AdminMember.User_Name;
            }
            return model;
        }

        public int UpdateUser(AdminMemberModel entity, string account, string username)
        {
            var r = -1;
            try
            {
                var oldmodel = _commonService.GetGeneral<Users>("ID=@ID", new Dictionary<string, string>() { { "ID", entity.ID.ToString() } });

                if (entity.Password.IsNullOrEmpty() == false)
                {
                    oldmodel.PWD = entity.Password;
                }

                oldmodel.Account = entity.Account;
                oldmodel.User_Name = entity.Name;
                oldmodel.Group_ID = entity.GroupId;

                r = base.UpdateObject(oldmodel) ? 1 : 0;

            }
            catch (Exception ex)
            {

            }

            return r;
        }

        public int CreateUser(AdminMemberModel entity, string account, string username)
        {
            var r = -1;
            try
            {

                var oldmodel = new Users();
                oldmodel.PWD = entity.Password;
                oldmodel.Account = entity.Account;
                oldmodel.User_Name = entity.Name;
                oldmodel.Group_ID = entity.GroupId;
                oldmodel.Readonly = false;
                oldmodel.User_Email = "";
                oldmodel.Enabled = true;
                r = (int)base.InsertObject(oldmodel);

            }
            catch (Exception ex)
            {

            }

            return r;
        }
        #endregion

        #region GroupList
        public Paging<UserGroupListResult> Paging(SearchModelBase model,string account)
        {
            var Paging = new Paging<UserGroupListResult>();
            Paging.rows = new List<UserGroupListResult>();
            var onepagecnt = model.Limit;
            var whereobj = new List<string>();
            var wherestr = new List<string>();

            string sql = string.Empty;

            sql = @"select 
                    t.ID,t.Site_ID,t.Sort,t.Group_Name,t.Seo_Manage,
                    case when t.ID = (select  t.Group_ID from Users t where t.Account = @account ) then 1 else t.Readonly end as Readonly ,
                      t.Enabled,t.UpdateDatetime,t.UpdateUser
                      from GroupUser t where 1=1 ";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@account", account));

            if (account != "admin")
            {
                sql += " and ID != 'administrators' ";
            }

            if (model.Search.IsNullOrEmpty() == false)
            {
                sql += " and Group_Name like @Group_Name";
                base.Parameter.Add(new SqlParameter("@Group_Name", "%" + model.Search + "%"));
            }

            var users = _commonService.GetGeneralList<Users>();

            var data = base.SearchListPage<GroupUser>(sql, model.Offset, model.Limit, " order by " + model.Sort);


            foreach (var d in data)
            {
                Paging.rows.Add(new UserGroupListResult()
                {
                    Enabled = d.Enabled.Value,
                    Group_Name = d.Group_Name,
                    ID = d.ID,
                    Readonly = d.Readonly.Value,
                    Seo_Manage = d.Seo_Manage,
                    Site_ID = d.Site_ID,
                    Sort = d.Sort,
                    UserCount = users.Count(v => v.Group_ID == d.ID)
                });
            }
            Paging.total = base.SearchCount(sql);
            return Paging;
        }

        public AdminFunctionModel GetAdminFunctionModel(string groupid, string langid,string account)
        {
            if (string.IsNullOrEmpty(groupid)) { groupid = "-1"; }
            var input = _commonService.GetGeneralList<AdminFunctionAuth>("GroupID=@GroupID and LangID=@LangID", new Dictionary<string, string>() { { "GroupID", groupid }, { "LangID", langid } });

            List<AdminFunction> data = new List<AdminFunction>();
            if(account.ToLower() == "admin")
            {
                data = _commonService.GetGeneralList<AdminFunction>().ToList();
            }
            else
            {
                string sql = @"select s.* from AdminFunctionAuth t
                                inner join AdminFunction s on t.ItemID = s.ID
                                where t.GroupID =
                                (select t.Group_ID from Users t where t.Account =@account)
                                and t.Type ='0' and t.LangID=@langid ";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@account", account));
                base.Parameter.Add(new SqlParameter("@langid", langid));

                data = base.SearchList<AdminFunction>(sql);

            }
            var model = new AdminFunctionModel();

            if (groupid != "-1")
            {
                var role = _commonService.GetGeneral<GroupUser>("ID=@ID", new Dictionary<string, string>() { { "ID", groupid } });

                model.GroupID = groupid;
                model.GroupName = role.Group_Name;
            }

            model.AdminFixFunctionInput = input.Where(v => v.Type == 0).ToList();
            model.AdminMenuFunctionInput = input.Where(v => v.Type == 1).ToList();
            if (data.Count() > 0)
            {
                var group = data.GroupBy(v => v.GroupID.Value);
                foreach (var g in group)
                {
                    model.AdminFunctionList.Add(g.Key, g.ToList());
                }
            }

            var menu = _commonService.GetGeneralList<Menu>("Status=1 and LangID=@LangID", new Dictionary<string, string>() { { "LangID", langid } });

            model.MenuList = menu.OrderBy(v => v.MenuLevel).ThenBy(v => v.Sort).ToList();

            return model;
        }

        public string GroupAuthSave(string languageID, string groupid, Dictionary<string, string> inputdata
                                    , string groupname, string account, string IsInsert)
        {
            var r = 0;
            string sql = string.Empty;

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        if (IsInsert == "1")
                        {
                            var maxcount = _commonService.GetMaxID("GroupUser", "Sort");

                            var model = new GroupUser()
                            {
                                ID = groupid,
                                Enabled = true,
                                Group_Name = groupname,
                                Readonly = false,
                                Seo_Manage = true,
                                Sort = maxcount,
                                UpdateDatetime = DateTime.Now,
                                UpdateUser = account
                            };

                            base.InsertObject(model, tran);
                        }
                        else
                        {
                            sql = @"update GroupUser set Group_Name=@Group_Name,UpdateDatetime=GetDate(),UpdateUser=@UpdateUser 
                                    where ID=@ID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@ID", groupid));
                            base.Parameter.Add(new SqlParameter("@Group_Name", groupname));
                            base.Parameter.Add(new SqlParameter("@UpdateUser", account));
                            base.ExeNonQuery(sql, tran);
                        }



                        //先刪除
                        sql = "delete AdminFunctionAuth where GroupID=@GroupID AND LangID=@LangID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@GroupID", groupid));
                        base.Parameter.Add(new SqlParameter("@LangID", 1));
                        base.ExeNonQuery(sql, tran);

                        var functiondata = _commonService.GetGeneralList<AdminFunction>(tran);

                        foreach (var key in inputdata.Keys)
                        {
                            var karr = key.Split('_');
                            if (inputdata[key] == "true")
                            {

                                var type = karr[0];
                                if (type == "fix")
                                {
                                    var fdata = functiondata.Where(v => v.ID == int.Parse(karr[1]));
                                    base.InsertObject(new AdminFunctionAuth()
                                    {
                                        GroupID = groupid,
                                        ItemID = int.Parse(karr[1]),
                                        LangID = int.Parse(languageID),
                                        Type = 0,
                                        GID = fdata.Count() > 0 ? fdata.First().GroupID.Value : 0
                                    }, tran);
                                }
                                else if (type == "menu")
                                {
                                    base.InsertObject(new AdminFunctionAuth()
                                    {
                                        GroupID = groupid,
                                        ItemID = int.Parse(karr[1]),
                                        LangID = int.Parse(languageID),
                                        Type = 1,
                                        GID = 0
                                    }, tran);
                                }

                            }
                        }

                        tran.Commit();

                        return "存檔成功";
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();

                        return "存檔失敗";
                    }
                }
            }
        }

        public string UpdateSeq(string id, int seq, string account, string username)
        {
            try
            {
                if (seq <= 0) { seq = 1; }
                var oldmodel = _commonService.GetGeneral<GroupUser>("ID=@ID", new Dictionary<string, string>() { { "ID", id.ToString() } });
                var diff = "";

                diff = diff.TrimStart(',');
                var r = 0;
                if (oldmodel.Sort != seq)
                {
                    IList<GroupUser> mtseqdata = null;
                    mtseqdata = _commonService.GetGeneralList<GroupUser>("Sort>@Sort", new Dictionary<string, string>() { { "Sort", seq.ToString() } }).OrderBy(v => v.Sort).ToArray();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        totalcnt = _commonService.GetGeneralList<GroupUser>().Count;
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<GroupUser> ltseqdata = null;
                    ltseqdata = _commonService.GetGeneralList<GroupUser>("Sort<=@Sort", new Dictionary<string, string>() { { "Sort", seq.ToString() } }).OrderBy(v => v.Sort).ToArray();
                    //更新seq+1
                    var sidx = 0;
                    for (var idx = 1; idx <= ltseqdata.Count(); idx++)
                    {
                        if (idx == seq && seq < oldmodel.Sort)
                        {
                            sidx += 1;
                        }
                        var tempmodel = ltseqdata[idx - 1];
                        if (tempmodel.ID == id.ToString())
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
                        base.UpdateObject(tempmodel);
                    }
                }
                oldmodel.Sort = seq;
                if (base.UpdateObject(oldmodel))
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
                logger.Debug(ex, "更新群組排序失敗:" + " error:" + ex.ToString());

                return "更新群組排序失敗:" + " error:" + ex.Message;
            }
        }

        public string SetGroupStatus(string id, bool status)
        {
            string sql = "update GroupUser set Enabled=@Enabled where ID=@id";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@id", id));
            base.Parameter.Add(new SqlParameter("@Enabled", status ? 1 : 0));

            try
            {
                base.ExeNonQuery(sql);
                return "更新成功";
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "更新狀態失敗,error:" + ex.ToString().NewLineReplace());
                return "更新失敗";
            }
        }

        public string SetGroupDelete(string[] idlist)
        {
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        string sql = "delete GroupUser where ID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);                        

                        tran.Commit();

                        return "刪除成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex, "刪除群組失敗,error:" + ex.ToString().NewLineReplace());
                        return "刪除失敗";
                    }
                }
            }

        }

        public bool CheckGroupExist(string[] idlist)
        {
            string sql = "select * from Users where Group_ID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));

            return base.SearchCount(sql) > 0;
        }
        #endregion

        #region 審核
        public Paging<VerifyDataResult> PagingVerify(ConfigSearchModel model, bool IsAdmin)
        {
            var Paging = new Paging<VerifyDataResult>();
            var onepagecnt = model.Limit;
            var whereobj = new List<string>();
            var wherestr = new List<string>();


            string sql = @"select t.*,s.M_Menu02 as ModuleName from VerifyData t
                            left join SET_MENU s on t.ModelID= s.M_Menu01
                            where LangID=@LangID ";
            base.Parameter.Clear();



            base.Parameter.Add(new SqlParameter("@LangID", model.LangId));


            if (model.Key.IsNullOrEmpty() == false)
            {
                sql += " and VerifyStatus=@VerifyStatus ";
                base.Parameter.Add(new SqlParameter("@VerifyStatus", model.Key));
            }

            if (model.ModuleName.IsNullOrEmpty() == false)
            {
                sql += " and ModelID=@ModelID ";
                base.Parameter.Add(new SqlParameter("@ModelID", model.ModuleName));
            }

            if (model.PublicshDateFrom.IsNullOrEmpty() == false)
            {
                sql += " and convert(date,UpdateDateTime) >=@PublicshDateFrom ";
                base.Parameter.Add(new SqlParameter("@PublicshDateFrom", model.PublicshDateFrom));
            }

            if (model.PublicshDateTo.IsNullOrEmpty() == false)
            {
                sql += " and convert(date,UpdateDateTime) <=@PublicshDateTo ";
                base.Parameter.Add(new SqlParameter("@PublicshDateTo", model.PublicshDateTo));
            }

            if (model.Title.IsNullOrEmpty() == false)
            {
                sql += " and ModelName like @ModelName ";
                base.Parameter.Add(new SqlParameter("@ModelName", "%" + model.Title + "%"));
            }

            if (model.Empolyee.IsNullOrEmpty() == false)
            {
                sql += " and UpdateUser like @VerifyName ";
                base.Parameter.Add(new SqlParameter("@VerifyName", "%" + model.Empolyee + "%"));
            }

            if (model.ChangeStatus.IsNullOrEmpty() == false)
            {
                sql += " and ModelStatus = @ModelStatus ";
                base.Parameter.Add(new SqlParameter("@ModelStatus", model.ChangeStatus));
            }

            Paging.total = base.SearchCount(sql);

            var data = base.SearchListPage<VerifyData>(sql, model.Offset, model.Limit, " order by " + model.Sort);

            foreach (var d in data)
            {
                Paging.rows.Add(new VerifyDataResult()
                {
                    Name = d.ModuleName,
                    Status = d.VerifyStatus == 0 ? "審核中" : (d.VerifyStatus == 1 ? "已通過" : "未通過"),
                    Title = d.ModelName,
                    UpdateStatus = d.ModelStatus == 1 ? "新增" : (d.ModelStatus == 2 ? "修改" : "刪除"),
                    UpdateTime = d.UpdateDateTime == null ? "" : d.UpdateDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss"),
                    UpdateUser = d.UpdateUser,
                    TitleLink = "<a href='#'  modid='" + d.ModelID + "'  mainid='" + d.ModelMainID + "' itemid='" + d.ModelItemID + "' class='verifyview'>" + d.ModelName + "</a>",
                    OptionHtml =
                    "<button class='btn blue verifyok' value='" + d.ModelID + "_" + d.ModelMainID + "_" + d.ModelItemID + "'>通過</button> " +
                    "<button class='btn grey-mint verifyrefuse' value='" + d.ModelID + "_" + d.ModelMainID + "_" + d.ModelItemID + "'>未通過</button>"
                });
            }
            return Paging;
        }

        public string SetVerifyOK(string id, string account)
        {
            if (string.IsNullOrEmpty(id)) { return "設定失敗,查無設定資料"; }
            var admin = _commonService.GetUsersByID(account);
            var idarr = id.Split('_');
            if (idarr.Length < 3) { return "設定失敗,資料來源不足"; }

            string sql = string.Empty;

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        sql = @"update VerifyData set VerifyStatus=1,VerifyDateTime=GetDate(),VerifyUser=@VerifyUser,VerifyName=@VerifyName
                                       where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";

                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@VerifyUser", account));
                        base.Parameter.Add(new SqlParameter("@VerifyName", admin.User_Name));
                        base.Parameter.Add(new SqlParameter("@ModelID", idarr[0]));
                        base.Parameter.Add(new SqlParameter("@ModelMainID", idarr[1]));
                        base.Parameter.Add(new SqlParameter("@ModelItemID", idarr[2]));
                        base.ExeNonQuery(sql, tran);

                        VerifyData verifyData = GetVerifyData(idarr[0], idarr[1], idarr[2], tran);

                        if (idarr[0] == "1")
                        {
                            UpdateVerify<PageIndexItem>(idarr[1], idarr[2], tran);
                        }
                        else if (_menuService.ChkModelInMessage(idarr[0]))
                        {
                            UpdateVerify<MessageItem>(idarr[1], idarr[2], tran);
                        }
                        else if (idarr[0] == "9")
                        {
                            UpdateVerify<VideoItem>(idarr[1], idarr[2], tran);
                        }

                        tran.Commit();

                        return "設定完成";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, $"審核失敗，id:{id}, error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();

                        return "設定失敗";
                    }
                }
            }
        }

        public string SetVerifyRefuse(string id, string account)
        {
            if (string.IsNullOrEmpty(id)) { return "設定失敗,查無設定資料"; }
            var admin = _commonService.GetUsersByID(account);
            var idarr = id.Split('_');
            if (idarr.Length < 3) { return "設定失敗,資料來源不足"; }

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    VerifyData verifyData = GetVerifyData(idarr[0], idarr[1], idarr[2], tran);

                    string sql = @"update VerifyData set VerifyStatus=2
                                                    ,VerifyDateTime=GetDate(),VerifyUser=@VerifyUser,VerifyName=@VerifyName
                           where ModelID=@ModelID and ModelMainID=@ModelMainID and ModelItemID=@ModelItemID";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@VerifyUser", account));
                    base.Parameter.Add(new SqlParameter("@VerifyName", admin.User_Name));
                    base.Parameter.Add(new SqlParameter("@ModelID", idarr[0]));
                    base.Parameter.Add(new SqlParameter("@ModelMainID", idarr[1]));
                    base.Parameter.Add(new SqlParameter("@ModelItemID", idarr[2]));

                    try
                    {
                        var r = base.ExeNonQuery(sql, tran);

                        if (r > 0)
                        {
                            tran.Commit();
                            return "設定完成";
                        }
                        else
                        {
                            tran.Rollback();
                            return "設定失敗";
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "審核失敗異常，error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        return "設定失敗";
                    }
                }
            }


        }

        public VerifyData GetVerifyData(string ModelID, string ModelMainID, string ModelItemID, SqlTransaction tran)
        {
            string sql = @"select * from VerifyData t
                            where t.ModelID =@ModelID and t.ModelMainID = @ModelMainID and t.ModelItemID = @ModelItemID ";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));
            base.Parameter.Add(new SqlParameter("@ModelMainID", ModelMainID));
            base.Parameter.Add(new SqlParameter("@ModelItemID", ModelItemID));

            return base.GetObject<VerifyData>(sql, tran);
        }

        public void UpdateVerify<T>(string ModelID, string ItemID, SqlTransaction tran) where T : class
        {
            string sql = $@"update {typeof(T).Name} set IsVerift=1 where ModelID=@ModelID and ItemID=@ItemID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));
            base.Parameter.Add(new SqlParameter("@ItemID", ItemID));

            try
            {
                base.ExeNonQuery(sql, tran);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新Item狀態異常，error:" + ex.ToString().NewLineReplace());
                throw ex;
            }
        }
        #endregion

        #region 變更密碼
        public string UpdatePassword(string id, SecureString newPassword, string account)
        {
            string sql = "update Users set PWD=@PWD,UpdateDatetime=GetDate(),UpdateUser=@account where ID=@id";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@id", id));
            base.Parameter.Add(new SqlParameter("@PWD", newPassword.SecureStringToString().EncodePassword()));
            base.Parameter.Add(new SqlParameter("@account", account));
            try
            {
                base.ExeNonQuery(sql);
                return "更新成功";
            }
            catch
            {
                return "更新失敗";
            }
        }
        #endregion

        #region 多語系
        public IList<SelectListItem> GetSelectList()
        {
            string sql = "select * from Lang where Enabled = 1 and Published=1 and Deleted=0";

            var list = base.SearchList<Lang>(sql);

            IList<System.Web.Mvc.SelectListItem> item = new List<System.Web.Mvc.SelectListItem>();
            foreach (var l in list)
            {
                item.Add(new System.Web.Mvc.SelectListItem() { Text = l.Disp_Name, Value = l.ID.ToString() });
            }
            return item;
        }

        public SiteLangModel GetModelById(string id)
        {
            var model = new SiteLangModel();
            var data = _commonService.GetHisEntity<Lang>("ID", id);

            model.ID = data.ID;
            model.Lang_Name = data.Lang_Name;
            model.Disp_Name = data.Disp_Name;
            model.Sub_Domain_Name = data.Sub_Domain_Name;
            model.Domain_Type = data.Domain_Type;
            model.Link_Lang_ID = data.Link_Lang_ID.Value;
            model.Link_Href = data.Link_Href;

            return model;
        }

        public Paging<Lang> PagingLang(SearchModelBase model)
        {
            string sql = string.Empty;

            var Paging = new Paging<Lang>();
            var onepagecnt = model.Limit;

            sql = "select * from Lang where Deleted=0";
            base.Parameter.Clear();
            if (model.Search.IsNullOrEmpty() == false)
            {
                sql += " and Lang_Name like @lang";
                base.Parameter.Add(new SqlParameter("@lang", "%" + model.Search + "%"));
            }


            Paging.rows = base.SearchListPage<Lang>(sql, model.Offset, model.Limit, " order by " + model.Sort);
            Paging.total = base.SearchCount(sql);

            return Paging;

        }

        public string UpdateLangSeq(int id, int seq, string account, string username)
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                Lang oldmodel = _commonService.GetGeneral<Lang>("ItemID=@ItemID", new Dictionary<string, string>() { { "ID", id.ToString() } });
                if (oldmodel.ID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<Lang> mtseqdata = null;

                    mtseqdata = _commonService.GetGeneralList<Lang>("Sort>@Sort"
                                                                                  , new Dictionary<string, string>() { { "Sort", seq.ToString() } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        sql = $@"select * from Lang ";
                        base.Parameter.Clear();


                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<Lang> ltseqdata = null;
                    ltseqdata = _commonService.GetGeneralList<Lang>("Sort<=@Sort "
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
                logger.Error(ex, $"更新 Lang 排序失敗:" + " error:" + ex.ToString().NewLineReplace());
                return "更新排序失敗:" + " error:" + ex.Message;
            }
        }

        public string DeleteLang(string[] idlist, string delaccount, string account, string username)
        {
            try
            {
                string sql = "update Lang set Deleted=1 where ID in (select ListItem from dbo.SplitList(',',@idlist))";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                base.ExeNonQuery(sql);
                return "刪除成功";
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "刪除多語系失敗，Error:" + ex.ToString().NewLineReplace());

                return "刪除失敗";
            }
        }

        public int CreateLang(SiteLangModel model, string account)
        {
            var alldata = _commonService.GetGeneralList<Lang>("Deleted=0");
            int maxsort = 1;
            if (alldata.Count > 0)
                maxsort = alldata.Max(v => v.Sort) ?? 0 + 1;

            var r = base.InsertObject(new Lang()
            {
                Lang_Name = model.Lang_Name,
                Disp_Name = model.Disp_Name,
                Sub_Domain_Name = model.Sub_Domain_Name,
                Domain_Type = model.Domain_Type,
                Link_Lang_ID = model.Link_Lang_ID,
                Link_Href = model.Link_Href,
                UpdateDatetime = DateTime.Now,
                UpdateUser = account,
                Enabled = true,
                Deleted = false,
                Published = false,
                Sort = maxsort
            });
            return (int)r;
        }

        public int UpdateLang(SiteLangModel model, string account)
        {

            var Lang = _commonService.GetGeneral<Lang>("ID=@ID", new Dictionary<string, string>() { { "ID", model.ID.ToString() } });

            Lang.Lang_Name = model.Lang_Name;
            Lang.Disp_Name = model.Disp_Name;
            Lang.Sub_Domain_Name = model.Sub_Domain_Name;
            Lang.Domain_Type = model.Domain_Type;
            Lang.Link_Lang_ID = model.Link_Lang_ID;
            Lang.Link_Href = model.Link_Href;
            Lang.UpdateDatetime = DateTime.Now;
            Lang.UpdateUser = account;

            var r = base.UpdateObject(Lang);
            return r ? 1 : 0;
        }

        public string SetPublish(string id)
        {
            string sql = "update Lang set Published=1 where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", id));
            try
            {
                base.ExeNonQuery(sql);
                return "發佈成功";
            }
            catch
            {
                return "發佈失敗";
            }
        }
        #endregion
    }
}

