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
    
    #line 1 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/EPaper/UnitSetting.cshtml")]
    public partial class _Areas_webadmin_Views_EPaper_UnitSetting_cshtml : System.Web.Mvc.WebViewPage<EPaperUnitSettingModel>
    {
        public _Areas_webadmin_Views_EPaper_UnitSetting_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 3 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
   
    Layout = "~/Areas/webadmin/Views/Shared/_Layout.cshtml";
    SET_EPAPER SET_EPAPER = (SET_EPAPER)ViewBag.SET_EPAPER;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("<script");

WriteAttribute("src", Tuple.Create(" src=\"", 197), Tuple.Create("\"", 264)
            
            #line 9 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 203), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/components-date-time-pickers.min.js")
            
            #line default
            #line hidden
, 203), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 284), Tuple.Create("\"", 343)
            
            #line 10 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 290), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/bootstrap-datepicker.min.js")
            
            #line default
            #line hidden
, 290), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 363), Tuple.Create("\"", 404)
            
            #line 11 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 369), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/custom.js")
            
            #line default
            #line hidden
, 369), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 424), Tuple.Create("\"", 476)
            
            #line 12 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 430), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/ckeditor/ckeditor.js")
            
            #line default
            #line hidden
, 430), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 496), Tuple.Create("\"", 542)
            
            #line 13 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 502), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/bootbox.min.js")
            
            #line default
            #line hidden
, 502), false)
);

WriteLiteral("></script>\r\n");

WriteLiteral("\r\n<!-- 載入 summernote 中文語系 -->\r\n<!--page bar start-->\r\n<div");

WriteLiteral(" class=\"page-bar\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" class=\"page-breadcrumb\"");

WriteLiteral(">\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 765), Tuple.Create("\"", 799)
            
            #line 20 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 772), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Home")
            
            #line default
            #line hidden
, 772), false)
);

WriteLiteral(">Home</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 896), Tuple.Create("\"", 931)
            
            #line 24 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 903), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Model")
            
            #line default
            #line hidden
, 903), false)
);

WriteLiteral(">模組管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 1028), Tuple.Create("\"", 1090)
            
            #line 28 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 1035), Tuple.Create<System.Object, System.Int32>(Url.Action("ModelItem",new {mainid = ViewBag.mainid })
            
            #line default
            #line hidden
, 1035), false)
);

WriteLiteral(">電子報管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">");

            
            #line 32 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                   Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral(" 管理</a>\r\n\r\n        </li>\r\n    </ul>\r\n\r\n</div>\r\n<!--page bar end-->\r\n<!--message s" +
"tart-->\r\n<div");

WriteLiteral(" class=\"title_01\"");

WriteLiteral(">");

            
            #line 40 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                 Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n\r\n<div");

WriteLiteral(" class=\"portlet light bordered\"");

WriteLiteral(">\r\n\r\n    <!--management item start-->\r\n    <div");

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

WriteAttribute("value", Tuple.Create(" value=\"", 1718), Tuple.Create("\"", 1782)
            
            #line 50 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 1726), Tuple.Create<System.Object, System.Int32>(Url.Action("ModelItem",new { mainid = ViewBag.mainid })
            
            #line default
            #line hidden
, 1726), false)
);

WriteLiteral(">編輯管理</option>\r\n                    <option");

WriteLiteral(" value=\"#\"");

WriteLiteral(" selected>模組設定</option>\r\n                    <option");

WriteAttribute("value", Tuple.Create(" value=\"", 1888), Tuple.Create("\"", 1953)
            
            #line 52 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 1896), Tuple.Create<System.Object, System.Int32>(Url.Action("Subscriber",new { mainid = ViewBag.mainid })
            
            #line default
            #line hidden
, 1896), false)
);

WriteLiteral(">訂閱者管理</option>\r\n                </select>\r\n            </div>\r\n        </div>\r\n " +
"   </div>\r\n    <!--<hr>-->\r\n    <!--management item end-->\r\n    <form");

WriteLiteral(" class=\"form-horizontal form-bordered\"");

WriteLiteral(" method=\"Post\"");

WriteLiteral(" id=\"editform\"");

WriteAttribute("action", Tuple.Create(" action=\'", 2170), Tuple.Create("\'", 2202)
            
            #line 59 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
    , Tuple.Create(Tuple.Create("", 2179), Tuple.Create<System.Object, System.Int32>(Url.Action("SaveUnit")
            
            #line default
            #line hidden
, 2179), false)
);

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 60 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
   Write(Html.HiddenFor(Model => Model.ID));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("        ");

            
            #line 61 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
   Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"portlet light form-fit bordered\"");

WriteLiteral(">\r\n\r\n            <div");

WriteLiteral(" class=\"portlet-body form\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-horizontal form-bordered\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-body\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 67 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                   Write(Html.HiddenFor(Model => Model.MainID));

            
            #line default
            #line hidden
WriteLiteral("\r\n                       ");

WriteLiteral("\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 search_item\"");

WriteLiteral(">列表顯示設定列表</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 bg-white mobile_white\"");

WriteLiteral(">\r\n                                <!--table start-->\r\n                          " +
"      <table");

WriteLiteral(" class=\"table table-bordered nomarginbottom\"");

WriteLiteral(" border=\"0\"");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral(" id=\"table_Column\"");

WriteLiteral(">\r\n                                    <thead>\r\n                                 " +
"       <tr>\r\n                                            <th");

WriteLiteral(" width=\"120\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">排序</th>\r\n                                            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">欄位名稱</th>\r\n                                            <th");

WriteLiteral(" width=\"60\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">顯示</th>\r\n                                        </tr>\r\n                        " +
"            </thead>\r\n                                    <tbody>\r\n");

            
            #line 82 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                        
            
            #line default
            #line hidden
            
            #line 82 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                          
                                            for (var idx = 0; idx < Model.columnSettings.Count(); idx++)
                                            {

            
            #line default
            #line hidden
WriteLiteral("                                                <tr");

WriteLiteral(" class=\"odd gradeX tr_col\"");

WriteAttribute("id", Tuple.Create(" id=\"", 3799), Tuple.Create("\"", 3811)
, Tuple.Create(Tuple.Create("", 3804), Tuple.Create("tr_", 3804), true)
            
            #line 85 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
     , Tuple.Create(Tuple.Create("", 3807), Tuple.Create<System.Object, System.Int32>(idx
            
            #line default
            #line hidden
, 3807), false)
);

WriteAttribute("seqidx", Tuple.Create(" seqidx=\"", 3812), Tuple.Create("\"", 3829)
            
            #line 85 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                    , Tuple.Create(Tuple.Create("", 3821), Tuple.Create<System.Object, System.Int32>(idx+1
            
            #line default
            #line hidden
, 3821), false)
);

WriteLiteral(">\r\n                                                    <td");

WriteLiteral(" class=\"text-center delete_th\"");

WriteLiteral(">\r\n                                                        <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"0\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-double-up\"");

WriteLiteral("></i></button>\r\n                                                        <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"-1\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-up\"");

WriteLiteral("></i></button>\r\n                                                        <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"+1\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-down\"");

WriteLiteral("></i></button>\r\n                                                        <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"max\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-double-down\"");

WriteLiteral("></i></button>\r\n                                                        \r\n       " +
"                                             </td>\r\n                            " +
"                        <td");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(" class=\"ColumnKey\"");

WriteLiteral(">");

            
            #line 93 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                                                                          Write(Model.columnSettings[idx].ColumnKey);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                                    <td");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(" class=\"Type\"");

WriteLiteral(">");

            
            #line 94 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                                                                     Write(Model.columnSettings[idx].Type);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                                    <td");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(" class=\"MainID\"");

WriteLiteral(">");

            
            #line 95 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                                                                       Write(Model.columnSettings[idx].MainID);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n\r\n                                                    <td");

WriteLiteral(" class=\"text-center columnname\"");

WriteLiteral(">");

            
            #line 97 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                                                                  Write(Model.columnSettings[idx].ColumnName);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                                    <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                                        <label");

WriteLiteral(" class=\"mt-checkbox mt-checkbox-single mt-checkbox-outline\"");

WriteLiteral(">\r\n                                                            <input");

WriteLiteral(" class=\"chkstatus\"");

WriteLiteral(" type=\"checkbox\"");

WriteAttribute("id", Tuple.Create(" id=\"", 5569), Tuple.Create("\"", 5583)
, Tuple.Create(Tuple.Create("", 5574), Tuple.Create("Used_", 5574), true)
            
            #line 100 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                              , Tuple.Create(Tuple.Create("", 5579), Tuple.Create<System.Object, System.Int32>(idx
            
            #line default
            #line hidden
, 5579), false)
);

WriteLiteral(" ");

            
            #line 100 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                                                                                                Write(Model.columnSettings[idx].Used == true ? "checked" : "");

            
            #line default
            #line hidden
WriteLiteral("><span></span>\r\n                                                        </label>\r" +
"\n                                                    </td>\r\n                    " +
"                            </tr>\r\n");

            
            #line 104 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                            }
                                        
            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </tbody>\r\n                                <" +
"/table>\r\n                                <!--table end-->\r\n                     " +
"       </div>\r\n                        </div>\r\n\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 search_item\"");

WriteLiteral("><span");

WriteLiteral(" class=\"red\"");

WriteLiteral(">*</span> 每頁顯示筆數</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 bg-white mobile_white\"");

WriteLiteral(">\r\n                                <select");

WriteLiteral(" class=\"form_02\"");

WriteLiteral(" id=\"ShowCount\"");

WriteLiteral(" name=\"ShowCount\"");

WriteLiteral(">\r\n\r\n");

            
            #line 117 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                    
            
            #line default
            #line hidden
            
            #line 117 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                     foreach (var m in SET_EPAPER.M_EPAPER13.Split(','))
                                    {

            
            #line default
            #line hidden
WriteLiteral("                                        <option");

WriteAttribute("value", Tuple.Create(" value=\"", 6653), Tuple.Create("\"", 6663)
            
            #line 119 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create("", 6661), Tuple.Create<System.Object, System.Int32>(m
            
            #line default
            #line hidden
, 6661), false)
);

WriteLiteral(">");

            
            #line 119 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                                      Write(m);

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 120 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                    }

            
            #line default
            #line hidden
WriteLiteral("                                </select>\r\n                            </div>\r\n  " +
"                      </div>\r\n                        <div");

WriteAttribute("class", Tuple.Create(" class=\"", 6856), Tuple.Create("\"", 6916)
, Tuple.Create(Tuple.Create("", 6864), Tuple.Create("form-group", 6864), true)
            
            #line 124 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
, Tuple.Create(Tuple.Create(" ", 6874), Tuple.Create<System.Object, System.Int32>(SET_EPAPER.M_EPAPER17==false?"hide":""
            
            #line default
            #line hidden
, 6875), false)
);

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-2 col-xs-12 bg-grey_1 search_item\"");

WriteLiteral(">簡介</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-10 col-xs-12 bg-white mobile_white\"");

WriteLiteral(">\r\n                                <textarea");

WriteLiteral(" cols=\"80\"");

WriteLiteral(" id=\"IntroductionHtml\"");

WriteLiteral(" rows=\"20\"");

WriteLiteral(">");

            
            #line 127 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                                                               Write(HttpUtility.HtmlDecode(Model.IntroductionHtml));

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n                            </div>\r\n                        </div>\r\n" +
"                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 search_item\"");

WriteLiteral("><span");

WriteLiteral(" class=\"red\"");

WriteLiteral(">*</span> 表格摘要</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 bg-white mobile_white\"");

WriteLiteral(">\r\n                                <textarea");

WriteLiteral(" cols=\"80\"");

WriteLiteral(" id=\"Summary\"");

WriteLiteral(" rows=\"3\"");

WriteLiteral(" name=\"Summary\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" maxlength=\"500\"");

WriteLiteral(">");

            
            #line 133 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                                                                                                         Write(Model.Summary);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n                            </div>\r\n                        </div>\r\n" +
"\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 search_item\"");

WriteLiteral(">基本語系設定</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 bg-white mobile_white\"");

WriteLiteral(">\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">序號</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 144 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column1, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">發佈日期</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 151 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column2, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">電子報名稱</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 158 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column3, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">類別</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 165 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column4, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">訂閱電子報</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 172 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column5, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">訂閱</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 179 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column6, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">取消");

WriteLiteral("</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 186 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column7, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                ");

WriteLiteral("\r\n\r\n                                ");

WriteLiteral("\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">EMail格式錯誤</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 207 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column10, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">此 Email 已有訂閱電子報!</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 214 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column11, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">電子報訂閱成功!</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 221 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column12, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">此 EMail 已經訂閱!</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 228 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column13, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </div>\r\n                                </d" +
"iv>\r\n\r\n                                <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                                    <label");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">電子報取消訂閱成功!</label>\r\n                                    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 235 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Html.EditorFor(model => model.Column14, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral(@"
                                    </div>
                                </div>


                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div");

WriteLiteral(" class=\"text-center search_padding\"");

WriteLiteral(">\r\n            <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn blue\"");

WriteLiteral(" id=\"btn_submit\"");

WriteLiteral(">確認送出</button>\r\n        </div>\r\n    </form>\r\n</div>\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n    <script>\r\n        $(function () {\r\n            $(\"#ShowCount\").val(\'");

            
            #line 256 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                            Write(Model.ShowCount);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#table_Column\").delegate((\".seq_list\"), \"click\", function () " +
"{\r\n                var alltr = $(\"#table_Column tbody tr\");\r\n                var" +
" nowtridx = $(this).parents(\'tr\').attr(\'seqidx\');\r\n                var sval = $(" +
"this).val();\r\n                var newtridx = 0;\r\n\r\n                if (sval == \"" +
"+1\") {\r\n                    newtridx = parseInt(nowtridx) + 1;\r\n                " +
"} else if (sval == \"-1\") {\r\n                    newtridx = parseInt(nowtridx) - " +
"1;\r\n                } else if (sval == \"max\") {\r\n                    newtridx = " +
"alltr.length;\r\n                }\r\n                if (newtridx == \"0\") { newtrid" +
"x = \"1\"; }\r\n                if (newtridx == \"\" || newtridx == nowtridx) {\r\n     " +
"               return false;\r\n                }\r\n                if (newtridx > " +
"alltr.length) { newtridx = alltr.length; }\r\n                if (nowtridx == newt" +
"ridx.toString()) {\r\n                    return false;\r\n                }\r\n\r\n    " +
"            var nowidxhtml = $(this).parents(\'tr\')[0];\r\n                var adda" +
"rr = [];\r\n                for (var idx = 1; idx <= alltr.length; idx++) {\r\n     " +
"               var nowtr = alltr[idx - 1];\r\n                    if (idx.toString" +
"() != nowtridx) {\r\n                        if ((newtridx > nowtridx)) {\r\n       " +
"                     addarr.push($(nowtr));\r\n                            if (idx" +
" == newtridx) {\r\n                                addarr.push($(nowidxhtml));\r\n  " +
"                          }\r\n                        } else {\r\n                 " +
"           if (idx == newtridx) {\r\n                                addarr.push($" +
"(nowidxhtml));\r\n                            }\r\n                            addar" +
"r.push($(nowtr));\r\n                        }\r\n                    }\r\n           " +
"     }\r\n                $(\"#table_Column tbody\").empty();\r\n\r\n                for" +
" (var idx = 0; idx < addarr.length; idx++) {\r\n                    $(addarr[idx])" +
".attr(\"id\", \"tr_\" + idx).attr(\'seqidx\', (idx + 1));\r\n                    $(\".Typ" +
"e\").attr(\"id\", \"Type_\" + idx);\r\n                    $(\".MainID\").attr(\"id\", \"Mai" +
"nID_\" + idx);\r\n                    $(\".ColumnKey\").attr(\"id\", \"ColumnKey_\" + idx" +
");\r\n\r\n                    $(\"#table_Column tbody\").append(addarr[idx]);\r\n       " +
"         }\r\n                var i = 0;\r\n                $(\'.tr_col\').each(functi" +
"on () {\r\n                    var checked = $(alltr[i]).children().children().fin" +
"d(\'.chkstatus\')[0].checked;\r\n                    console.log(checked);\r\n        " +
"            i++;\r\n                });\r\n\r\n            });\r\n            $(\'#editfo" +
"rm\').submit(function (event) {\r\n                var formData = new FormData();\r\n" +
"                var array = $(\'#editform\').serializeArray();\r\n                $." +
"each(array, function () {\r\n                    formData.append(this.name, this.v" +
"alue);\r\n                });\r\n\r\n                var i = 0;\r\n                var a" +
"lltr = $(\"#table_Column tbody tr\");\r\n                $(\'.tr_col\').each(function " +
"() {\r\n                    var Columnname = $(alltr[i]).find(\'.columnname\').text(" +
");\r\n                    var ColumnKey = $(alltr[i]).find(\'.ColumnKey\').text();\r\n" +
"                    var Type = $(alltr[i]).find(\'.Type\').text();\r\n              " +
"      var MainID = $(alltr[i]).find(\'.MainID\').text();\r\n\r\n                    va" +
"r checked = $(alltr[i]).children().children().find(\'.chkstatus\')[0].checked;\r\n  " +
"                  console.log(checked);\r\n                    formData.append(\"co" +
"lumnSettings[\" + i + \"].Type\", Type);\r\n                    formData.append(\"colu" +
"mnSettings[\" + i + \"].MainID\", MainID);\r\n                    formData.append(\"co" +
"lumnSettings[\" + i + \"].ColumnKey\", ColumnKey);\r\n                    formData.ap" +
"pend(\"columnSettings[\" + i + \"].ColumnName\", Columnname);\r\n                    f" +
"ormData.append(\"columnSettings[\" + i + \"].Used\", checked == true ? true : false)" +
";\r\n                    formData.append(\"columnSettings[\" + i + \"].Sort\", i + 1);" +
"\r\n                    \r\n                    i++;\r\n                    \r\n        " +
"        })\r\n                \r\n                formData.append(\"Summary\", encodeU" +
"RIComponent($(\'#Summary\').val()));\r\n                formData.append(\"Introductio" +
"nHtml\", encodeURIComponent($(\'IntroductionHtml\').val()));\r\n                //for" +
"mData.append(\"IntroductionHtml\", _html.encode(CKEDITOR.instances.IntroductionHtm" +
"l.getData()));\r\n                $.ajax({\r\n                    url: this.action,\r" +
"\n                    data: formData,\r\n                    type: \'POST\',\r\n       " +
"             cache: false,\r\n                    contentType: false,\r\n           " +
"         processData: false,\r\n                    success: function (data) {\r\n  " +
"                      if (data == \"\") {\r\n                            alert(\"儲存成功" +
"\");\r\n                        } else {\r\n                            alert(data);\r" +
"\n                            var obj = {};\r\n                                obj." +
"mainid = \'");

            
            #line 359 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                         Write(Model.MainID);

            
            #line default
            #line hidden
WriteLiteral("\'\r\n                            CreatePost(\'");

            
            #line 360 "..\..\Areas\webadmin\Views\EPaper\UnitSetting.cshtml"
                                   Write(Url.Action("UnitSetting"));

            
            #line default
            #line hidden
WriteLiteral("\', obj);\r\n                        }\r\n                    }, error: function () {\r" +
"\n                        // handle error\r\n                    }\r\n               " +
" });\r\n                return false;\r\n            });\r\n        });\r\n    </script>" +
"\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
