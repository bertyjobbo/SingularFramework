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
            var files = Directory.GetFiles(pathToBin, "*.dll").Where(x=>!x.StartsWith("System.")).ToList();
            foreach (var dllPath in files)
            {
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
                    Modules.Add(module.AreaName, module);
                }
            }
        }

        /// <summary>
        /// Modules
        /// </summary>
		public IDictionary<string, ISingularModuleConfiguration> Modules { get; private set; }

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
			fireModuleAppStartMethods();
            MvcIocManager.Current.FinalizeServices();
			return this;
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