using Castle.MicroKernel.Registration;
using Singular.Core.Configuration;
using System;
using System.Web.Mvc;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace Singular.Modules.Core.Configuration
{
	public class ModuleConfiguration : AreaRegistration, ISingularModuleConfiguration
	{
		public override string AreaName
		{
			get
			{
				return "Core";
			}
		}

		public IRegistration[] ServiceRegistrations
		{
			get
			{
			    return new IRegistration[]
			    {
			    };
			}
		}

	    public void OnAppStart()
	    {
	        EmbeddedResourceManager
                .Current
                .CreateCollection(this, "Singular.Modules.Core", "Core")
	            .SetRouteFolder(@"C:\Git\SingularFramework\Singular.Modules.Core") // replace with appSetting ?
                .SetProjectFileName("Singular.Modules.Core.csproj")
	            //.AddFolder("~/Views")
                //.AddFolder("~/Views/Home")
                //.AddFolder("~/Views/Shared")
	            .AddFile("~/Content/Css/Singular-Dashboard.min.css")
	            .AddFolder("~/Content/Images/Dummy");
	    }


	    public override void RegisterArea(AreaRegistrationContext context)
		{
			var routeObj = new { controller = "Home", action = "Index", id = UrlParameter.Optional };
			var strArrays = new string[] { "Singular.Modules.Core.Controllers" };
			context.MapRoute("Core", "Singular/Core/{controller}/{action}/{id}", routeObj, strArrays);
		}
	}
}