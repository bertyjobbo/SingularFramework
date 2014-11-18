using System.Collections.Generic;
using Singular.Core.Context;
using System;
using System.Runtime.CompilerServices;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.ViewModels
{
	public class CoreViewModelBase
	{
	    public CoreViewModelBase(ISingularContext ctx, ISectionManager mvcManager, ISiteContext siteContext)
	    {
	        SingularContext = ctx;
	        MvcSectionsManager = mvcManager;
	        Sections = MvcSectionsManager.GetSections();
	        SiteContext = siteContext;
	    }

	    public ISiteContext SiteContext { get; set; }
	    public IList<Section> Sections { get; private set; }
	    public ISingularContext SingularContext{get;private set;}
        public ISectionManager MvcSectionsManager { get; private set; }
	}
}