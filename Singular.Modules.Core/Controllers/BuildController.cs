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
        public BuildController(ISingularContext ctx)
            : base(ctx)
        {
            _resMgr = ctx.GetService<IEmbeddedResourceManager>();
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