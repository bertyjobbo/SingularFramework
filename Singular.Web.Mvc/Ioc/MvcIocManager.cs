using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

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
        private readonly List<IRegistration> _tmpRegistrations = new List<IRegistration>();

        /// <summary>
        /// Finalize
        /// </summary>
        public void FinalizeServices()
        {
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
        /// Add services
        /// </summary>
        /// <param name="registrations"></param>
        /// <returns></returns>
        public IMvcIocManager AddServices(params IRegistration[] registrations)
        {
            _tmpRegistrations.AddRange(registrations);
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
