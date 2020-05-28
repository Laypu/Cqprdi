using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net;
using Oaww.Utility;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Security;

namespace Oaww.Utility
{
    public static class CommonFun
    {

        public static List<SelectListItem> AddDefault(this List<SelectListItem> List)
        {
            List.Insert(0, new SelectListItem() { Value = "", Text = "全部" });

            return List;
        }

        public static List<SelectListItem> AddDefault(this List<SelectListItem> List, string DefaultString)
        {
            List.Insert(0, new SelectListItem() { Value = "", Text = DefaultString });

            return List;
        }

        public static string HtmlAttributeConvert(IDictionary<string, string> HtmlAttribute)
        {
            string returnstring = "";

            foreach (KeyValuePair<string, string> entry in HtmlAttribute)
            {
                if (entry.Key == "id")
                {
                    returnstring += "id='" + entry.Value + "' ";
                }
                else if (entry.Key == "style")
                {
                    returnstring += " style='" + entry.Value + "' ";
                }
                else if (entry.Key == "name")
                {
                    returnstring += " name='" + entry.Value + "' ";
                }
                else if (entry.Key == "width")
                {
                    returnstring += " width='" + entry.Value + "' ";
                }
                else if (entry.Key == "class")
                {
                    returnstring += " class='" + entry.Value + "' ";
                }
                else if (entry.Key == "disabled" && entry.Value != "false")
                {
                    returnstring += " disabled='" + entry.Value + "' ";
                }
                else if (entry.Key.ToLower() == "onchange")
                {
                    returnstring += " onChange=" + entry.Value + " ";
                }

            }

            return returnstring;
        }

        public static SmtpClient GetSMTPClient()
        {


            SmtpClient SmtpClient = new SmtpClient();
            SmtpClient.Host = System.Web.Configuration.WebConfigurationManager.AppSettings["MailHost"];

            string port = System.Web.Configuration.WebConfigurationManager.AppSettings["MailPort"];

            if (int.TryParse(port, out int portInt))
            {
                SmtpClient.Port = portInt;
            }
            else
            {
                SmtpClient.Port = 25;
            }

            string ssl = System.Web.Configuration.WebConfigurationManager.AppSettings["MailSSL"];

            if (bool.TryParse(ssl, out bool sslBool))
            {
                SmtpClient.EnableSsl = sslBool;
            }
            else
            {
                SmtpClient.EnableSsl = false;
            }

            SmtpClient.Timeout = 50 * 1000;

            string UserID = System.Web.Configuration.WebConfigurationManager.AppSettings["MailUserID"];

            SecureString valuePtr = new SecureString() ;

            if (System.Web.Configuration.WebConfigurationManager.AppSettings["MailPXD"] != null
                && System.Web.Configuration.WebConfigurationManager.AppSettings["MailPXD"].IsNullOrEmpty() == false)
            {
                valuePtr = System.Web.Configuration.WebConfigurationManager.AppSettings["MailPXD"].SecureString();
            }





            if (!string.IsNullOrEmpty(UserID) && System.Web.Configuration.WebConfigurationManager.AppSettings["MailPXD"].IsNullOrEmpty() == false)
            {

                SmtpClient.Credentials = new NetworkCredential(UserID, valuePtr.SecureStringToString());

            }



            return SmtpClient;
        }

        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        public static string GetPropValue(this object src, string propName)
        {
            var value = src.GetType().GetProperty(propName).GetValue(src, null);

            return value == null ? "" : value.ToString();
        }

        public static string FixLink(string source)
        {
            if (source.IsNullOrEmpty()) { return "#"; }
            if (source.ToLower().IndexOf("http") >= 0)
            {
                return source;
            }
            else if (source.ToLower().IndexOf("~") == 0)
            {
                UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                return helper.Content(source);
            }
            else if (source.ToLower().IndexOf("/") == 0)
            {
                UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                return helper.Content(source);
            }
            else
            {
                return VirtualPathUtility.ToAbsolute("~/") + source;
            }
        }

        public static string GetFileTypeByExtension(this string extension)
        {
            if (extension.ToLower() == "xls" || extension.ToLower() == "xlsx")
            {
                return "Excel";
            }
            else if (extension.ToLower() == "doc" || extension.ToLower() == "docx")
            {
                return "Word";
            }
            else if (extension.ToLower() == "zip" || extension.ToLower() == "7z")
            {
                return "ZIP";
            }
            else if (extension.ToLower() == "pdf")
            {
                return "PDF";
            }
            else if (extension.ToLower() == "png" || extension.ToLower() == "jpg" || extension.ToLower() == "gif" || extension.ToLower() == "jpeg")
            {
                return "IMAGE";
            }
            else
            {
                return "N/A";
            }
        }
    }
}
