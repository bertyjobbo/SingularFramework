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
        IWindsorContainer ApiControllerContainer { get; } // MOVE THIS TO MvcIocManager

        /// <summary>
        /// Container
        /// </summary>
        IWindsorContainer ControllerContainer { get; } // MOVE THIS TO MvcIocManager

        /// <summary>
        /// Add services
        /// </summary>
        /// <param name="registrations"></param>
        /// <returns></returns>
        IMvcIocManager AddServices(params IRegistration[] registrations); // MOVE THIS TO MvcIocManager

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