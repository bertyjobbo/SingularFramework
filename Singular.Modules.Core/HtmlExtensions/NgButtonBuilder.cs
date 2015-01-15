namespace Singular.Modules.Core.HtmlExtensions
{
    public class NgButtonBuilder : BuilderBase2
    {
        public string BootstrapButtonType { get; private set; }

        public string BeforeGlyphValue { get; private set; }

        public string AfterGlyphValue { get; private set; }

        public NgButtonBuilder BootstrapButton(string cssName)
        {
            BootstrapButtonType = cssName;
            return this;
        }

        public NgButtonBuilder BeforeGlyph(string glyph)
        {
            BeforeGlyphValue = glyph;
            return this;
        }

        public NgButtonBuilder AfterGlyph(string glyph)
        {
            AfterGlyphValue = glyph;
            return this;
        }
    }
}