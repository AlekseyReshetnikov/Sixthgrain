using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Grain
{
    public static class MyHtmlHelper
    {
        public static MvcHtmlString JavaScriptTag(this HtmlHelper htmlHelper, string srcStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\" src=\"");
            sb.Append(UrlHelper.GenerateContentUrl(srcStr, htmlHelper.ViewContext.HttpContext));
            sb.Append("\"></script>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}