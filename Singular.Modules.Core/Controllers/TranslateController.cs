using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Modules.Core.Data.Services;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class TranslateController: CoreControllerBase
    {
        public TranslateController(ISingularContext ctx): base(ctx)
        {
        }

        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }
    }
}