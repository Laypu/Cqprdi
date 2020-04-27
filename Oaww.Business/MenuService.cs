using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using Oaww.Entity.SET;
using System.Data.SqlClient;
using Oaww.Utility;
using Oaww.ViewModel;
using System.Web;

namespace Oaww.Business
{
    public class MenuService : BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();

        /// <summary>
        /// 取得語系跟語言相對應的Menu list
        /// </summary>
        /// <param name="languageID"></param>
        /// <param name="menutype"></param>
        /// <returns></returns>
        public List<Menu> GetMenu(string languageID, string menutype)
        {
            string sql = "select * from Menu where 1=1";

            base.Parameter.Clear();
            if (!languageID.IsNullOrEmpty())
            {
                sql += " and LangID=@LangID";
                base.Parameter.Add(new SqlParameter("@LangID", languageID));
            }

            if (!menutype.IsNullOrEmpty())
            {
                sql += " and MenuType=@MenuType";
                base.Parameter.Add(new SqlParameter("@MenuType", menutype));
            }

            sql += " order by Sort";

            return base.SearchList<Menu>(sql);
        }

        /// <summary>
        /// 取得Menu資料By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Menu GetMenuById(int id)
        {
            string sql = @"select * from Menu t where t.ID=@id";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@id", id));

            return base.GetObject<Menu>(sql);
        }

        /// <summary>
        /// 取得Edit資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuEditModel GetModel(string id)
        {
            string sql = @"select * from Menu where ID=@id";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@id", id));

            var model = base.GetObject<MenuEditModel>(sql);
            model.ImageUrl = VirtualPathUtility.ToAbsolute("~/UploadImage/MenuImage/" + model.ImgNameOri);
            model.DeleteUploadFile = "N";

            return model;
        }

        public List<Menu> GetListMenuByParent(int ParentID, int menuLevel, SqlTransaction tran)
        {
            string sql = @"select * from Menu t where t.ParentID=@ParentID and t.MenuLevel=@menuLevel";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ParentID", ParentID));
            base.Parameter.Add(new SqlParameter("@menuLevel", menuLevel));

            return base.SearchList<Menu>(sql, tran);
        }

        public List<Menu> GetListMenu(int? LangID, int? ParentID, int? MenuType)
        {
            string sql = @"select * from Menu t where LangID=@LangID and ParentID=@ParentID and MenuType=@MenuType order by Sort";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@LangID", LangID));
            base.Parameter.Add(new SqlParameter("@ParentID", ParentID));
            base.Parameter.Add(new SqlParameter("@MenuType", MenuType));

            return base.SearchList<Menu>(sql);
        }

        public bool ChkModelInMessage(string ModelID)
        {
            string sql = @"select * from SET_MESSAGE where M_MESSAGE01 =@ModelID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            return base.SearchCount(sql) > 0;
        }

        /// <summary>
        /// 取得該模組的子模組
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="langid"></param>
        /// <returns></returns>
        public string GetModelItem(string modelid, string langid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<option value=''>無</option>");
            sb.Append("<option value='-1'>新建單元</option>");

            SET_MENU SET_MENU = _commonService.GetGeneral<SET_MENU>("M_Menu01=@M_Menu01", new Dictionary<string, string>() { {"M_Menu01",modelid } });

            string sql = $@"select * from { SET_MENU.M_Menu11} where Status=1 and ModelID=@ModelID and Lang_ID=@Lang_ID Order By Sort";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", modelid));
            base.Parameter.Add(new SqlParameter("@Lang_ID", langid));

            var list = base.SearchList<BaseModel>(sql);
            foreach (var l in list)
            {
                sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            }

            //if (modelid == "1" || modelid == "11")
            //{
            //    var list = _commonService.GetGeneralList<ModelPageEditMain>("Status=1 and ModelID=@ModelID and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid }, { "ModelID", modelid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}
            //else if (ChkModelInMessage(modelid))
            //{
            //    var list = _commonService.GetGeneralList<ModelMessageMain>("Status=1 and ModelID=@ModelID and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid }, { "ModelID", modelid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}
            //else if (modelid == "4")
            //{
            //    var list = _commonService.GetGeneralList<ModelFormMain>("Status=1 and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}
            //else if (modelid == "8")
            //{
            //    var list = _commonService.GetGeneralList<ModelSiteMapMain>("Status=1 and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}
            //else if (modelid == "9")
            //{
            //    var list = _commonService.GetGeneralList<ModelVideoMain>("Status=1 and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}

            return sb.ToString();
        }
        public string GetModelItem(string modelid, string langid,string menuType)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<option value=''>無</option>");
            sb.Append("<option value='-1'>新建單元</option>");

            SET_MENU SET_MENU = _commonService.GetGeneral<SET_MENU>("M_Menu01=@M_Menu01", new Dictionary<string, string>() { { "M_Menu01", modelid } });

            string sql = $@"select t.* from { SET_MENU.M_Menu11} t
                            left join Menu s on t.ModelID = s.ModelID and t.ID = s.ModelItemID
                            where s.MenuType = @menuType and t.Status=1 and t.ModelID=@ModelID and t.Lang_ID=@Lang_ID Order By t.Sort ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", modelid));
            base.Parameter.Add(new SqlParameter("@Lang_ID", langid));
            base.Parameter.Add(new SqlParameter("@menuType", menuType));

            var list = base.SearchList<BaseModel>(sql);
            foreach (var l in list)
            {
                sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            }

            //if (modelid == "1" || modelid == "11")
            //{
            //    var list = _commonService.GetGeneralList<ModelPageEditMain>("Status=1 and ModelID=@ModelID and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid }, { "ModelID", modelid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}
            //else if (ChkModelInMessage(modelid))
            //{
            //    var list = _commonService.GetGeneralList<ModelMessageMain>("Status=1 and ModelID=@ModelID and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid }, { "ModelID", modelid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}
            //else if (modelid == "4")
            //{
            //    var list = _commonService.GetGeneralList<ModelFormMain>("Status=1 and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}
            //else if (modelid == "8")
            //{
            //    var list = _commonService.GetGeneralList<ModelSiteMapMain>("Status=1 and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}
            //else if (modelid == "9")
            //{
            //    var list = _commonService.GetGeneralList<ModelVideoMain>("Status=1 and Lang_ID=@Lang_ID Order By Sort", new Dictionary<string, string>() { { "Lang_ID", langid } });
            //    foreach (var l in list)
            //    {
            //        sb.Append("<option value='" + l.ID.ToString() + "'>" + l.Name + "</option>");
            //    }
            //}

            return sb.ToString();
        }
        /// <summary>
        /// 新增模組
        /// </summary>
        /// <param name="model"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string Create(MenuEditModel model, string account, string username)
        {
            var datetime = DateTime.Now;

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {

                        if (model.LinkMode == 2 && model.ModelItemID == -1)
                        {

                            SET_MENU SET_MENU = _commonService.GetGeneral<SET_MENU>("M_Menu01=@M_Menu01", new Dictionary<string, string>() { { "M_Menu01", model.ModelID.ToString() } });
                            

                          
                            if (SET_MENU.M_Menu11== "ModelPageEditMain")
                            {
                                model.ModelItemID = AddUnit<ModelPageEditMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }
                            else if (SET_MENU.M_Menu11 == "ModelMessageMain")
                            {
                                model.ModelItemID = AddUnit<ModelMessageMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }                            
                            else if (SET_MENU.M_Menu11 == "ModelFormMain")
                            {
                                model.ModelItemID = AddUnit<ModelFormMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }
                            else if (SET_MENU.M_Menu11 == "ModelVideoMain")
                            {
                                model.ModelItemID = AddUnit<ModelVideoMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }
                            else if (SET_MENU.M_Menu11 == "ModelCommonMain") //直接變成共用
                            {
                                model.ModelItemID = AddUnit<ModelCommonMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }
                        }

                        var sort = _commonService.GetMaxID(tran, "Menu", "Sort", "LangID", model.LangID.ToString(), "ParentID", model.ParentID.ToString());

                        var menu = new Menu()
                        {
                            ImageHeight = model.ImageHeight,
                            ImgNameOri = model.ImgNameOri,
                            ImgNameThumb = model.ImgNameThumb,
                            ImgShowName = model.ImgShowName,
                            LangID = model.LangID,
                            LinkMode = model.LinkMode,
                            MenuLevel = model.MenuLevel,
                            MenuName = model.MenuName,
                            ModelID = model.ModelID,
                            ModelItemID = model.ModelItemID,
                            OpenMode = model.OpenMode,
                            ShowMode = model.ShowMode,
                            Sort = sort,
                            Status = true,
                            UpdateDatetime = datetime,
                            UpdateUser = account,
                            WindowHeight = model.WindowHeight,
                            WindowWidth = model.WindowWidth,
                            MenuType = model.MenuType,
                            ParentID = model.ParentID,
                            LinkUrl = model.LinkUrl,
                            LinkUploadFileName = model.LinkUploadFileName,
                            LinkUploadFilePath = model.LinkUploadFilePath,
                            DisplayName = model.DisplayName,
                            FrontDisplay = model.FrontDisplay,
                            Icon = model.Icon
                        };

                        var r = base.InsertObject(menu);

                        tran.Commit();
                        return "新增成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, $"菜單新增失敗：error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        return "新增失敗";
                    }
                }
            }

        }

        /// <summary>
        /// 更新模組
        /// </summary>
        /// <param name="model"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string Update(MenuEditModel model, string account, string username)
        {
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {

                        var olddata = GetMenuById(model.ID);
                        var oldfilename = olddata.ImgNameThumb;
                        var oldfileoriname = olddata.ImgNameOri;
                        var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                            "\\UploadImage\\MenuImage\\" + oldfilename;
                        var oldoriroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath +
                          "\\UploadImage\\MenuImage\\" + oldfileoriname;
                        if (model.ImgNameOri.IsNullOrEmpty())
                        {
                            model.ImgNameThumb = "";
                            model.ImgNameOri = "";
                            model.ImgShowName = "";
                        }
                        bool r = false;
                        if (model.DeleteUploadFile == "Y" && olddata.LinkUploadFilePath.IsNullOrEmpty() == false)
                        {
                            if (System.IO.File.Exists(olddata.LinkUploadFilePath))
                            {
                                System.IO.File.Delete(olddata.LinkUploadFilePath);
                            }
                        }
                        if (model.LinkMode != 4)
                        {
                            if (System.IO.File.Exists(olddata.LinkUploadFilePath))
                            {
                                System.IO.File.Delete(olddata.LinkUploadFilePath);
                            }
                            model.LinkUploadFileName = "";
                            model.LinkUploadFilePath = "";
                        }
                        if (model.LinkMode != 3)
                        {
                            model.LinkUrl = "";
                        }
                        //1.create message
                        var datetime = DateTime.Now;
                        if (model.LinkMode == 2 && model.ModelItemID == -1)
                        {
                            SET_MENU SET_MENU = _commonService.GetGeneral<SET_MENU>("M_Menu01=@M_Menu01", new Dictionary<string, string>() { { "M_Menu01", model.ModelID.ToString() } });

                            if (SET_MENU.M_Menu11 == "ModelPageEditMain")
                            {
                                model.ModelItemID = AddUnit<ModelPageEditMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }
                            else if (SET_MENU.M_Menu11 == "ModelMessageMain")
                            {
                                model.ModelItemID = AddUnit<ModelMessageMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }
                            else if (SET_MENU.M_Menu11 == "ModelFormMain")
                            {
                                model.ModelItemID = AddUnit<ModelFormMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }
                            else if (SET_MENU.M_Menu11 == "ModelVideoMain")
                            {
                                model.ModelItemID = AddUnit<ModelVideoMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }
                            else if (SET_MENU.M_Menu11 == "ModelCommonMain") //直接變成共用
                            {
                                model.ModelItemID = AddUnit<ModelCommonMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }
                            else if (SET_MENU.M_Menu11 == "ModelSiteMapMain")
                            {
                                model.ModelItemID = AddUnit<ModelSiteMapMain>(model.MenuName, model.LangID.ToString(), account, model.ModelID, tran);
                            }

                        }
                        var ad = new Menu()
                        {
                            ImageHeight = model.ImageHeight,
                            ImgNameOri = model.ImgNameOri,
                            ImgNameThumb = model.ImgNameThumb,
                            ImgShowName = model.ImgShowName,
                            LangID = model.LangID,
                            LinkMode = model.LinkMode,
                            MenuLevel = model.MenuLevel,
                            MenuName = model.MenuName,
                            ModelID = model.ModelID,
                            ModelItemID = model.ModelItemID,
                            OpenMode = model.OpenMode,
                            ShowMode = model.ShowMode,
                            UpdateDatetime = datetime,
                            UpdateUser = account,
                            WindowHeight = model.WindowHeight,
                            WindowWidth = model.WindowWidth,
                            MenuType = model.MenuType,
                            Status = olddata.Status,
                            Sort = olddata.Sort,
                            ID = model.ID,
                            ParentID = olddata.ParentID,
                            LinkUrl = model.LinkUrl == null ? "" : model.LinkUrl,
                            LinkUploadFileName = model.LinkUploadFileName,
                            LinkUploadFilePath = model.LinkUploadFilePath,
                            DisplayName = model.DisplayName == null ? "" : model.DisplayName,
                            FrontDisplay = model.FrontDisplay,
                            Icon = model.Icon
                        };

                        r = base.UpdateObject(ad);

                        if (r)
                        {
                            if (model.ImgNameOri.IsNullOrEmpty() || model.ImgNameOri != oldfileoriname)
                            {
                                if (System.IO.File.Exists(oldroot))
                                {
                                    System.IO.File.Delete(oldroot);
                                }
                                if (System.IO.File.Exists(oldoriroot))
                                {
                                    System.IO.File.Delete(oldoriroot);
                                }
                            }

                        }

                        tran.Commit();
                        return "修改成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, $"菜單更新失敗：error:" + ex.ToString().NewLineReplace());
                        tran.Rollback();
                        return "修改失敗";
                    }
                }
            }

        }

        /// <summary>
        /// 根據泛型新增模組相對應的資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="langid"></param>
        /// <param name="account"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public int AddUnit<T>(string name, string langid, string account, int ModelID, SqlTransaction tran) where T : BaseModel, new()
        {
            //update Sort =>Sort+1
            _commonService.UpdateSortByTableAndLang(typeof(T).Name, langid, tran);

            T model = new T();

            model.Lang_ID = int.Parse(langid);
            model.Name = name;
            model.ModelID = ModelID;
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            model.CreateUser = account;
            model.UpdateUser = account;
            model.Sort = 1;
            model.Status = true;

            try
            {
                long id = base.InsertObject(model, tran);
                return (int)id;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"菜單單元新增失敗：error:" + ex.ToString().NewLineReplace());

                throw ex;
            }



        }

        public bool AddUnit<T>(string name, string langid, string account, int ModelID) where T : BaseModel, new()
        {
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        //update Sort =>Sort+1
                        _commonService.UpdateSortByTableAndLang(typeof(T).Name, langid, ModelID, tran);

                        T model = new T();

                        model.Lang_ID = int.Parse(langid);
                        model.Name = name;
                        model.ModelID = ModelID;
                        model.CreateDate = DateTime.Now;
                        model.UpdateDate = DateTime.Now;
                        model.CreateUser = account;
                        model.UpdateUser = account;
                        model.Sort = 1;
                        model.Status = true;

                        long id = base.InsertObject(model, tran);
                        tran.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, $"菜單單元新增失敗：error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        return false;
                    }
                }
            }



        }


        public bool UpdateUnit<T>(string name, string id, string account, string modelID)
        {
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        string sql = $@"update {typeof(T).Name} set Name=@Name where ID=@ID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@Name", name));
                        base.Parameter.Add(new SqlParameter("@ID", id));
                        base.ExeNonQuery(sql, tran);

                        sql = $@"update VerifyData set ModelName=@ModelName where ModelID=@ModelID and ModelMainID=@ModelMainID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ModelName", name));
                        base.Parameter.Add(new SqlParameter("@modelID", modelID));
                        base.Parameter.Add(new SqlParameter("@ModelMainID", id));
                        base.ExeNonQuery(sql, tran);

                        if (modelID == "1")
                        {
                            sql = "update PageIndexItem set ItemName=@name where ModelID=@ModelMainID";
                            base.Parameter.Clear();
                            base.Parameter.Add(new SqlParameter("@name", name));
                            base.Parameter.Add(new SqlParameter("@ModelMainID", id));
                            base.ExeNonQuery(sql, tran);
                        }

                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex, $"菜單單元新更新失敗：error:" + ex.ToString().NewLineReplace());
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }


        /// <summary>
        /// 刪除模組以及其下面的模組
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public string DeleteMenu(int menuid)
        {
            var olddata = GetMenuById(menuid);

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        //delete folder
                        var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadImage\\MenuImage\\";
                        var r = base.DeleteObject(olddata, tran);
                        if (r)
                        {
                            if (System.IO.File.Exists(oldroot + "\\" + olddata.ImgNameOri))
                            {
                                System.IO.File.Delete(oldroot + "\\" + olddata.ImgNameOri);
                            }
                            if (System.IO.File.Exists(oldroot + "\\" + olddata.ImgNameThumb))
                            {
                                System.IO.File.Delete(oldroot + "\\" + olddata.ImgNameThumb);
                            }
                        }


                        if (olddata.MenuLevel == 1)
                        {
                            var oldlevel2 = GetListMenuByParent(menuid, 2, tran);
                            _commonService.UpdateSortByTableAndLang(tran, "Menu", olddata.LangID.ToString(), olddata.Sort.Value, olddata.ParentID.Value.ToString(), "1");

                            foreach (var l2 in oldlevel2)
                            {
                                r = base.DeleteObject(l2, tran);
                                if (r)
                                {
                                    if (System.IO.File.Exists(oldroot + "\\" + l2.ImgNameOri))
                                    {
                                        System.IO.File.Delete(oldroot + "\\" + l2.ImgNameOri);
                                    }
                                    if (System.IO.File.Exists(oldroot + "\\" + l2.ImgNameThumb))
                                    {
                                        System.IO.File.Delete(oldroot + "\\" + l2.ImgNameThumb);
                                    }
                                    var oldlevel3 = GetListMenuByParent(l2.ID, 3, tran);
                                    foreach (var l3 in oldlevel3)
                                    {
                                        r = base.DeleteObject(l3, tran);
                                        if (r)
                                        {
                                            if (System.IO.File.Exists(oldroot + "\\" + l3.ImgNameOri))
                                            {
                                                System.IO.File.Delete(oldroot + "\\" + l3.ImgNameOri);
                                            }
                                            if (System.IO.File.Exists(oldroot + "\\" + l3.ImgNameThumb))
                                            {
                                                System.IO.File.Delete(oldroot + "\\" + l3.ImgNameThumb);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else if (olddata.MenuLevel == 2)
                        {

                            _commonService.UpdateSortByTableAndLang(tran, "Menu", olddata.LangID.ToString()
                                                                                , olddata.Sort.Value
                                                                                , olddata.ParentID.Value.ToString()
                                                                                , olddata.MenuLevel.ToString());



                            var oldlevel3 = GetListMenuByParent(menuid, 3, tran);
                            foreach (var l3 in oldlevel3)
                            {
                                r = base.DeleteObject(l3, tran);
                                if (r)
                                {
                                    if (System.IO.File.Exists(oldroot + "\\" + l3.ImgNameOri))
                                    {
                                        System.IO.File.Delete(oldroot + "\\" + l3.ImgNameOri);
                                    }
                                    if (System.IO.File.Exists(oldroot + "\\" + l3.ImgNameThumb))
                                    {
                                        System.IO.File.Delete(oldroot + "\\" + l3.ImgNameThumb);
                                    }
                                }
                            }
                        }

                        tran.Commit();
                        return "刪除成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, $"菜單單元新刪除失敗：error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        return "刪除失敗";
                    }
                }
            }





        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="type"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string UpdateSort(int menuid, string type, string account, string username)
        {
            try
            {
                string sql = string.Empty;

                var oldmodel = new List<Menu>();
                var olddata = GetMenuById(menuid);
                oldmodel = GetListMenu(olddata.LangID, olddata.ParentID, olddata.MenuType);
                if (oldmodel.Count() == 0) { return "更新失敗"; }
                var index = oldmodel.FindIndex(v => v.ID == menuid);
                var r = false;
                if (type == "next")
                {
                    if (index + 1 >= oldmodel.Count()) { return "更新成功"; }

                    sql = @"update Menu set Sort=@Sort where ID=@ID
                            update Menu set Sort=@Sort1 where ID=@ID1";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Sort", oldmodel[index + 1].Sort));
                    base.Parameter.Add(new SqlParameter("@ID", oldmodel[index].ID));
                    base.Parameter.Add(new SqlParameter("@Sort1", oldmodel[index].Sort));
                    base.Parameter.Add(new SqlParameter("@ID1", oldmodel[index + 1].ID));

                    base.ExeNonQuery(sql);
                }
                else
                {
                    if (index - 1 < 0) { return "更新成功"; }

                    sql = @"update Menu set Sort=@Sort where ID=@ID
                            update Menu set Sort=@Sort1 where ID=@ID1";
                    base.Parameter.Clear();
                    base.Parameter.Add(new SqlParameter("@Sort", oldmodel[index - 1].Sort));
                    base.Parameter.Add(new SqlParameter("@ID", oldmodel[index].ID));
                    base.Parameter.Add(new SqlParameter("@Sort1", oldmodel[index].Sort));
                    base.Parameter.Add(new SqlParameter("@ID1", oldmodel[index - 1].ID));

                    base.ExeNonQuery(sql);
                }

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
                logger.Error(ex, $"更新排序失敗：error:" + ex.ToString().NewLineReplace());
                return "更新排序失敗:" + " error:" + ex.Message;
            }
        }

        /// <summary>
        /// 更新Menu狀態
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string MenueStatusUpdate(string menuid, bool Status)
        {
            string sql = "update Menu set Status=@Status where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Status", Status));
            base.Parameter.Add(new SqlParameter("@ID", menuid));
            var r = base.ExeNonQuery(sql);
            if (r > 0) { return "修改成功"; } else { return "修改失敗"; }
        }



        /// <summary>
        /// 更新Menu URL
        /// </summary>
        /// <param name="linkurl"></param>
        /// <param name="menuid"></param>
        /// <param name="account"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string UpdateMenuLink(string linkurl, string menuid, string account, string userName)
        {
            string sql = "update Menu set LinkUrl=@LinkUrl where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@LinkUrl", linkurl));
            base.Parameter.Add(new SqlParameter("@ID", menuid));
            var r = base.ExeNonQuery(sql);
            if (r > 0) { return "設定完成"; } else { return "設定失敗"; }
        }
    }
}
