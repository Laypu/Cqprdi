﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using Oaww.Entity;
    using Oaww.Entity.SET;
    using Oaww.HtmlHelp;
    using Oaww.Utility;
    using Oaww.ViewModel;
    using Oaww.ViewModel.Config;
    using Oaww.ViewModel.Search;
    using Template;
    using webadmin;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/Shared/_Footer.cshtml")]
    public partial class _Areas_webadmin_Views_Shared__Footer_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_webadmin_Views_Shared__Footer_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 2 "..\..\Areas\webadmin\Views\Shared\_Footer.cshtml"
  

    SET_BASE SET_BASE = (SET_BASE)ViewBag.SET_BASE;

    Layout = null;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"page-footer\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"page-footer-inner\"");

WriteLiteral(">Copyright &copy; 2020 ");

            
            #line 10 "..\..\Areas\webadmin\Views\Shared\_Footer.cshtml"
                                                    Write(SET_BASE.M_Base01);

            
            #line default
            #line hidden
WriteLiteral(" All rights reserved.</div>\r\n    <div");

WriteLiteral(" class=\"scroll-to-top\"");

WriteLiteral(">\r\n        <i");

WriteLiteral(" class=\"icon-arrow-up\"");

WriteLiteral("></i>\r\n    </div>\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591
