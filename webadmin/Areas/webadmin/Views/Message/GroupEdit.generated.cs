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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/webadmin/Views/Message/GroupEdit.cshtml")]
    public partial class _Areas_webadmin_Views_Message_GroupEdit_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_webadmin_Views_Message_GroupEdit_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
  
    Layout = "~/Areas/webadmin/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 76), Tuple.Create("\"", 104)
, Tuple.Create(Tuple.Create("", 82), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/datatable.js")
, 82), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 124), Tuple.Create("\"", 149)
, Tuple.Create(Tuple.Create("", 130), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/custom.js")
, 130), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 169), Tuple.Create("\"", 199)
, Tuple.Create(Tuple.Create("", 175), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/bootbox.min.js")
, 175), false)
);

WriteLiteral("></script>\r\n<!--page bar start-->\r\n<div");

WriteLiteral(" class=\"page-bar\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" class=\"page-breadcrumb\"");

WriteLiteral(">\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 321), Tuple.Create("\"", 356)
            
            #line 11 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
, Tuple.Create(Tuple.Create("", 328), Tuple.Create<System.Object, System.Int32>(Url.Action(" Index","Home")
            
            #line default
            #line hidden
, 328), false)
);

WriteLiteral(">Home</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 453), Tuple.Create("\"", 489)
            
            #line 15 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
, Tuple.Create(Tuple.Create("", 460), Tuple.Create<System.Object, System.Int32>(Url.Action(" Index","Model")
            
            #line default
            #line hidden
, 460), false)
);

WriteLiteral(">模組元件</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 586), Tuple.Create("\"", 624)
            
            #line 19 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
, Tuple.Create(Tuple.Create("", 593), Tuple.Create<System.Object, System.Int32>(Url.Action(" Index","Message")
            
            #line default
            #line hidden
, 593), false)
);

WriteLiteral(">訊息管理</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 721), Tuple.Create("\"", 792)
            
            #line 23 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
, Tuple.Create(Tuple.Create("", 728), Tuple.Create<System.Object, System.Int32>(Url.Action("ModelItem","Message",new { mainid=ViewBag.mainid })
            
            #line default
            #line hidden
, 728), false)
);

WriteLiteral(">清單</a>\r\n            <i");

WriteLiteral(" class=\"fa fa-circle\"");

WriteLiteral("></i>\r\n        </li>\r\n        <li>\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(">類別管理</a>\r\n        </li>\r\n    </ul>\r\n\r\n</div>\r\n<!--page bar end-->\r\n<div");

WriteLiteral(" class=\"title_01\"");

WriteLiteral("> 類別管理設定</div>\r\n<div");

WriteLiteral(" class=\"portlet light bordered\"");

WriteLiteral(">\r\n    <!--set item start-->\r\n    <div");

WriteLiteral(" class=\"table-toolbar\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-md-6 col-sm-12 col-xs-12 mobile_left \"");

WriteLiteral(">\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-md-6 col-sm-12 col-xs-12 mobile_left \"");

WriteLiteral(">\r\n            <p");

WriteLiteral(" class=\"display_inline \"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" class=\"btn green-meadow\"");

WriteLiteral(" id=\'btn_add\'");

WriteLiteral(">新增 <i");

WriteLiteral(" class=\"fa fa-plus\"");

WriteLiteral("></i></button>\r\n                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn blue\"");

WriteLiteral(" id=\"btn_return\"");

WriteLiteral(">返回</button>\r\n            </p>\r\n        </div>\r\n    </div>\r\n    <!--set item end-" +
"->\r\n    <!--table start-->\r\n    <div");

WriteLiteral(" class=\"table-scrollable\"");

WriteLiteral(">\r\n        <table");

WriteLiteral(" class=\"table table-bordered table-hover\"");

WriteLiteral(" border=\"0\"");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral(" style=\"width:500px\"");

WriteLiteral("\r\n               id=\"eventtable\"");

WriteLiteral("\r\n               data-url=\"");

            
            #line 51 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                    Write(Url.Action("PagingGroup"));

            
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

WriteLiteral(" width=\"80px\"");

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

WriteLiteral(" filed-name=\'Group_Name\'");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" filed-type=\'link\'");

WriteLiteral(">類別名稱</th>\r\n                    <th");

WriteLiteral(" width=\"80px\"");

WriteLiteral(" filed-name=\'Enabled\'");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" filed-type=\'checkbox\'");

WriteLiteral(" filed-item-class=\"chkstatus\"");

WriteLiteral(">顯示</th>\r\n                </tr>\r\n            </thead>\r\n            <tbody></tbody" +
">\r\n        </table>\r\n    </div>\r\n    <div");

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

WriteLiteral(">\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <!--table end-->\r\n\r\n</div>" +
"\r\n<!--message end-->\r\n");

DefineSection("scripts", () => {

WriteLiteral(@"
    <script>
    var selvalue = [];
    $(document).ready(function () {
        mytable = $(""#eventtable"").myDataTable().TableList[0];
        mytable.settingTdData = myDataTableTr;
        mytable.settingTableInfo = function (tableinfoId, totalcnt, offsetcnt, nowpagecnt) {
            var totalclick = mytable.keepcheckid.length;
            $(""#"" + tableinfoId).html('總筆數:' + totalcnt + '，顯示' + (totalcnt <= 0 ? 0 : offsetcnt) + ""~"" + (offsetcnt + nowpagecnt - 1));
        };
        mytable.SearchModelBase.ModelID = '");

            
            #line 93 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                                      Write(ViewBag.mainid);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        mytable.GetData(1);\r\n        var obj = {};\r\n        obj.item = \"類別\";\r" +
"\n        obj.mainid = \'");

            
            #line 97 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                 Write(ViewBag.mainid);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        RegisterOrder(\"#eventtable\", \".sortedit\", \'");

            
            #line 98 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                                              Write(Url.Action("EditGroupSeq"));

            
            #line default
            #line hidden
WriteLiteral("\', obj);\r\n        RegisterClickAll(\"#chk_all\", \'#eventtable tbody .chksel\', \"#sel" +
"value\");\r\n        RegisterDelete(\"#btn_del\", \'#eventtable .chksel:checked\', \'");

            
            #line 100 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                                                              Write(Url.Action("SetGroupDelete"));

            
            #line default
            #line hidden
WriteLiteral("\', obj);\r\n        RegisterClicklink(\"#eventtable\", \".edit\", \'");

            
            #line 101 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                                              Write(Url.Action("EditGroup"));

            
            #line default
            #line hidden
WriteLiteral("\', obj, \'UpdateItem\');\r\n        RegisterClicklink(\"#eventtable\", \".manage\", \'");

            
            #line 102 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                                                Write(Url.Action("ModelItem"));

            
            #line default
            #line hidden
WriteLiteral("\', obj);\r\n        RegisterClick(\"#eventtable\", \".chkstatus\", \'");

            
            #line 103 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                                               Write(Url.Action("SetGroupStatus"));

            
            #line default
            #line hidden
WriteLiteral("\', obj);\r\n        $(\"#btn_add\").click(function () { CallAddDialog(\'");

            
            #line 104 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                                                    Write(Url.Action("EditGroup"));

            
            #line default
            #line hidden
WriteLiteral("\', obj); });\r\n        $(\"#btn_return\").click(function () {\r\n            CreatePos" +
"t(\'");

            
            #line 106 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                   Write(Url.Action("ModelItem"));

            
            #line default
            #line hidden
WriteLiteral("\', { mainid: \'");

            
            #line 106 "..\..\Areas\webadmin\Views\Message\GroupEdit.cshtml"
                                                         Write(ViewBag.mainid);

            
            #line default
            #line hidden
WriteLiteral("\' });\r\n        });\r\n    });\r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
