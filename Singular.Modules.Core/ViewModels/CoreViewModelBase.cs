using System.Collections.Generic;
using Singular.Core.Context;
using System;
using System.Runtime.CompilerServices;
using Singular.Modules.Core.Data.Services;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.ViewModels
{
	public class CoreViewModelBase
	{
	    public CoreViewModelBase()
	    {
	        
	    }

	    public void Reconstruct(ISingularContext ctx, ISectionManager mvcManager, ISiteContext siteContext, ITranslationService translationService)
	    {
            SingularContext = ctx;
            MvcSectionsManager = mvcManager;
            Sections = MvcSectionsManager.GetSections();
            SiteContext = siteContext;
	        TranslationService = translationService;
	    }

	    public CoreViewModelBase(ISingularContext ctx, ISectionManager mvcManager, ISiteContext siteContext, ITranslationService translationService)
	    {
	        SingularContext = ctx;
	        MvcSectionsManager = mvcManager;
	        Sections = MvcSectionsManager.GetSections();
	        SiteContext = siteContext;
	        TranslationService = translationService;
	    }

	    public ITranslationService TranslationService { get; private set; }
	    public ISiteContext SiteContext { get; set; }
	    public IList<Section> Sections { get; private set; }
	    public ISingularContext SingularContext{get;private set;}
        public ISectionManager MvcSectionsManager { get; private set; }
	}
}