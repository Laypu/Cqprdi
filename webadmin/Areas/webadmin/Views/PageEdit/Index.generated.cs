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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/PageEdit/Index.cshtml")]
    public partial class _Areas_webadmin_Views_PageEdit_Index_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_webadmin_Views_PageEdit_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
  
    Layout = "~/Areas/webadmin/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 76), Tuple.Create("\"", 104)
, Tuple.Create(Tuple.Create("", 82), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/datatable.js")
, 82), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 124), Tuple.Create("\"", 154)
, Tuple.Create(Tuple.Create("", 130), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootbox.min.js")
, 130), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 174), Tuple.Create("\"", 206)
, Tuple.Create(Tuple.Create("", 180), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/clipboard.min.js")
, 180), false)
);

WriteLiteral("></script>\r\n<!--page bar start-->\r\n<div");

WriteLiteral(" class=\"page-bar all_width\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" class=\"page-breadcrumb\"");

WriteLiteral(">\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 338), Tuple.Create("\"", 372)
            
            #line 11 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
, Tuple.Create(Tuple.Create("", 345), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Home")
            
            #line default
            #line hidden
, 345), false)
);

WriteLiteral(">Home</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 469), Tuple.Create("\"", 504)
            
            #line 15 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
, Tuple.Create(Tuple.Create("", 476), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Model")
            
            #line default
            #line hidden
, 476), false)
);

WriteLiteral(">模組管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">圖文編輯</a>\r\n        </li>\r\n    </ul>\r\n</div>\r\n<!--page bar end-->\r\n<div");

WriteLiteral(" class=\"title_01\"");

WriteLiteral(">");

            
            #line 24 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
                 Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n<div");

WriteLiteral(" class=\"portlet light bordered\"");

WriteLiteral(">\r\n    <!--set item start-->\r\n    <div");

WriteLiteral(" class=\"table-toolbar\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-md-6 col-sm-12 col-xs-12\"");

WriteLiteral(">\r\n\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-md-6 col-sm-12 col-xs-12 mobile_left\"");

WriteLiteral(">\r\n            <p");

WriteLiteral(" class=\"display_inline\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" class=\"btn green-meadow\"");

WriteLiteral(" id=\'btn_add\'");

WriteLiteral(">新增 <i");

WriteLiteral(" class=\"fa fa-plus\"");

WriteLiteral("></i></button>\r\n            </p>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"table-scrollable\"");

WriteLiteral(">\r\n        <table");

WriteLiteral(" class=\"table table-bordered table-hover\"");

WriteLiteral(" border=\"0\"");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral("\r\n               id=\"eventtable\"");

WriteLiteral("\r\n               data-url=\"");

            
            #line 40 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
                    Write(Url.Action("PagingMain"));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n               data-sort-name=\'Sort\'");

WriteLiteral("\r\n               data-page-size=\'10\'");

WriteLiteral("\r\n               data-filed-id=\'ID\'");

WriteLiteral("\r\n               data-max-pagination=\'10\'");

WriteLiteral(">\r\n            <thead>\r\n                <tr");

WriteLiteral(" class=\"bg-grey_1\"");

WriteLiteral(" filed-class=\'odd gradeX\'");

WriteLiteral(">\r\n                    <th");

WriteLiteral(" width=\"80\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" filed-type=\'delcheckbox\'");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" class=\'mt-checkbox mt-checkbox-single mt-checkbox-outline\'");

WriteLiteral(">\r\n                            <input");

WriteLiteral(" type=\'checkbox\'");

WriteLiteral(" class=\'checkboxes\'");

WriteLiteral(" id=\"chk_all\"");

WriteLiteral(" /><span></span>\r\n                        </label>\r\n                        <butt" +
"on");

WriteLiteral(" class=\"btn red-mint btn-xs\"");

WriteLiteral(" id=\'btn_del\'");

WriteLiteral("><i");

WriteLiteral(" class=\"glyphicon glyphicon-trash\"");

WriteLiteral("></i></button>\r\n                    </th>\r\n                    <th");

WriteLiteral(" filed-name=\'Sort\'");

WriteLiteral(" width=\"160px\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" filed-type=\'numbertextcheck\'");

WriteLiteral(" filed-item-class=\"sortedit\"");

WriteLiteral(">排序</th>\r\n                    <th");

WriteLiteral(" filed-name=\'Name\'");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" filed-type=\'link\'");

WriteLiteral(">單元名稱</th>\r\n                    <th");

WriteLiteral(" filed-name=\'customertr\'");

WriteLiteral(" width=\"350px\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" filed-type=\'function\'");

WriteLiteral(">單元路徑</th>\r\n                    <th");

WriteLiteral(" filed-name=\'管理\'");

WriteLiteral(" width=\"80px\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" filed-type=\'button\'");

WriteLiteral(" filed-item-class=\"btn grey-mint manage\"");

WriteLiteral(">管理</th>\r\n                </tr>\r\n            </thead>\r\n            <tbody></tbody" +
">\r\n        </table>\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" id=\"page_number\"");

WriteLiteral(" class=\"table-toolbar\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-md-4 col-sm-12 col-xs-12 page_total\"");

WriteLiteral(" id=\"tableinfo\"");

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" class=\"col-md-8 col-sm-12 col-xs-12 page_icon\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"btn-group\"");

WriteLiteral(" id=\"pagination\"");

WriteLiteral(">\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <!--table end-->\r\n</div>\r\n" +
"\r\n<!--message end-->\r\n");

DefineSection("scripts", () => {

WriteLiteral(@"
    <script>
        $(document).ready(function () {
            mytable = $(""#eventtable"").myDataTable().TableList[0];
            mytable.settingTdData = myDataTableTr;
            mytable.settingTableInfo = function (tableinfoId, totalcnt, offsetcnt, nowpagecnt) {
                var totalclick = mytable.keepcheckid.length;
                $(""#"" + tableinfoId).html('總筆數:' + totalcnt + '，顯示' + (totalcnt <= 0 ? 0 : offsetcnt) + ""~"" + (offsetcnt + nowpagecnt - 1));
            };
            var SearchModelBase = mytable.SearchModelBase;
            mytable.GetData(1);
            var obj = {};
            obj.item = ""單元名稱"";
            RegisterOrder(""#eventtable"", "".sortedit"", '");

            
            #line 87 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
                                                  Write(Url.Action("EditSeq"));

            
            #line default
            #line hidden
WriteLiteral("\', obj);\r\n            RegisterClickAll(\"#chk_all\", \'#eventtable tbody .chksel\', \"" +
"#selvalue\");\r\n            RegisterDelete(\"#btn_del\", \'#eventtable .chksel:checke" +
"d\', \'");

            
            #line 89 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
                                                                  Write(Url.Action("SetMainDelete"));

            
            #line default
            #line hidden
WriteLiteral("\', obj);\r\n            RegisterClicklink(\"#eventtable\", \".edit\", \'");

            
            #line 90 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
                                                  Write(Url.Action("EditUnit"));

            
            #line default
            #line hidden
WriteLiteral("\', obj, \'UpdateItem\');\r\n            RegisterClicklink(\"#eventtable\", \".manage\", \'" +
"");

            
            #line 91 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
                                                    Write(Url.Action("ModelItem"));

            
            #line default
            #line hidden
WriteLiteral("\', obj);\r\n\r\n            $(\"#btn_add\").click(function () { CallAddDialog(\'");

            
            #line 93 "..\..\Areas\webadmin\Views\PageEdit\Index.cshtml"
                                                        Write(Url.Action("EditUnit"));

            
            #line default
            #line hidden
WriteLiteral(@"', obj); });
            var clipboard = new Clipboard('.btn', {
                text: function (event) {
                    return $(event).attr('copy');
                }
            });
        });
        function customertr(row, columnidx, idx, filedname, tableid, type, fileditemclass) {
            return ""<td><input  type='text' readonly=readonly style='width:250px; display:inline' class='form-control' value='Page/Index?itemid="" + row[tableid] + ""'/><button class='btn grey-mint' style='margin-left:20px' copy='Page/Index?itemid="" + row[tableid] + ""'>複製</button></td>"";
        }
    </script>
");

});

        }
    }
}
#pragma warning restore 1591
