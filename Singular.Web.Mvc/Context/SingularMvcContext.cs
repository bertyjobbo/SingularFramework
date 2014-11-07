using System.Web.Hosting;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Singular.Core.Configuration;
using Singular.Core.Context;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.Mvc;

namespace Singular.Web.Mvc.Context
{
	public class SingularMvcContext : ISingularContext
	{
        // singleton
        /// <summary>
        /// Current
        /// </summary>
        public static ISingularContext Current
        {
            get { return _current ?? (_current = new SingularMvcContext()); }
        }
        private SingularMvcContext()
        {
            Modules = new Dictionary<string, ISingularModuleConfiguration>();
        }
        private static ISingularContext _current;

        // fields
		private readonly List<IRegistration> _tmpRegistrations = new List<IRegistration>();
		private List<Assembly> _assemblies;
		private readonly Type _moduleConfigType = typeof(ISingularModuleConfiguration);
		private readonly Type _areaRegType = typeof(AreaRegistration);

        // private methods
        private void fireModuleAppStartMethods()
        {
            foreach (KeyValuePair<string, ISingularModuleConfiguration> singularModuleConfiguration in Modules)
            {
                singularModuleConfiguration.Value.OnAppStart();
            }
        }
        private void setAssemblies()
        {
            _assemblies = new List<Assembly>();
            var pathToBin = AppDomain.CurrentDomain.BaseDirectory;
            if (!pathToBin.Contains("bin"))
            {
                pathToBin = Path.Combine(pathToBin, "bin");
            }
            var files = Directory.GetFiles(pathToBin, "*.dll");
            for (var i = 0; i < files.Length; i++)
            {
                var dllPath = files[i];
                var assembly = Assembly.Load(AssemblyName.GetAssemblyName(dllPath));
                _assemblies.Add(assembly);
            }
        }
        private void setModules()
        {
            foreach (Assembly assembly in _assemblies)
            {
                IEnumerable<Type> appTypes =
                     assembly.GetTypes().Where(
                        x => (x.GetInterfaces().Contains(_moduleConfigType) && x != _moduleConfigType));
                foreach (var appType in appTypes)
                {
                    if (!appType.IsSubclassOf(_areaRegType))
                    {
                        throw new Exception("An implementaiton of ISingularModuleConfiguration must inherit from AreaRegistration");
                    }
                    var module = (ISingularModuleConfiguration)Activator.CreateInstance(appType);
                    AddServices(module.ServiceRegistrations);
                    Modules.Add(module.AreaName, module);
                }
            }
        }

        /// <summary>
        /// Api container
        /// </summary>
		public IWindsorContainer ApiControllerContainer { get; private set; }

        /// <summary>
        /// Controller container
        /// </summary>
		public IWindsorContainer ControllerContainer { get; private set; }

        /// <summary>
        /// Modules
        /// </summary>
		public IDictionary<string, ISingularModuleConfiguration> Modules { get; private set; }

        /// <summary>
        /// Add services
        /// </summary>
        /// <param name="registrations"></param>
        /// <returns></returns>
		public ISingularContext AddServices(params IRegistration[] registrations)
		{
			_tmpRegistrations.AddRange(registrations);
			return this;
		}

        /// <summary>
        /// Register the modules
        /// </summary>
        /// <returns></returns>
		public ISingularContext RegisterModules()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalFilters.Filters.Add(new HandleErrorAttribute());
			GlobalConfiguration.Configure((config) => {
				config.MapHttpAttributeRoutes();
				config.Formatters.Clear();
				config.Formatters.Add(new JsonMediaTypeFormatter());
			});
			setAssemblies();
			setModules();
			var windsorContainer = new WindsorContainer();
			var controllerInstaller = new IWindsorInstaller[] { new ControllerInstaller() };
			ControllerContainer = windsorContainer.Install(controllerInstaller);
			ControllerContainer.Register(_tmpRegistrations.ToArray());
			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(ControllerContainer.Kernel));
			var windsorContainer1 = new WindsorContainer();
			controllerInstaller = new IWindsorInstaller[] { new ApiControllerInstaller() };
			ApiControllerContainer = windsorContainer1.Install(controllerInstaller);
			GlobalConfiguration.Configuration.DependencyResolver = new WindsorWebApiDependencyResolver(ApiControllerContainer);
			ApiControllerContainer.Register(_tmpRegistrations.ToArray());
			fireModuleAppStartMethods();
			return this;
		}

        /// <summary>
        /// Get service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
	    public T GetService<T>()
	    {
	        if (ControllerContainer != null)
	        {
	            return ControllerContainer.Resolve<T>();
	        }
	        return default (T);
	    }

        /// <summary>
        /// Load resources
        /// </summary>
        /// <returns></returns>
	    public ISingularContext LoadResources()
	    {
	        EmbeddedResourceManager.Current.LoadResources();
	        return this;
	    }
	}
}