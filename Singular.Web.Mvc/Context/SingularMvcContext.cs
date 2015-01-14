using System.Web;
using System.Web.Hosting;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Singular.Core.Authentication;
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
        private List<Type> _types;
        private readonly Type _moduleConfigType = typeof(ISingularModuleConfiguration);
        private readonly Type _areaRegType = typeof(AreaRegistration);
        private IPermissionService _permissionService;
        private bool _firstRequestComplete;
        private IUserFactory _userFactory;

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
            if (_assemblies == null)
            {
                _assemblies = new List<Assembly>();
                var pathToBin = AppDomain.CurrentDomain.BaseDirectory;
                if (!pathToBin.Contains("bin"))
                {
                    pathToBin = Path.Combine(pathToBin, "bin");
                }
                var files = Directory.GetFiles(pathToBin, "*.dll").Where(x => !x.StartsWith("System.")).ToList();
                foreach (var dllPath in files)
                {
                    var assembly = Assembly.Load(AssemblyName.GetAssemblyName(dllPath));
                    _assemblies.Add(assembly);
                }
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
        /// Types
        /// </summary>
        public IList<Type> Types {
            get { return _types ?? (_types = Assemblies.SelectMany(x => x.GetTypes()).ToList()); }
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return MvcIocManager.Current.GetService<T>();
        }

        /// <summary>
        /// Register the modules
        /// </summary>
        /// <returns></returns>
        public ISingularContext RegisterModules()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            GlobalConfiguration.Configure((config) =>
            {
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

        /// <summary>
        /// User is allowed
        /// </summary>
        /// <param name="users"></param>
        /// <param name="roles"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public bool UserIsAllowed(IList<string> users, IList<string> roles, IList<string> modules)
        {
            if (CurrentUser == null) return false;
            return _permissionService.UserIsAllowed(CurrentUser.LogonName, users, roles, modules);
        }

        /// <summary>
        /// Current user
        /// </summary>
        public SingularUser CurrentUser
        {
            get
            {
                var s = Session;
                if (s == null) return default(SingularUser);
                return s.User;
            }
        }

        /// <summary>
        /// Set current user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ISingularContext SetCurrentUser(SingularUser user)
        {
            var s = new SingularSession(user);
            HttpContext.Current.Session.Add(session_key, s);
            return this;
        }

        /// <summary>
        /// Remove current user
        /// </summary>
        /// <returns></returns>
        public ISingularContext RemoveCurrentUser()
        {
            HttpContext.Current.Session.Remove(session_key);
            return this;
        }

        /// <summary>
        /// Session
        /// </summary>
        public SingularSession Session
        {
            get
            {
                var s = HttpContext.Current.Session[session_key];
                if (s == null) return default(SingularSession);
                return (SingularSession)s;
            }
        }
        private const string session_key = "SINGULAR_USER_SESSION";

        /// <summary>
        /// Is authenticated
        /// </summary>
        public bool IsAuthenticated { get { return CurrentUser != null && Session != null; } }

        /// <summary>
        /// Assemblies
        /// </summary>
        public IList<Assembly> Assemblies
        {
            get
            {
                if (_assemblies == null)
                {
                    setAssemblies();
                }
                return _assemblies;
            }
        }
        
        /// <summary>
        /// On first request
        /// </summary>
        public void OnBeginRequest()
        {
            if (!_firstRequestComplete)
            {
                _firstRequestComplete = true;
                _permissionService = MvcIocManager.Current.GetService<IPermissionService>();
                _userFactory = MvcIocManager.Current.GetService<IUserFactory>();
            }

            if (
                HttpContext.Current.User != null &&
                HttpContext.Current.User.Identity.IsAuthenticated &&
                HttpContext.Current.User.Identity.AuthenticationType == "NTLM" &&
                !IsAuthenticated)
            {
                SetCurrentUser(
                    _userFactory.Build(
                        HttpContext.Current.User.Identity.AuthenticationType,
                        HttpContext.Current.User.Identity.Name
                    )
                );
            }
        }
    }
}