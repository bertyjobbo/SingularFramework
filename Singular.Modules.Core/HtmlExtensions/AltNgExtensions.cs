using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Singular.Useful;

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
        /// <param name="parentViewFolder"></param>
        /// <param name="useCache"></param>
        /// <param name="useDataPrefix"></param>
        /// <returns></returns>
        public static MvcHtmlString AltView(this HtmlHelper html, string parentViewFolder, bool useCache, bool useDataPrefix = false)
        {
            return altView(html, parentViewFolder, useCache, "~/Singular/Core/NgView/", useDataPrefix);
        }

        /// <summary>
        /// Alt view (customisable)
        /// </summary>
        /// <param name="html"></param>
        /// <param name="parentViewFolder"></param>
        /// <param name="useCache"></param>
        /// <param name="baseUrl"></param>
        /// <param name="useDataPrefix"></param>
        /// <returns></returns>
        public static MvcHtmlString AltView(this HtmlHelper html, string parentViewFolder, bool useCache, string baseUrl,
            bool useDataPrefix = false)
        {
            return altView(html, parentViewFolder, useCache, baseUrl, useDataPrefix);
        }

        private static MvcHtmlString altView(this HtmlHelper html, string parentViewFolder, bool useCache, string baseUrl,
            bool useDataPrefix = false)
        {
            var model = html.ViewData.Model;
            var guid = Guid.Empty;
            if (model != null)
            {
                // get type
                var type = model.GetType();

                // get from cache
                var fromCache = ViewModelDictionary.FirstOrDefault(x => x.Value == type);
                if (fromCache.IsKeyValuePairNull())
                {
                    guid = Guid.NewGuid();
                    ViewModelDictionary.Add(guid, type);
                }
                else
                {
                    guid = fromCache.Key;
                }

            }

            if (baseUrl.Last() != '/')
                baseUrl += "/";

            // url
            var url = new StringBuilder("'");
            url.Append(VirtualPathUtility.ToAbsolute(baseUrl));
            url.Append("?folder=" + parentViewFolder);
            url.Append("&c=' + $controller + '&a=' + $action + '&guid=");
            url.Append(HttpUtility.UrlEncode(guid.ToString()));
            url.Append("'");
            
            // finally
            var output = new StringBuilder("<section");
            output.AppendFormat(" {0}alt-view=\"", useDataPrefix ? "data-" : "");
            output.Append(url);
            output.AppendFormat("\" alt-view-cache=\"{0}\"", useCache.ToString().ToLower());
            output.Append("></section>");

            return MvcHtmlString.Create(output.ToString());
        }

        public static readonly IDictionary<Guid, Type> ViewModelDictionary = new Dictionary<Guid, Type>();
    }

}