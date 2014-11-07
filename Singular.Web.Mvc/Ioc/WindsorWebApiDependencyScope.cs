using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Http.Dependencies;

namespace Singular.Web.Mvc.Ioc
{
	public class WindsorWebApiDependencyScope : IDependencyScope, IDisposable
	{
		private bool _disposed;

		private IDisposable _scope;

		public IWindsorContainer Container
		{
			get;
			protected set;
		}

		public WindsorWebApiDependencyScope(IWindsorContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			this.Container = container;
			this._scope = LifestyleExtensions.BeginScope(this.Container);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					if (this._scope != null)
					{
						this._scope.Dispose();
						this._scope = null;
					}
				}
				this._disposed = true;
			}
		}

		~WindsorWebApiDependencyScope()
		{
			this.Dispose(false);
		}

		public object GetService(Type serviceType)
		{
			object obj;
			try
			{
				obj = this.Container.Resolve(serviceType);
			}
			catch (ComponentNotFoundException componentNotFoundException)
			{
				obj = null;
			}
			return obj;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return this.Container.ResolveAll(serviceType).Cast<object>();
		}
	}
}