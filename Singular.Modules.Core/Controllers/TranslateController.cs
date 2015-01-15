using System.Web.Mvc;
using Singular.Core.Context;

namespace Singular.Modules.Core.Controllers
{
    public class TranslateController : CoreControllerBase
    {
        public TranslateController(ISingularContext ctx) : base(ctx)
        {
        }

        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }
    }
}