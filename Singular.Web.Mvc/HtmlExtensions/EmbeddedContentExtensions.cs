using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using System.Web.UI;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace Singular.Web.Mvc.HtmlExtensions
{
    public static class EmbeddedContentExtensions
    {
        private readonly static Page _page;

        static EmbeddedContentExtensions()
        {
            _page = new Page();
        }

        public static string EmbeddedContent(this UrlHelper url, object fromAssembly, string virtualPath, IncludeBehaviour behaviour = IncludeBehaviour.None)
        {
            // get type
            var type = fromAssembly.GetType();

            // get full name of assembly
            string fullName = type.Assembly.FullName;

            // char array
            var chrArray = new[] { ',' };

            // get name
            var name = string.Concat(fullName.Split(chrArray)[0], virtualPath.Replace("~", "").Replace("/", "."));

            // check behaviour
            if (behaviour == IncludeBehaviour.MinifyAlways)
            {
                name = name.Replace(".css", ".min.css").Replace(".js", ".min.js");
            }

#if (DEBUG)

#else
            // check behaviour
            if (behaviour == IncludeBehaviour.MinifyWhenNotDebug)
            {
                name = name.Replace(".css", ".min.css").Replace(".js", ".min.js");
            }
#endif

            return _page.ClientScript.GetWebResourceUrl(type, name);
        }

        public static string EmbeddedContent(this UrlHelper url, string areaName, string virtualPath,
            IncludeBehaviour behaviour = IncludeBehaviour.None)
        {
            // find include set
            var set =
                EmbeddedResourceManager.Current.Cache
                .FirstOrDefault(x => x.InnerCollection.AreaName == areaName);

            // check 
            if (set == null)
            {
                return "#";
            }

            //
            return EmbeddedContent(url, set.InnerCollection.AssemblyObject, virtualPath, behaviour);
        }
    }
}