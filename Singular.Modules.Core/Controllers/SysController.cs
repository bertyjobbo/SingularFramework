﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Singular.Core.Context;

namespace Singular.Modules.Core.Controllers
{
    public class SysController:CoreControllerBaseNoAuth
    {
        public SysController(ISingularContext ctx) : base(ctx)
        {

        }

        public ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View(GetCoreModelBaseInstance());
        }
    }
}