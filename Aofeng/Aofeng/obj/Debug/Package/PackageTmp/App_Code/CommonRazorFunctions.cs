using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oaww.Business;
using Oaww.Utility;
namespace ASP
{
    public static partial class LangExt
    {
        
        public static string GetLang(this string sstr)
        {
            CommonService _commonService = new CommonService();

            var Lang = HttpContext.Current.Session["LangID"].ToString();

            if (_commonService.chkExist(sstr))
            {
                string convert = _commonService.GetMulti(sstr, Lang);

                sstr = convert.IsNullOrEmpty() ? sstr : convert;
            }
            else
            {
                _commonService.AddMultiKey(sstr);
            }

            return sstr;
        }
    }
}