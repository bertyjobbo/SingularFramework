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
    using Singular.Modules.Core.HtmlExtensions;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Shared\_Sidebar.cshtml"
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

            
            #line 6 "..\..\Views\Shared\_Sidebar.cshtml"
        
            
            #line default
            #line hidden
            
            #line 6 "..\..\Views\Shared\_Sidebar.cshtml"
         foreach (var section in Model.Sections)
        {

            
            #line default
            #line hidden
WriteLiteral("            <li");

WriteAttribute("class", Tuple.Create(" class=\"", 315), Tuple.Create("\"", 367)
            
            #line 8 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create("", 323), Tuple.Create<System.Object, System.Int32>(section.IsActive.Invoke() ? "active" : ""
            
            #line default
            #line hidden
, 323), false)
);

WriteLiteral(">\r\n                <a");

WriteLiteral(" data-ng-click=\"Ui.ShowLoader(\'");

            
            #line 9 "..\..\Views\Shared\_Sidebar.cshtml"
                                            Write(Url.Action(section.Action, section.Controller, section.RouteValues));

            
            #line default
            #line hidden
WriteLiteral("\')\"");

WriteAttribute("title", Tuple.Create(" title=\"", 491), Tuple.Create("\"", 571)
            
            #line 9 "..\..\Views\Shared\_Sidebar.cshtml"
                                                  , Tuple.Create(Tuple.Create("", 499), Tuple.Create<System.Object, System.Int32>(Html.Translate(section.Name)
            
            #line default
            #line hidden
, 499), false)
, Tuple.Create(Tuple.Create(" ", 530), Tuple.Create("|", 531), true)
            
            #line 9 "..\..\Views\Shared\_Sidebar.cshtml"
                                                                                   , Tuple.Create(Tuple.Create(" ", 532), Tuple.Create<System.Object, System.Int32>(Html.Translate(section.Description)
            
            #line default
            #line hidden
, 533), false)
);

WriteAttribute("href", Tuple.Create("\r\n                   href=\"", 572), Tuple.Create("\"", 667)
            
            #line 10 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create("", 599), Tuple.Create<System.Object, System.Int32>(Url.Action(section.Action, section.Controller, section.RouteValues)
            
            #line default
            #line hidden
, 599), false)
);

WriteLiteral(">\r\n                    <img");

WriteAttribute("src", Tuple.Create(" src=\"", 695), Tuple.Create("\"", 765)
            
            #line 11 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create("", 701), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent(section.AreaName, section.ImageVirtualPath)
            
            #line default
            #line hidden
, 701), false)
);

WriteAttribute("alt", Tuple.Create("\r\n                         alt=\"", 766), Tuple.Create("\"", 835)
            
            #line 12 "..\..\Views\Shared\_Sidebar.cshtml"
, Tuple.Create(Tuple.Create("", 798), Tuple.Create<System.Object, System.Int32>(Html.Translate(section.Name)
            
            #line default
            #line hidden
, 798), false)
, Tuple.Create(Tuple.Create(" ", 829), Tuple.Create("Image", 830), true)
);

WriteLiteral(" />\r\n                </a>\r\n            </li>\r\n");

            
            #line 15 "..\..\Views\Shared\_Sidebar.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </ul>\r\n    <div");

WriteLiteral(" id=\"after_sidebar\"");

WriteLiteral("></div>\r\n</nav>\r\n<div");

WriteLiteral(" id=\"sidebar_hidescroll\"");

WriteLiteral(" class=\"dt-only\"");

WriteLiteral("></div>");

        }
    }
}
#pragma warning restore 1591
