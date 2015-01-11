using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Singular.Modules.Core.HtmlExtensions
{
    /// <summary>
    /// Alt-ng extensions
    /// </summary>
    public static class AltNgExtensions
    {
        /// <summary>
        /// Alt View
        /// </summary>
        /// <param name="html"></param>
        /// <param name="parentFolder"></param>
        /// <param name="useDataPrefix"></param>
        /// <returns></returns>
        public static MvcHtmlString AltView(this HtmlHelper html, string parentFolder, bool useDataPrefix = false)
        {
            var model = html.ViewData.Model;
            var modelStr = string.Empty;
            if (model != null)
            {
                modelStr = model.GetType().FullName;
            }

            var url = new StringBuilder("'");
                url.Append(VirtualPathUtility.ToAbsolute("~/Singular/Core/NgView/"));
            url.Append("?folder=" + parentFolder);
            url.Append("&c=' + $controller + '&a=' + $action + '&model=");
            url.Append(HttpUtility.UrlEncode(modelStr));
            url.Append("'"); 

            var output = new StringBuilder("<section");
            output.AppendFormat(" {0}alt-view=\"", useDataPrefix ? "data-" : "");
            output.Append(url);
            output.Append("\"></section>");

            return MvcHtmlString.Create(output.ToString());

        }
        
    }
    
}