namespace Singular.Modules.Core.HtmlExtensions
{
    public class NgFormBuilder : BuilderBase
    {
        public string SubmitExpression { get; private set; }

        public string CssClassExpression { get; private set; }

        public string DataPrefix { get; private set; }

        public NgFormBuilder OnSubmit(string expression)
        {
            SubmitExpression = expression;
            return this;
        }

        public NgFormBuilder CssClass(string expression)
        {
            CssClassExpression = expression;
            return this;
        }

        public NgFormBuilder UseDataPrefix()
        {
            DataPrefix = "data-";
            return this;
        }
    }
}