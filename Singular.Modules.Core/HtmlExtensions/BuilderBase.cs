using System.Collections.Generic;

namespace Singular.Modules.Core.HtmlExtensions
{
    public abstract class BuilderBase
    {
        public string CssClassExpression { get; private set; }

        public string DataPrefix { get; private set; }

        public BuilderBase CssClass(string expression)
        {
            CssClassExpression = expression;
            return this;
        }

        public BuilderBase UseDataPrefix()
        {
            DataPrefix = "data-";
            return this;
        }
    }

    public abstract class BuilderBase2 : BuilderBase
    {
        protected BuilderBase2()
        {
            Attributes = new Dictionary<string, string>();
        }

        public string LabelTextValue { get; private set; }

        public Dictionary<string, string> Attributes { get; private set; }


        public BuilderBase2 LabelText(string text)
        {
            LabelTextValue = text;
            return this;
        }

        public BuilderBase2 Attribute(string key, string value)
        {
            Attributes.Add(key, value);
            return this;
        }
    }
}