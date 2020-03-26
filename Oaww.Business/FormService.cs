using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Oaww.Utility;
using Oaww.Entity;
using Oaww.ViewModel;
using System.Data.SqlClient;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using Newtonsoft.Json;
using System.Threading;

namespace Oaww.Business
{
    public class FormService : Oaww.BaseBusiness.BaseBusiness
    {
        CommonService _commonService = new CommonService();
        private static SmtpClient client;
        public FormService()
        {
            client = CommonFun.GetSMTPClient();
        }
        #region Main

        public List<FormSelItem> GetListFormSelItem(string ItemID)
        {
            return _commonService.GetGeneralList<FormSelItem>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", ItemID } })
                                                               .ToList();
        }

        public FormSetting GetItemFormSetting(string ItemID)
        {
            return _commonService.GetGeneral<FormSetting>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", ItemID } });
        }

        public FormSelItem GetFormSelItem(string ID)
        {
            return _commonService.GetHisEntity<FormSelItem>("ID", ID);
        }

        public ModelFormMain GetModelFormMain(string ID)
        {
            string sql = @"select * from ModelFormMain where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", ID));

            return base.GetObject<ModelFormMain>(sql);
        }

        public Paging<ModelFormMain> Paging(SearchModelBase model)
        {
            string sql = string.Empty;

            var Paging = new Paging<ModelFormMain>();
            var onepagecnt = model.Limit;

            sql = $"select * from ModelFormMain where Lang_ID=@Lang_ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Lang_ID", model.LangId));
            Paging.rows = base.SearchListPage<ModelFormMain>(sql, model.Offset, model.Limit, " order by " + model.Sort);
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

            sql = "select * from Menu where ModelID=5 and ModelItemID in (select ListItem from dbo.SplitList(',',@idlist))";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));

            if (base.SearchCount(sql) > 0)
            {
                return "刪除失敗,刪除的項目使用中..";
            }

            var oldroot = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UploadImage\\FormItem\\";

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        //delete  ModelMessageMain _OK
                        sql = @"delete ModelFormMain where ID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //delete VerifyData_OK
                        sql = @"delete VerifyData where ModelID=5 and ModelMainID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //刪除FormInput
                        sql = @"delete FormInput where MainID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //刪除FormSetting
                        sql = @"delete FormSetting where ItemID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //刪除FormSelItem
                        sql = @"delete FormSelItem where ItemID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);

                        //刪除FormInputNote
                        sql = @"delete FormInputNote where InputID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        base.ExeNonQuery(sql, tran);


                        //update sort order
                        sql = @"with cte as
                                    (
                                    select 
                                     ROW_NUMBER()OVER(order by sort) as ROW,t.ID
                                     from ModelFormMain t where t.Lang_ID=1
                                    )
                                    update s set s.Sort = t.ROW from cte  t
                                    left join ModelFormMain s on t.ID = s.ID";
                        base.Parameter.Clear();
                        base.ExeNonQuery(sql, tran);

                        tran.Commit();
                        rstr = "刪除成功";

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "表單模組刪除異常，error:" + ex.ToString().NewLineReplace());

                        tran.Rollback();
                        rstr = "刪除失敗";
                    }

                    return rstr;
                }
            }
        }

        public string MainModelName(string mainid)
        {
            string sql = "select Name from ModelFormMain where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ID", mainid));

            return base.ExecuteScalar(sql, "").ToString();
        }
        #endregion

        #region Mail
        public Paging<FormInputResult> PagingMail(MailSearchModel model)
        {
            var Paging = new Paging<FormInputResult>();
            Paging.rows = new List<FormInputResult>();
            var onepagecnt = model.Limit;
            var whereobj = new List<string>();
            var wherestr = new List<string>();

            string sql = string.Empty;

            sql = @"select t.ID from FormSelItem t where t.ItemID=@MainID ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@MainID", model.ModelID.ToString()));
            List<string> IDs = base.SearchList<string>(sql);

            sql = @"select t.ID from FormSelItem t where t.ItemID=@MainID
                    and t.Title ='主題'";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@MainID", model.ModelID.ToString()));
            string TitleID = base.ExecuteScalar(sql, "0").ToString();

            sql = @"select Progress,
                           ID,
                           Name,
                           ReplyAccount,
                           CreateDatetime," +
                  $" JSON_VALUE(JSONStr,'$.\"{TitleID}\"') as Title  from FormInput " +
                "　where MainID=@MainID ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@MainID", model.ModelID.ToString()));



            if (string.IsNullOrEmpty(model.Name) == false)
            {
                sql += " and Name like @Name";
                base.Parameter.Add(new SqlParameter("@Name", "%" + model.Name + "%"));
            }
            if (string.IsNullOrEmpty(model.EMail) == false)
            {
                sql += " and EMail like @EMail";
                base.Parameter.Add(new SqlParameter("@EMail", "%" + model.EMail + "%"));
            }
            if (string.IsNullOrEmpty(model.KeyWord) == false)
            {
                sql += @" and ( 
                                ID in (select t.InputID from FormInputNote t where t.NoteText like @KeyWord)";
                IDs.ForEach(t =>
                {
                    sql += $" or  JSON_VALUE(JSONStr,'$.\"{t}\"')  like @KeyWord";
                });

                sql += " )";
                base.Parameter.Add(new SqlParameter("@KeyWord", "%" + model.KeyWord + "%"));

            }

            if (string.IsNullOrEmpty(model.Reply) == false)
            {
                if (model.Reply == "1") //未處理
                {
                    sql += " and Progress = '0'";
                }
                else if (model.Reply == "3") //已處理
                {
                    sql += " and Progress = '100'";
                }
                else //處理中
                {
                    sql += " and Progress in ('10','20','30','40','50','60','70','80','90')";
                }
            }
            if (string.IsNullOrEmpty(model.InputDateFrom) == false)
            {

                sql += " and CreateDatetime >=@InputDateFrom";
                base.Parameter.Add(new SqlParameter("@InputDateFrom", model.InputDateFrom));
            }
            if (string.IsNullOrEmpty(model.InputDateTo) == false)
            {

                sql += " and CreateDatetime <=@InputDateTo";
                base.Parameter.Add(new SqlParameter("@InputDateTo", DateTime.Parse(model.InputDateTo).AddDays(1).ToString("yyyy/MM/dd")));
            }

            List<FormInput> data = base.SearchListPage<FormInput>(sql, model.Offset, model.Limit, " order by " + model.Sort);

            Paging.total = base.SearchCount(sql);

            foreach (var d in data)
            {


                Paging.rows.Add(new FormInputResult()
                {
                    ID = d.ID,
                    Name = d.Name,
                    Title = d.Title,
                    Progress = d.Progress == "0" ? "未處理" : (d.Progress == "100" ? "已處理" : "處理中"),
                    ReplyNote = string.IsNullOrEmpty(d.ReplyAccount) ? "未回覆" : "已回覆",
                    CreateDatetime = d.CreateDatetime.Value.ToString("yyyy/MM/dd HH:mm:ss")

                });
            }
            return Paging;
        }

        public string SetMailDelete(string[] idlist, string delaccount, string account, string username)
        {
            try
            {
                var r = 0;
                string sql = @"delete FormInput where ID in (select ListItem from dbo.SplitList(',',@idlist))";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                r = base.ExeNonQuery(sql);

                var rstr = "";
                if (r >= 0)
                {

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

                logger.Error(ex, "刪除表單模組-Mail異常,error:" + ex.ToString().NewLineReplace());
                return "刪除失敗";
            }
        }

        public byte[] GetExport(MailSearchModel model)
        {
            var whereobj = new List<string>();
            var wherestr = new List<string>();
            string sql = string.Empty;

            sql = @"select t.ID from FormSelItem t where t.ItemID=@MainID ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@MainID", model.ModelID.ToString()));
            List<string> IDs = base.SearchList<string>(sql);

            sql = "select * from FormInput where MainID=@MainID ";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@MainID", model.ModelID.ToString()));

            if (string.IsNullOrEmpty(model.Name) == false)
            {
                sql += " and Name like @Name";
                base.Parameter.Add(new SqlParameter("@Name", "%" + model.Name + "%"));
            }
            if (string.IsNullOrEmpty(model.EMail) == false)
            {
                sql += " and EMail like @EMail";
                base.Parameter.Add(new SqlParameter("@EMail", "%" + model.EMail + "%"));
            }
            if (string.IsNullOrEmpty(model.KeyWord) == false)
            {
                sql += @" and ( 
                                ID in (select t.InputID from FormInputNote t where t.NoteText like @KeyWord)";
                IDs.ForEach(t =>
                {
                    sql += $" or  JSON_VALUE(JSONStr,'$.\"{t}\"')  like @KeyWord";
                });

                sql += " )";
                base.Parameter.Add(new SqlParameter("@KeyWord", "%" + model.KeyWord + "%"));

            }
            if (string.IsNullOrEmpty(model.Process) == false)
            {
                if (model.Process != "1")
                {
                    sql += " and Progress=@Process";
                    base.Parameter.Add(new SqlParameter("@Process", model.Process));
                }
                else
                {
                    sql += " and Progress>0 and Progress<100";
                }
            }
            if (string.IsNullOrEmpty(model.Reply) == false)
            {
                if (model.Reply == "1") //未處理
                {
                    sql += " and Progress = '0'";
                }
                else if (model.Reply == "3") //已處理
                {
                    sql += " and Progress = '100'";
                }
                else //處理中
                {
                    sql += " and Progress in ('10','20','30','40','50','60','70','80','90')";
                }
            }
            if (string.IsNullOrEmpty(model.InputDateFrom) == false)
            {

                sql += " and CreateDatetime >=@InputDateFrom";
                base.Parameter.Add(new SqlParameter("@InputDateFrom", model.InputDateFrom));
            }
            if (string.IsNullOrEmpty(model.InputDateTo) == false)
            {

                sql += " and CreateDatetime <=@InputDateTo";
                base.Parameter.Add(new SqlParameter("@InputDateTo", DateTime.Parse(model.InputDateTo).AddDays(1).ToString("yyyy/MM/dd")));
            }

            sql += " order by " + model.Sort;

            List<FormInput> datalist = base.SearchList<FormInput>(sql);
            MemoryStream ms = new MemoryStream();
            XSSFWorkbook hssfworkbook = new XSSFWorkbook();
            IFont font = hssfworkbook.CreateFont();
            font.FontHeightInPoints = 9;
            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.WrapText = true;
            style.SetFont(font);

            ISheet sheet = hssfworkbook.CreateSheet("匯出資料");
            IRow row = sheet.CreateRow(0);
            row.HeightInPoints = 16;

            SetValue(sheet, "諮詢時間     ", 0, 0, style);

            sql = "select * from FormSelItem where ItemID=@ItemID order by sort";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ItemID", model.ModelID));
            var inputitem = base.SearchList<FormSelItem>(sql).ToDictionary(v => v.ID.ToString(), v => v.Title);
            var idx = 1;
            foreach (var value in inputitem.Values)
            {
                SetValue(sheet, value + "                  ", 0, idx, style);
                idx += 1;
            }
            SetValue(sheet, "處理進度     ", 0, idx, style);
            var endidx = idx;
            SetValue(sheet, "處理備註     ", 0, idx + 1, style);
            SetValue(sheet, "回覆內容     ", 0, idx + 2, style);

            sheet.SetColumnWidth(endidx + 1, 50 * 256);
            sheet.SetColumnWidth(endidx + 2, 50 * 256);

            sql = @"select  * from FormInputNote  where Type='P' and MainID=@MainID  order by CreateDateTime";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@MainID", model.ModelID));
            var processnotes = base.SearchList<FormInputNote>(sql);

            sql = @"select  * from FormInputNote  where Type='R' and MainID=@MainID  order by CreateDateTime";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@MainID", model.ModelID));
            var replynotes = base.SearchList<FormInputNote>(sql);

            for (var ridx = 1; ridx <= datalist.Count(); ridx++)
            {
                var data = datalist[ridx - 1];
                SetValue(sheet, data.CreateDatetime.Value.ToString("yyyy/MM/dd HH:mm:ss"), ridx, 0, style);
                var input = data.JSONStr;
                var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(input);
                idx = 1;
                foreach (var key in inputitem.Keys)
                {
                    if (dict.ContainsKey(key))
                    {
                        SetValue(sheet, dict[key].Replace("^", " "), ridx, idx, style);
                    }
                    else
                    {
                        SetValue(sheet, "", ridx, idx, style);
                    }
                    idx += 1;
                }
                if (data.Progress.IsNullOrEmpty())
                {
                    SetValue(sheet, "0%", ridx, idx, style);
                }
                else
                {
                    SetValue(sheet, data.Progress + "%", ridx, idx, style);
                }
                var ProcessNote = processnotes.Where(v => v.InputID == data.ID);
                if (ProcessNote.Count() > 0)
                {
                    SetValue(sheet, string.Join(Environment.NewLine, ProcessNote.Select(v => v.NoteText).ToArray()), ridx, idx + 1, style);
                }
                else
                {
                    SetValue(sheet, "     ", ridx, idx + 1, style);
                }
                var ReplyNote = replynotes.Where(v => v.InputID == data.ID);
                if (ReplyNote.Count() > 0)
                {
                    SetValue(sheet, string.Join(Environment.NewLine, ReplyNote.Select(v => v.NoteText).ToArray()), ridx, idx + 2, style);
                }
                else
                {
                    SetValue(sheet, "     ", ridx, idx + 2, style);
                }
            }
            for (int i = 0; i <= endidx; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            hssfworkbook.Write(ms);
            hssfworkbook = null;
            byte[] bytes = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return bytes;
        }

        private void SetValue(ISheet sheet, string value, int _r, int _c, ICellStyle style)
        {
            if (sheet.GetRow(_r) == null)
            {
                sheet.CreateRow(_r);
            }
            if (sheet.GetRow(_r).GetCell(_c) == null)
            {
                sheet.GetRow(_r).CreateCell(_c);
            }
            sheet.GetRow(_r).GetCell(_c).CellStyle = style;
            if (value == null) { value = ""; }
            sheet.GetRow(_r).GetCell(_c).SetCellValue(value);
        }

        public MailInputModel GetMailInput(string id)
        {
            string sql = string.Empty;

            var data = _commonService.GetGeneralList<FormInput>("ID=@ID", new Dictionary<string, string>() { { "ID", id } }).ToList();
            var model = new MailInputModel();
            model.CreateDatetime = data.First().CreateDatetime.Value;

            sql = @"select  * from FormInputNote  where Type='P' and InputId=@InputId  order by CreateDateTime";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@InputId", id));
            var processnotes = base.SearchList<FormInputNote>(sql);

            sql = @"select  * from FormInputNote  where Type='R' and InputId=@InputId  order by CreateDateTime";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@InputId", id));
            var replynotes = base.SearchList<FormInputNote>(sql);

            model.ProcessNote = processnotes.ToArray();
            model.ReplyNote = replynotes.ToArray();
            model.ID = data.First().ID;
            model.Progress = data.First().Progress;
            model.MainID = data.First().MainID.Value;


            var input = data.First().JSONStr;
            var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(input);

            sql = @"select * from FormSelItem where ItemID=@ItemID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ItemID", data.First().MainID));
            var inputitem = base.SearchList<FormSelItem>(sql).ToDictionary(v => v.ID.ToString(), v => v.Title);

            foreach (var key in dict.Keys)
            {
                if (inputitem.ContainsKey(key))
                {
                    model.InputKey.Add(inputitem[key]);
                    if (dict[key].IndexOf("^") >= 0)
                    {
                        model.InputValue.Add(dict[key].Replace("^", " "));
                    }
                    else
                    {
                        model.InputValue.Add(dict[key]);
                    }

                }
            }

            return model;
        }

        public string SaveProgressNote(string text, string id, string account)
        {
            var olddata = _commonService.GetHisEntity<FormInput>("ID", id);

            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {

                        var r = base.InsertObject(new FormInputNote()
                        {
                            Account = account,
                            CreateDateTime = DateTime.Now,
                            InputID = int.Parse(id),
                            NoteText = text,
                            Type = "P",
                            MainID = olddata.MainID.Value
                        }, tran);

                        string sql = "update FormInput set ProcessAccount=@ProcessAccount,ProcessDatetime=GETDATE() where ID=@ID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ProcessAccount", account));
                        base.Parameter.Add(new SqlParameter("@ID", id));
                        base.ExeNonQuery(sql, tran);

                        tran.Commit();
                        return "設定成功";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "刪除表單模組-Email備註");

                        tran.Rollback();
                        return "設定失敗";
                    }
                }
            }

        }

        public string SaveProgress(string progress, string id)
        {
            string sql = @"update FormInput set Progress=@Progress
                                               
                            where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@Progress", progress));
            base.Parameter.Add(new SqlParameter("@ID", id));

            try
            {
                var r = base.ExeNonQuery(sql);

                if (r > 0) { return "設定成功"; } else { return "設定失敗"; }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新表單模組-Mail進度異常,error:" + ex.ToString().NewLineReplace());

                return "設定失敗";
            }
        }
        public string SaveReplyStatus(string ReplyStatus, string id, string account)
        {
            string sql = "update FormInput set ReplyStatus=@ReplyStatus where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ReplyStatus", ReplyStatus));
            base.Parameter.Add(new SqlParameter("@ID", id));

            try
            {
                var r = base.ExeNonQuery(sql);

                if (r > 0) { return "設定成功"; } else { return "設定失敗"; }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新表單模組-Mail進度異常,error:" + ex.ToString().NewLineReplace());

                return "設定失敗";
            }
        }

        public string SaveReponseDept(string ReponseDept, string id, string account)
        {
            string sql = "update FormInput set ReponseDept=@ReponseDept where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ReponseDept", ReponseDept));
            base.Parameter.Add(new SqlParameter("@ID", id));

            try
            {
                var r = base.ExeNonQuery(sql);

                if (r > 0) { return "設定成功"; } else { return "設定失敗"; }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新表單模組-Mail進度異常,error:" + ex.ToString().NewLineReplace());

                return "設定失敗";
            }
        }
        public string SaveReply(string text, string id, string account)
        {
            //NLogManagement.SystemLogInfo("text=" + text + " id=" + id + " account=" + account);
            var olddata = _commonService.GetHisEntity<FormInput>("ID", id);
            if (olddata.ID == 0)
            {
                return "查無資料";
            }
            //NLogManagement.SystemLogInfo("olddata.Count()=" + olddata.Count());
            var r = base.InsertObject(new FormInputNote()
            {
                Account = account,
                CreateDateTime = DateTime.Now,
                InputID = int.Parse(id),
                NoteText = text,
                Type = "R",
                MainID = olddata.MainID.Value
            });

            //NLogManagement.SystemLogInfo("r=" + r);

            string sql = "update FormInput set ReplyAccount=@ReplyAccount,ReplyDatetime=GETDATE() where ID=@ID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ReplyAccount", account));
            base.Parameter.Add(new SqlParameter("@ID", id));

            r = base.ExeNonQuery(sql);
            //NLogManagement.SystemLogInfo("r2=" + r);
            var data = _commonService.GetHisEntity<FormInput>("ID", id);
            var maindata = _commonService.GetHisEntity<ModelFormMain>("ID", data.MainID.Value.ToString());
            //NLogManagement.SystemLogInfo("data count=" + data.Count());
            //NLogManagement.SystemLogInfo("maindata count=" + maindata.Count());
            try
            {
                IList<System.Net.Mail.MailMessage> mailmessage = new List<System.Net.Mail.MailMessage>();

                var mailfrom = System.Web.Configuration.WebConfigurationManager.AppSettings["mailfrom"];

                var NoticeSenderEMail = mailfrom;
                var NoticeSenderName = "客服回覆";
                var NoticeSubject = "客服信箱回覆通知信";
                var setting = _commonService.GetHisEntity<FormSetting>("ItemID", data.MainID.Value.ToString());

                if (setting.ID > 0)
                {
                    if (setting.AdminSenderEMail.IsNullOrEmpty() == false)
                    {
                        NoticeSenderEMail = string.IsNullOrEmpty(setting.AdminSenderEMail) ? mailfrom : setting.AdminSenderEMail;
                        NoticeSenderName = string.IsNullOrEmpty(setting.AdminSenderName) ? "管理者回覆" : setting.AdminSenderName;
                        NoticeSubject = string.IsNullOrEmpty(setting.AdminSenderTitle) ? "管理者回覆通知信" : setting.AdminSenderTitle;


                        var slist = data.EMail.Split(';');
                        foreach (var sender in slist)
                        {
                            if (sender.Trim() == "") { continue; }
                            MailMessage message = new MailMessage();
                            message.From = new MailAddress(NoticeSenderEMail, NoticeSenderName);
                            message.To.Add(new MailAddress(data.EMail));
                            message.SubjectEncoding = System.Text.Encoding.UTF8;
                            message.Subject = NoticeSubject;
                            message.BodyEncoding = System.Text.Encoding.UTF8;
                            string body = " " + data.Name + " ，您好：<br/>" + text;
                            message.Body = body;
                            message.IsBodyHtml = true;
                            message.Priority = MailPriority.High;
                            mailmessage.Add(message);
                        }

                        logger.Info(JsonConvert.SerializeObject(mailmessage).NewLineReplace());

                        if (mailmessage.Count() > 0)
                        {
                            ThreadPool.QueueUserWorkItem(new WaitCallback(DoThreadAndSendMail), new object[] { mailmessage, "" });
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "通知信寄信失敗:寄信給" + data.EMail.NewLineReplace() + "失敗 原因:" + ex.ToString().NewLineReplace());
                return "通知信寄信失敗:寄信給" + data.EMail + "失敗 原因:" + ex.Message;
            }
            return "更新成功";


        }

        public static void DoThreadAndSendMail(object dataarray)
        {
            try
            {

                object[] objarr = (object[])dataarray;
                IList<System.Net.Mail.MailMessage> mailmessage = (IList<System.Net.Mail.MailMessage>)objarr[0];
                string filepath = (string)objarr[1];

                foreach (var ms in mailmessage)
                {
                    if (string.IsNullOrEmpty(filepath) == false)
                    {
                        using (Attachment attachment = new Attachment(filepath))
                        {
                            ms.Attachments.Add(attachment);
                        }
                    }

                    client.Send(ms);
                }
                logger.Info("寄信成功！，data:" + JsonConvert.SerializeObject(mailmessage).NewLineReplace());

            }
            catch (Exception ex)
            {
                logger.Error(ex, "寄信失敗，error:" + ex.ToString().NewLineReplace());
            }
        }

        public DataTable ABTCOR010(string serno)
        {
            DataTable dt = new DataTable();
            dt.TableName = "data";

            dt.Columns.Add("serno", typeof(string));
            dt.Columns.Add("Cusname", typeof(string));
            dt.Columns.Add("Cus_Email", typeof(string));
            dt.Columns.Add("Subject", typeof(string));
            dt.Columns.Add("Cus_tel", typeof(string));
            dt.Columns.Add("Cus_Content", typeof(string));
            dt.Columns.Add("Pstatus", typeof(string));
            dt.Columns.Add("Rsp_kind", typeof(string));
            dt.Columns.Add("Rsp_Dept", typeof(string));
            dt.Columns.Add("Rsp_Content", typeof(string));
            dt.Columns.Add("CreateTime", typeof(string));
            dt.Columns.Add("RspTime", typeof(string));
            dt.Columns.Add("UpdateUserid", typeof(string));
            dt.Columns.Add("BankName", typeof(string));

            DataRow row = dt.NewRow();

            MailInputModel model = this.GetMailInput(serno);

            List<FormSelItem> ListFormSelItem = GetListFormSelItem(model.MainID.ToString());

            row["serno"] = serno;
            row["Cusname"] = model.InputValue[0];
            row["Cus_Email"] = model.InputValue[1];
            row["Subject"] = model.InputValue[2];
            row["Cus_tel"] = model.InputValue[3];
            row["Cus_Content"] = model.InputValue[4];
            row["Pstatus"] = "";
            row["Rsp_kind"] = "";
            row["Rsp_Dept"] = "";
            row["Rsp_Content"] = "";
            row["CreateTime"] = model.CreateDatetime.ToString("yyyy/MM/dd HH:mm:ss");
            row["RspTime"] = "";
            row["UpdateUserid"] = "";
            row["BankName"] = "";



            dt.Rows.Add(row);
            dt.AcceptChanges();

            return dt;
        }



        #endregion

        #region FormManager

        public Paging<FormSelItem> PagingSelItem(SearchModelBase model)
        {
            var Paging = new Paging<FormSelItem>();

            string sql = @"select * from FormSelItem where ItemID=@ItemID";
            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ItemID", model.ModelID.ToString()));
            if (model.Search != "")
            {
                if (string.IsNullOrEmpty(model.Search) == false)
                {
                    sql += " and Name like @Name"; ;
                    base.Parameter.Add(new SqlParameter("@Name", "%" + model.Search + "%"));
                }
            }

            var onepagecnt = model.Limit;

            Paging.rows = base.SearchListPage<FormSelItem>(sql, model.Offset, model.Limit, " order by " + model.Sort);
            Paging.total = base.SearchCount(sql);
            return Paging;
        }

        public string UpdateItemSeq(string modelid, int id, int seq, string account, string username)
        {
            string sql = string.Empty;
            try
            {
                if (seq <= 0) { seq = 1; }
                FormSelItem oldmodel = _commonService.GetGeneral<FormSelItem>("ID=@ID", new Dictionary<string, string>() { { "ID", id.ToString() } });
                if (oldmodel.ID == 0) { return "更新失敗"; }
                var diff = "";

                diff = diff.TrimStart(',');
                bool r = false;
                if (oldmodel.Sort != seq)
                {
                    IList<FormSelItem> mtseqdata = null;

                    mtseqdata = _commonService.GetGeneralList<FormSelItem>("Sort>@Sort and ItemID=@ItemID"
                                                                                  , new Dictionary<string, string>() { { "Sort", seq.ToString() }, { "ItemID", modelid } })
                                                                                  .OrderBy(v => v.Sort)
                                                                                  .ToList();

                    var updatetime = DateTime.Now;
                    if (mtseqdata.Count() == 0)
                    {
                        var totalcnt = 0;
                        sql = $@"select * from FormSelItem where ItemID=@ItemID";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@ItemID", modelid));

                        totalcnt = base.SearchCount(sql); //_sqlrepository.GetCountUseWhere("Lang_ID=@1", new object[] { langid });
                        seq = totalcnt;
                    }

                    var qseq = (seq > oldmodel.Sort) ? seq : oldmodel.Sort;
                    IList<FormSelItem> ltseqdata = null;
                    ltseqdata = _commonService.GetGeneralList<FormSelItem>("Sort<=@Sort and ItemID=@ItemID"
                                                                                  , new Dictionary<string, string>() { { "Sort", qseq.ToString() }, { "ItemID", modelid } })
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
                logger.Error(ex, "更新表單模組-表單設定，error:" + ex.ToString().NewLineReplace());
                return "更新失敗";
            }
        }

        public string SetItemIsMust(string id, bool status, string account, string username)
        {
            try
            {
                string sql = "update FormSelItem set MustInput=@MustInput where ID=@ID";
                base.Parameter.Clear();
                base.Parameter.Add(new SqlParameter("@MustInput", status));
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
                logger.Error(ex, "修改FormSelItem顯示狀態失敗:" + ex.ToString().NewLineReplace());
                return "更新失敗";
            }
        }

        public string DeleteItem(string[] idlist, string delaccount, string account, string username)
        {
            using (SqlConnection connection = base.OpenConnection())
            {
                using (SqlTransaction tran = base.GetTransaction(connection))
                {
                    try
                    {
                        var r = 0;



                        string sql = @"delete FormSelItem where ID in (select ListItem from dbo.SplitList(',',@idlist))";
                        base.Parameter.Clear();
                        base.Parameter.Add(new SqlParameter("@idlist", string.Join(",", idlist)));
                        r = base.ExeNonQuery(sql, tran);

                        var rstr = "";
                        if (r >= 0)
                        {
                            //NLogManagement.SystemLogInfo("刪除FormSelItem:" + delaccount);
                            rstr = "刪除成功";
                        }
                        else
                        {
                            rstr = "刪除失敗";
                        }

                        //update sort order
                        sql = @"with cte as
                                (
                                select 
                                    ROW_NUMBER()OVER(partition by t.ItemID  order by sort) as ROW,t.ID
                                    from FormSelItem t 
                                )
                                update s set s.Sort = t.ROW from cte  t
                                left join FormSelItem s on t.ID = s.ID";
                        base.Parameter.Clear();
                        base.ExeNonQuery(sql, tran);

                        tran.Commit();

                        return rstr;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        logger.Error(ex, "刪除FormSelItem失敗:" + ex.ToString().NewLineReplace());
                        return "刪除失敗";
                    }
                }
            }
        }

        public FormItemSettingModel GetSelItemByID(string id)
        {
            var data = _commonService.GetHisEntity<FormSelItem>("ID", id);
            if (data.ID > 0)
            {
                var model = new FormItemSettingModel()
                {
                    ID = data.ID,
                    Title = data.Title,
                    ColumnNum = data.ColumnNum == null ? "" : data.ColumnNum.ToString(),
                    DefaultText = data.DefaultText,
                    Description = data.Description == null ? "" : data.Description,
                    ItemMode = data.ItemMode.Value,
                    MainID = data.ItemID,
                    SelList = data.SelList,
                    TextLength = data.TextLength == null ? "" : data.TextLength.ToString(),
                    RowNum = data.RowNum == null ? "" : data.RowNum.ToString()
                };
                if (model.ItemMode == 1) { model.ItemModeName = "單行輸入"; }
                if (model.ItemMode == 2) { model.ItemModeName = "多行輸入"; }
                if (model.ItemMode == 3) { model.ItemModeName = "單選"; }
                if (model.ItemMode == 4) { model.ItemModeName = "複選"; }
                if (model.ItemMode == 5) { model.ItemModeName = "下拉選單"; }
                return model;
            }
            else
            {
                return null;
            }
        }

        public string EditSelItem(FormItemSettingModel model)
        {
            var alldata = _commonService.GetGeneralList<FormSelItem>("ItemID=@ItemID", new Dictionary<string, string>() { { "ItemID", model.MainID.Value.ToString() } })
                                        .ToList();

            var maxsort = _commonService.GetMaxID("FormSelitem", "Sort", "ItemID", model.MainID.Value.ToString());

            try
            {
                if (model.ID < 0)
                {
                    var savemodel = new FormSelItem()
                    {
                        ItemID = model.MainID,
                        DefaultText = model.DefaultText,
                        SelList = model.SelList,
                        Description = model.Description == null ? "" : model.Description,
                        MustInput = true,
                        Sort = maxsort,
                        Status = true,
                        Title = model.Title,
                        ItemMode = model.ItemMode
                    };
                    if (model.ColumnNum != null)
                    {
                        savemodel.ColumnNum = int.Parse(model.ColumnNum);
                    }
                    if (model.TextLength != null)
                    {
                        savemodel.TextLength = int.Parse(model.TextLength);
                    }
                    if (model.RowNum != null)
                    {
                        savemodel.RowNum = int.Parse(model.RowNum);
                    }
                    var r = base.InsertObject(savemodel);

                    if (r > 0) { return "新增成功"; }
                    else
                    {
                        return "新增失敗";
                    }
                }
                else
                {
                    var olddata = _commonService.GetHisEntity<FormSelItem>("ID", model.ID.ToString());
                    olddata.DefaultText = model.DefaultText == null ? "" : model.DefaultText;
                    olddata.SelList = model.SelList == null ? "" : model.SelList;
                    olddata.Description = model.Description == null ? "" : model.Description;
                    olddata.Title = model.Title == null ? "" : model.Title;
                    if (model.ColumnNum != null)
                    {
                        olddata.ColumnNum = int.Parse(model.ColumnNum);
                    }
                    if (model.TextLength != null)
                    {
                        olddata.TextLength = int.Parse(model.TextLength);
                    }
                    if (model.RowNum != null)
                    {
                        olddata.RowNum = int.Parse(model.RowNum);
                    }

                    var r = base.UpdateObject(olddata);

                    if (r) { return "修改成功"; }
                    else
                    {
                        return "修改失敗";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "修改表單設計項目異常，error:" + ex.ToString().NewLineReplace());
                return "修改失敗";
            }
        }
        #endregion

        #region FormSetting
        public FormSettingModel GetFormSetting(string mainid)
        {
            var model = _commonService.GetHisEntity<FormSetting>("ItemID", mainid);
            if (model.ID > 0)
            {
                return new FormSettingModel()
                {
                    ID = model.ID,
                    AdminSenderEMail = model.AdminSenderEMail,
                    AdminSenderName = model.AdminSenderName,
                    AdminSenderTitle = model.AdminSenderTitle,
                    ConfirmContent = model.ConfirmContent,
                    FormDesc = model.FormDesc,
                    ItemID = model.ItemID.Value,
                    ReceiveMail = model.ReceiveMail,
                    SenderEMail = model.SenderEMail,
                    SenderName = model.SenderName,
                    SenderTitle = model.SenderTitle
                };
            }
            else
            {
                return new FormSettingModel() { ItemID = int.Parse(mainid) };
            }
        }

        public string SaveSetting(FormSettingModel model)
        {
            try
            {
                if (model.ID < 0)
                {
                    var r = base.InsertObject(new FormSetting
                    {
                        ItemID = model.ItemID,
                        ReceiveMail = model.ReceiveMail,
                        AdminSenderEMail = model.AdminSenderEMail,
                        AdminSenderName = model.AdminSenderName,
                        AdminSenderTitle = model.AdminSenderTitle,
                        ConfirmContent = model.ConfirmContent,
                        FormDesc = model.FormDesc,
                        SenderEMail = model.SenderEMail,
                        SenderName = model.SenderName,
                        SenderTitle = model.SenderTitle
                    });
                    if (r > 0)
                    {
                        return "設定成功";
                    }
                    else
                    {
                        return "設定失敗";
                    }
                }
                else
                {
                    var r = base.UpdateObject(new FormSetting
                    {
                        ID = model.ID,
                        ItemID = model.ItemID,
                        ReceiveMail = model.ReceiveMail == null ? "" : model.ReceiveMail,
                        AdminSenderEMail = model.AdminSenderEMail == null ? "" : model.AdminSenderEMail,
                        AdminSenderName = model.AdminSenderName == null ? "" : model.AdminSenderName,
                        AdminSenderTitle = model.AdminSenderTitle == null ? "" : model.AdminSenderTitle,
                        ConfirmContent = model.ConfirmContent == null ? "" : model.ConfirmContent,
                        FormDesc = model.FormDesc == null ? "" : model.FormDesc,
                        SenderEMail = model.SenderEMail == null ? "" : model.SenderEMail,
                        SenderName = model.SenderName == null ? "" : model.SenderName,
                        SenderTitle = model.SenderTitle == null ? "" : model.SenderTitle,
                    });
                    if (r)
                    {
                        return "設定成功";
                    }
                    else
                    {
                        return "設定失敗";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "修改表單設定異常,error:" + ex.ToString().NewLineReplace());

                return "設定失敗";
            }

        }
        #endregion

        #region 前台
        public string SaveForm(string jsonstr, Dictionary<string, string> tempd, string itemid)
        {

            if (tempd.Count() == 0) { return "無輸入資料"; }
            var name = "";
            var email = "";
            string title = string.Empty;
            var lists = _commonService.GetGeneralList<FormSelItem>("ItemID=@ItemID Order by  Sort", new Dictionary<string, string>() { { "ItemID", itemid } }).ToList();
            var namekey = lists.Where(v => v.KayName == "Name");
            if (namekey.Count() > 0)
            {
                if (tempd.ContainsKey(namekey.First().ID.ToString()))
                {
                    name = tempd[namekey.First().ID.ToString()];
                }
            }
            else
            {
                namekey = lists.Where(v => v.Title.Contains("姓名") || v.Title == "聯絡人" );
                if (namekey.Count() > 0)
                {
                    if (tempd.ContainsKey(namekey.First().ID.ToString()))
                    {
                        name = tempd[namekey.First().ID.ToString()];
                    }
                }

            }

            if (lists.Where(t => t.Title == "主題").Count() > 0)
            {
                title = tempd[lists.Where(t => t.Title == "主題").First().ID.ToString()];
            }


            var sendbodyarr = new List<string>();
            foreach (var tempkey in tempd.Keys)
            {
                var keydata = lists.Where(v => v.ID == int.Parse(tempkey));
                if (keydata.Count() > 0)
                {
                    sendbodyarr.Add(keydata.First().Title + ":" + tempd[tempkey]);
                }
            }
            var sendbodystr = string.Join("<br>", sendbodyarr);
            var emailkey = lists.Where(v => v.KayName == "EMail");
            if (emailkey.Count() > 0)
            {
                if (tempd.ContainsKey(emailkey.First().ID.ToString()))
                {
                    email = tempd[emailkey.First().ID.ToString()];
                }
            }
            else
            {
                emailkey = lists.Where(v => v.Title.Replace("-", "").ToLower() == "email");
                if (emailkey.Count() > 0)
                {
                    if (tempd.ContainsKey(emailkey.First().ID.ToString()))
                    {
                        email = tempd[emailkey.First().ID.ToString()];
                    }
                }
            }
            var r = base.InsertObject(new FormInput()
            {
                CreateDatetime = DateTime.Now,
                EMail = email,
                JSONStr = jsonstr,
                MainID = int.Parse(itemid),
                Name = name,
                Progress = "0"
            });
            if (r < 0)
            {
                return "輸入失敗";
            }
            else
            {
                try
                {
                    IList<System.Net.Mail.MailMessage> mailmessage = new List<System.Net.Mail.MailMessage>();

                    var mailfrom = System.Web.Configuration.WebConfigurationManager.AppSettings["mailfrom"];
                    var setting = _commonService.GetGeneral<FormSetting>("ItemID=@ItemID", new Dictionary<string, string> { { "ItemID", itemid } });

                    if (setting.ID > 0)
                    {
                        if (setting.SenderEMail.IsNullOrEmpty() == false)
                        {
                            var maindata = _commonService.GetGeneral<ModelFormMain>("ID=@ID", new Dictionary<string, string>() { { "ID", itemid } });

                            var NoticeSenderEMail = setting.SenderEMail;
                            var NoticeSenderName = setting.SenderName;
                            var NoticeSubject = setting.SenderTitle;
                            NoticeSenderEMail = string.IsNullOrEmpty(setting.SenderEMail) ? mailfrom : setting.SenderEMail;
                            NoticeSenderName = string.IsNullOrEmpty(setting.SenderName) ? "會員填寫表單回覆" : setting.SenderName;
                            NoticeSubject = string.IsNullOrEmpty(setting.SenderTitle) ? "會員填寫表單回覆通知信" : setting.SenderTitle;




                            var slist = setting.ReceiveMail.Split(';');
                            foreach (var sender in slist)
                            {
                                if (sender.Trim() == "") { continue; }
                                MailMessage message = new MailMessage();
                                message.From = new MailAddress(NoticeSenderEMail, NoticeSenderName);
                                message.To.Add(new MailAddress(sender));
                                message.SubjectEncoding = System.Text.Encoding.UTF8;
                                message.Subject = NoticeSubject;
                                message.BodyEncoding = System.Text.Encoding.UTF8;
                                string body = "您好：<br/>" +
                                name + "於" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "填寫表單【" + maindata.Name + "】<br>" + sendbodystr;

                                message.Body = body;
                                message.IsBodyHtml = true;
                                message.Priority = MailPriority.High;
                                mailmessage.Add(message);
                            }

                            if (mailmessage.Count() > 0)
                            {
                                ThreadPool.QueueUserWorkItem(new WaitCallback(DoThreadAndSendMail), new object[] { mailmessage, "" });
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    logger.Error(ex, "通知信寄信失敗 原因:" + ex.ToString().NewLineReplace());
                }
                return "";
            }

        }
        #endregion
    }
}
