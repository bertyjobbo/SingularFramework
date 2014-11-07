using Singular.Core.Context;
using Singular.Modules.Core.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;

namespace Singular.Modules.Core.Controllers
{
	public abstract class CoreControllerBase : Controller
	{
		public ISingularContext SingularContext{get;private set;}
        public IEmbeddedResourceManager ResourceManager { get; private set; }

		protected CoreControllerBase(ISingularContext ctx, IEmbeddedResourceManager resourceManager)
		{
			SingularContext = ctx;
		    ResourceManager = resourceManager;
		}

		protected CoreViewModelBase GetCoreModelBaseInstance()
		{
			return new CoreViewModelBase(this.SingularContext);
		}
	}
}