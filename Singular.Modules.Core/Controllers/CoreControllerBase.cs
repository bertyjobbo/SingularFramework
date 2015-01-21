using Singular.Core.Context;
using Singular.Web.Mvc.Authentication;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    /// <summary>
    ///     Core controller base (authorized)
    /// </summary>
    [SingularAuthorize]
    public abstract class CoreControllerBase : CoreControllerBaseNoAuth
    {
        protected CoreControllerBase(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext)
            : base(ctx, sectionManager, siteContext)
        {
        }
    }
}