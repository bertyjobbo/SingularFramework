using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.Common.HtmlExtensions
{
    public class NgButtonBuilder : BuilderBase2
    {
        public NgButtonBuilder BootstrapButton(string cssName)
        {
            BootstrapButtonType = cssName;
            return this;
        }

        public string BootstrapButtonType { get; private set; }

        public NgButtonBuilder BeforeGlyph(string glyph)
        {
            BeforeGlyphValue = glyph;
            return this;
        }

        public string BeforeGlyphValue { get; private set; }

        public NgButtonBuilder AfterGlyph(string glyph)
        {
            AfterGlyphValue = glyph;
            return this;
        }

        public string AfterGlyphValue { get; private set; }
    }
}
