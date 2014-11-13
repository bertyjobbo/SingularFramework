using System.Web.Mvc;

namespace Singular.Modules.Core.HtmlExtensions
{
    public static class TranslateExtensions
    {
        public static string Translate(this HtmlHelper html, string originalValue)
        {
            // TODO - move this into separate module (core?) 
            return originalValue;
        }
    }
}
