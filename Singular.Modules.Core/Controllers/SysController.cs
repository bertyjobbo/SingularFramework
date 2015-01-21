using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class SysController : CoreControllerBaseNoAuth
    {
        public SysController(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext)
            : base(ctx,sectionManager,siteContext)
        {
        }

        public ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View(GetCoreModelBaseInstance());
        }

        public ActionResult AccessDenied()
        {
            Response.StatusCode = 403;
            return View(GetCoreModelBaseInstance());
        }

        public ActionResult Error()
        {
            Response.StatusCode = 500;
            return View(GetCoreModelBaseInstance());
        }
    }
}