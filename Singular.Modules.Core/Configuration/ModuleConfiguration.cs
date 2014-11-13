using System.Web;
using Castle.MicroKernel.Registration;
using Singular.Core.Configuration;
using System;
using System.Web.Mvc;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

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
                .AddFolder("~/Content/Css")
                .AddFolder("~/Content/Bootstrap/css")
                .AddFolder("~/Content/Bootstrap/fonts")
                .AddFolder("~/Content/Images")
                .AddFolder("~/Content/Ng")
                .AddFolder("~/Content/Ng/Common")
                .AddFolder("~/Content/Ng/Modules")
                .AddFolder("~/Scripts")
                .AddFolder("~/Scripts/angularui");

            MvcSectionManager.Current
                .AddSection(new MvcSection
                {
                    Order = -10000,
                    Name = "Tree",
                    Description = "Content tree for storing data, pages etc",
                    Action = "Index",
                    AreaName = "Core",
                    Controller = "Tree",
                    ImageVirtualPath = "~/Content/Images/Tree.png",
                    RouteValues = new { area = "Core" },
                    IsActive = () => HttpContext.Current != null &&
                                     HttpContext.Current.Request.Url.ToString().ToLower().Contains("/singular/core/tree")
                })
                .AddSection(new MvcSection
                {
                    Order = -9500,
                    Name = "UserManagement",
                    Description = "User management - users, groups, permissions",
                    Action = "Index",
                    AreaName = "Core",
                    Controller = "Users",
                    ImageVirtualPath = "~/Content/Images/User.png",
                    RouteValues = new { area = "Core" },
                    IsActive = () => HttpContext.Current != null &&
                                     HttpContext.Current.Request.Url.ToString().ToLower().Contains("/singular/core/users")
                });
        }


        public override void RegisterArea(AreaRegistrationContext context)
        {
            var routeObj = new { controller = "Home", action = "Index", id = UrlParameter.Optional };
            var strArrays = new [] { "Singular.Modules.Core.Controllers" };
            context.MapRoute("Core", "Singular/Core/{controller}/{action}/{id}", routeObj, strArrays);
        }
    }
}