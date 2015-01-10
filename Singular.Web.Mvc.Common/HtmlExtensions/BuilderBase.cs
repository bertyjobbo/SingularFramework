using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.Common.HtmlExtensions
{
    public abstract class BuilderBase
    {
        public BuilderBase CssClass(string expression)
        {
            CssClassExpression = expression;
            return this;
        }
        public string CssClassExpression { get; private set; }

        public BuilderBase UseDataPrefix()
        {
            DataPrefix = "data-";
            return this;
        }
        public string DataPrefix { get; private set; }
    }

    public abstract class BuilderBase2 :BuilderBase
    {
        protected BuilderBase2()
        {
            Attributes=new Dictionary<string, string>();
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
