using System.Data.Entity;
using System.Web;
using System.Web.Hosting;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Singular.Core.Authentication;
using Singular.Core.Configuration;
using Singular.Core.Context;
using Singular.Core.Data.DataContext;
using Singular.Core.Data.Entities;
using Singular.Core.Data.EntityFramework;
using Singular.Core.Data.Repository;
using Singular.Core.Data.Service;
using Singular.Core.Data.Transaction;
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
        public IList<Type> Types
        {
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

            // standard mvc
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            GlobalConfiguration.Configure((config) =>
            {
                config.Formatters.Clear();
                config.Formatters.Add(new JsonMediaTypeFormatter());
                config.MapHttpAttributeRoutes();
                //config.Routes.MapHttpRoute(
                //name: "DefaultApi",
                //routeTemplate: "SingularApi/{controller}/{id}",
                //defaults: new { id = RouteParameter.Optional }
                //);
            });

            // singular
            setAssemblies();
            setModules();
            fireModuleAppStartMethods();
            MvcIocManager.Current.FinalizeServices();

            //
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
            if (HttpContext.Current.Session == null)
            {
                throw new Exception("HttpContext.Current.Session is null at ISingularContext.SetCurrentUser");
            }
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
                if (HttpContext.Current == null || HttpContext.Current.Session == null) return default(SingularSession);
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
            // these conditions mean that it's windows logon
            if (!IsAuthenticated && HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // so we need to add the current user
                var ctx = (DbContext) new SingularEntityFrameworkContext();
                using (var uow = (IUnitOfWork) new EntityFrameworkUnitOfWork(ctx, this))
                {
                    var repo = (IRepository<SingularUser>)new EntityFrameworkRepository<SingularUser>(ctx, uow);
                    var userService = (IUserService)new UserService(repo, uow);
                    var user = userService.GetUserByDomainLogon(HttpContext.Current.User.Identity.Name);
                    if (user == null)
                    {
                        // user doesn't exist in database yet, so redirect to register page
                        // todo: HttpContext.Current.Response.Redirect("~/Singular/Core/Users/RegisterDomainUser",true);
                        throw new NotImplementedException("Domain logon is not currently supported");
                    }    
                }
            }
        }
    }
}