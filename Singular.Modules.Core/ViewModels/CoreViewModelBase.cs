using System.Collections.Generic;
using Singular.Core.Context;
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

        public CoreViewModelBase(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext)
        {
            construct(ctx,sectionManager,siteContext);
        }

        public ISiteContext SiteContext { get; set; }
        public IList<Section> Sections { get; private set; }
        public ISingularContext SingularContext { get; private set; }
        public ISectionManager MvcSectionsManager { get; private set; }

        public void Reconstruct(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext)
        {
            construct(ctx, sectionManager, siteContext);
        }

        private void construct(ISingularContext ctx, ISectionManager sectionManager, ISiteContext siteContext)
        {
            SingularContext = ctx;
            MvcSectionsManager = sectionManager;
            Sections = MvcSectionsManager.GetSections();
            SiteContext = siteContext;
        }
    }
}