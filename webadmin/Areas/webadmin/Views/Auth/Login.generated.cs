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
    
    #line 1 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
    using CaptchaMvc.HtmlHelpers;
    
    #line default
    #line hidden
    using Oaww.Entity;
    using Oaww.Entity.SET;
    using Oaww.HtmlHelp;
    
    #line 2 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
    using Oaww.Utility;
    
    #line default
    #line hidden
    using Oaww.ViewModel;
    using Oaww.ViewModel.Config;
    using Oaww.ViewModel.Lang;
    using Oaww.ViewModel.Search;
    using Oaww.ViewModel.SiteMap;
    using Template;
    using webadmin;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/Auth/Login.cshtml")]
    public partial class _Areas_webadmin_Views_Auth_Login_cshtml : System.Web.Mvc.WebViewPage<SET_LOGIN>
    {
        public _Areas_webadmin_Views_Auth_Login_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 5 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
  
    Layout = null;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<!DOCTYPE html>\r\n<html");

WriteLiteral(" lang=\"zh\"");

WriteLiteral(">\r\n\r\n<head>\r\n    <meta");

WriteLiteral(" charset=\"utf-8\"");

WriteLiteral(" />\r\n    <title>");

            
            #line 14 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
      Write(Model.M_Login01);

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n    <meta");

WriteLiteral(" http-equiv=\"X-UA-Compatible\"");

WriteLiteral(" content=\"IE=edge,chrome=1\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, minimum-scale=1.0, maximum-scale=1.0, user-scalable" +
"=no\"");

WriteLiteral(">\r\n    <link");

WriteLiteral(" href=\"http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=al" +
"l\"");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 535), Tuple.Create("\"", 569)
, Tuple.Create(Tuple.Create("", 542), Tuple.Create<System.Object, System.Int32>(Href("~/Content/bootstrap.min.css")
, 542), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 601), Tuple.Create("\"", 636)
, Tuple.Create(Tuple.Create("", 608), Tuple.Create<System.Object, System.Int32>(Href("~/Content/components.min.css")
, 608), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 668), Tuple.Create("\"", 705)
, Tuple.Create(Tuple.Create("", 675), Tuple.Create<System.Object, System.Int32>(Href("~/Content/font-awesome.min.css")
, 675), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 737), Tuple.Create("\"", 767)
, Tuple.Create(Tuple.Create("", 744), Tuple.Create<System.Object, System.Int32>(Href("~/Content/css/style.css")
, 744), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteLiteral(" rel=\"shortcut icon\"");

WriteAttribute("href", Tuple.Create(" href=\"", 819), Tuple.Create("\"", 851)
, Tuple.Create(Tuple.Create("", 826), Tuple.Create<System.Object, System.Int32>(Href("~/Content/img/favicon.ico")
, 826), false)
);

WriteLiteral(" />\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 868), Tuple.Create("\"", 903)
, Tuple.Create(Tuple.Create("", 874), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery-3.4.1.min.js")
, 874), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 927), Tuple.Create("\"", 970)
, Tuple.Create(Tuple.Create("", 933), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jquery-migrate-3.0.0.min.js")
, 933), false)
);

WriteLiteral("></script>\r\n\r\n</head>\r\n\r\n<body>\r\n    <div");

WriteLiteral(" class=\"login\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"right\"");

WriteAttribute("style", Tuple.Create(" style=\"", 1055), Tuple.Create("\"", 1108)
, Tuple.Create(Tuple.Create("", 1063), Tuple.Create("background:url(", 1063), true)
            
            #line 30 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
, Tuple.Create(Tuple.Create("", 1078), Tuple.Create<System.Object, System.Int32>(Url.Content(Model.M_Login05)
            
            #line default
            #line hidden
, 1078), false)
, Tuple.Create(Tuple.Create("", 1107), Tuple.Create(")", 1107), true)
);

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"title\"");

WriteLiteral(">");

            
            #line 31 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                          Write(Model.M_Login01);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            <div");

WriteLiteral(" class=\"font_con\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"text-shadow\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 34 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
               Write(Html.Raw(Model.M_Login02.safeHtmlFragment()));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </span>\r\n\r\n");

            
            #line 37 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                
            
            #line default
            #line hidden
            
            #line 37 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                 if (Model.M_Login03.IsNullOrEmpty() == false)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <span");

WriteLiteral(" class=\"fa-stack fa-xs\"");

WriteLiteral(">\r\n                        <i");

WriteLiteral(" class=\"fa fa-circle fa-stack-2x\"");

WriteLiteral("></i>\r\n                        <i");

WriteLiteral(" class=\"fa fa-phone fa-stack-1x fa-inverse\"");

WriteLiteral("></i>\r\n                    </span>\r\n");

WriteLiteral("                    <span");

WriteLiteral(" class=\"text-shadow\"");

WriteLiteral(">");

            
            #line 43 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                                         Write(Model.M_Login03);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n");

WriteLiteral("                    <span>&nbsp; &nbsp; &nbsp;</span>\r\n");

            
            #line 45 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 47 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                
            
            #line default
            #line hidden
            
            #line 47 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                 if (Model.M_Login04.IsNullOrEmpty() == false)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <span");

WriteLiteral(" class=\"fa-stack fa-xs\"");

WriteLiteral(">\r\n                        <i");

WriteLiteral(" class=\"fa fa-circle fa-stack-2x\"");

WriteLiteral("></i>\r\n                        <i");

WriteLiteral(" class=\"fa fa-envelope-o fa-stack-1x fa-inverse\"");

WriteLiteral("></i>\r\n                    </span>\r\n");

WriteLiteral("                    <span");

WriteLiteral(" class=\"text-shadow\"");

WriteLiteral(">");

            
            #line 53 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                                         Write(Model.M_Login04);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n");

            
            #line 54 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"left\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"logo\"");

WriteLiteral("><img");

WriteAttribute("src", Tuple.Create(" src=\"", 2293), Tuple.Create("\"", 2328)
            
            #line 59 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
, Tuple.Create(Tuple.Create("", 2299), Tuple.Create<System.Object, System.Int32>(Url.Content(Model.M_Login06)
            
            #line default
            #line hidden
, 2299), false)
);

WriteLiteral(" alt=\"\"");

WriteLiteral(" /></div>\r\n\r\n");

            
            #line 61 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
            
            
            #line default
            #line hidden
            
            #line 61 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
             using (Html.BeginForm("LoginCheck", "Auth", FormMethod.Post, new { @id = "form1", @class = "form-horizontal form-validate", @enctype = "multipart/form-data" }))
            {

            
            #line default
            #line hidden
WriteLiteral("                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"Login\"");

WriteLiteral(" id=\"hidLogin\"");

WriteLiteral(" />\r\n");

WriteLiteral("                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"Password\"");

WriteLiteral(" id=\"hidPwd\"");

WriteLiteral(" />\r\n");

WriteLiteral("                <div");

WriteLiteral(" class=\"input_box\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"input-group\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"input-group-addon input-circle-left\"");

WriteLiteral(">\r\n                            <i");

WriteLiteral(" class=\"fa fa-user\"");

WriteLiteral("></i>\r\n                        </span>\r\n                        <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control input-circle-right\"");

WriteLiteral(" placeholder=\"ID\"");

WriteLiteral(" id=\"txtLogin\"");

WriteLiteral(" MaxLength=\"10\"");

WriteLiteral(">\r\n                    </div>\r\n\r\n                    <div");

WriteLiteral(" class=\"input-group\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"input-group-addon input-circle-left\"");

WriteLiteral(">\r\n                            <i");

WriteLiteral(" class=\"fa fa-key\"");

WriteLiteral("></i>\r\n                        </span>\r\n                        <input");

WriteLiteral(" type=\"password\"");

WriteLiteral(" class=\"form-control input-circle-right\"");

WriteLiteral(" placeholder=\"Password\"");

WriteLiteral(" id=\"txtPwd\"");

WriteLiteral(">\r\n                    </div>\r\n\r\n");

WriteLiteral("                    ");

            
            #line 80 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
               Write(Html.Captcha(4, "Captcha"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                    <div");

WriteLiteral(" class=\"input-group\"");

WriteLiteral(" style=\"width:100%;\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" style=\"color:red\"");

WriteLiteral(">");

            
            #line 83 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
                                            Write(TempData["Error"]);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n                    </div>\r\n\r\n                    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ReturnUrl\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3737), Tuple.Create("\"", 3767)
            
            #line 86 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
, Tuple.Create(Tuple.Create("", 3745), Tuple.Create<System.Object, System.Int32>(ViewData["ReturnUrl"]
            
            #line default
            #line hidden
, 3745), false)
);

WriteLiteral(" />\r\n                </div>\r\n");

            
            #line 88 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"


            
            #line default
            #line hidden
WriteLiteral("                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn green-seagreen\"");

WriteLiteral(" style=\"margin-top:20px\"");

WriteLiteral(" onclick=\"Send()\"");

WriteLiteral(">登入</button>\r\n");

            
            #line 90 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n</body>\r\n\r\n</html>\r\n\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 3992), Tuple.Create("\"", 4014)
, Tuple.Create(Tuple.Create("", 3998), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/aes.js")
, 3998), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 4034), Tuple.Create("\"", 4067)
, Tuple.Create(Tuple.Create("", 4040), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/aesEncrytDecry.js")
, 4040), false)
);

WriteLiteral("></script>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n    $().ready(function () {\r\n\r\n        if (\'");

            
            #line 105 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
        Write(TempData["alterMessage"]);

            
            #line default
            #line hidden
WriteLiteral("\' != \'\') {\r\n            alert(\'");

            
            #line 106 "..\..\Areas\webadmin\Views\Auth\Login.cshtml"
              Write(TempData["alterMessage"]);

            
            #line default
            #line hidden
WriteLiteral(@"');
        }

    })

    function Captcha(e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            Send();
        }
    }



    function Send() {
        event.preventDefault();

        if (DataValidate()) {


            $('#hidLogin').val(aesEncryDecry.encryptStringAES($('#txtLogin').val()));
            $('#hidPwd').val(aesEncryDecry.encryptStringAES($('#txtPwd').val()));

            $('#form1').submit();
        }
    }

    function DataValidate() {

        var msg = '';
        if ($('#txtLogin').val() == '') {
            msg += '- 請輸入使用者代號!! \r\n';
        }

        if ($('#txtPwd').val() == '') {
            msg += '- 請輸入密碼!! \r\n';
        }

        if (msg == '') {
            return true;
        }
        else {
            alert(msg);
            return false;
        }
    }
</script>");

        }
    }
}
#pragma warning restore 1591
