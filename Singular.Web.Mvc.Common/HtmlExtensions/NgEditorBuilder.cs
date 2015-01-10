using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.Common.HtmlExtensions
{
    public class NgEditorBuilder : BuilderBase2
    {
        public string Editor { get; private set; }
        
        public bool IsBootstrapFormControl { get; private set; }
        public bool IsBootstrapFormGroup { get; private set; }
        public string ModelPrefixValue { get; private set; }

        public NgEditorBuilder BootstrapFormControl()
        {
            IsBootstrapFormControl = true;
            return this;
        }
        public NgEditorBuilder BootstrapFormGroup()
        {
            IsBootstrapFormGroup = true;
            return this;
        }
        public NgEditorBuilder ModelPrefix(string prefix)
        {
            ModelPrefixValue = prefix;
            return this;
        }

        #region editor choices

        public NgEditorBuilder TextBox()
        {
            Editor = "TextBox";
            return this;
        }

        #endregion
        
    }

    
}
