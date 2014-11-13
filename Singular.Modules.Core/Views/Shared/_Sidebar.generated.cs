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

namespace Singular.Modules.Core.Views.Shared
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
    
    #line 1 "..\..\Views\Shared\_Sidebar.cshtml"
    using Singular.Web.Mvc.HtmlExtensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_Sidebar.cshtml")]
    public partial class Sidebar : System.Web.Mvc.WebViewPage<Singular.Modules.Core.ViewModels.CoreViewModelBase>
    {
        public Sidebar()
        {
        }
        public override void Execute()
        {
WriteLiteral("<nav");

WriteLiteral(" id=\"sidebar\"");

WriteLiteral(" class=\"dt-only\"");

WriteLiteral(">\r\n    <ul");

WriteLiteral(" role=\"navigation\"");

WriteLiteral(">\r\n");

            
            #line 5 "..\..\Views\Shared\_Sidebar.cshtml"
        
            
            #line default
            #line hidden
            
            #line 5 "..\..\Views\Shared\_Sidebar.cshtml"
         foreach (var section in Model.Sections)
        {

            
            #line default
            #line hidden
WriteLiteral("            <li");

WriteAttribute("class", Tuple.Create(" class=\"", 270), Tuple.Create("\"", 320)
            
            #line 7 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create("", 278), Tuple.Create<System.Object, System.Int32>(section.IsActive.Invoke() ? "active":""
            
            #line default
            #line hidden
, 278), false)
);

WriteLiteral(">\r\n                <a");

WriteAttribute("title", Tuple.Create(" title=\"", 342), Tuple.Create("\"", 422)
            
            #line 8 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create("", 350), Tuple.Create<System.Object, System.Int32>(Html.Translate(section.Name)
            
            #line default
            #line hidden
, 350), false)
, Tuple.Create(Tuple.Create(" ", 381), Tuple.Create("|", 382), true)
            
            #line 8 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create(" ", 383), Tuple.Create<System.Object, System.Int32>(Html.Translate(section.Description)
            
            #line default
            #line hidden
, 384), false)
);

WriteAttribute("href", Tuple.Create("\r\n                   href=\"", 423), Tuple.Create("\"", 518)
            
            #line 9 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create("", 450), Tuple.Create<System.Object, System.Int32>(Url.Action(section.Action, section.Controller, section.RouteValues)
            
            #line default
            #line hidden
, 450), false)
);

WriteLiteral(">\r\n                    <img");

WriteAttribute("src", Tuple.Create(" src=\"", 546), Tuple.Create("\"", 616)
            
            #line 10 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create("", 552), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent(section.AreaName, section.ImageVirtualPath)
            
            #line default
            #line hidden
, 552), false)
);

WriteAttribute("alt", Tuple.Create("\r\n                         alt=\"", 617), Tuple.Create("\"", 686)
            
            #line 11 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create("", 649), Tuple.Create<System.Object, System.Int32>(Html.Translate(section.Name)
            
            #line default
            #line hidden
, 649), false)
, Tuple.Create(Tuple.Create(" ", 680), Tuple.Create("Image", 681), true)
);

WriteLiteral(" />\r\n                </a>\r\n            </li>\r\n");

            
            #line 14 "..\..\Views\Shared\_Sidebar.cshtml"

        }

            
            #line default
            #line hidden
WriteLiteral("    </ul>\r\n</nav>\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
