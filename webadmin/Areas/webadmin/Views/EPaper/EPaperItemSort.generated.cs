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
    using Oaww.ViewModel.Lang;
    using Oaww.ViewModel.Search;
    using Oaww.ViewModel.SiteMap;
    using Template;
    using webadmin;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/EPaper/EPaperItemSort.cshtml")]
    public partial class _Areas_webadmin_Views_EPaper_EPaperItemSort_cshtml : System.Web.Mvc.WebViewPage<List<EPaperItemEdit>>
    {
        public _Areas_webadmin_Views_EPaper_EPaperItemSort_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteAttribute("src", Tuple.Create(" src=\"", 39), Tuple.Create("\"", 106)
            
            #line 3 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 45), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/components-date-time-pickers.min.js")
            
            #line default
            #line hidden
, 45), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 126), Tuple.Create("\"", 185)
            
            #line 4 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 132), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/bootstrap-datepicker.min.js")
            
            #line default
            #line hidden
, 132), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 205), Tuple.Create("\"", 246)
            
            #line 5 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 211), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/custom.js")
            
            #line default
            #line hidden
, 211), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 266), Tuple.Create("\"", 318)
            
            #line 6 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 272), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/ckeditor/ckeditor.js")
            
            #line default
            #line hidden
, 272), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 338), Tuple.Create("\"", 384)
            
            #line 7 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 344), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/bootbox.min.js")
            
            #line default
            #line hidden
, 344), false)
);

WriteLiteral("></script>\r\n");

            
            #line 8 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
  
    Layout = "~/Areas/webadmin/Views/Shared/_Layout.cshtml";


            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("\r\n<!-- 載入 summernote 中文語系 -->\r\n<!--page bar start-->\r\n<div");

WriteLiteral(" class=\"page-bar\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" class=\"page-breadcrumb\"");

WriteLiteral(">\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 680), Tuple.Create("\"", 714)
            
            #line 19 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 687), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Home")
            
            #line default
            #line hidden
, 687), false)
);

WriteLiteral(">Home</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 811), Tuple.Create("\"", 846)
            
            #line 23 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 818), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Model")
            
            #line default
            #line hidden
, 818), false)
);

WriteLiteral(">模組管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 943), Tuple.Create("\"", 979)
            
            #line 27 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 950), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","EPaper")
            
            #line default
            #line hidden
, 950), false)
);

WriteLiteral(">電子報管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 1077), Tuple.Create("\"", 1139)
            
            #line 31 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 1084), Tuple.Create<System.Object, System.Int32>(Url.Action("ModelItem",new {mainid = ViewBag.mainid })
            
            #line default
            #line hidden
, 1084), false)
);

WriteLiteral(">");

            
            #line 31 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                                                         Write(ViewBag.ModelItemTitle);

            
            #line default
            #line hidden
WriteLiteral(" 管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">");

            
            #line 35 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                   Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral(" 訊息排序</a>\r\n        </li>\r\n    </ul>\r\n\r\n</div>\r\n<!--page bar end-->\r\n<!--message s" +
"tart-->\r\n<h2>");

            
            #line 42 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n\r\n<div");

WriteLiteral(" class=\"portlet light bordered\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"table-toolbar\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-md-12 col-sm-12 col-xs-12 mobile_left\"");

WriteLiteral(">\r\n            <p");

WriteLiteral(" class=\"display_inline\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" class=\"btn grey-mint\"");

WriteAttribute("onclick", Tuple.Create(" onclick=\"", 1620), Tuple.Create("\"", 1701)
, Tuple.Create(Tuple.Create("", 1630), Tuple.Create("location.href=\'", 1630), true)
            
            #line 48 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 1645), Tuple.Create<System.Object, System.Int32>(Url.Action("ModelItem",new {mainid = ViewBag.mainid })
            
            #line default
            #line hidden
, 1645), false)
, Tuple.Create(Tuple.Create("", 1700), Tuple.Create("\'", 1700), true)
);

WriteLiteral(">回上一頁</button>\r\n            </p>\r\n        </div>\r\n    </div>\r\n    <!--set item en" +
"d-->\r\n    <!--table start-->\r\n    <form");

WriteLiteral(" class=\"form-horizontal form-bordered\"");

WriteLiteral(" method=\"Post\"");

WriteLiteral(" id=\"editform\"");

WriteAttribute("action", Tuple.Create(" action=\'", 1888), Tuple.Create("\'", 1930)
            
            #line 54 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
    , Tuple.Create(Tuple.Create("", 1897), Tuple.Create<System.Object, System.Int32>(Url.Action("SaveEPaperItemSort")
            
            #line default
            #line hidden
, 1897), false)
);

WriteLiteral(" enctype=\"multipart/form-data\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 55 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
   Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"table-scrollable\"");

WriteLiteral(">\r\n            <table");

WriteLiteral(" class=\"table table-bordered table-hover\"");

WriteLiteral(" border=\"0\"");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral(" id=\"table_selContent\"");

WriteLiteral(">\r\n                <thead>\r\n                    <tr>\r\n                        <th" +
"");

WriteLiteral(" width=\"60\"");

WriteLiteral(" class=\"text-center delete_th\"");

WriteLiteral(">\r\n                            <label");

WriteLiteral(" class=\"mt-checkbox mt-checkbox-single mt-checkbox-outline\"");

WriteLiteral(">\r\n                                <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"checkboxes\"");

WriteLiteral(" id=\"chk_all\"");

WriteLiteral(" />\r\n                                <span");

WriteLiteral(" id=\"checkAll\"");

WriteLiteral("></span>\r\n                            </label>\r\n                            <butt" +
"on");

WriteLiteral(" class=\"btn red-mint btn-xs\"");

WriteLiteral(" id=\"btn_del\"");

WriteLiteral(" type=\"button\"");

WriteLiteral("><i");

WriteLiteral(" class=\"glyphicon glyphicon-trash\"");

WriteLiteral("></i></button>\r\n                        </th>\r\n                        <th");

WriteLiteral(" width=\"160\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">排序</th>\r\n                        <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" style=\"min-width:100px\"");

WriteLiteral(">標題</th>\r\n                        <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">已選取項目</th>\r\n                    </tr>\r\n                </thead>\r\n               " +
" <tbody>\r\n");

            
            #line 73 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 73 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                     for (var idx = 0; idx < Model.Count(); idx++)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <tr");

WriteAttribute("id", Tuple.Create(" id=\"", 3153), Tuple.Create("\"", 3180)
, Tuple.Create(Tuple.Create("", 3158), Tuple.Create("trm_", 3158), true)
            
            #line 75 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 3162), Tuple.Create<System.Object, System.Int32>(Model[idx].MenuID
            
            #line default
            #line hidden
, 3162), false)
);

WriteLiteral(" class=\"mainselitemtr\"");

WriteAttribute("seqidx", Tuple.Create(" seqidx=\"", 3203), Tuple.Create("\"", 3220)
            
            #line 75 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
       , Tuple.Create(Tuple.Create("", 3212), Tuple.Create<System.Object, System.Int32>(idx+1
            
            #line default
            #line hidden
, 3212), false)
);

WriteLiteral(">\r\n                            <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" class=\"mt-checkbox mt-checkbox-single mt-checkbox-outline\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"checkboxes chksel\"");

WriteAttribute("id", Tuple.Create(" id=\"", 3462), Tuple.Create("\"", 3491)
, Tuple.Create(Tuple.Create("", 3467), Tuple.Create("chk_m_", 3467), true)
            
            #line 78 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
               , Tuple.Create(Tuple.Create("", 3473), Tuple.Create<System.Object, System.Int32>(Model[idx].MenuID
            
            #line default
            #line hidden
, 3473), false)
);

WriteLiteral(" />\r\n                                    <span");

WriteLiteral(" name=\"check_1\"");

WriteLiteral("></span>\r\n                                </label>\r\n                            <" +
"/td>\r\n                            <td");

WriteLiteral(" class=\"text-center delete_th\"");

WriteLiteral(">\r\n                                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs mainseq_list\"");

WriteLiteral(" value=\"0\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-double-up\"");

WriteLiteral("></i></button>\r\n                                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs mainseq_list\"");

WriteLiteral(" value=\"-1\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-up\"");

WriteLiteral("></i></button>\r\n                                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs mainseq_list\"");

WriteLiteral(" value=\"+1\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-down\"");

WriteLiteral("></i></button>\r\n                                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs mainseq_list\"");

WriteLiteral(" value=\"max\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-double-down\"");

WriteLiteral("></i></button>\r\n                                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"btn btn-default btn-xs sequence_list\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4417), Tuple.Create("\"", 4433)
            
            #line 87 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                        , Tuple.Create(Tuple.Create("", 4425), Tuple.Create<System.Object, System.Int32>(idx+1
            
            #line default
            #line hidden
, 4425), false)
);

WriteLiteral(" />\r\n                            </td>\r\n                            <td>");

            
            #line 89 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                            Write(Model[idx].Name);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                            <td>\r\n                                <!--tabl" +
"e start-->\r\n                                <table");

WriteLiteral(" class=\"table table-bordered nomarginbottom subsorttable\"");

WriteLiteral(" border=\"0\"");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral(">\r\n                                    <thead>\r\n                                 " +
"       <tr>\r\n                                            <th");

WriteLiteral(" width=\"60\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" filed-type=\"delcheckbox\"");

WriteLiteral(">刪除</th>\r\n                                            <th");

WriteLiteral(" width=\"120\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">排序</th>\r\n                                            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">標題</th>\r\n                                        </tr>\r\n                        " +
"            </thead>\r\n                                    <tbody>\r\n");

            
            #line 101 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                        
            
            #line default
            #line hidden
            
            #line 101 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                         for (var subidx = 0; subidx < Model[idx].ItemKey.Count(); subidx++)
                                        {

            
            #line default
            #line hidden
WriteLiteral("                                            <tr");

WriteAttribute("id", Tuple.Create(" id=\"", 5466), Tuple.Create("\"", 5498)
            
            #line 103 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
, Tuple.Create(Tuple.Create("", 5471), Tuple.Create<System.Object, System.Int32>(Model[idx].ItemKey[subidx]
            
            #line default
            #line hidden
, 5471), false)
);

WriteLiteral(" class=\"selitem\"");

WriteAttribute("seqidx", Tuple.Create(" seqidx=\"", 5515), Tuple.Create("\"", 5535)
            
            #line 103 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                          , Tuple.Create(Tuple.Create("", 5524), Tuple.Create<System.Object, System.Int32>(subidx+1
            
            #line default
            #line hidden
, 5524), false)
);

WriteLiteral(">\r\n                                                <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                                    <label");

WriteLiteral(" class=\"mt-checkbox mt-checkbox-single mt-checkbox-outline\"");

WriteLiteral(">\r\n                                                        <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"checkboxes chksel\"");

WriteAttribute("id", Tuple.Create(" id=\"", 5837), Tuple.Create("\"", 5875)
, Tuple.Create(Tuple.Create("", 5842), Tuple.Create("chk_s_", 5842), true)
            
            #line 106 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                   , Tuple.Create(Tuple.Create("", 5848), Tuple.Create<System.Object, System.Int32>(Model[idx].ItemKey[subidx]
            
            #line default
            #line hidden
, 5848), false)
);

WriteLiteral(">\r\n                                                        <span></span>\r\n       " +
"                                             </label>\r\n                         " +
"                       </td>\r\n                                                <t" +
"d");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"0\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-double-up\"");

WriteLiteral("></i></button>\r\n                                                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"-1\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-up\"");

WriteLiteral("></i></button>\r\n                                                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"+1\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-down\"");

WriteLiteral("></i></button>\r\n                                                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"max\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-double-down\"");

WriteLiteral("></i></button>\r\n                                                </td>\r\n          " +
"                                      <td>");

            
            #line 116 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                               Write(Model[idx].ItemName[subidx]);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                            </tr>\r\n");

            
            #line 118 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                        }

            
            #line default
            #line hidden
WriteLiteral("                                    </tbody>\r\n                                </t" +
"able>\r\n                                <!--table end-->\r\n                       " +
"     </td>\r\n                        </tr>\r\n");

            
            #line 124 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n        <!--table" +
" end-->\r\n        <div");

WriteLiteral(" class=\"text-center search_padding\"");

WriteLiteral(">\r\n            <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn blue\"");

WriteLiteral(">確認</button>\r\n        </div>\r\n    </form>\r\n</div>\r\n");

DefineSection("scripts", () => {

WriteLiteral(@"
    <script>
        var iseditsub = 0;
        $(function () {
            RegisterClickAll(""#chk_all"", '#table_selContent tbody .chksel', ""#selvalue"");
            $(""#btn_del"").click(function () {
                bootbox.confirm({
                    title: ""確定刪除?"",
                    message: ""是否確定刪除?"",
                    buttons: { cancel: { label: '<i class=""fa fa-times""></i>取消' }, confirm: { label: '<i class=""fa fa-check""></i> 確認' } },
                    callback: function (result) {
                        if (result) {
                            debugger
                            var chksel = $(""#table_selContent tbody :checked"");
                            var delid = [];
                            for (var idx = 0; idx < chksel.length; idx++) { delid.push($(chksel[idx]).attr('id')); }
                            var obj = {};
                            obj.delarrid = delid;
                            obj.eid = '");

            
            #line 152 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                  Write(ViewBag.ID);

            
            #line default
            #line hidden
WriteLiteral("\' ;\r\n                            $.post(\'");

            
            #line 153 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                               Write(Url.Action("DeleteEPaperItemSort"));

            
            #line default
            #line hidden
WriteLiteral("\', obj, function (data) {\r\n                                 alert(data);\r\n       " +
"                         CreatePost(\'");

            
            #line 155 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                       Write(Url.Action("EPaperItemSort"));

            
            #line default
            #line hidden
WriteLiteral("\', { id: \'");

            
            #line 155 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                                                              Write(ViewBag.ID);

            
            #line default
            #line hidden
WriteLiteral("\', mainid:\'");

            
            #line 155 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                                                                                    Write(ViewBag.mainid);

            
            #line default
            #line hidden
WriteLiteral("\'});\r\n                               });\r\n                        }\r\n            " +
"        }\r\n                });\r\n            });\r\n            $(\"#table_selConten" +
"t\").delegate((\".seq_list\"), \"click\", function () {\r\n                iseditsub = " +
"1;\r\n                var mytable = $(this).parents(\'.subsorttable\');\r\n           " +
"     var alltr = mytable.find(\"tbody tr\");\r\n                var nowtridx = $(thi" +
"s).parents(\'tr\').attr(\'seqidx\');\r\n                var sval = $(this).val();\r\n   " +
"             var newtridx = 0;\r\n\r\n                if (sval == \"+1\") {\r\n         " +
"           newtridx = parseInt(nowtridx) + 1;\r\n                } else if (sval =" +
"= \"-1\") {\r\n                    newtridx = parseInt(nowtridx) - 1;\r\n             " +
"   } else if (sval == \"max\") {\r\n                    newtridx = alltr.length;\r\n  " +
"              }\r\n                if (newtridx == \"0\") { newtridx = \"1\"; }\r\n     " +
"           if (newtridx == \"\" || newtridx == nowtridx) { return false; }\r\n      " +
"          if (newtridx > alltr.length) { newtridx = alltr.length; }\r\n           " +
"     if (nowtridx == newtridx.toString()) { return false; }\r\n                var" +
" nowidxhtml = $(this).parents(\'tr\')[0];\r\n                var addarr = [];\r\n     " +
"           for (var idx = 1; idx <= alltr.length; idx++) {\r\n                    " +
"var nowtr = alltr[idx - 1];\r\n                    if (idx.toString() != nowtridx)" +
" {\r\n                        if ((newtridx > nowtridx)) {\r\n                      " +
"      addarr.push($(nowtr));\r\n                            if (idx == newtridx) {" +
" addarr.push($(nowidxhtml)); }\r\n                        } else {\r\n              " +
"              if (idx == newtridx) { addarr.push($(nowidxhtml)); }\r\n            " +
"                addarr.push($(nowtr));\r\n                        }\r\n             " +
"       }\r\n                }\r\n                mytable.find(\"tbody\").empty();\r\n   " +
"             for (var idx = 0; idx < addarr.length; idx++) {\r\n                  " +
"  $(addarr[idx]).attr(\'seqidx\', (idx + 1));\r\n                    mytable.find(\"t" +
"body\").append(addarr[idx]);\r\n                }\r\n            });\r\n            $(\"" +
"#table_selContent\").delegate((\".mainseq_list\"), \"click\", function () {\r\n        " +
"        var alltr = $(\"#table_selContent tbody .mainselitemtr\");\r\n              " +
"  var nowtridx = $(this).parents(\'tr\').attr(\'seqidx\');\r\n                var sval" +
" = $(this).val();\r\n                var newtridx = 0;\r\n                if (sval =" +
"= \"+1\") {\r\n                    newtridx = parseInt(nowtridx) + 1;\r\n             " +
"   } else if (sval == \"-1\") {\r\n                    newtridx = parseInt(nowtridx)" +
" - 1;\r\n                } else if (sval == \"max\") {\r\n                    newtridx" +
" = alltr.length;\r\n                }\r\n                if (newtridx == \"0\") { newt" +
"ridx = \"1\"; }\r\n                if (newtridx == \"\" || newtridx == nowtridx) { ret" +
"urn false; }\r\n                if (newtridx > alltr.length) { newtridx = alltr.le" +
"ngth; }\r\n                if (nowtridx == newtridx.toString()) { return false; }\r" +
"\n                var nowidxhtml = $(this).parents(\'tr\')[0];\r\n                var" +
" addarr = [];\r\n                for (var idx = 1; idx <= alltr.length; idx++) {\r\n" +
"                    var nowtr = alltr[idx - 1];\r\n                    if (idx.toS" +
"tring() != nowtridx) {\r\n                        if ((newtridx > nowtridx)) {\r\n  " +
"                          addarr.push($(nowtr));\r\n                            if" +
" (idx == newtridx) { addarr.push($(nowidxhtml)); }\r\n                        } el" +
"se {\r\n                            if (idx == newtridx) { addarr.push($(nowidxhtm" +
"l)); }\r\n                            addarr.push($(nowtr));\r\n                    " +
"    }\r\n                    }\r\n                }\r\n                $(\"#table_selCo" +
"ntent tbody .mainselitemtr\").remove();\r\n                for (var idx = 0; idx < " +
"addarr.length; idx++) {\r\n                    $(addarr[idx]).attr(\'seqidx\', (idx " +
"+ 1));\r\n                    $(addarr[idx]).find(\'.sequence_list\').val((idx + 1))" +
";\r\n                    $(\"#table_selContent\").first(\'tbody\').append(addarr[idx])" +
";\r\n                }\r\n            });\r\n            $(\"#table_selContent\").delega" +
"te((\".sequence_list\"), \"change\", function () {\r\n                var alltr = $(\"#" +
"table_selContent tbody .mainselitemtr\");\r\n                var nowtridx = $(this)" +
".parents(\'tr\').attr(\'seqidx\');\r\n                var newtridx = $(this).val();\r\n " +
"               if (newtridx == \"0\") { newtridx = \"1\"; }\r\n                if (new" +
"tridx == \"\" || newtridx == nowtridx) {\r\n                    $(this).val(nowtridx" +
");\r\n                    return false;\r\n                }\r\n                var ch" +
"eckint = $(this).val().match(/^[0-9]+$/g);\r\n                if (checkint == null" +
") {\r\n                    $(this).val(nowtridx);\r\n                    return fals" +
"e;\r\n                }\r\n                if (newtridx > alltr.length) { newtridx =" +
" alltr.length; }\r\n                if (nowtridx == newtridx.toString()) {\r\n      " +
"              $(this).val(nowtridx);\r\n                    return false;\r\n       " +
"         }\r\n                var nowidxhtml = $(this).parents(\'tr\')[0];\r\n        " +
"        var addarr = [];\r\n                for (var idx = 1; idx <= alltr.length;" +
" idx++) {\r\n                    var nowtr = alltr[idx - 1];\r\n                    " +
"if (idx.toString() != nowtridx) {\r\n                        if ((newtridx > nowtr" +
"idx)) {\r\n                            addarr.push($(nowtr));\r\n                   " +
"         if (idx == newtridx) {\r\n                                addarr.push($(n" +
"owidxhtml));\r\n                            }\r\n                        } else {\r\n " +
"                           if (idx == newtridx) {\r\n                             " +
"   addarr.push($(nowidxhtml));\r\n                            }\r\n                 " +
"           addarr.push($(nowtr));\r\n                        }\r\n                  " +
"  }\r\n                }\r\n                $(\"#table_selContent tbody .mainselitemt" +
"r\").remove();\r\n                for (var idx = 0; idx < addarr.length; idx++) {\r\n" +
"                    $(addarr[idx]).attr(\'seqidx\', (idx + 1));\r\n                 " +
"   $(addarr[idx]).find(\'.sequence_list\').val((idx + 1));\r\n                    $(" +
"\"#table_selContent\").first(\'tbody\').append(addarr[idx]);\r\n                }\r\n   " +
"         });\r\n\r\n\r\n            $(\'#editform\').submit(function (event) {\r\n        " +
"        var formData = new FormData();\r\n                var allkeytr = $(\"#table" +
"_selContent tbody .mainselitemtr\");\r\n                for (var idx = 0; idx < all" +
"keytr.length; idx++) {\r\n                    var key = $(allkeytr[idx]).attr(\'id\'" +
");\r\n                    var subtr = $(allkeytr[idx]).find(\"tbody tr\");\r\n        " +
"            var subarr = [];\r\n                    for (var sidx = 0; sidx < subt" +
"r.length; sidx++) {\r\n                        subarr.push($(subtr[sidx]).attr(\'id" +
"\'));\r\n                    }\r\n                    formData.append(\"allitemkey[\" +" +
" key + \"]\", subarr.join(\',\'));\r\n                }\r\n                formData.appe" +
"nd(\"eid\", \'");

            
            #line 295 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                                   Write(ViewBag.ID);

            
            #line default
            #line hidden
WriteLiteral(@"');
                formData.append(""iseditsub"",iseditsub);

                $.ajax({
                    url: this.action,
                    data: formData,
                    type: 'POST',
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        alert(data);
                       CreatePost('");

            
            #line 307 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSort.cshtml"
                              Write(Url.Action("ModelItem",new {mainid = ViewBag.mainid }));

            
            #line default
            #line hidden
WriteLiteral("\');\r\n                    }, error: function () {\r\n                        // hand" +
"le error\r\n                    }\r\n                });\r\n                return fal" +
"se;\r\n            });\r\n        });\r\n    </script>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
