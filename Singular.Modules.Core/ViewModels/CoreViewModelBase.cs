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

	    public void Reconstruct(ISingularContext ctx)
	    {
            construct(ctx);
	    }

	    public CoreViewModelBase(ISingularContext ctx)
	    {
	        construct(ctx);
	    }

	    private void construct(ISingularContext ctx)
	    {
	        SingularContext = ctx;
            MvcSectionsManager = ctx.GetService<ISectionManager>();
            Sections = MvcSectionsManager.GetSections();
            SiteContext = ctx.GetService<ISiteContext>();
            TranslationService = ctx.GetService<ITranslationService>();
	    }

	    public ITranslationService TranslationService { get; private set; }
	    public ISiteContext SiteContext { get; set; }
	    public IList<Section> Sections { get; private set; }
	    public ISingularContext SingularContext{get;private set;}
        public ISectionManager MvcSectionsManager { get; private set; }
	}
}