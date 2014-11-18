using Castle.MicroKernel.Registration;
using Singular.Core.Context;
using Singular.Web.Mvc.Context;
using System;
using System.Runtime.CompilerServices;
using System.Web;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Ioc;
using Singular.Web.Mvc.Section;

namespace Singular.Web.Mvc.Application
{
	public class SingularMvcApplication : HttpApplication
	{
		public SingularMvcApplication()
		{
		}

		protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
		{
			this.SingularApplicationAuthenticateRequest(sender, e);
		}

		protected virtual void Application_BeginRequest(object sender, EventArgs e)
		{
			this.SingularApplicationBeginRequest(sender, e);
		}

		protected virtual void Application_End(object sender, EventArgs e)
		{
			this.SingularApplicationEnd(sender, e);
		}

		protected virtual void Application_Error(object sender, EventArgs e)
		{
			this.SingularApplicationError(sender, e);
		}

		protected virtual void Application_Start(object sender, EventArgs e)
		{
			this.SingularApplicationStart(sender, e);
		}

		protected virtual void Session_End(object sender, EventArgs e)
		{
			this.SingularSessionEnd(sender, e);
		}

		protected virtual void Session_Start(object sender, EventArgs e)
		{
			this.SingularSessionStart(sender, e);
		}

		protected virtual void SingularApplicationAuthenticateRequest(object sender, EventArgs eventArgs)
		{
		}

		protected virtual void SingularApplicationBeginRequest(object sender, EventArgs eventArgs)
		{
		    if (Request.Url.ToString().Contains("/Areas/"))
		    {
		        var x = 0;
		    }
		}

		protected virtual void SingularApplicationEnd(object sender, EventArgs eventArgs)
		{
		}

		protected virtual void SingularApplicationError(object sender, EventArgs eventArgs)
		{
		}

		protected virtual void SingularApplicationStart(object sender, EventArgs eventArgs)
		{

		    MvcIocManager.Current.AddServices(

		        Component
		            .For<IEmbeddedResourceManager>()
		            .UsingFactoryMethod(() => EmbeddedResourceManager.Current)
		            .LifestyleSingleton(),

		        Component
		            .For<ISingularContext>()
		            .UsingFactoryMethod<ISingularContext>(() => SingularMvcContext.Current)
		            .LifestyleSingleton(),

		        Component
		            .For<ISectionManager>()
		            .UsingFactoryMethod(() => SectionManager.Current)
		            .LifestyleSingleton(),

                Component
                    .For<ISiteContext>()
                    .UsingFactoryMethod(()=> SiteContext.Current)
                    .LifestyleSingleton()

		        );

		    SingularMvcContext
		        .Current
		        .RegisterModules()
                .LoadResources();

		}

		protected virtual void SingularSessionEnd(object sender, EventArgs eventArgs)
		{
		}

		protected virtual void SingularSessionStart(object sender, EventArgs eventArgs)
		{
		}
	}
}