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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/Form/ItemEdit.cshtml")]
    public partial class _Areas_webadmin_Views_Form_ItemEdit_cshtml : System.Web.Mvc.WebViewPage<FormItemSettingModel>
    {
        public _Areas_webadmin_Views_Form_ItemEdit_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n\r\n");

            
            #line 4 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
  
    Layout = "~/Areas/webadmin/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<!--page bar start-->\r\n<div");

WriteLiteral(" class=\"page-bar all_width\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" class=\"page-breadcrumb\"");

WriteLiteral(">\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 221), Tuple.Create("\"", 255)
            
            #line 11 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
, Tuple.Create(Tuple.Create("", 228), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Home")
            
            #line default
            #line hidden
, 228), false)
);

WriteLiteral(">Home</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 352), Tuple.Create("\"", 387)
            
            #line 15 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
, Tuple.Create(Tuple.Create("", 359), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Model")
            
            #line default
            #line hidden
, 359), false)
);

WriteLiteral(">模組管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 484), Tuple.Create("\"", 518)
            
            #line 19 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
, Tuple.Create(Tuple.Create("", 491), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Form")
            
            #line default
            #line hidden
, 491), false)
);

WriteLiteral(">表單管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">欄位編輯</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n    </ul>\r\n</div>\r\n<!--page bar end-->\r\n<!--message start--" +
">\r\n<div");

WriteLiteral(" class=\"title_01\"");

WriteLiteral(">");

            
            #line 30 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                 Write(ViewBag.mainname);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n<div");

WriteLiteral(" class=\"portlet light bordered\"");

WriteLiteral(">\r\n    <form");

WriteLiteral(" class=\"form-horizontal form-bordered\"");

WriteLiteral(" method=\"Post\"");

WriteLiteral(" id=\"editform\"");

WriteAttribute("action", Tuple.Create(" action=\'", 914), Tuple.Create("\'", 949)
            
            #line 32 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
     , Tuple.Create(Tuple.Create("", 923), Tuple.Create<System.Object, System.Int32>(Url.Action("SaveSelItem")
            
            #line default
            #line hidden
, 923), false)
);

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 33 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
   Write(Html.HiddenFor(Model => Model.ID));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("        ");

            
            #line 34 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
   Write(Html.HiddenFor(Model => Model.MainID));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("        ");

            
            #line 35 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
   Write(Html.HiddenFor(Model => Model.SelList));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"portlet light form-fit bordered\"");

WriteLiteral(" id=\"div_seo\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"portlet-body form\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-horizontal form-bordered\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-body\"");

WriteLiteral(" id=\"form-body\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 col-xs-12 bg-grey_1 search_item\"");

WriteLiteral(">欄位名稱</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 col-xs-12 bg-white mobile_white\"");

WriteLiteral(">\r\n");

WriteLiteral("                                ");

            
            #line 43 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                           Write(Html.EditorFor(model => model.Title, new { htmlAttributes = new { @class = "form-control checkitem" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                <span");

WriteLiteral(" class=\"required\"");

WriteLiteral(" id=\"Title-error\"");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">欄位名稱 必須填寫！</span>\r\n                            </div>\r\n                        <" +
"/div>\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 col-xs-12 bg-grey_1 search_item\"");

WriteLiteral(">欄位說明</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 col-xs-12 bg-white mobile_white\"");

WriteLiteral(">\r\n");

WriteLiteral("                                ");

            
            #line 50 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                           Write(Html.EditorFor(model => model.Description, new { htmlAttributes = new { @class = "form-control" } }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n           " +
"             <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 col-xs-12 bg-grey_1 search_item\"");

WriteLiteral(">表單元件</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 col-xs-12 bg-white mobile_white form-horizontal table" +
"_font\"");

WriteLiteral(">\r\n                                <label");

WriteLiteral(" id=\"lbl_ItemMode\"");

WriteLiteral("></label>\r\n");

WriteLiteral("                                ");

            
            #line 57 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                           Write(Html.RadioButtonFor(model => model.ItemMode, 1, new { style = "margin-left :0px", @checked = "checked", @class = "ItemMode" }));

            
            #line default
            #line hidden
WriteLiteral(" <label");

WriteLiteral(" class=\"lblItemMode\"");

WriteLiteral(">單行輸入</label>\r\n");

WriteLiteral("                                ");

            
            #line 58 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                           Write(Html.RadioButtonFor(model => model.ItemMode, 2, new { style = "margin-left :40px", @class = "ItemMode" }));

            
            #line default
            #line hidden
WriteLiteral(" <label");

WriteLiteral(" class=\"lblItemMode\"");

WriteLiteral(">多行輸入</label>\r\n");

WriteLiteral("                                ");

            
            #line 59 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                           Write(Html.RadioButtonFor(model => model.ItemMode, 3, new { style = "margin-left :40px", @class = "ItemMode" }));

            
            #line default
            #line hidden
WriteLiteral(" <label");

WriteLiteral(" class=\"lblItemMode\"");

WriteLiteral(">單選</label>\r\n");

WriteLiteral("                                ");

            
            #line 60 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                           Write(Html.RadioButtonFor(model => model.ItemMode, 4, new { style = "margin-left :40px", @class = "ItemMode" }));

            
            #line default
            #line hidden
WriteLiteral(" <label");

WriteLiteral(" class=\"lblItemMode\"");

WriteLiteral(">複選</label>\r\n");

WriteLiteral("                                ");

            
            #line 61 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                           Write(Html.RadioButtonFor(model => model.ItemMode, 5, new { style = "margin-left :40px", @class = "ItemMode" }));

            
            #line default
            #line hidden
WriteLiteral(" <label");

WriteLiteral(" class=\"lblItemMode\"");

WriteLiteral(">下拉選單</label>\r\n                            </div>\r\n                        </div>" +
"\r\n                        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(" id=\"div_textitem\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 col-xs-12 bg-grey_1 search_item\"");

WriteLiteral(">表單元件選項</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 col-xs-12 bg-white mobile_white form-horizontal\"");

WriteLiteral(">\r\n                                <p>欄位寬度 ");

            
            #line 67 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                   Write(Html.EditorFor(model => model.ColumnNum, new { htmlAttributes = new { @class = "form-control input-xsmall inline-block checkmaxlength" } }));

            
            #line default
            #line hidden
WriteLiteral("字</p>\r\n                                <p");

WriteLiteral(" id=\"p_rowcount\"");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(">欄位列數 ");

            
            #line 68 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                                                        Write(Html.EditorFor(model => model.RowNum, new { htmlAttributes = new { @class = "form-control input-xsmall inline-block checkmaxlength" } }));

            
            #line default
            #line hidden
WriteLiteral("字</p>\r\n                                <p>最大可輸入字數 ");

            
            #line 69 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                      Write(Html.EditorFor(model => model.TextLength, new { htmlAttributes = new { @class = "form-control input-xsmall inline-block checkmaxlengths" } }));

            
            #line default
            #line hidden
WriteLiteral("字</p>\r\n                                <p>預設文字 ");

            
            #line 70 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                   Write(Html.EditorFor(model => model.DefaultText, new { htmlAttributes = new { @class = "form-control  inline-block", style = "width:80%" } }));

            
            #line default
            #line hidden
WriteLiteral(" </p>\r\n                            </div>\r\n                        </div>\r\n      " +
"                  <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(" id=\"div_selitem\"");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"col-md-2 col-sm-12 search_item\"");

WriteLiteral(">表單元件選項</div>\r\n                            <div");

WriteLiteral(" class=\"col-md-10 col-sm-12 bg-white mobile_white\"");

WriteLiteral(">\r\n                                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn green-meadow margin_bottom\"");

WriteLiteral(" id=\"btn_seladd\"");

WriteLiteral(">新增 <i");

WriteLiteral(" class=\"fa fa-plus\"");

WriteLiteral("></i></button>\r\n                                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn red-mint margin_bottom\"");

WriteLiteral(" id=\"btn_del\"");

WriteLiteral(">刪除 <i");

WriteLiteral(" class=\"glyphicon glyphicon-trash\"");

WriteLiteral("></i></button>\r\n                                <br>\r\n                           " +
"     <!--投票項目列表 start-->\r\n                                <div");

WriteLiteral(" class=\"table-scrollable\"");

WriteLiteral(">\r\n                                    <table");

WriteLiteral(" class=\"table table-bordered nomarginbottom\"");

WriteLiteral(" border=\"0\"");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral(" id=\"table_selContent\"");

WriteLiteral(">\r\n                                        <thead>\r\n                             " +
"               <tr>\r\n                                                <th");

WriteLiteral(" width=\"60\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                                    <label");

WriteLiteral(" class=\"mt-checkbox mt-checkbox-single mt-checkbox-outline\"");

WriteLiteral(">\r\n                                                        <input");

WriteLiteral(" type=\'checkbox\'");

WriteLiteral(" class=\'checkboxes\'");

WriteLiteral(" id=\"chk_all\"");

WriteLiteral(" /><span></span>\r\n                                                        <span");

WriteLiteral(" name=\"check_1\"");

WriteLiteral("></span>\r\n                                                    </label>\r\n         " +
"                                       </th>\r\n                                  " +
"              <th");

WriteLiteral(" width=\"120px\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">排序</th>\r\n                                                <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">選項名稱</th>\r\n                                            </tr>\r\n                  " +
"                      </thead>\r\n                                        <tbody>\r" +
"\n");

            
            #line 95 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                            
            
            #line default
            #line hidden
            
            #line 95 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                              
                                                if (string.IsNullOrEmpty(Model.SelList) == false)
                                                {
                                                    var arr = Model.SelList.Split('^');
                                                    var idx = 0;
                                                    foreach (var sel in arr)
                                                    {

            
            #line default
            #line hidden
WriteLiteral("                                                        <tr");

WriteAttribute("id", Tuple.Create(" id=\"", 7195), Tuple.Create("\"", 7207)
, Tuple.Create(Tuple.Create("", 7200), Tuple.Create("tr_", 7200), true)
            
            #line 102 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
, Tuple.Create(Tuple.Create("", 7203), Tuple.Create<System.Object, System.Int32>(idx
            
            #line default
            #line hidden
, 7203), false)
);

WriteLiteral(" class=\"selitem\"");

WriteAttribute("seqidx", Tuple.Create(" seqidx=\"", 7224), Tuple.Create("\"", 7241)
            
            #line 102 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                  , Tuple.Create(Tuple.Create("", 7233), Tuple.Create<System.Object, System.Int32>(idx+1
            
            #line default
            #line hidden
, 7233), false)
);

WriteLiteral(">\r\n                                                            <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                                                <label");

WriteLiteral(" class=\"mt-checkbox mt-checkbox-single mt-checkbox-outline\"");

WriteLiteral(">\r\n                                                                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" class=\"checkboxes\"");

WriteLiteral(">\r\n                                                                    <span");

WriteAttribute("name", Tuple.Create(" name=\"", 7648), Tuple.Create("\"", 7665)
, Tuple.Create(Tuple.Create("", 7655), Tuple.Create("check_", 7655), true)
            
            #line 106 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
      , Tuple.Create(Tuple.Create("", 7661), Tuple.Create<System.Object, System.Int32>(idx
            
            #line default
            #line hidden
, 7661), false)
);

WriteLiteral("></span>\r\n                                                                </label" +
">\r\n                                                            </td>\r\n          " +
"                                                  <td");

WriteLiteral(" class=\"text-center delete_th\"");

WriteLiteral(">\r\n                                                                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"0\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-double-up\"");

WriteLiteral("></i></button>\r\n                                                                <" +
"button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"-1\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-up\"");

WriteLiteral("></i></button>\r\n                                                                <" +
"button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"+1\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-down\"");

WriteLiteral("></i></button>\r\n                                                                <" +
"button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-default btn-xs seq_list\"");

WriteLiteral(" value=\"max\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-angle-double-down\"");

WriteLiteral("></i></button>\r\n                                                            </td>" +
"\r\n                                                            <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n                                                                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control seltext\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8907), Tuple.Create("\"", 8919)
            
            #line 116 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                       , Tuple.Create(Tuple.Create("", 8915), Tuple.Create<System.Object, System.Int32>(sel
            
            #line default
            #line hidden
, 8915), false)
);

WriteLiteral(">\r\n                                                            </td>\r\n           " +
"                                             </tr>\r\n");

            
            #line 119 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                                        idx += 1;
                                                    }
                                                }
                                            
            
            #line default
            #line hidden
WriteLiteral(@"
                                        </tbody>
                                    </table>
                                </div>
                                <!--投票項目列表 end-->
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

WriteLiteral(">確認送出</button>\r\n            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn blue\"");

WriteLiteral(" id=\"btn_return\"");

WriteAttribute("onclick", Tuple.Create(" onclick=\"", 9814), Tuple.Create("\"", 9869)
, Tuple.Create(Tuple.Create("", 9824), Tuple.Create("document.location.href=\'", 9824), true)
            
            #line 135 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                    , Tuple.Create(Tuple.Create("", 9848), Tuple.Create<System.Object, System.Int32>(Url.Action("Index")
            
            #line default
            #line hidden
, 9848), false)
, Tuple.Create(Tuple.Create("", 9868), Tuple.Create("\'", 9868), true)
);

WriteLiteral(">返回</button>\r\n        </div>\r\n    </form>\r\n");

DefineSection("scripts", () => {

            
            #line 138 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
    
            
            #line default
            #line hidden
WriteLiteral("\r\n        <script");

WriteAttribute("src", Tuple.Create(" src=\"", 9952), Tuple.Create("\"", 9996)
            
            #line 139 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
, Tuple.Create(Tuple.Create("", 9958), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/datatable.js")
            
            #line default
            #line hidden
, 9958), false)
);

WriteLiteral("></script>\r\n        <script");

WriteAttribute("src", Tuple.Create(" src=\"", 10024), Tuple.Create("\"", 10065)
            
            #line 140 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
, Tuple.Create(Tuple.Create("", 10030), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/custom.js")
            
            #line default
            #line hidden
, 10030), false)
);

WriteLiteral("></script>\r\n        <script>\r\n        $(function () {\r\n            if (\'");

            
            #line 143 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
            Write(Model.ID);

            
            #line default
            #line hidden
WriteLiteral("\' != \'-1\') {\r\n                $(\'.ItemMode,.lblItemMode\').hide();\r\n              " +
"  $(\'#lbl_ItemMode\').html(\'");

            
            #line 145 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                    Write(Model.ItemModeName);

            
            #line default
            #line hidden
WriteLiteral("\')\r\n                   if (\'");

            
            #line 146 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                   Write(Model.ItemMode);

            
            #line default
            #line hidden
WriteLiteral("\' == \"2\") {\r\n                        $(\'#p_rowcount\').show();\r\n                  " +
"  }\r\n                if (\'");

            
            #line 149 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                Write(Model.ItemMode);

            
            #line default
            #line hidden
WriteLiteral("\' != \'1\' && \'");

            
            #line 149 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                            Write(Model.ItemMode);

            
            #line default
            #line hidden
WriteLiteral("\' != \'2\') {\r\n                    $(\'#div_textitem\').hide();\r\n                    " +
"$(\'#div_selitem\').show();\r\n                }\r\n            }\r\n                 $(" +
"\"#btn_return\").click(function () { CreatePost(\'");

            
            #line 154 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                                             Write(Url.Action("FormManager"));

            
            #line default
            #line hidden
WriteLiteral("\', { mainid: \'");

            
            #line 154 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                                                                                     Write(Model.MainID);

            
            #line default
            #line hidden
WriteLiteral(@"'});});
            $('.ItemMode').change(function () {
                var value = $('input[name=ItemMode]:checked').val();
                if (value == ""1"" || value == ""2"") {
                    $('#div_textitem').show();
                    $('#div_selitem').hide();
                    $('#' +'");

            
            #line 160 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                       Write(Html.IdFor(m=>m.ColumnNum));

            
            #line default
            #line hidden
WriteLiteral("\').val(\'\');\r\n                     $(\'#\'+\'");

            
            #line 161 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                       Write(Html.IdFor(m=>m.DefaultText));

            
            #line default
            #line hidden
WriteLiteral("\').val(\'\');\r\n                     $(\'#\' + \'");

            
            #line 162 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                         Write(Html.IdFor(m=>m.TextLength));

            
            #line default
            #line hidden
WriteLiteral("\').val(\'\');\r\n                      $(\'#\' + \'");

            
            #line 163 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                          Write(Html.IdFor(m=>m.RowNum));

            
            #line default
            #line hidden
WriteLiteral(@"').val('');
                      if (value == ""2"") {
                          $('#p_rowcount').show();
                      } else {
                          $('#p_rowcount').hide();
                      }
                } else {
                    $(""#table_selContent tbody"").empty();
                    $('#div_textitem').hide();
                    $('#div_selitem').show();
                }
            });
            $('#btn_seladd').click(function () {
                var selitems = $("".selitem"").length;
                $.post('");

            
            #line 177 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                   Write(Url.Action("AddSelItem"));

            
            #line default
            #line hidden
WriteLiteral("\', { \"index\": selitems }, function (data) {\r\n                    $(data).appendTo" +
"(\"#table_selContent tbody\");\r\n                });\r\n            });\r\n\r\n          " +
"  $(\"#btn_del\").click(function () {\r\n                var checked = $(\"#table_sel" +
"Content :checked\");\r\n                for (var idx = 0; idx < checked.length; idx" +
"++) {\r\n                    $(checked[idx]).parents(\'tr\').remove();\r\n            " +
"    }\r\n                var voteitems = $(\".selitem\");\r\n                for (var " +
"idx = 0; idx < voteitems.length; idx++) {\r\n                    var seqtext = $(v" +
"oteitems[idx]).find(\".seq_list\");\r\n                    $(seqtext).val((idx + 1))" +
";\r\n                }\r\n            });\r\n            RegisterClickAll(\"#chk_all\", " +
"\'#table_selContent tbody .checkboxes\', \"#selvalue\");\r\n            $(\"#table_selC" +
"ontent\").delegate((\".seq_list\"), \"click\", function () {\r\n                var all" +
"tr = $(\"#table_selContent tbody tr\");\r\n                var nowtridx = $(this).pa" +
"rents(\'tr\').attr(\'seqidx\');\r\n                var sval = $(this).val();\r\n        " +
"        var newtridx = 0;\r\n\r\n                if (sval == \"+1\") {\r\n              " +
"      newtridx = parseInt(nowtridx) + 1;\r\n                } else if (sval == \"-1" +
"\") {\r\n                    newtridx = parseInt(nowtridx) - 1;\r\n                } " +
"else if (sval == \"max\") {\r\n                    newtridx = alltr.length;\r\n       " +
"         }\r\n                if (newtridx == \"0\") { newtridx = \"1\"; }\r\n          " +
"      if (newtridx == \"\" || newtridx == nowtridx) {\r\n                    return " +
"false;\r\n                }\r\n                if (newtridx > alltr.length) { newtri" +
"dx = alltr.length; }\r\n                if (nowtridx == newtridx.toString()) {\r\n  " +
"                  return false;\r\n                }\r\n\r\n                var nowidx" +
"html = $(this).parents(\'tr\')[0];\r\n                var addarr = [];\r\n            " +
"    for (var idx = 1; idx <= alltr.length; idx++) {\r\n                    var now" +
"tr = alltr[idx - 1];\r\n                    if (idx.toString() != nowtridx) {\r\n   " +
"                     if ((newtridx > nowtridx)) {\r\n                            a" +
"ddarr.push($(nowtr));\r\n                            if (idx == newtridx) {\r\n     " +
"                           addarr.push($(nowidxhtml));\r\n                        " +
"    }\r\n                        } else {\r\n                            if (idx == " +
"newtridx) {\r\n                                addarr.push($(nowidxhtml));\r\n      " +
"                      }\r\n                            addarr.push($(nowtr));\r\n   " +
"                     }\r\n                    }\r\n                }\r\n              " +
"  $(\"#table_selContent tbody\").empty();\r\n\r\n                for (var idx = 0; idx" +
" < addarr.length; idx++) {\r\n                    $(addarr[idx]).attr(\"id\", \"tr_\" " +
"+ idx).attr(\'seqidx\', (idx + 1));\r\n                    $(\"#table_selContent tbod" +
"y\").append(addarr[idx]);\r\n                }\r\n            });\r\n\r\n            $(\'#" +
"editform\').submit(function (event) {\r\n                $(\".required\").hide();\r\n  " +
"             var inputval = $(\"#editform :input\").filter(function () { return $(" +
"this).val() == \"\" && $(this).hasClass(\'checkitem\'); });\r\n                for (va" +
"r idx = 0; idx < inputval.length; idx++) {\r\n                    $(\"#\" + inputval" +
"[idx].name + \"-error\").show();\r\n                }\r\n                if (inputval." +
"length > 0) { return false; }\r\n                var value = $(\'input[name=ItemMod" +
"e]:checked\').val();\r\n                var formData = new FormData();\r\n           " +
"     if (value != \"1\" && value != \"2\") {\r\n                    var seltext = $(\'." +
"seltext\');\r\n                    if (seltext.length == 0) { alert(\'請確實輸入項目\'); ret" +
"urn false; }\r\n                    var arr = [];\r\n                    for (var id" +
"x = 0; idx < seltext.length; idx++) {\r\n                        if ($(seltext[idx" +
"]).val() == \'\') {\r\n                            alert(\'請確實輸入項目\'); return false;\r\n" +
"                        }\r\n                        arr.push($(seltext[idx]).val(" +
"));\r\n                    }\r\n                    $(\'#SelList\').val(arr.join(\'^\'))" +
";\r\n                } else {\r\n                    if ($(\'#\' + \'");

            
            #line 263 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                            Write(Html.IdFor(m=>m.ColumnNum));

            
            #line default
            #line hidden
WriteLiteral("\').val() == \'\') {\r\n                        alert(\'請確實輸入欄位寬度\'); return false;\r\n   " +
"                 }\r\n                     if ($(\'#\' + \'");

            
            #line 266 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                             Write(Html.IdFor(m=>m.TextLength));

            
            #line default
            #line hidden
WriteLiteral("\').val() == \'\') {\r\n                         alert(\'請確實輸入最大可輸入字數\'); return false;\r" +
"\n                    }\r\n                     if (value == \"2\") {\r\n              " +
"              if ($(\'#\' + \'");

            
            #line 270 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                    Write(Html.IdFor(m=>m.RowNum));

            
            #line default
            #line hidden
WriteLiteral("\').val() == \'\') {\r\n                                alert(\'請確實輸入欄位列數\'); return fal" +
"se;}\r\n                      var checkintrow =$(\'#\' + \'");

            
            #line 272 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                           Write(Html.IdFor(m=>m.RowNum));

            
            #line default
            #line hidden
WriteLiteral(@"').val().match(/^[0-9]+$/g);
                      if (checkintrow == null) {
                          alert('欄位列數必須為整數'); return false;
                         return false;
                       }
                     }
                     var checkint =$('#' + '");

            
            #line 278 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                       Write(Html.IdFor(m=>m.ColumnNum));

            
            #line default
            #line hidden
WriteLiteral("\').val().match(/^[0-9]+$/g);\r\n                     if (checkint == null) {\r\n     " +
"                    alert(\'欄位寬度必須為整數\'); return false;\r\n                         " +
"return false;\r\n                     }\r\n\r\n                    checkint =$(\'#\' + \'" +
"");

            
            #line 284 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                  Write(Html.IdFor(m=>m.TextLength));

            
            #line default
            #line hidden
WriteLiteral(@"').val().match(/^[0-9]+$/g);
                     if (checkint == null) {
                         alert('可輸入字數必須為整數'); return false;
                         return false;
                     }
                }
                var array = $('#editform').serializeArray();
                $.each(array, function () {
                    formData.append(this.name, this.value);
                });
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

            
            #line 303 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                              Write(Url.Action("FormManager"));

            
            #line default
            #line hidden
WriteLiteral("\', { mainid: \'");

            
            #line 303 "..\..\Areas\webadmin\Views\Form\ItemEdit.cshtml"
                                                                      Write(Model.MainID);

            
            #line default
            #line hidden
WriteLiteral("\'});\r\n                    }, error: function () {\r\n                        // han" +
"dle error\r\n                    }\r\n                });\r\n                return fa" +
"lse;\r\n            });\r\n        });\r\n        </script>\r\n\r\n    ");

});

WriteLiteral("\r\n</div>");

        }
    }
}
#pragma warning restore 1591
