using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class BuildController : CoreControllerBaseNoAuth
    {
        private readonly IEmbeddedResourceManager _resMgr;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="sectionManager"></param>
        /// <param name="siteContext"></param>
        public BuildController(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext)
            : base(ctx, sectionManager, siteContext)
        {
#if DEBUG
            _resMgr = ctx.GetService<IEmbeddedResourceManager>();
#else
            throw new Exception("");
#endif
            
        }

        /// <summary>
        ///     Build site
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
            return Content(msg);
        }

        /// <summary>
        ///     Success message
        /// </summary>
        /// <returns></returns>
        public ContentResult Success()
        {
            return base.Content("Success");
        }
    }
}