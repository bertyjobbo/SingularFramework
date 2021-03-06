﻿using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class UsersController : CoreControllerBase
    {
        public UsersController(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext)
            : base(ctx, sectionManager, siteContext)
        {
        }

        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }
    }
}