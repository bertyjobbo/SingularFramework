using System.Collections.Generic;
using Singular.Core.Context;
using System;
using System.Runtime.CompilerServices;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.ViewModels
{
	public class CoreViewModelBase
	{
	    public CoreViewModelBase(ISingularContext ctx, IMvcSectionManager mvcManager)
	    {
	        SingularContext = ctx;
	        MvcSectionsManager = mvcManager;
	        Sections = MvcSectionsManager.GetSections();
	    }

	    public IList<MvcSection> Sections { get; private set; }
	    public ISingularContext SingularContext{get;private set;}
        public IMvcSectionManager MvcSectionsManager { get; private set; }
	}
}