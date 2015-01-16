using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Singular.Web.Mvc.Ioc
{
	public class ApiControllerInstaller : IWindsorInstaller
	{
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var pathToBin = AppDomain.CurrentDomain.BaseDirectory;
            if (!pathToBin.Contains("bin"))
            {
                pathToBin = Path.Combine(pathToBin, "bin");
            }
            var registrationArray = new IRegistration[] { Classes.FromAssemblyInDirectory(new AssemblyFilter(pathToBin, null)).BasedOn<ApiController>().LifestyleTransient() };
            container.Register(registrationArray);
        }
	}
}