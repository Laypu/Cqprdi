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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/EPaper/EPaperItemSelect.cshtml")]
    public partial class _Areas_webadmin_Views_EPaper_EPaperItemSelect_cshtml : System.Web.Mvc.WebViewPage<SearchModelBase>
    {
        public _Areas_webadmin_Views_EPaper_EPaperItemSelect_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
WriteLiteral("\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 57), Tuple.Create("\"", 101)
            
            #line 3 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
, Tuple.Create(Tuple.Create("", 63), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/datatable.js")
            
            #line default
            #line hidden
, 63), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 121), Tuple.Create("\"", 162)
            
            #line 4 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
, Tuple.Create(Tuple.Create("", 127), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/custom.js")
            
            #line default
            #line hidden
, 127), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 182), Tuple.Create("\"", 228)
            
            #line 5 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
, Tuple.Create(Tuple.Create("", 188), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/bootbox.min.js")
            
            #line default
            #line hidden
, 188), false)
);

WriteLiteral("></script>\r\n");

WriteLiteral("\r\n");

            
            #line 7 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
   
    Layout = "~/Areas/webadmin/Views/Shared/_Layout.cshtml";


            
            #line default
            #line hidden
WriteLiteral("\r\n<!-- 載入 summernote 中文語系 -->\r\n<!--page bar start-->\r\n<div");

WriteLiteral(" class=\"page-bar\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" class=\"page-breadcrumb\"");

WriteLiteral(">\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 523), Tuple.Create("\"", 558)
            
            #line 16 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
, Tuple.Create(Tuple.Create("", 530), Tuple.Create<System.Object, System.Int32>(Url.Action("Index", "Home")
            
            #line default
            #line hidden
, 530), false)
);

WriteLiteral(">Home</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 655), Tuple.Create("\"", 690)
            
            #line 20 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
, Tuple.Create(Tuple.Create("", 662), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Model")
            
            #line default
            #line hidden
, 662), false)
);

WriteLiteral(">模組管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 787), Tuple.Create("\"", 849)
            
            #line 24 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
, Tuple.Create(Tuple.Create("", 794), Tuple.Create<System.Object, System.Int32>(Url.Action("ModelItem",new {mainid = ViewBag.mainid })
            
            #line default
            #line hidden
, 794), false)
);

WriteLiteral(">電子報管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 947), Tuple.Create("\"", 1035)
            
            #line 28 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
, Tuple.Create(Tuple.Create("", 954), Tuple.Create<System.Object, System.Int32>(Url.Action("EPaperContentMenu",new {id = ViewBag.ID , mainid = ViewBag.mainid })
            
            #line default
            #line hidden
, 954), false)
);

WriteLiteral(">");

            
            #line 28 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
                                                                                                   Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral(" 管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">");

            
            #line 32 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
                   Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral(" 選擇訊息</a>\r\n        </li>\r\n    </ul>\r\n\r\n</div>\r\n<!--page bar end-->\r\n<!--message s" +
"tart-->\r\n<h2>");

            
            #line 39 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n\r\n<div");

WriteLiteral(" class=\"portlet light bordered\"");

WriteLiteral(">\r\n    <!--set item start-->\r\n    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn blue\"");

WriteLiteral(" id=\"btn_return\"");

WriteAttribute("onclick", Tuple.Create(" onclick=\"", 1409), Tuple.Create("\"", 1516)
, Tuple.Create(Tuple.Create("", 1419), Tuple.Create("location.href=\'", 1419), true)
            
            #line 43 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
   , Tuple.Create(Tuple.Create("", 1434), Tuple.Create<System.Object, System.Int32>(Url.Action("EPaperContentMenu",new {id = ViewBag.ID , mainid = ViewBag.mainid })
            
            #line default
            #line hidden
, 1434), false)
, Tuple.Create(Tuple.Create("", 1515), Tuple.Create("\'", 1515), true)
);

WriteLiteral(">返回列表</button>\r\n    <!--table start-->\r\n    <div");

WriteLiteral(" class=\"table-scrollable\"");

WriteLiteral(">\r\n        <table");

WriteLiteral(" class=\"table table-bordered table-hover\"");

WriteLiteral(" border=\"0\"");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral("\r\n               id=\"eventtable\"");

WriteLiteral("\r\n               data-url=\"");

            
            #line 48 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
                    Write(Url.Action("PagingMenuItem"));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n               data-sort-name=\'Sort\'");

WriteLiteral("\r\n               data-page-size=\'10\'");

WriteLiteral("\r\n               data-filed-id=\'ItemID\'");

WriteLiteral("\r\n               data-max-pagination=\'10\'");

WriteLiteral(">\r\n            <thead>\r\n                <tr");

WriteLiteral(" class=\"bg-grey_1\"");

WriteLiteral(" filed-class=\'odd gradeX\'");

WriteLiteral(">\r\n                    <th");

WriteLiteral(" filed-name=\'Selected\'");

WriteLiteral(" width=\"80px\"");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" filed-type=\'checkbox\'");

WriteLiteral(" filed-item-class=\"checkboxes selitem\"");

WriteLiteral(">選取</th>\r\n                    <th");

WriteLiteral(" filed-name=\'customertr\'");

WriteLiteral(" filed-type=\'function\'");

WriteLiteral(">標題</th>\r\n                    <th");

WriteLiteral(" filed-name=\'customertr\'");

WriteLiteral(" width=\"80px\"");

WriteLiteral(" filed-type=\'function\'");

WriteLiteral(">前台顯示</th>\r\n                </tr>\r\n            </thead>\r\n            <tbody></tbo" +
"dy>\r\n        </table>\r\n    </div>\r\n\r\n    <div");

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
"");

DefineSection("scripts", () => {

WriteLiteral("\r\n    <script>\r\n         $(document).ready(function () {\r\n            var menuid " +
"= \'");

            
            #line 76 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
                     Write(ViewBag.MenuId);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n            var id = \'");

            
            #line 77 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
                 Write(ViewBag.ID);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n            var mainid = \'");

            
            #line 78 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
                     Write(ViewBag.MainID);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n            var modelid = \'");

            
            #line 79 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
                      Write(ViewBag.ModelID);

            
            #line default
            #line hidden
WriteLiteral(@"';
            mytable = $(""#eventtable"").myDataTable().TableList[0];
            mytable.settingTdData = myDataTableTr;
            mytable.settingTableInfo = function (tableinfoId, totalcnt, offsetcnt, nowpagecnt) {
                var totalclick = mytable.keepcheckid.length;
                $(""#"" + tableinfoId).html('總筆數:' + totalcnt + '，顯示' + (totalcnt <= 0 ? 0 : offsetcnt) + ""~"" + (offsetcnt + nowpagecnt - 1));
            };
            mytable.SearchModelBase.ModelID = menuid;
            mytable.SearchModelBase.Key = id;
            mytable.GetData(1);

            $(""#eventtable"").delegate("".selitem"", ""change"", function () {

                var issel = $(this)[0].checked;
                var obj = {};

                //是否選取
                obj.issel = issel;

                //選取的訊息ID
                obj.itemid = $(this).attr('value');

                //Menu的 ID
                obj.menuid = menuid;

                //模組ID(訊息為2)
                obj.modelid = modelid;

                //ModelMessageMain 的ID
                obj.mainid = mainid;

                //EPaperItem 的ID
                obj.id = '");

            
            #line 111 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
                     Write(ViewBag.ID);

            
            #line default
            #line hidden
WriteLiteral("\';;\r\n\r\n\r\n                $.post(\'");

            
            #line 114 "..\..\Areas\webadmin\Views\EPaper\EPaperItemSelect.cshtml"
                   Write(Url.Action("SetEpaperItem"));

            
            #line default
            #line hidden
WriteLiteral(@"', obj, function (data) {

                });
            });
        });

        function customertr(row, columnidx, idx, filedname, tableid, type, fileditemclass) {
            if (columnidx == ""2"") {
                return ""<td>"" + row[""Title""] + ""</td>""
            } else {
                if (row['Enabled']) {
                    return ""<td class='text-center'><label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>"" +
                        ""<input type='checkbox' class='checkboxes' checked='checked'  disabled='disabled'/><span></span></label></td>"";
                } else {
                    return ""<td class='text-center'><label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>"" +
                        ""<input type='checkbox' class='checkboxes' disabled='disabled'/><span></span></label></td>"";
                }
            }
        }
    </script>
");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
