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

namespace Singular.Modules.Core.Views.Users
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
    
    #line 1 "..\..\Views\Users\Index.cshtml"
    using Singular.Web.Mvc.HtmlExtensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Users/Index.cshtml")]
    public partial class Index : System.Web.Mvc.WebViewPage<Singular.Modules.Core.ViewModels.CoreViewModelBase>
    {
        public Index()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Users\Index.cshtml"
  
    ViewBag.Title = Html.Translate("User management");
    ViewBag.NgAppName = "Singular.Modules.SingularUsersApp";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>");

            
            #line 9 "..\..\Views\Users\Index.cshtml"
Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 287), Tuple.Create("\"", 393)
            
            #line 13 "..\..\Views\Users\Index.cshtml"
, Tuple.Create(Tuple.Create("", 293), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("Core","~/Content/Ng/Modules/UsersApp.js", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 293), false)
);

WriteLiteral("></script>\r\n");

});

        }
    }
}
#pragma warning restore 1591