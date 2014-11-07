using System;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace MySingularApplication
{
    public class Global : Singular.Web.Mvc.Application.SingularMvcApplication
    {
        protected override void SingularApplicationStart(object sender, EventArgs eventArgs)
        {
            // quick overrides
            EmbeddedResourceManager
                .Current
                .SetMsBuildPath(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe")
                .SetSolutionPath(@"C:\Git\SingularFramework\Singular.sln")
                .SetRazorGeneratorPsScriptPath(@"C:\Git\SingularFramework\packages\RazorGenerator.Mvc.2.2.3\tools\Init.ps1");

            // base go!
            base.SingularApplicationStart(sender, eventArgs);
        }
    }
}