using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.IO;

namespace Singular.Web.Mvc.Ioc
{
	public class ControllerInstaller : IWindsorInstaller
	{
		public ControllerInstaller()
		{
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			var pathToBin = AppDomain.CurrentDomain.BaseDirectory;
			if (!pathToBin.Contains("bin"))
			{
				pathToBin = Path.Combine(pathToBin, "bin");
			}
			var registrationArray = new IRegistration[] { Classes.FromAssemblyInDirectory(new AssemblyFilter(pathToBin, null)).BasedOn<IController>().LifestyleTransient() };
			container.Register(registrationArray);
		}
	}
}