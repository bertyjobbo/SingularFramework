using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Singular.Core.Configuration;
using System;
using System.Collections.Generic;

namespace Singular.Core.Context
{
    /// <summary>
    /// Context abstraction
    /// </summary>
	public interface ISingularContext
	{
        /// <summary>
        /// Api container
        /// </summary>
		IWindsorContainer ApiControllerContainer{get;}

        /// <summary>
        /// Container
        /// </summary>
		IWindsorContainer ControllerContainer{get;}

        /// <summary>
        /// Modules
        /// </summary>
		IDictionary<string, ISingularModuleConfiguration> Modules{get;}

        /// <summary>
        /// Add services
        /// </summary>
        /// <param name="registrations"></param>
        /// <returns></returns>
		ISingularContext AddServices(params IRegistration[] registrations);

        /// <summary>
        /// Register modules
        /// </summary>
        /// <returns></returns>
		ISingularContext RegisterModules();
        
        /// <summary>
        /// Get a service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
	    T GetService<T>();

        /// <summary>
        /// Load resources
        /// </summary>
        /// <returns></returns>
        ISingularContext LoadResources();
	}
}