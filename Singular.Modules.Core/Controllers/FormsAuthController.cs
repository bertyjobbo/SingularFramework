﻿using System.Web.Mvc;
using System.Web.Security;
using Singular.Core.Context;
using Singular.Modules.Core.Data.Service;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    /// <summary>
    ///     Forms auth controller
    /// </summary>
    public class FormsAuthController : CoreControllerBaseNoAuth
    {
        /// <summary>
        /// Auth service
        /// </summary>
        private IAuthenticationService _authService;

        public FormsAuthController(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext, IAuthenticationService authenticationService)
            : base(ctx, sectionManager, siteContext)
        {
            _authService = authenticationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            SingularContext.DestroySingularSession();
            return Redirect("~/Singular/Core/FormsAuth/#/Login");
        }
    }
}