using System.Threading;
using Singular.Core.Context;
using Singular.Modules.Core.Authentication;
using Singular.Modules.Core.Data.Services;
using Singular.Modules.Core.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public abstract class CoreControllerBaseNoAuth : Controller
    {
        public ISingularContext SingularContext { get; private set; }
        protected ISectionManager SectionManager { get; private set; }
        public ISiteContext SiteContext { get; private set; }
        public ITranslationService TranslationService { get; private set; }

        protected CoreControllerBaseNoAuth(ISingularContext ctx, ISectionManager sectionService, ISiteContext siteContext, ITranslationService translationService)
        {
            SingularContext = ctx;
            SectionManager = sectionService;
            SiteContext = siteContext;
            TranslationService = translationService;
        }

        protected CoreViewModelBase GetCoreModelBaseInstance()
		{
            //Thread.Sleep(2000);
			return new CoreViewModelBase(SingularContext, SectionManager, SiteContext, TranslationService);
		}

    }
}