using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Singular.Modules.Core.HtmlExtensions
{
    public static class DebugExtensions
    {
        public static MvcHtmlString DebugOnlyWrite(this HtmlHelper html, string text)
        {
#if DEBUG
            return MvcHtmlString.Create(text);
#endif
            return null;
        }
    }
}