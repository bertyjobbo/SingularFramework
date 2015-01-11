using System.Threading;
using Singular.Core.Context;
using Singular.Modules.Core.Data.Services;
using Singular.Modules.Core.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Singular.Web.Mvc.Authentication;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.ApiControllers
{
    /// <summary>
    /// Core controller base (authorized)
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