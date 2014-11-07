using Singular.Core.Context;
using System;
using System.Web.Mvc;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class HomeController : CoreControllerBase
    {
        public HomeController(ISingularContext ctx, IMvcSectionManager sectionService)
            : base(ctx,  sectionService)
        {
        }

        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }
    }
}