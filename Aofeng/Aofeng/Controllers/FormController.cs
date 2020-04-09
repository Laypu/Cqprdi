using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oaww.Business;
using Oaww.Entity;
using Oaww.ViewModel;
using System.Configuration;
using System.IO;
using Oaww.Utility;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Aofeng.Controllers
{
    public class FormController : BaseController
    {
        FormService _Service = new FormService();

        // GET: Form
        public ActionResult Index(string itemid,string mid)
        {
            if (string.IsNullOrEmpty(itemid)|| string.IsNullOrEmpty(mid))
            {
                return RedirectToAction("Index", "Home");
            }
            FormViewModel model = new FormViewModel() { mid = mid, itemid = itemid};
            
            model.FormSetting = _Service.GetItemFormSetting(itemid);
            model.formSelItems = _Service.GetListFormSelItem(itemid);
            Dictionary<string, string> json = new Dictionary<string, string>();

            if (Session["json"] != null)
            {
                json = (Dictionary<string, string>)Session["json"];

                Session["json"] = null;
            }

            model.formSelItems.ForEach(t =>
            {
                if (json != null && json.ContainsKey(t.ID.ToString()))
                {
                    t.DefaultText = json[t.ID.ToString()];
                }
                else
                {
                    t.DefaultText = t.DefaultText;
                }
            });
            ViewBag.itemid = itemid;
            ViewBag.mid = mid;

            return View(model);
        }

        public ActionResult SaveForm(string itemid, string mid)
        {
            Session["ErrorMessage"] = "";
            string jsonstr = string.Empty;
            string btn = string.Empty;

            Dictionary<string, string> json = new Dictionary<string, string>();
            List<FormSelItem> ListFormSelItem = _Service.GetListFormSelItem(itemid);
            string ErrorMessage = string.Empty;

            ListFormSelItem.OrderBy(t=>t.ID).ToList().ForEach(t =>
            {
                if (Request[t.ID.ToString()] != null)
                {
                    json.Add(t.ID.ToString(), Request[t.ID.ToString()]);
                }

                if (t.MustInput && (Request[t.ID.ToString()] == null || Request[t.ID.ToString()].IsNullOrEmpty()))
                {
                    ErrorMessage += t.Title + "不得為空白！ <br/>";
                    btn = t.ID.ToString();
                    
                }


                if (t.Title.ToUpper().Replace("-", "") == "EMAIL" && (Request[t.ID.ToString()] != null && Request[t.ID.ToString()].IsNullOrEmpty() == false))
                {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(Request[t.ID.ToString()].ToString());

                    if (match.Success == false)
                    {
                        ErrorMessage += t.Title + "格式錯誤！ <br/>";
                        btn = t.ID.ToString()+"Express";
                    }
                }
            });

            Session["json"] = json;

            if (ErrorMessage.IsNullOrEmpty() == false)
            {
                Session["ErrorMessage"] = ErrorMessage;
                return RedirectToAction("Index", new { mid = mid, itemid = itemid});
            }

            //if (!this.IsCaptchaValid("驗證失敗！") && (itemid == "1" || itemid =="2"))
            //{
            //    Session["ErrorMessage"] = "驗證碼失敗，請重新輸入！";
            //    btn = "Captcha";

            //    return RedirectToAction("Index", new { mid = mid, itemid = itemid});
            //}

            string result = _Service.SaveForm(JsonConvert.SerializeObject(json, Formatting.None), json, itemid);

            if(itemid == "1" || itemid == "2")
            {
                if (result == "輸入失敗")
                {
                    Session["ErrorMessage"] = "輸入失敗！";

                    return RedirectToAction("Index", new { mid = mid, itemid = itemid, first = false, btn = btn });
                }
                else
                {
                    Session["json"] = null;

                    Session["ErrorMessage"] = result;
                    return RedirectToAction("ConfirmContent", new { mid = mid, itemid = itemid });
                }
            }
            else
            {
                return Json(result);
            }
        }
        public ActionResult ConfirmContent(string mid, string itemid)
        {
            FormSetting formSetting = _Service.GetItemFormSetting(itemid);
            return View(formSetting);
        }
    }
}