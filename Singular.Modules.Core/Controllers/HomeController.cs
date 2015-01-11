using Singular.Core.Context;
using System;
using System.Web.Mvc;
using Singular.Modules.Core.Data.Services;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class HomeController : CoreControllerBase
    {
        public HomeController(ISingularContext ctx):base(ctx)
        {
        }

        public ActionResult Index()
        {
            return View(SiteContext.HomepageViewPath, GetCoreModelBaseInstance());
        }
    }
}