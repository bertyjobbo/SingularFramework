using System.Web.Http;
using Singular.Core.Context;
using Singular.Modules.Core.Data.Services;

namespace Singular.Modules.Core.ApiControllers
{
    public abstract class CoreApiControllerBaseNoAuth : ApiController
    {
        protected CoreApiControllerBaseNoAuth(ISingularContext ctx)
        {
            SingularContext = ctx;
        }

        public ISingularContext SingularContext { get; private set; }
    }
}