using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Singular.Core.Context;

namespace Singular.Web.Mvc.Ioc
{
    public interface IMvcIocManager
    {
        /// <summary>
        /// Api container
        /// </summary>
        IWindsorContainer ApiControllerContainer { get; }

        /// <summary>
        /// Container
        /// </summary>
        IWindsorContainer ControllerContainer { get; } 

        /// <summary>
        /// Add services for controllers
        /// </summary>
        /// <param name="registrations"></param>
        /// <returns></returns>
        IMvcIocManager AddServices(params IRegistration[] registrations);

        /// <summary>
        /// Add services for webapi controllers
        /// </summary>
        /// <param name="registrations"></param>
        /// <returns></returns>
        IMvcIocManager AddWebApiServices(params IRegistration[] registrations);

        /// <summary>
        /// Get a service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetService<T>(); // MOVE THIS TO MvcIocManager

        /// <summary>
        /// Finalize
        /// </summary>
        void FinalizeServices();
    }
}