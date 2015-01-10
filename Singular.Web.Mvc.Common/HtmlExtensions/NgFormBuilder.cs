using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.Common.HtmlExtensions
{
    public class NgFormBuilder:BuilderBase
    {
        public NgFormBuilder OnSubmit(string expression)
        {
            SubmitExpression = expression;
            return this;
        }
        public string SubmitExpression { get; private set; }

        public NgFormBuilder CssClass(string expression)
        {
            CssClassExpression = expression;
            return this;
        }
        public string CssClassExpression { get; private set; }

        public NgFormBuilder UseDataPrefix()
        {
            DataPrefix = "data-";
            return this;
        }
        public string DataPrefix { get; private set; }
    }
}
