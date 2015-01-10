using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Singular.Core.Configuration;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace AltNg.SingularConfig
{
    public class ModuleConfiguration : AreaRegistration, ISingularModuleConfiguration
    {
        public override string AreaName
        {
            get { return "AltNg"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            
        }


        public void OnAppStart()
        {
            EmbeddedResourceManager
                .Current
                .CreateCollection(this, "AltNg", "AltNg")
                .SetRouteFolder(@"C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\AltNg") // replace with appSetting ?
                .SetProjectFileName("AltNg.csproj")
                .AddFolder("~/alt");
        }
    }
}