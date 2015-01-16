using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Singular.Core.Configuration;
using Singular.Modules.Core.Data.Services;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Ioc;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Configuration
{
    public class ModuleConfiguration : AreaRegistration, ISingularModuleConfiguration
    {
        public override string AreaName
        {
            get { return "Core"; }
        }

        public void OnAppStart()
        {
            MvcIocManager.Current.AddServices(new IRegistration[]
            {
                Component
                    .For<ITranslationService>()
                    .ImplementedBy<TranslationService>()
                    .LifestylePerWebRequest(),
                Component
                    .For<IAuthenticationService>()
                    .ImplementedBy<AuthenticationService>()
                    .LifestylePerWebRequest()
            });

            EmbeddedResourceManager
                .Current
                .CreateCollection(this, "Singular.Modules.Core", "Core")
                .SetRouteFolder(@"C:\ffd248e91f0c43bbbe5921e4\Git\SingularFramework\Singular.Modules.Core")
                // replace with appSetting ?
                .SetProjectFileName("Singular.Modules.Core.csproj")
                .AddFolder("~/Content/Css")
                .AddFolder("~/Content/Scripts")
                .AddFolder("~/Content/Bootstrap/css")
                .AddFolder("~/Content/Bootstrap/fonts")
                .AddFolder("~/Content/Images")
                .AddFolder("~/Content/Ng")
                .AddFolder("~/Content/Ng/Common")
                .AddFolder("~/Content/Ng/Modules")
                .AddFolder("~/Scripts")
                .AddFolder("~/Scripts/angularui");

            SectionManager.Current
                .AddSection(new Section
                {
                    Order = -10000,
                    Name = "Tree",
                    Description = "Content tree for storing data, pages etc",
                    Action = "Index",
                    AreaName = "Core",
                    Controller = "Tree",
                    ImageVirtualPath = "~/Content/Images/Tree.png",
                    RouteValues = new {area = "Core"},
                    IsActive = () => HttpContext.Current != null &&
                                     HttpContext.Current.Request.Url.ToString()
                                         .ToLower()
                                         .Contains("/singular/core/tree")
                })
                .AddSection(new Section
                {
                    Order = -9500,
                    Name = "User Management",
                    Description = "User management - users, groups, permissions",
                    Action = "Index",
                    AreaName = "Core",
                    Controller = "Users",
                    ImageVirtualPath = "~/Content/Images/User.png",
                    RouteValues = new {area = "Core"},
                    IsActive = () => HttpContext.Current != null &&
                                     HttpContext.Current.Request.Url.ToString()
                                         .ToLower()
                                         .Contains("/singular/core/users")
                })
                .AddSection(new Section
                {
                    Order = -9000,
                    Name = "Translate",
                    Description = "Multilingual text management",
                    Action = "Index",
                    AreaName = "Core",
                    Controller = "Translate",
                    ImageVirtualPath = "~/Content/Images/Translate.png",
                    RouteValues = new {area = "Core"},
                    IsActive = () => HttpContext.Current != null &&
                                     HttpContext.Current.Request.Url.ToString()
                                         .ToLower()
                                         .Contains("/singular/core/translate")
                });
        }


        public override void RegisterArea(AreaRegistrationContext context)
        {
            var routeObj = new {controller = "Home", action = "Index", id = UrlParameter.Optional};
            var strArrays = new[] {"Singular.Modules.Core.Controllers", "Singular.Modules.Core.ApiControllers"};
            context.MapRoute("Core", "Singular/Core/{controller}/{action}/{id}", routeObj, strArrays);
        }
    }
}