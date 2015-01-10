using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using Singular.Useful;

namespace Singular.Web.Mvc.Common.HtmlExtensions
{
    /// <summary>
    /// Angular Html Extensions
    /// </summary>
    public static class SgMvcHtmlExtensions
    {
        /// <summary>
        /// NgForm
        /// </summary>
        /// <param name="html"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static NgForm BeginNgForm(this HtmlHelper html, Action<NgFormBuilder> func)
        {
            var formBuilder = new NgFormBuilder();
            func.Invoke(formBuilder);

            var tagBuilder = new TagBuilder("form");

            if (formBuilder.CssClassExpression.HasValue())
            {
                tagBuilder.AddCssClass(formBuilder.CssClassExpression);
            }

            if (formBuilder.SubmitExpression.HasValue())
            {
                tagBuilder.Attributes.Add(formBuilder.DataPrefix + "ng-submit", formBuilder.SubmitExpression);
            }


            html.ViewContext.Writer
                            .Write(tagBuilder.ToString(TagRenderMode.StartTag));
            return new NgForm(html.ViewContext);
        }

        /// <summary>
        /// Bootstrap form group
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static MvcHtmlString NgEditorFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> propertyExpression, Action<NgEditorBuilder> func)
        {
            // check model
            var model = html.ViewData.Model;
            if (model.IsNotNull())
            {
                // start output
                var output = new StringBuilder();

                // get property name
                var propName = SingularExpressionHelper.GetPropertyName(propertyExpression);

                // get editor builder
                var edBuilder = new NgEditorBuilder();
                func.Invoke(edBuilder);

                // begin form group?
                if (edBuilder.IsBootstrapFormGroup)
                {
                    output.AppendFormat("<div class=\"form-group{0}\">", edBuilder.Editor == "UiRadioList" ? " uiradio-formgroup" : "");
                    output.Append(html.Label(propName, edBuilder.LabelTextValue ?? propName));
                }

                // switch
                switch (edBuilder.Editor)
                {
                    case "Input":
                        {
                            output.Append("<input");
                            doNgModel(edBuilder, output, propName, edBuilder.DataPrefix);
                            doIdAndName(output, propName);
                            doAttributes(edBuilder, output);
                            doClass(edBuilder, output);
                            output.Append(" />");
                            break;
                        }
                    case "UiCheckbox":
                    {
                        output.Append(
                            "<button type=\"button\" btn-checkbox btn-checkbox-true=\"true\" btn-checkbox-false=\"false\"");
                        doNgModel(edBuilder,output,propName,edBuilder.DataPrefix);
                        doIdAndName(output, propName);
                        doAttributes(edBuilder,output);
                        doClass(edBuilder,output);
                        output.Append(" >" + (edBuilder.LabelTextValue ?? propName) + "</button>");
                        break;
                    }
                    case "UiRadioList":
                    {
                        // start
                        output.Append("<div class=\"btn-group\">");

                        // loop data
                        if (edBuilder.ListData.HasItems())
                        {
                            foreach (var item in edBuilder.ListData)
                            {
                                output.AppendFormat(
                                    "<button type=\"button\"" +
                                    " class=\"btn btn-{0}\"" +
                                    " {1}ng-model=\"{2}{3}\"" +
                                    " name=\"{2}{3}\""+
                                    " btn-radio=\"{4}\">{5}</button>", 
                                    edBuilder.RadioListClass, 
                                    edBuilder.DataPrefix,
                                    edBuilder.ModelPrefixValue.HasValue() ? edBuilder.ModelPrefixValue + "." : "Model.",
                                    propName,
                                    item.Value,
                                    item.Text
                                    );
                            }
                        }

                        // end
                        output.Append("</div>");
                        break;
                    }
                    default:
                        break;
                }

                // end form group?
                if (edBuilder.IsBootstrapFormGroup)
                {
                    output.Append("</div>");
                }

                // return
                var final = output.ToString();
                return MvcHtmlString.Create(final);
            }

            return null;
        }

        /// <summary>
        /// BootstrapButton
        /// </summary>
        /// <param name="html"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapButton(this HtmlHelper html, Action<NgButtonBuilder> func)
        {
            var output = new StringBuilder();
            var builder = new NgButtonBuilder();
            func.Invoke(builder);
            output.Append("<button");
            doAttributes(builder, output);
            doClass(builder, output);
            output.Append(">");

            if (builder.BeforeGlyphValue.HasValue())
            {
                output.AppendFormat("<span class=\"glyphicon glyphicon-{0}\"></span> ", builder.BeforeGlyphValue);
            }

            output.Append(builder.LabelTextValue);

            if (builder.AfterGlyphValue.HasValue())
            {
                output.AppendFormat(" <span class=\"glyphicon glyphicon-{0}\"></span>", builder.AfterGlyphValue);
            }

            output.Append("</button>");

            return MvcHtmlString.Create(output.ToString());
        }

        /// <summary>
        /// Form group
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static BootstrapFormGroup BeginBootstrapFormGroup(this HtmlHelper html)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("form-group");
            html.ViewContext.Writer
                            .Write(tagBuilder.ToString(TagRenderMode.StartTag));
            return new BootstrapFormGroup(html.ViewContext);
        }

        /// <summary>
        /// Property expression - strongly typed string.format
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="formatString"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public static string PropertyExpression<TModel, TProperty>(this HtmlHelper<TModel> html,
            string formatString,
            params Expression<Func<TModel, TProperty>>[] propertyExpressions)
        {
            for (var i = 0; i < propertyExpressions.Length; i++)
            {
                var expr = propertyExpressions[i];
                var prop = SingularExpressionHelper.GetPropertyName(expr);
                formatString = formatString.Replace("{" + i + "}", prop);
            }
            return formatString;
        }

        // private
        private static void doAttributes(BuilderBase2 edBuilder, StringBuilder output)
        {
            foreach (var attr in edBuilder.Attributes)
            {
                if (attr.Key.HasValue() && attr.Key.ToLower() != "class")
                {
                    output.AppendFormat(" {0}=\"{1}\"", attr.Key, attr.Value);
                }
            }
        }
        private static void doClass(BuilderBase builder, StringBuilder output)
        {
            var css = builder.CssClassExpression;
         
            // check editor builder
            var edBuilder = builder as NgEditorBuilder;
            if (edBuilder != null)
            {
                if(edBuilder.IsBootstrapFormControl) css = "form-control " + css;
                if (edBuilder.Editor == "UiCheckbox") css = "btn btn-success" + css;
            }

            // check button builder
            var ngButtonBuilder = builder as NgButtonBuilder;
            if (ngButtonBuilder != null)
            {
                var btnBuilder = ngButtonBuilder;
                if (btnBuilder.BootstrapButtonType.HasValue())
                    css = "btn btn-" + btnBuilder.BootstrapButtonType + " " + css;
            }
            output.AppendFormat(" class=\"{0}\"", css);
        }
        private static void doNgModel(NgEditorBuilder builder,  StringBuilder output, string propName, string prefix)
        {
            var modelprefix = builder.ModelPrefixValue.HasValue() ? builder.ModelPrefixValue + "." : "Model.";
            output.Append(" " + prefix + "ng-model=\""+ modelprefix + propName + "\"");
        }
        private static void doIdAndName(StringBuilder output, string propName)
        {
            output.Append(" id=\"" + propName.Replace(".", "_") + "\"");
            output.Append(" name=\"" + propName + "\"");
        }
    }
}
