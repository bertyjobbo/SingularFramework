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
    public abstract class CoreControllerBase : Controller
    {
        public ISingularContext SingularContext { get; private set; }
        protected ISectionManager SectionManager { get; private set; }
        public ISiteContext SiteContext { get; private set; }
        
        protected CoreControllerBase(ISingularContext ctx, ISectionManager sectionService, ISiteContext siteContext)
        {
            SingularContext = ctx;
            SectionManager = sectionService;
            SiteContext = siteContext;
        }

        protected CoreViewModelBase GetCoreModelBaseInstance()
		{
            //Thread.Sleep(2000);
			return new CoreViewModelBase(SingularContext, SectionManager, SiteContext);
		}

    }
}