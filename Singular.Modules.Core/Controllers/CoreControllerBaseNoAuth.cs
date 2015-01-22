using System.Web;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Modules.Core.ViewModels;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public abstract class CoreControllerBaseNoAuth : Controller
    {
        protected CoreControllerBaseNoAuth(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext)
        {
            SingularContext = ctx;
            SectionManager = sectionManager;
            SiteContext = siteContext;
        }

        public ISingularContext SingularContext { get; private set; }
        protected ISectionManager SectionManager { get; private set; }
        public ISiteContext SiteContext { get; private set; }

        protected CoreViewModelBase GetCoreModelBaseInstance()
        {
            //Thread.Sleep(2000);
            return new CoreViewModelBase(SingularContext,SectionManager,SiteContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Request.IsAuthenticated && !SingularContext.IsAuthenticated)
            {
                if (Request.Url != null)
                    Response.Redirect(
                        Url.Content(
                            "~/Singular/Core/FormsAuth/AddUserToContext/?returnUrl=" + 
                            HttpUtility.UrlEncode(Request.Url.ToString())
                        ),
                        true
                    );
                return;
            }

            base.OnActionExecuted(filterContext);
        }
    }
}