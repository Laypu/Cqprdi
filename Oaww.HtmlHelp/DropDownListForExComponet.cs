using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace Oaww.HtmlHelp
{
    public static class DropDownListForExComponet
    {
        public static MvcHtmlString DropDownListForEx(this HtmlHelper helper, IEnumerable<SelectListItem> List, string SelectValue, object HtmlAttributeObj)
        {
            StringBuilder s = new StringBuilder();

            IDictionary<string, string> HtmlAttribute = HtmlAttributeObj.ToDictionary<string>();



            string returnString = "<select ";

            returnString += HtmlAttributeConvert(HtmlAttribute);

            returnString += ">";

            foreach (SelectListItem m in List)
            {
                if (m.Value == SelectValue)
                {
                    returnString += "<option value='" + m.Value + "' selected>" + m.Text + "</option>";
                }
                else
                {
                    returnString += "<option value='" + m.Value + "'>" + m.Text + "</option>";
                }
            }

            returnString += "</select>";

            return MvcHtmlString.Create(returnString);
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
    }
}
