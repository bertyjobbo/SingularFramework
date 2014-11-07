using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Singular.Web.Mvc.HtmlExtensions
{
    public static class TranslateExtensions
    {
        public static string Translate(this HtmlHelper html, string originalValue)
        {
            return "!" + originalValue + "!";
        }
    }
}
