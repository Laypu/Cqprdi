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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/Video/UnitSetting.cshtml")]
    public partial class _Areas_webadmin_Views_Video_UnitSetting_cshtml : System.Web.Mvc.WebViewPage<VideoUnitSettingModel>
    {
        public _Areas_webadmin_Views_Video_UnitSetting_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
  
    Layout = "~/Areas/webadmin/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<!--page bar start-->\r\n<div");

WriteLiteral(" class=\"page-bar\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" class=\"page-breadcrumb\"");

WriteLiteral(">\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 208), Tuple.Create("\"", 242)
            
            #line 9 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 215), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Home")
            
            #line default
            #line hidden
, 215), false)
);

WriteLiteral(">Home</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 339), Tuple.Create("\"", 374)
            
            #line 13 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 346), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Model")
            
            #line default
            #line hidden
, 346), false)
);

WriteLiteral(">模組管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 471), Tuple.Create("\"", 506)
            
            #line 17 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 478), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Video")
            
            #line default
            #line hidden
, 478), false)
);

WriteLiteral(">影音管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">單元設定</a>\r\n        </li>\r\n    </ul>\r\n\r\n</div>\r\n<!--page bar end-->\r\n<!--message s" +
"tart-->\r\n<div");

WriteLiteral(" class=\"title_01\"");

WriteLiteral(">");

            
            #line 28 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
                 Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n\r\n<div");

WriteLiteral(" class=\"portlet light bordered\"");

WriteLiteral(">\r\n    <!--management item start-->\r\n    <div");

WriteLiteral(" class=\"table-toolbar\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-md-12 col-sm-12 col-xs-12\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"display_inline\"");

WriteLiteral(">\r\n                <p");

WriteLiteral(" class=\"class_title\"");

WriteLiteral(">管理項目</p>\r\n                <select");

WriteLiteral(" class=\"form-control_1\"");

WriteLiteral(" onChange=\"window.location.href=this.value\"");

WriteLiteral(">\r\n                    <option");

WriteAttribute("value", Tuple.Create(" value=\"", 1116), Tuple.Create("\"", 1175)
            
            #line 37 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 1124), Tuple.Create<System.Object, System.Int32>(Url.Action("ModelItem",new { mainid=Model.MainID})
            
            #line default
            #line hidden
, 1124), false)
);

WriteLiteral(" selected>影音管理</option>\r\n                    <option");

WriteLiteral(" value=\"#\"");

WriteLiteral(" selected>單元管理</option>\r\n                </select>\r\n            </div>\r\n        <" +
"/div>\r\n    </div>\r\n    <!--management item end-->\r\n    <form");

WriteLiteral(" class=\"form-horizontal form-bordered\"");

WriteLiteral(" method=\"Post\"");

WriteLiteral(" id=\"editform\"");

WriteAttribute("action", Tuple.Create(" action=\'", 1445), Tuple.Create("\'", 1477)
            
            #line 44 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
    , Tuple.Create(Tuple.Create("", 1454), Tuple.Create<System.Object, System.Int32>(Url.Action("SaveUnit")
            
            #line default
            #line hidden
, 1454), false)
);

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"portlet light form-fit bordered\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"portlet-body form\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-horizontal form-bordered\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-body\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 49 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
                   Write(Html.HiddenFor(Model => Model.MainID));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 50 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
                   Write(Html.HiddenFor(Model => Model.ID));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 search_item\"");

WriteLiteral(">功能提供</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 bg-white mobile_white\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" class=\"mt-checkbox mt-checkbox-outline\"");

WriteLiteral(">\r\n");

WriteLiteral("                                    ");

            
            #line 55 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
                               Write(Html.CheckBoxFor(m => m.IsForward, new { @class = "checkboxes" }));

            
            #line default
            #line hidden
WriteLiteral("轉寄好友\r\n                                    <span></span>\r\n                        " +
"        </label>\r\n\r\n                                <label");

WriteLiteral(" class=\"mt-checkbox mt-checkbox-outline\"");

WriteLiteral(">\r\n");

WriteLiteral("                                    ");

            
            #line 60 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
                               Write(Html.CheckBoxFor(m => m.IsPrint, new { @class = "checkboxes" }));

            
            #line default
            #line hidden
WriteLiteral("友善列印\r\n                                    <span></span>\r\n                        " +
"        </label>\r\n\r\n                                <label");

WriteLiteral(" class=\"mt-checkbox mt-checkbox-outline\"");

WriteLiteral(">\r\n");

WriteLiteral("                                    ");

            
            #line 65 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
                               Write(Html.CheckBoxFor(m => m.IsShare, new { @class = "checkboxes" }));

            
            #line default
            #line hidden
WriteLiteral("分享網站\r\n                                    <span></span>\r\n                        " +
"        </label>\r\n                            </div>\r\n                        </" +
"div>\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-2 col-xs-12 bg-grey_1 search_item\"");

WriteLiteral(">每頁顯示筆數</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-4 col-sm-4 col-xs-12 bg-white mobile_white\"");

WriteLiteral(">\r\n                                <select");

WriteLiteral(" class=\"form_02\"");

WriteLiteral(" id=\"ShowCount\"");

WriteLiteral(" name=\"ShowCount\"");

WriteLiteral(">\r\n                                    <option");

WriteLiteral(" value=\"9\"");

WriteLiteral(">9</option>\r\n                                    <option");

WriteLiteral(" value=\"12\"");

WriteLiteral(">12</option>\r\n                                    <option");

WriteLiteral(" value=\"15\"");

WriteLiteral(">15</option>\r\n                                    <option");

WriteLiteral(" value=\"18\"");

WriteLiteral(">18</option>\r\n                                </select>\r\n                        " +
"    </div>\r\n                            <div");

WriteLiteral(" class=\"col-md-6 col-sm-6 hidden-xs bg-white\"");

WriteLiteral(">&nbsp;</div>\r\n                        </div>\r\n                    </div>\r\n      " +
"          </div>\r\n            </div>\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"text-center search_padding\"");

WriteLiteral(">\r\n            <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn blue\"");

WriteLiteral(" id=\"btn_submit\"");

WriteLiteral(">確認送出</button>\r\n        </div>\r\n    </form>\r\n</div>\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 4045), Tuple.Create("\"", 4097)
            
            #line 93 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 4051), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/ckeditor/ckeditor.js")
            
            #line default
            #line hidden
, 4051), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 4121), Tuple.Create("\"", 4165)
            
            #line 94 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 4127), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/datatable.js")
            
            #line default
            #line hidden
, 4127), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 4189), Tuple.Create("\"", 4230)
            
            #line 95 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 4195), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/custom.js")
            
            #line default
            #line hidden
, 4195), false)
);

WriteLiteral("></script>\r\n    <script>\r\n        $(function () {\r\n\r\n            if (\'");

            
            #line 99 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
            Write(Model.ID);

            
            #line default
            #line hidden
WriteLiteral("\' > 0) {\r\n                $(\'#ShowCount\').val(\'");

            
            #line 100 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
                                Write(Model.ShowCount);

            
            #line default
            #line hidden
WriteLiteral(@"');
            }

             $('#editform').submit(function (event) {
                    var t = $(this).serialize();
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result == """") {
                                alert(""儲存成功"");
                            } else {
                                alert(result);
                                    var obj = {};
                                obj.mainid = '");

            
            #line 115 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
                                         Write(Model.MainID);

            
            #line default
            #line hidden
WriteLiteral("\'\r\n                                  CreatePost(\'");

            
            #line 116 "..\..\Areas\webadmin\Views\Video\UnitSetting.cshtml"
                                         Write(Url.Action("UnitSetting"));

            
            #line default
            #line hidden
WriteLiteral("\', obj);\r\n                            }\r\n                        }\r\n             " +
"      });\r\n                    return false;\r\n            })\r\n        });\r\n\r\n   " +
" </script>\r\n\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
