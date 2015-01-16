using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Singular.Useful;

namespace Singular.Web.Mvc.Ioc
{
    public class MvcIocManager : IMvcIocManager
    {
        #region singleton
        private MvcIocManager() { }
        private static IMvcIocManager _current;
        public static IMvcIocManager Current { get { return _current ?? (_current = new MvcIocManager()); } }
        #endregion

        // fields
        private readonly List<IRegistration> _registrations = new List<IRegistration>();
        private readonly List<IRegistration> _apiRegistrations = new List<IRegistration>();


        /// <summary>
        /// Finalize
        /// </summary>
        public void FinalizeServices()
        {
            var container = new WindsorContainer();
            var controllerInstaller = new IWindsorInstaller[] { new ControllerInstaller() };
            ControllerContainer = container.Install(controllerInstaller);
            ControllerContainer.Register(_registrations.ToArray());
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(ControllerContainer.Kernel));


            var container2 = new WindsorContainer();
            var controllerInstaller1 = new IWindsorInstaller[] { new ApiControllerInstaller() };
            ApiControllerContainer = container2.Install(controllerInstaller1);
            ApiControllerContainer.Register(_apiRegistrations.ToArray());
            //ApiControllerContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(ApiControllerContainer.Kernel, true));
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorWebApiDependencyResolver(ApiControllerContainer);
            //GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(ApiControllerContainer));
        }

        /// <summary>
        /// Widn
        /// </summary>
        public IWindsorContainer WindsorContainer { get; private set; }

        /// <summary>
        /// Api container
        /// </summary>
        public IWindsorContainer ApiControllerContainer { get; private set; }

        /// <summary>
        /// Controller container
        /// </summary>
        public IWindsorContainer ControllerContainer { get; private set; }

        /// <summary>
        /// Add services
        /// </summary>
        /// <param name="registrations"></param>
        /// <returns></returns>
        public IMvcIocManager AddServices(params IRegistration[] registrations)
        {
            _registrations.AddRange(registrations);
            return this;
        }

        /// <summary>
        /// Add services
        /// </summary>
        /// <param name="registrations"></param>
        /// <returns></returns>
        public IMvcIocManager AddWebApiServices(params IRegistration[] registrations)
        {
            _apiRegistrations.AddRange(registrations);
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
            return default(T);
        }
    }
}
