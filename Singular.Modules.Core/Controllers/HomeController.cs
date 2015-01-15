using System.Web.Mvc;
using Singular.Core.Context;

namespace Singular.Modules.Core.Controllers
{
    public class HomeController : CoreControllerBase
    {
        public HomeController(ISingularContext ctx) : base(ctx)
        {
        }

        public ActionResult Index()
        {
            return View(SiteContext.HomepageViewPath, GetCoreModelBaseInstance());
        }
    }
}