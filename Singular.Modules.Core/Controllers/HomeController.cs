using Singular.Core.Context;
using System;
using System.Web.Mvc;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace Singular.Modules.Core.Controllers
{
	public class HomeController : CoreControllerBase
	{
		public HomeController(ISingularContext ctx, IEmbeddedResourceManager resourceManager) : base(ctx,resourceManager)
		{
		}

		public ActionResult Index()
		{
			return View(
               /*this.GetViewPath("~/Views/Home/Index.cshtml"), */
               /*"~/Views/Dummy.cshtml",*/
                GetCoreModelBaseInstance());
		}
	}
}