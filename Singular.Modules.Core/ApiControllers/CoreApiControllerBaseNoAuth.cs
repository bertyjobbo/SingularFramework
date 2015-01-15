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
            TranslationService = ctx.GetService<ITranslationService>();
        }

        public ISingularContext SingularContext { get; private set; }
        public ITranslationService TranslationService { get; private set; }
    }
}