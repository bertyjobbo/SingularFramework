﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
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
    
    #line 1 "..\..\Views\FormsAuth\Index.cshtml"
    using Singular.Modules.Core.HtmlExtensions;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\FormsAuth\Index.cshtml"
    using Singular.Web.Mvc.HtmlExtensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/FormsAuth/Index.cshtml")]
    public partial class Index : System.Web.Mvc.WebViewPage<Singular.Modules.Core.ViewModels.CoreViewModelBase>
    {
        public Index()
        {
        }
        public override void Execute()
        {
            
            #line 5 "..\..\Views\FormsAuth\Index.cshtml"
  
    ViewBag.NgAppName = "Singular.Modules.SingularFormsAuthApp";
    ViewBag.Title = Html.Translate("Authentication");

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 339), Tuple.Create("\"", 458)
            
            #line 12 "..\..\Views\FormsAuth\Index.cshtml"
, Tuple.Create(Tuple.Create("", 345), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("Core", "~/Content/Ng/Modules/SingularFormsAuthApp.js", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 345), false)
);

WriteLiteral("></script>\r\n");

});

            
            #line 14 "..\..\Views\FormsAuth\Index.cshtml"
Write(Html.AltView("~/Views/FormsAuth", false, false));

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
