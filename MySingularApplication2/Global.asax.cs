using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Singular.Web.Mvc.Application;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace MySingularApplication2
{
    public class Global : SingularMvcApplication
    {
        protected override void SingularApplicationStart(object sender, EventArgs eventArgs)
        {
            // set embed path
            EmbeddedResourceManager
                .Current
                .SetMsBuildPath(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe")
                .SetSolutionPath(@"C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\Singular.sln")
                .SetRazorGeneratorPsScriptPath(@"C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\packages\RazorGenerator.Mvc.2.2.3\tools\Init.ps1");

            // override views etc
            SiteContext
                .Current
                .OverrideBrandView("~/Views/Shared/_MyBrand.cshtml")
                .OverrideHomepageView("~/Views/Shared/_MyHomepage.cshtml")
                .SetSiteName("My Application");

            // base go!
            base.SingularApplicationStart(sender, eventArgs);
        }
    }
}