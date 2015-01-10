using System;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace MySingularApplication
{
    public class Global : Singular.Web.Mvc.Application.SingularMvcApplication
    {
        protected override void SingularApplicationStart(object sender, EventArgs eventArgs)
        {
            // set embed path
            EmbeddedResourceManager
                .Current
                .SetMsBuildPath(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe")
                .SetSolutionPath(@"C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\Singular.sln")
                .SetRazorGeneratorPsScriptPath(@"C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\packages\RazorGenerator.Mvc.2.2.3\tools\Init.ps1");

            // override

            // base go!
            base.SingularApplicationStart(sender, eventArgs);
        }
    }
}