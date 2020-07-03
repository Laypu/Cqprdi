using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Filter;
using Oaww.Business;
using Oaww.Utility;
using Oaww.Entity;
using System.IO;
using Oaww.ViewModel;
using Oaww.ViewModel.Config;
using Oaww.ViewModel.Search;
using System.Text;
using System.Security.Cryptography;
using Oaww.Entity.SET;
using System.Web.Configuration;
using Oaww.ViewModel.Lang;


namespace Template.webadmin.Areas.webadmin.Controllers
{
    public class ConfigController : BaseController
    {
        private const string salt = "+LoOWf5Fqxjfsa5BM0jq9w==";

        ConfigService _service = new ConfigService();
        CommonService _commonService = new CommonService();

        #region UserList
        [AuthFilter(_FuncionID = "Config/UserList")]
        public ActionResult UserList()
        {


            var list = _commonService.GetGeneralList<GroupUser>("Enabled=1")
                                     .Select(t => new SelectListItem() { Value = t.ID, Text = t.Group_Name })                                    
                                     .ToList();


            if(this.Account != "admin")
            {
                list = list.Where(t => t.Value != "administrators").ToList();
            }

            list.Insert(0, new SelectListItem() { Value = "", Text = "全部" });
            ViewBag.grouplist = list;
            ViewBag.Account = this.Account.AntiXss();
            return View();


        }
        public ActionResult PagingUser(AuthoritySearchModel searchModel)
        {
            return Json(_service.UserPaging(searchModel, this.Account));
        }

        [HttpPost]
        public ActionResult SetUserStatus(string id, bool status)
        {
            return Json(_service.UpdateStatus(id, status, this.Account, this.UserName));
        }

        [HttpPost]

        public ActionResult SetUserDelete(string[] idlist, string delaccount)
        {
            return Json(_service.UserDelete(idlist, delaccount, this.Account, this.UserName));
        }

        [AuthFilter(_FuncionID = "Config/UserList")]
        public ActionResult UserListEdit(string ID = "")
        {


            var list = _commonService.GetGeneralList<GroupUser>("Enabled=1")
                                .Select(t => new SelectListItem() { Value = t.ID, Text = t.Group_Name })                               
                                .ToList();
            

            if (this.Account != "admin")
            {
                list = list.Where(t => t.Value != "administrators").ToList();
            }

            list.Insert(0, new SelectListItem() { Value = "", Text = "請選擇" });

            ViewBag.grouplist = list;
            var model = _service.GetAdminMemberModelByID(ID);
            if (model.ID >= 0)
            {
                ViewBag.IsAdd = "N";
                ViewBag.Title = "編輯管理帳號";
            }
            else
            {
                ViewBag.IsAdd = "Y";
                ViewBag.Title = "新增管理帳號";
            }
            return View(model);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUserList(AdminMemberModel model)
        {

            if (model.ID > 0)
            {
                var oldaccount = _commonService.GetGeneralList<Users>("Account=@Account", new Dictionary<string, string>() { { "Account", model.Account } });
                if (oldaccount.Count() > 0)
                {
                    if (oldaccount.First().ID != model.ID) { return Json("修改的帳號已經存在"); }
                }

                if (model.Password.IsNullOrEmpty() == false )
                {
                    model.Password = Server.HtmlEncode(model.Password);                   
                    var spassword = model.Password.SecureString();
                    model.Password = spassword.SecureStringToString().EncodePassword();
                }



                if (_service.UpdateUser(model, this.Account, this.UserName) < 0)
                {
                    return Json("儲存失敗");
                }
                return Json("");
            }
            else
            {
                var oldaccount = _commonService.GetGeneralList<Users>("Account=@Account", new Dictionary<string, string>() { { "Account", model.Account } });
                if (oldaccount.Count() > 0) { return Json("新增的帳號已經存在"); }
                model.Password = EncodePassword(model.Password);
                if ((_service.CreateUser(model, this.Account, this.UserName) < 0))
                {
                    return Json("儲存失敗");
                }
                return Json("");
            }

        }

        internal string EncodePassword(string pass)
        {


            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            byte[] bRet = null;

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);

            using (SHA256 s = SHA256.Create())
            {
                bRet = s.ComputeHash(bAll);
            }

            return Convert.ToBase64String(bRet);
        }
        #endregion

        #region GroupEdit
        [AuthFilter(_FuncionID = "Config/UserList")]
        public ActionResult GroupEdit()
        {
            return View();
        }

        public ActionResult PagingGroup(SearchModelBase searchModel)
        {
            return Json(_service.Paging(searchModel,this.Account));
        }

        [HttpPost]
        public ActionResult GroupAuth(string id, string groupname, string sellist)
        {
            string langid = this.LanguageID;
            AdminFunctionModel model = _service.GetAdminFunctionModel(id, langid,this.Account);


            ViewBag.groupid = model.GroupID;
            ViewBag.langid = langid;

            return View(model);
        }

        public ActionResult EditGroupSeq(string id, int seq)
        {

            var user = Request.GetOwinContext().Authentication.User;
            var account = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var name = user.Identity.Name;

            return Json(_service.UpdateSeq(id, seq, account.Value, name));

        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult GroupAuthSave(Dictionary<string, string> inputdata, string langid, string groupid, string groupname, string IsInsert)
        {
            string result = _service.GroupAuthSave(this.LanguageID, groupid, inputdata, groupname, Account, IsInsert);


            return Content(result);
        }

        public ActionResult SetGroupStatus(string id,bool status)
        {
            return Content(_service.SetGroupStatus(id, status));
        }

        public ActionResult SetGroupDelete(string[] idlist,string delaccount)
        {
            if (_service.CheckGroupExist(idlist))
            {
                return Content("群組內還有使用者");
            }

            return Content(_service.SetGroupDelete(idlist));
        }

        #endregion

        #region 流量分析
        [AuthFilter(_FuncionID = "Config/SiteFlow")]
        public ActionResult SiteFlow()
        {
            Session["menuNowID"] = 0;

            var SiteFlow = _service.GetSiteFlow();
            if (SiteFlow.ID != 0)
            {
                var link = SiteFlow.Siteflow_Link;
                if (link.IsNullOrEmpty() == false)
                {
                    ViewBag.isClick = "Y";
                    ViewBag.SiteFlowURL = SiteFlow.Siteflow_Link;
                }
            }
            return View();
        }

        /// <summary>
        /// 編輯流量分析
        /// </summary>
        /// <returns></returns>
        [AuthFilter(_FuncionID = "Config/SiteFlow")]
        public ActionResult SiteFlowEdit()
        {

            var SiteFlow = _service.GetSiteFlow();
            if (SiteFlow.ID != 0)
            {
                ViewBag.Code = HttpUtility.HtmlDecode(SiteFlow.Siteflow_Code);
                ViewBag.Path = SiteFlow.Siteflow_Link;
                ViewBag.ID = SiteFlow.ID;
            }
            return View();
        }

        /// <summary>
        /// 匯出檔案
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSiteFlowFile()
        {
            try
            {
                var root = Request.PhysicalApplicationPath;
                var filepath = root + "\\Example\\Google流量分析申請說明.pdf";
                string filename = System.IO.Path.GetFileName(filepath);
                //讀成串流
                Stream iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                //回傳出檔案
                return File(iStream, "application/octet-stream", filename);
            }
            catch (Exception ex)
            {
                // NLogManagement.SystemLogInfo("匯出系統權限管理失敗=" + ex.Message);
            }
            return Content("");
        }

        /// <summary>
        /// 流量分析存檔
        /// </summary>
        /// <param name="code"></param>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public ActionResult SetSiteFlowCode(string code, string path, string id)
        {

            var r = _service.UpdateSiteFlow(code, path, id);

            if (r)
            {
                SiteFlow siteFlow = _service.GetSiteFlow();

                return Content("");
            }
            else
            {
                return Content("更新失敗");
            }

        }
        #endregion

        #region 審核
        [AuthFilter(_FuncionID = "Config/Verify")]
        public ActionResult Verify()
        {
            Session["menuNowID"] = 0;

            List<SelectListItem> ListGroup = _commonService.GetGeneralList<SET_MENU>("M_Menu03=1")
                                                            .Select(t => new SelectListItem() { Value = t.M_Menu01.ToString(), Text = t.M_Menu02 })
                                                            .ToList();

            ListGroup.Insert(0, new SelectListItem() { Value = "", Text = "全部" });

            ViewData["ListGroup"] = ListGroup;

            List<SET_MENU> ListMenu = _commonService.GetGeneralList<SET_MENU>("M_Menu04 !='' and M_Menu05 != ''").ToList();

            ViewData["ListMenu"] = ListMenu;

            return View();
        }

        public ActionResult PagingVerify(ConfigSearchModel model)
        {
            model.LangId = this.LanguageID;
            return Json(_service.PagingVerify(model, CheckIsAdmin()));
        }

        public ActionResult SetVerifyOK(string id)
        {
            return Content(VerifyOK(id));
        }

        public string VerifyOK(string id)
        {
            var idarr = id.Split('_');
            if (idarr.Length < 3)
            {
                return "設定失敗,資料來源不足";
            }


            string result = _service.SetVerifyOK(id, this.Account);




            return result;
        }

        public ActionResult SetVerifyRefuse(string id)
        {
            string result = _service.SetVerifyRefuse(id, this.Account);


            return Content(result);
        }
        #endregion

        #region Password Edit
        [AuthFilter(_FuncionID = "Config/PasswordEdit")]
        public ActionResult PasswordEdit()
        {
            ViewBag.account = this.Account.AntiXss();
            ViewBag.username = this.UserName.safeHtmlFragment();
            return View();
        }

        public ActionResult SavePasswordEdit(string Password, string NewPassword, string ConfirmPassword)
        {

            Password = Server.HtmlEncode(Password);
            NewPassword = Server.HtmlEncode(NewPassword);
            ConfirmPassword = Server.HtmlEncode(ConfirmPassword);
            var spassword = Password.SecureString();
            var snewpassword = NewPassword.SecureString();
            var sconpassword = ConfirmPassword.SecureString();

            var user = Request.GetOwinContext().Authentication.User;


            var id = user.FindFirst("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider").Value;
            var oldaccount = _commonService.GetGeneral<Users>("ID=@ID", new Dictionary<string, string>() { { "ID", id } });

            NewPassword = snewpassword.SecureStringToString().EncodePassword();
            ConfirmPassword = sconpassword.SecureStringToString().EncodePassword();
            if (spassword.SecureStringToString().EncodePassword() != oldaccount.PWD)
            {
                return Json("原密碼輸入錯誤");
            }
            if (NewPassword != ConfirmPassword)
            {
                return Json("新密碼確認與新密碼並不一致");
            }
            Password = null;
            NewPassword = null;
            ConfirmPassword = null;

            return Json(_service.UpdatePassword(id, snewpassword, this.Account));

        }
        #endregion

        #region Mail Setting
        [AuthFilter(_FuncionID = "Config/MailEdit")]
        public ActionResult MailEdit()
        {
            MailServerEdit mailSearchModel = new MailServerEdit();

            mailSearchModel.Host = System.Web.Configuration.WebConfigurationManager.AppSettings["MailHost"];
            mailSearchModel.Port = System.Web.Configuration.WebConfigurationManager.AppSettings["MailPort"];

            if (bool.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["MailSSL"], out bool ssl))
            {
                mailSearchModel.SSL = ssl;
            }
            else
            {
                mailSearchModel.SSL = false;
            }

            mailSearchModel.UserID = System.Web.Configuration.WebConfigurationManager.AppSettings["MailUserID"];
            mailSearchModel.PXD = System.Web.Configuration.WebConfigurationManager.AppSettings["MailPXD"];

            return View(mailSearchModel);
        }

        public ActionResult SaveMailEdit(MailServerEdit mailSearchModel)
        {
            try
            {
                var myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

                myConfiguration.AppSettings.Settings["MailHost"].Value = mailSearchModel.Host;
                myConfiguration.AppSettings.Settings["MailPort"].Value = mailSearchModel.Port;
                myConfiguration.AppSettings.Settings["MailSSL"].Value = mailSearchModel.SSL ? "true" : "false";
                myConfiguration.AppSettings.Settings["MailUserID"].Value = mailSearchModel.UserID;
                myConfiguration.AppSettings.Settings["MailPXD"].Value = mailSearchModel.PXD;
                myConfiguration.Save();

                return Content("更新成功");
            }
            catch
            {
                return Content("更新失敗");
            }
        }
        #endregion

        #region 多語系
        [AuthFilter(_FuncionID = "Config/SiteLang")]
        public ActionResult SiteLang()
        {
            return View();
        }

        public ActionResult SiteLangEdit(string id)
        {

            id = Server.HtmlEncode(id);

            var model = new SiteLangModel();
            ViewBag.langlist = _service.GetSelectList();
            if (id.IsNullOrEmpty() == false)
            {
                model = _service.GetModelById(id);
            }
            var alldata = _commonService.GetGeneralList<Lang>().Where(v => v.Deleted == false);
            if (id.IsNullOrEmpty())
            {
                id = "-1";
            }
            if (alldata.Any(v => v.ID == int.Parse(id)) == false)
            {
                if (alldata.Count() >= 3)
                {
                    ViewBag.disableadd = "Y";
                }
                else
                {
                    ViewBag.disableadd = "N";
                }
            }

            return View(model);



        }

        public ActionResult PagingLang(SearchModelBase searchModel)
        {
            return Json(_service.PagingLang(searchModel));
        }

        public ActionResult EditSeq(int? id, int seq, string type)
        {
            return Json(_service.UpdateLangSeq(id.Value, seq, this.Account, this.UserName));
        }

        public ActionResult SetLangDelete(string[] idlist, string delaccount, string type)
        {
            var user = Request.GetOwinContext().Authentication.User;
            var account = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var name = user.Identity.Name;
            var str = _service.DeleteLang(idlist, delaccount, account.Value, name);
            //todo update cache
            return Json(str);
        }

        public ActionResult SaveLang(SiteLangModel model)
        {

            var user = Request.GetOwinContext().Authentication.User;
            var account = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var name = user.Identity.Name;
            var alldata = _commonService.GetGeneralList<Lang>().Where(v => v.Deleted == false);

            if (model.ID > 0)
            {
                alldata = alldata.Where(v => v.ID != model.ID);
                if (alldata.Any(v => v.Lang_Name == model.Lang_Name || v.Sub_Domain_Name == model.Sub_Domain_Name))
                {
                    return Json("語系、網址 不可以重複");
                }
                _service.UpdateLang(model, this.Account);
            }
            else
            {
                if (alldata.Count() > 3)
                {
                    return Json("語系無法再新增");
                }
                if (alldata.Any(v => v.Lang_Name == model.Lang_Name || v.Sub_Domain_Name == model.Sub_Domain_Name))
                {
                    return Json("語系、網址 不可以重複");
                }
                _service.CreateLang(model, this.Account);
            }

            return Json("");



        }

        public ActionResult SetPublish(string id)
        {
            var str = _service.SetPublish(id);
            return Json(str);
        }
        #endregion
    }
}