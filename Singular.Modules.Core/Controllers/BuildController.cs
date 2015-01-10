using Singular.Core.Context;
using System.Web.Mvc;
using Singular.Modules.Core.Data.Services;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class BuildController : CoreControllerBaseNoAuth
    {
        private readonly IEmbeddedResourceManager _resMgr;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="sectionService"></param>
        /// <param name="siteContext"></param>
        /// <param name="mgr"></param>
        /// <param name="translationService"></param>
        public BuildController(ISingularContext ctx, ISectionManager sectionService, ISiteContext siteContext, IEmbeddedResourceManager mgr, ITranslationService translationService)
            : base(ctx, sectionService, siteContext, translationService)
        {
            _resMgr = mgr;
        }

        /// <summary>
        /// Build site
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string msg = null;
            if (_resMgr.BuildResources(ref msg))
            {
                Response.Redirect(Url.Content("~/Singular/Core/Build/Success"));
                return null;
            }
            else
            {
                return Content(msg);
            }
        }

        /// <summary>
        /// Success message
        /// </summary>
        /// <returns></returns>
        public ContentResult Success()
        {
            return base.Content("Success");
        }
    }
}