using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Modules.Core.Data.Services;
using Singular.Modules.Core.ViewModels;
using Singular.Web.Mvc.Context;
using Singular.Web.Mvc.Section;

namespace Singular.Modules.Core.Controllers
{
    public class FormsAuthController : CoreControllerBaseNoAuth
    {
        public FormsAuthController(ISingularContext ctx, ISectionManager sectionService, ISiteContext siteContext, ITranslationService translationService) : base(ctx, sectionService, siteContext,translationService)
        {

        }

        public ActionResult Index()
        {
            return View(new FormsAuthViewModel(SingularContext,SectionManager, SiteContext, TranslationService));
        }
    }
}