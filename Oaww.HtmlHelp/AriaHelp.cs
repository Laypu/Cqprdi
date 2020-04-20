using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Oaww.Entity;
using Oaww.Business;

namespace Oaww.HtmlHelp
{
    public static class AriaHelp
    {
        public static MvcHtmlString aira_HyperLink_Menu(this HtmlHelper helper, Menu menu, object htmlAttributes = null, string appendText = "")
        {
            var builder = new TagBuilder("a");
            if (menu.LinkMode == 1)
            {
                builder.MergeAttribute("href", "#");
            }
            else if (menu.LinkMode == 2) //模組
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                GetFrontLink(ref builder, menu, urlHelper);

                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }

            }
            else if (menu.LinkMode == 3) //超連結
            {
                if (menu.LinkUrl.Contains("http"))
                {
                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }
                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }

                }

                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }
            }
            else if (menu.LinkMode == 4) //檔案
            {
                builder.MergeAttribute("alt", menu.LinkUploadFileName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));
            }

            builder.MergeAttribute("title", menu.MenuName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));

            if (menu.OpenMode == 2)
            {
                builder.MergeAttribute("target", "_blank");
            }

            builder.InnerHtml = menu.MenuName + appendText;

            if (htmlAttributes != null)
            {
                RouteValueDictionary result = new RouteValueDictionary();
                foreach (System.ComponentModel.PropertyDescriptor property in System.ComponentModel.TypeDescriptor.GetProperties(htmlAttributes))
                {
                    result.Add(property.Name.Replace("data_", "data-"), property.GetValue(htmlAttributes));
                }
                builder.MergeAttributes(result);
            }

            return MvcHtmlString.Create(builder.ToString());
        }
        public static MvcHtmlString aira_HyperLink_Menu(this HtmlHelper helper, Menu menu, string innerHtml, string title, object htmlAttributes = null)
        {
            var builder = new TagBuilder("a");
            if (menu.LinkMode == 1)
            {
                builder.MergeAttribute("href", "#");
            }
            else if (menu.LinkMode == 2) //模組
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                GetFrontLink(ref builder, menu, urlHelper);

                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }

            }
            else if (menu.LinkMode == 3) //超連結
            {
                if (menu.LinkUrl.Contains("http"))
                {
                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }

                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }


                }

                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }
            }
            else if (menu.LinkMode == 4) //檔案
            {
                builder.MergeAttribute("alt", menu.LinkUploadFileName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));
            }

            if (menu.OpenMode == 2)
            {
                builder.MergeAttribute("target", "_blank");
            }

            builder.MergeAttribute("title", title + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));

            builder.InnerHtml = innerHtml;

            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }

            return MvcHtmlString.Create(builder.ToString());
        }
        public static MvcHtmlString aira_Top_HyperLink_Menu(this HtmlHelper helper, Menu menu, object htmlAttributes = null)
        {
            var builder = new TagBuilder("a");
            if (menu.LinkMode == 1)
            {
                builder.MergeAttribute("href", "#");
            }
            else if (menu.LinkMode == 2) //模組
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                GetFrontLink(ref builder, menu, urlHelper);

                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }

            }
            else if (menu.LinkMode == 3) //超連結
            {
                if (menu.LinkUrl.Contains("http"))
                {
                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }

                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }
                }


                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }
            }
            else if (menu.LinkMode == 4) //檔案
            {
                builder.MergeAttribute("alt", menu.LinkUploadFileName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));
            }

            if (menu.OpenMode == 2)
            {
                builder.MergeAttribute("target", "_blank");
            }

            builder.MergeAttribute("title", menu.MenuName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));

            builder.InnerHtml = menu.MenuName + $"<div class=\"small\">{menu.DisplayName}</div>";

            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }



            return MvcHtmlString.Create(builder.ToString());
        }
        public static MvcHtmlString aira_Top_HyperLink_Menu(this HtmlHelper helper, Menu menu, string icon, object htmlAttributes = null)
        {
            var builder = new TagBuilder("a");
            if (menu.LinkMode == 1)
            {
                builder.MergeAttribute("href", "#");
            }
            else if (menu.LinkMode == 2) //模組
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                GetFrontLink(ref builder, menu, urlHelper);

                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }

            }
            else if (menu.LinkMode == 3) //超連結
            {
                if (menu.LinkUrl.Contains("http"))
                {
                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }

                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }
                }


                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }
            }
            else if (menu.LinkMode == 4) //檔案
            {
                builder.MergeAttribute("alt", menu.LinkUploadFileName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));
            }

            if (menu.OpenMode == 2)
            {
                builder.MergeAttribute("target", "_blank");
            }

            builder.MergeAttribute("title", menu.MenuName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));

            builder.InnerHtml = $"<i class=\"{icon}\" aria-hidden=\"true\"></i><b>{menu.MenuName}</b>";

            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }



            return MvcHtmlString.Create(builder.ToString());
        }
        public static MvcHtmlString aira_HyperLink_Menu(this HtmlHelper helper, Menu menu, object htmlAttributes = null)
        {
            var builder = new TagBuilder("a");
            if (menu.LinkMode == 1)
            {
                builder.MergeAttribute("href", "#");
            }
            else if (menu.LinkMode == 2) //模組
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                GetFrontLink(ref builder, menu, urlHelper);

                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }

            }
            else if (menu.LinkMode == 3) //超連結
            {
                if (menu.LinkUrl.Contains("http"))
                {
                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }

                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }
                }


                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }
            }
            else if (menu.LinkMode == 4) //檔案
            {
                builder.MergeAttribute("alt", menu.LinkUploadFileName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));
            }

            if (menu.OpenMode == 2)
            {
                builder.MergeAttribute("target", "_blank");
            }

            builder.MergeAttribute("title", menu.MenuName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));

            builder.InnerHtml = menu.MenuName;

            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }



            return MvcHtmlString.Create(builder.ToString());
        }
        public static MvcHtmlString aira_HyperLink(this HtmlHelper helper, string text, string Url, string title, bool newWindow, bool more)
        {
            var builder = new TagBuilder("a");
            builder.MergeAttribute("title", (more ? aria.more : "") + title + (newWindow ? aria.newWindow : ""));
            builder.MergeAttribute("href", Url);
            builder.MergeAttribute("target", newWindow ? "_blank" : "_self");
            builder.InnerHtml = text;
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString aira_Icon_HyperLink_Menu(this HtmlHelper helper, Menu menu, string icon, object htmlAttributes = null)
        {
            var builder = new TagBuilder("a");
            if (menu.LinkMode == 1)
            {
                builder.MergeAttribute("href", "#");
            }
            else if (menu.LinkMode == 2) //模組
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                GetFrontLink(ref builder, menu, urlHelper);

                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }

            }
            else if (menu.LinkMode == 3) //超連結
            {
                if (menu.LinkUrl.Contains("http"))
                {
                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }

                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }
                }


                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }
            }
            else if (menu.LinkMode == 4) //檔案
            {
                builder.MergeAttribute("alt", menu.LinkUploadFileName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));
            }

            if (menu.OpenMode == 2)
            {
                builder.MergeAttribute("target", "_blank");
            }

            builder.MergeAttribute("title", menu.MenuName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));

            builder.InnerHtml = $"<i class=\"{icon}\" aria-hidden=\"true\"></i>{menu.MenuName}";

            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }



            return MvcHtmlString.Create(builder.ToString());
        }
        /// <summary>
        /// 無障礙圖片
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <param name="alt"></param>
        /// <param name="title">圖加連結不加title</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString aria_img(this HtmlHelper helper, string src, string alt, object htmlAttributes = null)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.Attributes.Add("src", src);

            builder.Attributes.Add("alt", alt.Length > 150 ? alt.Substring(0, 150) : alt);

            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString Require(this HtmlHelper helper)
        {
            return MvcHtmlString.Create("<span class=red>*</span>");
        }

        public static MvcHtmlString FileTypeAndSize(this HtmlHelper helper, string FileType, int? FileSize)
        {
            string result = "<div>";

            string icon = string.Empty;

            switch (FileType)
            {
                case "Word":
                    icon = "<i class=\"fa fa-file-word-o\" aria-hidden=\"true\"></i>";
                    break;
                case "Excel":
                    icon = "<i class=\"fa fa-file-excel-o\" aria-hidden=\"true\"></i>";
                    break;
                case "ZIP":
                    icon = "<i class=\"fa fa-file-archive-o\" aria-hidden=\"true\"></i>";
                    break;
                case "PDF":
                    icon = "<i class=\"fa fa-file-pdf-o\" aria-hidden=\"true\"></i>";
                    break;
                case "IMAGE":
                    icon = "<i class=\"fa fa-file-image-o\" aria-hidden=\"true\"></i>";
                    break;
                default:
                    icon = "";
                    break;
            }


            result += icon;

            result += $" 檔案大小: {FileSize}kb | 檔案格式: {FileType.ToLower()}";

            result += "</div>";

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString FileType(this HtmlHelper helper, string FileType)
        {
            string icon = string.Empty;

            switch (FileType)
            {
                case "Word":
                    icon = "<i class=\"fa fa-file-word-o\" aria-hidden=\"true\"></i>";
                    break;
                case "Excel":
                    icon = "<i class=\"fa fa-file-excel-o\" aria-hidden=\"true\"></i>";
                    break;
                case "ZIP":
                    icon = "<i class=\"fa fa-file-archive-o\" aria-hidden=\"true\"></i>";
                    break;
                case "PDF":
                    icon = "<i class=\"fa fa-file-pdf-o\" aria-hidden=\"true\"></i>";
                    break;
                case "IMAGE":
                    icon = "<i class=\"fa fa-file-image-o\" aria-hidden=\"true\"></i>";
                    break;
                default:
                    icon = "";
                    break;
            }

            return MvcHtmlString.Create(icon);
        }

        public static void GetFrontLink(ref TagBuilder builder, Menu menu, UrlHelper urlHelper)
        {
            CommonService _commonService = new CommonService();

            string Url = _commonService.GetFrontUrl(menu.ModelID.Value);

            builder.MergeAttribute("href", urlHelper.Action("Index", Url, new { @itemid = menu.ModelItemID, @mid = menu.ID }));

        }

        public static MvcHtmlString aira_HyperLink_SiteMapMenu(this HtmlHelper helper, Menu menu, object htmlAttributes = null)
        {
            var builder = new TagBuilder("a");
            if (menu.LinkMode == 1)
            {
                builder.MergeAttribute("href", "javascript:void(0);");
            }
            else if (menu.LinkMode == 2) //模組
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                GetFrontLink(ref builder, menu, urlHelper);

                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }

            }
            else if (menu.LinkMode == 3) //超連結
            {
                if (menu.LinkUrl.Contains("http"))
                {
                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }

                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                    if (menu.LinkUrl.Contains("mid"))
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl);
                    }
                    else
                    {
                        builder.MergeAttribute("href", urlHelper.Content("~") + menu.LinkUrl + $"{(menu.LinkUrl.Contains("?") ? "&" : "?")}mid=" + menu.ID);
                    }
                }


                if (menu.OpenMode == 3)
                {
                    builder.MergeAttribute("onclick", $"window.open(this.href, '{menu.MenuName}','width={menu.WindowWidth},height={menu.ImageHeight}'); return false; ");
                }
            }
            else if (menu.LinkMode == 4) //檔案
            {
                builder.MergeAttribute("alt", menu.LinkUploadFileName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));
            }

            if (menu.OpenMode == 2)
            {
                builder.MergeAttribute("target", "_blank");
            }

            builder.MergeAttribute("title", menu.MenuName + (menu.OpenMode == 2 || menu.OpenMode == 3 ? aria.newWindow : ""));

            builder.InnerHtml = menu.MenuName;

            if (htmlAttributes != null)
            {
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }



            return MvcHtmlString.Create(builder.ToString());
        }



    }
}
