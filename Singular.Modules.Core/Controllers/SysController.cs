using System.Web.Mvc;
using Singular.Core.Context;

namespace Singular.Modules.Core.Controllers
{
    public class SysController : CoreControllerBaseNoAuth
    {
        public SysController(ISingularContext ctx) : base(ctx)
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