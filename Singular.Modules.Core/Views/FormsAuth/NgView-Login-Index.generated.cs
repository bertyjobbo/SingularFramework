﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Singular.Modules.Core.Views.FormsAuth
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 1 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
    using Singular.Modules.Core.HtmlExtensions;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
    using Singular.Web.Mvc.Common.HtmlExtensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/FormsAuth/NgView-Login-Index.cshtml")]
    public partial class NgView_Login_Index : System.Web.Mvc.WebViewPage<Singular.Modules.Core.Data.Models.FormsAuthModel>
    {
        public NgView_Login_Index()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
  
    Layout = null;

            
            #line default
            #line hidden
WriteLiteral("\r\n<h2>");

            
            #line 7 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
Write(Html.Translate("Login"));

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n\r\n");

            
            #line 9 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
 using (Html.BeginNgForm(x => x
        .OnSubmit("Login()")
        .CssClass("sg-form")
        ))
{
    
            
            #line default
            #line hidden
            
            #line 14 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
Write(Html.NgEditorFor(x => x.Email, x => x
                .Input()
                .ModelPrefix("Model")
                .BootstrapFormControl()
                .BootstrapFormGroup()
                .LabelText(Html.Translate("Email"))
                .Attribute("type", "email")
        //.Attribute("ng-required", "true")
        //.Attribute("autocomplete", "off")
        ));

            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
         
    
            
            #line default
            #line hidden
            
            #line 24 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
Write(Html.NgEditorFor(x => x.Password, x => x
                .Input()
                .BootstrapFormControl()
                .BootstrapFormGroup()
                .LabelText(Html.Translate("Password"))
                .Attribute("type", "password")
        //.Attribute("ng-required", "true")
        ));

            
            #line default
            #line hidden
            
            #line 31 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
         
    
            
            #line default
            #line hidden
            
            #line 43 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
           
    
            
            #line default
            #line hidden
            
            #line 44 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
Write(Html.NgEditorFor(x => x.RememberMe, x => x
                .UiCheckbox("left")
                .BootstrapFormControl()
                .BootstrapFormGroup()
                .LabelText(Html.Translate("Stay signed in?"))
        ));

            
            #line default
            #line hidden
            
            #line 49 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
         
    
            
            #line default
            #line hidden
            
            #line 54 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
           
    using (Html.BeginBootstrapFormGroup())
    {
        
            
            #line default
            #line hidden
            
            #line 57 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
   Write(Html.BootstrapButton(x => x
                .BootstrapButton("primary")
                .BeforeGlyph("ok")
                .LabelText(Html.Translate("Submit"))
                .Attribute("type", "submit")
                .Attribute("ng-disabled", "!FormIsValid()")
                .CssClass("margin-right-half")
            ));

            
            #line default
            #line hidden
            
            #line 64 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
             

        
            
            #line default
            #line hidden
            
            #line 66 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
   Write(Html.BootstrapButton(x => x
                .BootstrapButton("danger")
                .AfterGlyph("remove")
                .LabelText(Html.Translate("Reset"))
                .Attribute("type", "reset")
                .Attribute("ng-click", Html.PropertyExpression("Model.{0} = true;", y => y.RememberMe))
            ));

            
            #line default
            #line hidden
            
            #line 72 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
             
    }


            
            #line default
            #line hidden
WriteLiteral("    <hr />\r\n");

WriteLiteral("    <p><a");

WriteLiteral(" alt-href=\"Login,Forgot\"");

WriteLiteral(">");

            
            #line 76 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
                             Write(Html.Translate("Click here if you have forgotten your password"));

            
            #line default
            #line hidden
WriteLiteral("</a></p>\r\n");

            
            #line 77 "..\..\Views\FormsAuth\NgView-Login-Index.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

        }
    }
}
#pragma warning restore 1591