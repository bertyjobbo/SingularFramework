using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Singular.Core.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class TranslateController: CoreControllerBase
    {
        public TranslateController(ISingularContext ctx, IMvcSectionManager sectionService)
            : base(ctx, sectionService)
        {
        }

        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }
    }
}