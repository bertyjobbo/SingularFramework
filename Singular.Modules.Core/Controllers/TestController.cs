using Singular.Core.Context;
using System;
using System.Web.Mvc;

namespace Singular.Modules.Core.Controllers
{
	public class TestController : Controller
	{
		private ISingularContext _ctx;

		public TestController(ISingularContext ctx)
		{
			this._ctx = ctx;
		}

		public ActionResult Index()
		{
			return base.Content("This is the core module.");
		}
	}
}