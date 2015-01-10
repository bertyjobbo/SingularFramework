﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class UsersController: CoreControllerBase
    {
        public UsersController(ISingularContext ctx, ISectionManager sectionService, ISiteContext siteContext) : base(ctx, sectionService, siteContext)
        {

        }

        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }
    }
}