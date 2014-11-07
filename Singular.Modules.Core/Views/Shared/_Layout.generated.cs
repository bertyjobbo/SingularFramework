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
    
    #line 1 "..\..\Views\Shared\_Layout.cshtml"
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
            
            #line 3 "..\..\Views\Shared\_Layout.cshtml"
  
    Layout = null;

            
            #line default
            #line hidden
WriteLiteral("\r\n<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width\"");

WriteLiteral(" />\r\n    <title>_Layout</title>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteAttribute("href", Tuple.Create(" href=\"", 303), Tuple.Create("\"", 408)
            
            #line 11 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 310), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent("Core","~/Content/Css/Singular-Dashboard.css", IncludeBehaviour.MinifyAlways)
            
            #line default
            #line hidden
, 310), false)
);

WriteLiteral(" />\r\n</head>\r\n<body>\r\n    <header>\r\n        <div");

WriteLiteral(" id=\"header_inner\"");

WriteLiteral(">\r\n            <h1>Singular</h1>\r\n        </div>\r\n    </header>\r\n    <section");

WriteLiteral(" id=\"sec_top\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"sec_top_inner\"");

WriteLiteral(">\r\n            <p>&nbsp;</p>\r\n        </div>\r\n    </section>\r\n    <section");

WriteLiteral(" id=\"sec_sidebar\"");

WriteLiteral(">\r\n        <ul");

WriteLiteral(" role=\"navigation\"");

WriteLiteral(">\r\n");

            
            #line 26 "..\..\Views\Shared\_Layout.cshtml"
            
            
            #line default
            #line hidden
            
            #line 26 "..\..\Views\Shared\_Layout.cshtml"
             foreach (var section in Model.Sections)
            {
                if (section.IsActive.Invoke())
                {

            
            #line default
            #line hidden
WriteLiteral("                    <li");

WriteLiteral(" class=\"sec_sidebar-active\"");

WriteLiteral(">\r\n                        <a");

WriteAttribute("title", Tuple.Create(" title=\"", 940), Tuple.Create("\"", 988)
            
            #line 31 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 948), Tuple.Create<System.Object, System.Int32>(section.Name
            
            #line default
            #line hidden
, 948), false)
, Tuple.Create(Tuple.Create(" ", 963), Tuple.Create("|", 964), true)
            
            #line 31 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create(" ", 965), Tuple.Create<System.Object, System.Int32>(section.Description
            
            #line default
            #line hidden
, 966), false)
);

WriteAttribute("href", Tuple.Create("\r\n                           href=\"", 989), Tuple.Create("\"", 1092)
            
            #line 32 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 1024), Tuple.Create<System.Object, System.Int32>(Url.Action(section.Action, section.Controller, section.RouteValues)
            
            #line default
            #line hidden
, 1024), false)
);

WriteLiteral(">\r\n                            <img");

WriteAttribute("src", Tuple.Create(" src=\"", 1128), Tuple.Create("\"", 1198)
            
            #line 33 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 1134), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent(section.AreaName, section.ImageVirtualPath)
            
            #line default
            #line hidden
, 1134), false)
);

WriteAttribute("alt", Tuple.Create("\r\n                                 alt=\"", 1199), Tuple.Create("\"", 1260)
            
            #line 34 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 1239), Tuple.Create<System.Object, System.Int32>(section.Name
            
            #line default
            #line hidden
, 1239), false)
, Tuple.Create(Tuple.Create(" ", 1254), Tuple.Create("Image", 1255), true)
);

WriteLiteral(" />\r\n                        </a>\r\n                    </li>\r\n");

            
            #line 37 "..\..\Views\Shared\_Layout.cshtml"
                }
                else
                {

            
            #line default
            #line hidden
WriteLiteral("                    <li>\r\n                        <a");

WriteAttribute("title", Tuple.Create(" title=\"", 1435), Tuple.Create("\"", 1483)
            
            #line 41 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 1443), Tuple.Create<System.Object, System.Int32>(section.Name
            
            #line default
            #line hidden
, 1443), false)
, Tuple.Create(Tuple.Create(" ", 1458), Tuple.Create("|", 1459), true)
            
            #line 41 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create(" ", 1460), Tuple.Create<System.Object, System.Int32>(section.Description
            
            #line default
            #line hidden
, 1461), false)
);

WriteAttribute("href", Tuple.Create("\r\n                           href=\"", 1484), Tuple.Create("\"", 1587)
            
            #line 42 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 1519), Tuple.Create<System.Object, System.Int32>(Url.Action(section.Action, section.Controller, section.RouteValues)
            
            #line default
            #line hidden
, 1519), false)
);

WriteLiteral(">\r\n                            <img");

WriteAttribute("src", Tuple.Create(" src=\"", 1623), Tuple.Create("\"", 1693)
            
            #line 43 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 1629), Tuple.Create<System.Object, System.Int32>(Url.EmbeddedContent(section.AreaName, section.ImageVirtualPath)
            
            #line default
            #line hidden
, 1629), false)
);

WriteAttribute("alt", Tuple.Create("\r\n                                 alt=\"", 1694), Tuple.Create("\"", 1755)
            
            #line 44 "..\..\Views\Shared\_Layout.cshtml"
, Tuple.Create(Tuple.Create("", 1734), Tuple.Create<System.Object, System.Int32>(section.Name
            
            #line default
            #line hidden
, 1734), false)
, Tuple.Create(Tuple.Create(" ", 1749), Tuple.Create("Image", 1750), true)
);

WriteLiteral(" />\r\n                        </a>\r\n                    </li>\r\n");

            
            #line 47 "..\..\Views\Shared\_Layout.cshtml"
                }

            }

            
            #line default
            #line hidden
WriteLiteral("        </ul>\r\n        <p");

WriteLiteral(" id=\"p_copyright\"");

WriteLiteral(">&copy;2014 Rob Johnson</p>\r\n    </section>\r\n    <span");

WriteLiteral(" id=\"spn_hide_scroll\"");

WriteLiteral("></span>\r\n    <section");

WriteLiteral(" id=\"sec_dashboard\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"sec_dashboard_inner\"");

WriteLiteral(">\r\n\r\n");

WriteLiteral("            ");

            
            #line 57 "..\..\Views\Shared\_Layout.cshtml"
       Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cra" +
"s ac commodo eros, quis semper est. Sed suscipit justo enim, ac placerat arcu ve" +
"nenatis sit amet. Sed luctus faucibus tortor, vel tempor tellus porta vel. Vesti" +
"bulum ornare id metus at porta. Praesent ultricies purus id erat tincidunt, eu p" +
"ulvinar dolor pellentesque. Sed ante purus, fringilla ultrices eros quis, bibend" +
"um auctor felis. Suspendisse auctor arcu eu vehicula maximus. Praesent gravida e" +
"uismod nulla, eget convallis est. Vivamus eget sollicitudin nisi, at sagittis ma" +
"uris. Nam tempus non erat non feugiat. Cras sed felis ut lacus pulvinar rutrum. " +
"Cras mauris erat, feugiat et convallis semper, ultrices et tortor. Duis eget pur" +
"us eleifend, condimentum lorem a, lacinia metus. Sed pulvinar orci nisl, eget sc" +
"elerisque ipsum dignissim sed. Fusce consequat commodo erat, venenatis ornare or" +
"ci volutpat id.</p>\r\n\r\n            <p>Etiam consectetur id mauris mattis tincidu" +
"nt. Praesent aliquet a justo ut dignissim. Donec sit amet orci hendrerit orci ac" +
"cumsan feugiat. Proin ante massa, mollis sed tempor finibus, mattis cursus lectu" +
"s. Duis diam urna, commodo et pellentesque vel, viverra nec ex. Phasellus eu ris" +
"us ut enim rutrum mollis nec ac est. Suspendisse metus leo, imperdiet in quam in" +
", mollis posuere tortor. Fusce auctor massa eget mollis auctor. Nullam in magna " +
"nunc. Vivamus vehicula pretium ligula, vitae maximus est hendrerit vitae. Sed di" +
"ctum at nisi id ultrices. Sed ultricies nunc vel urna tempor, sed varius nisi au" +
"ctor. Phasellus bibendum sollicitudin elit in scelerisque. Ut finibus iaculis pu" +
"rus, sit amet varius lectus. Mauris posuere turpis eleifend felis vehicula, in e" +
"gestas turpis rhoncus.</p>\r\n\r\n            <p>Sed volutpat nisi ac cursus laoreet" +
". Duis at lobortis felis, sed egestas lacus. Proin vel nibh ut erat molestie tin" +
"cidunt. Ut et velit quam. Sed vitae lectus non libero condimentum imperdiet a a " +
"ipsum. Nam ac lobortis nibh. Quisque sit amet risus finibus, convallis est quis," +
" tempor nisi. Fusce a sapien sed ipsum ullamcorper porttitor. Praesent eu tortor" +
" dignissim, molestie sem quis, porttitor dolor. Nunc lobortis orci non nunc matt" +
"is, et maximus erat dapibus. In luctus erat libero, quis cursus sem feugiat id. " +
"Donec ut tellus ante. Nulla mollis sem at ultricies aliquam. Donec sollicitudin " +
"leo ac volutpat finibus. Morbi mollis fermentum mauris in sodales. Fusce id volu" +
"tpat arcu, in feugiat nisi.</p>\r\n\r\n            <p>Nunc elit augue, vehicula quis" +
" varius vel, cursus et quam. Etiam suscipit pretium est a lobortis. Integer a se" +
"m lorem. Phasellus commodo sed metus ut dignissim. Aliquam placerat orci erat, n" +
"ec posuere nulla varius ut. Pellentesque cursus faucibus tellus a bibendum. Viva" +
"mus blandit pulvinar semper. Mauris pretium ultricies purus vel ullamcorper.</p>" +
"\r\n\r\n            <p>Curabitur eget iaculis erat. Pellentesque faucibus semper lor" +
"em, sed placerat augue ultrices a. Sed quis lacinia diam. Donec eget sapien moll" +
"is, ornare arcu nec, suscipit augue. Quisque consequat tempus urna, sit amet com" +
"modo augue mollis sed. Ut vehicula dolor ante, eget volutpat lacus feugiat in. F" +
"usce gravida sollicitudin eros vel bibendum.</p>\r\n\r\n            <p>Nullam at fer" +
"mentum lacus. Fusce ut lacinia leo, id molestie ligula. Donec tempus ipsum ac ve" +
"nenatis tristique. Vestibulum laoreet ligula elit, a condimentum ex ultrices non" +
". Duis vehicula diam non lorem bibendum, in luctus dui imperdiet. Proin iaculis " +
"pretium ante, et placerat arcu. Cras vehicula rutrum diam, eget pellentesque lib" +
"ero rhoncus nec. Mauris vestibulum urna mi, id mollis nulla accumsan nec. Phasel" +
"lus sodales lorem eu mauris eleifend, a aliquam ex dignissim.</p>\r\n\r\n           " +
" <p>Duis ligula purus, scelerisque vitae mollis facilisis, malesuada varius risu" +
"s. Nulla placerat efficitur ligula et sollicitudin. Vestibulum vel turpis et mi " +
"pharetra scelerisque in eu lectus. Cras ac dictum ex, nec faucibus nisl. Praesen" +
"t imperdiet enim neque, volutpat dapibus elit dictum et. Integer a ornare orci, " +
"in molestie massa. Duis condimentum cursus eros a posuere. Mauris venenatis ligu" +
"la at lorem commodo euismod. Integer eget lacinia metus. Integer et pulvinar arc" +
"u.</p>\r\n\r\n            <p>Proin viverra viverra metus, vel molestie ante efficitu" +
"r vel. Nulla laoreet est id felis dignissim, in porttitor odio molestie. Proin v" +
"estibulum placerat tellus, vitae fringilla leo pretium ac. In consectetur, turpi" +
"s quis scelerisque auctor, massa tortor ullamcorper ipsum, et placerat justo orc" +
"i quis lorem. Nunc tellus diam, fermentum a tristique sit amet, dictum eu augue." +
" Etiam aliquam egestas eros, ut porta purus blandit eu. Curabitur id enim mattis" +
", ultrices augue non, aliquet quam. Phasellus nec tempor nisi. Sed bibendum laci" +
"nia lacus vel varius. Maecenas condimentum, ligula quis pulvinar efficitur, lect" +
"us diam auctor neque, at ornare justo enim ac arcu. Duis non aliquet dui. Etiam " +
"vel mi sed massa maximus hendrerit. Sed egestas nisl a diam condimentum, in moll" +
"is felis venenatis.</p>\r\n\r\n            <p>Quisque porta, arcu sed laoreet bibend" +
"um, risus mi faucibus erat, ac congue tellus nibh in sem. Maecenas id vehicula j" +
"usto. Phasellus non lobortis risus, in tristique leo. In mi nibh, laoreet at gra" +
"vida elementum, gravida pharetra risus. Proin urna lectus, consectetur ac porta " +
"ac, varius nec sem. Curabitur aliquet nibh ac euismod molestie. Morbi quis conse" +
"ctetur libero. Vivamus interdum enim vel orci varius, eget eleifend enim tristiq" +
"ue. Mauris id erat odio. Proin nec iaculis metus. Integer sodales eros efficitur" +
" ipsum scelerisque scelerisque.</p>\r\n\r\n            <p>Vestibulum porttitor leo n" +
"isi, et egestas neque suscipit ac. Nam faucibus placerat rhoncus. Fusce tempor n" +
"isi at neque sollicitudin, eu malesuada neque porttitor. Quisque fringilla conse" +
"ctetur condimentum. Proin fringilla egestas justo sed porttitor. Cras sed velit " +
"sem. Morbi sollicitudin nunc id pretium semper. Nullam aliquet ultrices massa a " +
"consequat. Quisque tempus cursus porta. Nullam et porta neque. Pellentesque a ia" +
"culis arcu. In nec justo non ante eleifend convallis pharetra ac ante. Duis dict" +
"um venenatis nulla nec euismod. In iaculis consectetur elementum.</p>\r\n        <" +
"/div>\r\n    </section>\r\n</body>\r\n</html>\r\n\r\n");

        }
    }
}
#pragma warning restore 1591