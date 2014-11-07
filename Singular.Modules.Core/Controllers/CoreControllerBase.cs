using Singular.Core.Context;
using Singular.Modules.Core.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public abstract class CoreControllerBase : Controller
    {
        public ISingularContext SingularContext { get; private set; }
        protected IMvcSectionManager SectionService { get; private set; }

        protected CoreControllerBase(ISingularContext ctx, IMvcSectionManager sectionService)
        {
            SingularContext = ctx;
            SectionService = sectionService;
        }

        protected CoreViewModelBase GetCoreModelBaseInstance()
		{
			return new CoreViewModelBase(SingularContext, SectionService);
		}
    }
}