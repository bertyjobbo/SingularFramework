using Singular.Core.Context;
using System.Web.Mvc;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace Singular.Modules.Core.Controllers
{
    public class BuildController : CoreControllerBase
    {
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="resourceManager"></param>
        public BuildController(ISingularContext ctx, IEmbeddedResourceManager resourceManager) : base(ctx,resourceManager)
        {
            
        }

        /// <summary>
        /// Build site
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string msg = null;
            if (ResourceManager.BuildResources(ref msg))
            {
                Response.Redirect(base.Url.Content("~/Singular/Core/Build/Success"));
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