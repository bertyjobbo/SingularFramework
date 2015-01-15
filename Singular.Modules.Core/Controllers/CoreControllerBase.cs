using Singular.Core.Context;
using Singular.Web.Mvc.Authentication;

namespace Singular.Modules.Core.Controllers
{
    /// <summary>
    ///     Core controller base (authorized)
    /// </summary>
    [SingularAuthorize]
    public abstract class CoreControllerBase : CoreControllerBaseNoAuth
    {
        protected CoreControllerBase(ISingularContext ctx)
            : base(ctx)
        {
        }
    }
}