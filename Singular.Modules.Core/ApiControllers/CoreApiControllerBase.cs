using Singular.Core.Context;
using Singular.Web.Mvc.Authentication;

namespace Singular.Modules.Core.ApiControllers
{
    /// <summary>
    ///     Core controller base (authorized)
    /// </summary>
    [SingularAuthorize]
    public abstract class CoreApiControllerBase : CoreApiControllerBaseNoAuth
    {
        protected CoreApiControllerBase(ISingularContext ctx)
            : base(ctx)
        {
        }
    }
}