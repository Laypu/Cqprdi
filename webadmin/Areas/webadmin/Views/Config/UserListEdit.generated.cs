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
    
    #line 1 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
    using Oaww.ViewModel;
    
    #line default
    #line hidden
    using Oaww.ViewModel.Config;
    using Oaww.ViewModel.Lang;
    using Oaww.ViewModel.Search;
    using Oaww.ViewModel.SiteMap;
    using Template;
    using webadmin;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/Config/UserListEdit.cshtml")]
    public partial class _Areas_webadmin_Views_Config_UserListEdit_cshtml : System.Web.Mvc.WebViewPage<AdminMemberModel>
    {
        public _Areas_webadmin_Views_Config_UserListEdit_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
  
    Layout = "~/Areas/webadmin/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<!--page bar start-->\r\n<div");

WriteLiteral(" class=\"page-bar all_width\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" class=\"page-breadcrumb\"");

WriteLiteral(">\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 237), Tuple.Create("\"", 271)
            
            #line 10 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
, Tuple.Create(Tuple.Create("", 244), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Home")
            
            #line default
            #line hidden
, 244), false)
);

WriteLiteral(">Home</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            系統管理\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 457), Tuple.Create("\"", 502)
            
            #line 18 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
, Tuple.Create(Tuple.Create("", 464), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Config/UserList")
            
            #line default
            #line hidden
, 464), false)
);

WriteLiteral(">權限管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n");

WriteLiteral("            ");

            
            #line 22 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
       Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("\r\n        </li>\r\n    </ul>\r\n</div>\r\n<!--page bar end-->\r\n<!--message start-->\r\n<d" +
"iv");

WriteLiteral(" class=\"title_01\"");

WriteLiteral(">");

            
            #line 28 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                 Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n<div");

WriteLiteral(" class=\"portlet light bordered\"");

WriteLiteral(">\r\n    <form");

WriteLiteral(" class=\"form-horizontal form-bordered\"");

WriteLiteral(" method=\"Post\"");

WriteLiteral(" id=\"editform\"");

WriteAttribute("action", Tuple.Create(" action=\'", 847), Tuple.Create("\'", 883)
            
            #line 30 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
     , Tuple.Create(Tuple.Create("", 856), Tuple.Create<System.Object, System.Int32>(Url.Action("SaveUserList")
            
            #line default
            #line hidden
, 856), false)
);

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 31 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
   Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"portlet light form-fit bordered\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"portlet-body form\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-horizontal form-bordered\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-body\"");

WriteLiteral(" id=\"form-body\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 36 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                   Write(Html.HiddenFor(model => model.ID));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        <input");

WriteLiteral(" id=\"hid_isAdd\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"isAdd\"");

WriteAttribute("value", Tuple.Create(" value=\'", 1274), Tuple.Create("\'", 1296)
            
            #line 37 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
, Tuple.Create(Tuple.Create("", 1282), Tuple.Create<System.Object, System.Int32>(ViewBag.IsAdd
            
            #line default
            #line hidden
, 1282), false)
);

WriteLiteral(" />\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 col-xs-12 bg-grey_1 search_item\"");

WriteLiteral("><span");

WriteLiteral(" class=\"red\"");

WriteLiteral(">*</span>群組</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 col-xs-12 bg-white mobile_white\"");

WriteLiteral(">\r\n");

WriteLiteral("                                ");

            
            #line 41 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                           Write(Html.DropDownListFor(model => model.GroupId, (IEnumerable<SelectListItem>)ViewBag.grouplist, new { @class = "form-control w-auto checkitem", @id = "GroupId", @name = "GroupId" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                                ");

            
            #line 42 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                           Write(Html.HiddenFor(model => model.GroupId));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                <span");

WriteLiteral(" class=\"required\"");

WriteLiteral(" id=\"GroupId-error\"");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(">請選擇群組</span>\r\n                            </div>\r\n                        </div>" +
"\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 col-xs-12 search_item\"");

WriteLiteral("><span");

WriteLiteral(" class=\"red\"");

WriteLiteral(">*</span>姓名</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 col-xs-12 bg-white mobile_white\"");

WriteLiteral(">\r\n");

WriteLiteral("                                ");

            
            #line 49 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                           Write(Html.EditorFor(model => model.Name, new { htmlAttributes = new { @class = "form-control checkitem" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                <span");

WriteLiteral(" class=\"required\"");

WriteLiteral(" id=\"Name-error\"");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">姓名 必須填寫！</span>\r\n                            </div>\r\n                        </d" +
"iv>\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 col-xs-12 search_item\"");

WriteLiteral("><span");

WriteLiteral(" class=\"red\"");

WriteLiteral(">*</span>帳號</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 col-xs-12 bg-white mobile_white\"");

WriteLiteral(">\r\n");

WriteLiteral("                                ");

            
            #line 56 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                           Write(Html.EditorFor(model => model.Account, new { htmlAttributes = new { @class = "form-control checkitem", @maxlength="25" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                <span");

WriteLiteral(" class=\"required\"");

WriteLiteral(" id=\"Account-error\"");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">帳號 必須填寫！</span>\r\n                            </div>\r\n                        </d" +
"iv>\r\n\r\n");

            
            #line 61 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 61 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                         if (Model.ID == 0)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 col-xs-12 search_item\"");

WriteLiteral("><span");

WriteLiteral(" class=\"red\"");

WriteLiteral(">*</span>密碼</div>\r\n                                <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 col-xs-12 bg-white mobile_white form_blank\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" id=\"Password\"");

WriteLiteral(" type=\"password\"");

WriteLiteral(" class=\"form-control checkitem\"");

WriteLiteral(" name=\"Password\"");

WriteLiteral(" />\r\n                                    <span");

WriteLiteral(" class=\"required\"");

WriteLiteral(" id=\"Password-error\"");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">密碼 必須填寫！</span>\r\n                                </div>\r\n                       " +
"     </div>\r\n");

            
            #line 70 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                        }
                        else
                        {


            
            #line default
            #line hidden
WriteLiteral("                            <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 col-xs-12 search_item\"");

WriteLiteral(">密碼</div>\r\n                                <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 col-xs-12 bg-white mobile_white form_blank\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" id=\"Password\"");

WriteLiteral(" type=\"password\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" name=\"Password\"");

WriteLiteral(" />                                    \r\n                                </div>\r\n" +
"                            </div>\r\n");

            
            #line 80 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n        <" +
"/div>\r\n\r\n        <div");

WriteLiteral(" class=\"text-center search_padding\"");

WriteLiteral(">\r\n            <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn blue\"");

WriteLiteral(" id=\"btn_submit\"");

WriteLiteral(">確認送出</button>\r\n            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn grey-mint\"");

WriteLiteral(" id=\"btn_return\"");

WriteAttribute("onclick", Tuple.Create(" onclick=\"", 4789), Tuple.Create("\"", 4847)
, Tuple.Create(Tuple.Create("", 4799), Tuple.Create("document.location.href=\'", 4799), true)
            
            #line 88 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                         , Tuple.Create(Tuple.Create("", 4823), Tuple.Create<System.Object, System.Int32>(Url.Action("UserList")
            
            #line default
            #line hidden
, 4823), false)
, Tuple.Create(Tuple.Create("", 4846), Tuple.Create("\'", 4846), true)
);

WriteLiteral(">返回列表</button>\r\n        </div>\r\n    </form>\r\n</div>\r\n");

DefineSection("scripts", () => {

WriteLiteral(@"
    <script>
        $(function () {
            var issave = false;

            $('#SelGroupId').change(function () {
                $('#GroupId').val($('#SelGroupId').val());
            })
            $('#editform').submit(function (event) {
                debugger;
                var inputval = $(""#form-body :input"").filter(function () { return $(this).val() == """" && $(this).hasClass('checkitem'); });
                for (var idx = 0; idx < inputval.length; idx++) {
                    $(""#"" + inputval[idx].name + ""-error"").show();
                }
                if (inputval.length > 0) { return false; }
                $(""#GroupName"").val($(""#"" + '");

            
            #line 107 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                                        Write(Html.IdFor(model => model.GroupId));

            
            #line default
            #line hidden
WriteLiteral(@"' + "" option:selected"").text());
                //$(""#GroupId"").val($(""#SelGroupId"").val());

                var iscon = true;

                

                if (iscon == false) { return false;}
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result == """") {
                            alert(""儲存成功"");
                            document.location.href = '");

            
            #line 122 "..\..\Areas\webadmin\Views\Config\UserListEdit.cshtml"
                                                 Write(Url.Action("UserList"));

            
            #line default
            #line hidden
WriteLiteral("\'\r\n                        } else {\r\n                            alert(result);\r\n" +
"                        }\r\n                    }\r\n                });\r\n         " +
"       return false;\r\n            })\r\n        });\r\n    </script>\r\n\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
