using Singular.Core.Context;
using System;
using System.Runtime.CompilerServices;

namespace Singular.Modules.Core.ViewModels
{
	public class CoreViewModelBase
	{
		public ISingularContext SingularContext
		{
			get;
			private set;
		}

		public CoreViewModelBase(ISingularContext ctx)
		{
			this.SingularContext = ctx;
		}
	}
}