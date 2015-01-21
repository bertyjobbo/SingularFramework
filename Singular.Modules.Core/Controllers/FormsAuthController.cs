using System.Web.Mvc;
using System.Web.Security;
using Singular.Core.Context;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    /// <summary>
    ///     Forms auth controller
    /// </summary>
    public class FormsAuthController : CoreControllerBaseNoAuth
    {
        public FormsAuthController(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext)
            : base(ctx, sectionManager, siteContext)
        {
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
            SingularContext.RemoveCurrentUser();
            return Redirect("~/Singular/Core/FormsAuth/#/Login");
        }
    }
}