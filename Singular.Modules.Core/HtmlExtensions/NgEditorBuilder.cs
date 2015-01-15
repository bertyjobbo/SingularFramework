using System.Collections.Generic;
using System.Web.Mvc;

namespace Singular.Modules.Core.HtmlExtensions
{
    public class NgEditorBuilder : BuilderBase2
    {
        public string Editor { get; private set; }

        public bool IsBootstrapFormControl { get; private set; }
        public bool IsBootstrapFormGroup { get; private set; }
        public string ModelPrefixValue { get; private set; }
        public bool EditorRequired { get; private set; }


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

        public NgEditorBuilder Required()
        {
            EditorRequired = true;
            return this;
        }

        public NgEditorBuilder ValidationErrorsPropertyName(string val)
        {
            ValidationErrorsPropertyNameValue = val;
            return this;
        }

        #region editor choices

        public IList<SelectListItem> ListData { get; private set; }
        public string RadioListClass { get; private set; }
        public string UiCheckboxLabelPosition { get; private set; }
        public string ValidationErrorsPropertyNameValue { get; private set; }

        public NgEditorBuilder Input()
        {
            Editor = "Input";
            return this;
        }

        public NgEditorBuilder UiCheckbox(string labelPos)
        {
            UiCheckboxLabelPosition = labelPos;
            Editor = "UiCheckbox";
            return this;
        }

        public NgEditorBuilder UiRadioList(IList<SelectListItem> data, string btnClass)
        {
            RadioListClass = btnClass;
            ListData = data;
            Editor = "UiRadioList";
            return this;
        }

        #endregion
    }
}