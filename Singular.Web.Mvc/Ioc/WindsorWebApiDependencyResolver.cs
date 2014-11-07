using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using Castle.Windsor;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace Singular.Web.Mvc.Ioc
{
	public class WindsorWebApiDependencyResolver : IDependencyResolver, IDependencyScope, IDisposable
	{
		private bool _disposed;

		public IWindsorContainer Container
		{
			get;
			protected set;
		}

		public WindsorWebApiDependencyResolver(IWindsorContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container", "The instance of the container cannot be null.");
			}
			this.Container = container;
		}

		public IDependencyScope BeginScope()
		{
			return new WindsorWebApiDependencyScope(this.Container);
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
					if (this.Container != null)
					{
						this.Container.Dispose();
						this.Container = null;
					}
				}
				this._disposed = true;
			}
		}

		~WindsorWebApiDependencyResolver()
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