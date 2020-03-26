using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.ViewModel;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Oaww.Utility;
using CaptchaMvc.HtmlHelpers;
using System.ComponentModel.DataAnnotations;

namespace Aofeng.Controllers
{
    public class CommonController : BaseController
    {
        CommonService _commonService = new CommonService();
        public ActionResult Forward(string Url, string Name, string sender = "", string sender_mail = "", string receive_email = "", string message = "", string btn = "")
        {
            ForwardViewModel model = new ForwardViewModel();

            model.Url = HttpUtility.UrlDecode(Url);
            model.Name = HttpUtility.UrlDecode(Name);
            model.sender = sender;
            model.sender_mail = sender_mail;
            model.receive_email = receive_email;
            model.message = message;
            model.btn = btn;

            ViewBag.BoxClass = "inner_box page";
            ViewBag.mid = "Forward";
            return View(model);
        }

        public ActionResult Forward_OK(string Url, string Name, string sender, string sender_mail, string receive_email, string message)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var echeck = new EmailAddressAttribute();
            string result = string.Empty;
            if(sender.IsNullOrEmpty())
            {
                return RedirectToAction("Forward", new { @Url = Url, @Name = Name, @sender = sender, @sender_mail = sender_mail, @receive_email = receive_email, @message = message, @btn = "sender" });
            }
            else if(sender_mail.IsNullOrEmpty())
            {
                return RedirectToAction("Forward", new { @Url = Url, @Name = Name, @sender = sender, @sender_mail = sender_mail, @receive_email = receive_email, @message = message, @btn = "sender_mail" });
            }
            else if (echeck.IsValid(sender_mail) == false)
            {
                return RedirectToAction("Forward", new { @Url = Url, @Name = Name, @sender = sender, @sender_mail = sender_mail, @receive_email = receive_email, @message = message, @btn = "sender_mail_E" });
            }
            else if (receive_email.IsNullOrEmpty())
            {
                return RedirectToAction("Forward", new { @Url = Url, @Name = Name, @sender = sender, @sender_mail = sender_mail, @receive_email = receive_email, @message = message, @btn = "receive_email" });
            }          
            else if (echeck.IsValid(receive_email) == false)
            {
                return RedirectToAction("Forward", new { @Url = Url, @Name = Name, @sender = sender, @sender_mail = sender_mail, @receive_email = receive_email, @message = message, @btn = "receive_email_E" });        
            }
            else if(!this.IsCaptchaValid("驗證失敗！"))
            {
                return RedirectToAction("Forward", new { @Url = Url, @Name = Name, @sender = sender, @sender_mail = sender_mail, @receive_email = receive_email, @message = message, @btn = "Captcha" });
            }
            else
            {
                try
                {

                    SmtpClient smtp = CommonFun.GetSMTPClient();
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(sender_mail, sender);
                    msg.Subject = $"您的朋友 【{sender}】 轉寄中華穀類這個網站連結給您。與您分享！";
                    msg.BodyEncoding = System.Text.Encoding.UTF8;
                    msg.IsBodyHtml = true;

                    msg.Body = message + "<br/>" + HttpUtility.UrlDecode(Url);

                    foreach (string m in receive_email.Split(';'))
                    {
                        msg.To.Add(new MailAddress(m));
                    }
                    smtp.Send(msg);

                    result = "轉寄好友寄送成功";
                }
                catch (Exception ex)
                {
                    result = "轉寄好友寄送失敗";
                }

                ViewBag.result = result;
                ViewBag.mid = "Forward";

                return View();
            }
        }

        
    }
}