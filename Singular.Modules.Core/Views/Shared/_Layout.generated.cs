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
    
    #line 1 "..\..\Views\Shared\_Layout.cshtml"
    using System.Web.Mvc.Html;
    
    #line default
    #line hidden
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    
    #line 2 "..\..\Views\Shared\_Layout.cshtml"
    using System.Web.UI.WebControls;
    
    #line default
    #line hidden
    using System.Web.WebPages;
    
    #line 3 "..\..\Views\Shared\_Layout.cshtml"
    using Singular.Web.Mvc.HtmlExtensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_Layout.cshtml")]
    public partial class Layout : System.Web.Mvc.WebViewPage<Singular.Modules.Core.ViewModels.CoreViewModelBase>
    {
        public Layout()
        {
        }
        public override void Execute()
        {
            
            #line 5 "..\..\Views\Shared\_Layout.cshtml"
  
    Layout = null;

            
            #line default
            #line hidden
WriteLiteral("<!DOCTYPE html>\r\n<html");

WriteLiteral(" data-ng-app=\"");

            
            #line 8 "..\..\Views\Shared\_Layout.cshtml"
              Write(ViewBag.NgAppName);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(">\r\n<head>\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width\"");

WriteLiteral(" />\r\n    <title>Singular | ");

            
            #line 11 "..\..\Views\Shared\_Layout.cshtml"
                  Write(ViewBag.Title ?? "[Set ViewBage.Title]");

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteAttribute("href", Tuple.Create(" href=\"", 442), Tuple.Create("\"", 554)
            
            #line 12 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 449), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("Core","~/Content/Bootstrap/css/bootstrap.css", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 449), false)
);

WriteLiteral(" />\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteAttribute("href", Tuple.Create(" href=\"", 586), Tuple.Create("\"", 692)
            
            #line 13 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 593), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("Core","~/Content/Css/Singular.Core.css", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 593), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("    ");

            
            #line 14 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderSection("styles", false));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    ");

WriteLiteral("\r\n    <style>\r\n\r\n        .collapse.in {\r\n            height: ");

            
            #line 22 "..\..\Views\Shared\_Layout.cshtml"
                Write((Model.Sections.Count * 60) + 100);

            
            #line default
            #line hidden
WriteLiteral("px;\r\n            overflow: hidden;\r\n        }\r\n        ");

WriteLiteral("@font-face {\r\n            font-family: \'Glyphicons Halflings\';\r\n            src: " +
"url(\'");

            
            #line 27 "..\..\Views\Shared\_Layout.cshtml"
                 Write(Url.EmbeddedContent("Core", "~/Content/Bootstrap/fonts/glyphicons-halflings-regular.eot"));

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            src: url(\'");

            
            #line 28 "..\..\Views\Shared\_Layout.cshtml"
                 Write(Url.EmbeddedContent("Core", "~/Content/Bootstrap/fonts/glyphicons-halflings-regular.eot"));

            
            #line default
            #line hidden
WriteLiteral("?#iefix\') format(\'embedded-opentype\'), url(\'");

            
            #line 28 "..\..\Views\Shared\_Layout.cshtml"
                                                                                                                                                       Write(Url.EmbeddedContent("Core", "~/Content/Bootstrap/fonts/glyphicons-halflings-regular.woff"));

            
            #line default
            #line hidden
WriteLiteral("\') format(\'woff\'), url(\'");

            
            #line 28 "..\..\Views\Shared\_Layout.cshtml"
                                                                                                                                                                                                                                                                          Write(Url.EmbeddedContent("Core", "~/Content/Bootstrap/fonts/glyphicons-halflings-regular.ttf"));

            
            #line default
            #line hidden
WriteLiteral("\') format(\'truetype\'), url(\'");

            
            #line 28 "..\..\Views\Shared\_Layout.cshtml"
                                                                                                                                                                                                                                                                                                                                                                                                Write(Url.EmbeddedContent("Core", "~/Content/Bootstrap/fonts/glyphicons-halflings-regular.svg"));

            
            #line default
            #line hidden
WriteLiteral("#glyphicons_halflingsregular\') format(\'svg\');\r\n        }\r\n    </style>\r\n    \r\n\r\n<" +
"/head>\r\n<body");

WriteLiteral(" data-ng-controller=\"SingularMasterAppController\"");

WriteLiteral(">\r\n    <header>\r\n");

WriteLiteral("        ");

            
            #line 36 "..\..\Views\Shared\_Layout.cshtml"
   Write(Html.Partial("_Nav"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </header>\r\n    <div");

WriteLiteral(" class=\"contains-floats\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 39 "..\..\Views\Shared\_Layout.cshtml"
   Write(Html.Partial("_Sidebar"));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <section");

WriteLiteral(" id=\"dashboard\"");

WriteLiteral(">");

            
            #line 40 "..\..\Views\Shared\_Layout.cshtml"
                           Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("</section>\r\n    </div>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1945), Tuple.Create("\"", 2039)
            
            #line 42 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 1951), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("Core","~/Scripts/angular.js", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 1951), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2063), Tuple.Create("\"", 2165)
            
            #line 43 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 2069), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("Core","~/Scripts/angular-animate.js", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 2069), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2189), Tuple.Create("\"", 2304)
            
            #line 44 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 2195), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("Core", "~/Scripts/angularui/ui-bootstrap-tpls.js", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 2195), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2328), Tuple.Create("\"", 2421)
            
            #line 45 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 2334), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("AltNg","~/alt/alt.route.js", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 2334), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2445), Tuple.Create("\"", 2539)
            
            #line 46 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 2451), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("AltNg","~/alt/alt.repeat.js", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 2451), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2563), Tuple.Create("\"", 2653)
            
            #line 47 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 2569), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("AltNg","~/alt/alt.ui.js", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 2569), false)
);

WriteLiteral("></script>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 2677), Tuple.Create("\"", 2791)
            
            #line 48 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 2683), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("Core","~/Content/Ng/Common/SingularMasterApp.js", IncludeBehaviour.MinifyWhenNotDebug)
            
            #line default
            #line hidden
, 2683), false)
);

WriteLiteral("></script>\r\n");

WriteLiteral("    ");

            
            #line 49 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderSection("scripts", false));

            
            #line default
            #line hidden
WriteLiteral("\r\n</body>\r\n</html>\r\n");

        }
    }
}
#pragma warning restore 1591
