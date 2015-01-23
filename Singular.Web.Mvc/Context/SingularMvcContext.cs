using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using Singular.Core.Authentication;
using Singular.Core.Configuration;
using Singular.Core.Context;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Enums;
using Singular.Core.Data.Service;
using Singular.Core.Exceptions;
using Singular.Web.Mvc.EmbeddedResourceConfiguration;
using Singular.Web.Mvc.Ioc;

namespace Singular.Web.Mvc.Context
{
    public class SingularMvcContext : ISingularContext
    {
        // singleton

        private const string session_key = "SINGULAR_USER_SESSION";
        private static ISingularContext _current;
        private readonly Type _areaRegType = typeof (AreaRegistration);
        private readonly Type _moduleConfigType = typeof (ISingularModuleConfiguration);

        // fields
        private List<Assembly> _assemblies;
        private List<Type> _types;

        private SingularMvcContext()
        {
            Modules = new Dictionary<string, ISingularModuleConfiguration>();
        }

        /// <summary>
        ///     Current
        /// </summary>
        public static ISingularContext Current
        {
            get { return _current ?? (_current = new SingularMvcContext()); }
        }

        /// <summary>
        ///     Modules
        /// </summary>
        public IDictionary<string, ISingularModuleConfiguration> Modules { get; private set; }

        /// <summary>
        ///     Types
        /// </summary>
        public IList<Type> Types
        {
            get { return _types ?? (_types = Assemblies.SelectMany(x => x.GetTypes()).ToList()); }
        }

        /// <summary>
        ///     Get service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return MvcIocManager.Current.GetService<T>();
        }

        /// <summary>
        ///     Register the modules
        /// </summary>
        /// <returns></returns>
        public ISingularContext RegisterModules()
        {
            // standard mvc
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            GlobalConfiguration.Configure(config =>
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
        ///     Load resources
        /// </summary>
        /// <returns></returns>
        public ISingularContext LoadResources()
        {
            EmbeddedResourceManager.Current.LoadResources();
            return this;
        }

        /// <summary>
        ///     Current user
        /// </summary>
        public SingularUser CurrentUser
        {
            get
            {
                if (Session == null) return default(SingularUser);
                return Session.User;
            }
        }

        /// <summary>
        ///     Set current user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ISingularContext CreateSingularSession(SingularUser user)
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
        ///     Remove current user
        /// </summary>
        /// <returns></returns>
        public ISingularContext DestroySingularSession()
        {
            if (HttpContext.Current.Session == null)
            {
                throw new Exception("HttpContext.Current.Session is null at ISingularContext.SetCurrentUser");
            }
            HttpContext.Current.Session.Remove(session_key);
            return this;
        }

        /// <summary>
        ///     Session
        /// </summary>
        public SingularSession Session
        {
            get
            {
                // check no possible session
                if (HttpContext.Current == null || HttpContext.Current.Session == null) return default(SingularSession);

                // now try to get session
                var s = HttpContext.Current.Session[session_key];
                if (s == null)
                {
                    // check aspnet auth token exists but user doesn't
                    checkAuthenticatedButNoSingularSession();
                }
                
                return (SingularSession) s;
            }
        }

        /// <summary>
        ///     Is authenticated
        /// </summary>
        public bool IsAuthenticated
        {
            get { return Session != null; }
        }

        /// <summary>
        ///     Assemblies
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

        private void fireModuleAppStartMethods()
        {
            foreach (var singularModuleConfiguration in Modules)
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
            foreach (var assembly in _assemblies)
            {
                var appTypes =
                    assembly.GetTypes().Where(
                        x => (x.GetInterfaces().Contains(_moduleConfigType) && x != _moduleConfigType));
                foreach (var appType in appTypes)
                {
                    if (!appType.IsSubclassOf(_areaRegType))
                    {
                        throw new Exception(
                            "An implementaiton of ISingularModuleConfiguration must inherit from AreaRegistration");
                    }
                    var module = (ISingularModuleConfiguration) Activator.CreateInstance(appType);
                    Modules.Add(module.AreaName, module);
                }
            }
        }

        private void checkAuthenticatedButNoSingularSession()
        {
            // check if aspnet login exists, but Singular one doesn't
            if (HttpContext.Current != null && HttpContext.Current.User != null &&
                HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var isWindowsLogon = SingularMvcAuthenticationContext.Current.AuthenticationType ==
                                     AuthenticationType.Domain;
                var isFormsLogon = SingularMvcAuthenticationContext.Current.AuthenticationType ==
                                   AuthenticationType.Forms;
                var isAdLogon = SingularMvcAuthenticationContext.Current.AuthenticationType ==
                                AuthenticationType.ActiveDirectory;

                var service = GetService<IUserService>();
                if (service == null)
                {
                    throw new SingularServiceResolverException<IUserService>("At SingularMvcContext.Session");
                }
                SingularUser user = null;
                if (isWindowsLogon)
                    user = service.GetUserByDomainLogon(HttpContext.Current.User.Identity.Name, false);
                if (isAdLogon)
                    user = service.GetUserByActiveDirectoryLogon(HttpContext.Current.User.Identity.Name, false);
                if (isFormsLogon)
                    user = service.GetUserByEmail(HttpContext.Current.User.Identity.Name, false);

                if (user == null)
                {
                    if (isWindowsLogon)
                    {
                        // user doesn't exist in database yet, so redirect to register page
                        // todo: HttpContext.Current.Response.Redirect("~/Singular/Core/Users/RegisterDomainUser",true);
                        throw new NotImplementedException("Domain logon is not currently supported");
                    }
                    throw new Exception("Cannot find user");
                }
                if (user.IsLockedOut || !user.IsActive)
                {
                    if (!isWindowsLogon)
                    {
                        FormsAuthentication.SignOut();
                    }
                    throw new HttpException(403, "Access Denied");
                }
                CreateSingularSession(user);
            }
        }
    }
}