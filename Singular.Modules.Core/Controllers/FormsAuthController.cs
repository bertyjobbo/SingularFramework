using System.Web.Mvc;
using Singular.Core.Context;

namespace Singular.Modules.Core.Controllers
{
    /// <summary>
    ///     Forms auth controller
    /// </summary>
    public class FormsAuthController : CoreControllerBaseNoAuth
    {
        public FormsAuthController(ISingularContext ctx) : base(ctx)
        {
        }

        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }
    }
}