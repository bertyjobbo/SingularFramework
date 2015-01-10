using System.Threading;
using Singular.Core.Context;
using Singular.Modules.Core.Authentication;
using Singular.Modules.Core.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    [SingularAuthorize]
    public abstract class CoreControllerBase : CoreControllerBaseNoAuth
    {
        protected CoreControllerBase(ISingularContext ctx, ISectionManager sectionService, ISiteContext siteContext) : base(ctx, sectionService, siteContext)
        {
        }
    }
}