using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Singular.Core.Context;
using Singular.Modules.Core.Data.Services;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class TreeController: CoreControllerBase
    {
        public TreeController(ISingularContext ctx)
            : base(ctx)
        {
        }

        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }
    }
}