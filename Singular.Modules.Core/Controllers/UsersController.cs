using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Singular.Core.Context;
using System;
using System.Web.Mvc;
using Singular.Modules.Core.AspNetAuth;
using Singular.Modules.Core.AspNetAuth.Models;
using Singular.Modules.Core.ViewModels;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class UsersController : CoreControllerBase
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UsersController(ISingularContext ctx, ISectionManager sectionService, ISiteContext siteContext)
            : base(ctx, sectionService, siteContext)
        {
        }

        public ActionResult Index()
        {
            return View(GetCoreModelBaseInstance());
        }



        /* ASP.NET AUTH STUFF */

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            //private set
            //{
            //    _signInManager = value;
            //}
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            //private set
            //{
            //    _userManager = value;
            //}
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new SingularLoginViewModel(SingularContext, SectionManager, SiteContext));
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(SingularLoginViewModel model, string returnUrl)
        {
            model.Reconstruct(SingularContext, SectionManager, SiteContext);
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return redirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        private ActionResult redirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}