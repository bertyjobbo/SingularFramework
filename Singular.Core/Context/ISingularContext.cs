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
        /// Modules
        /// </summary>
        IDictionary<string, ISingularModuleConfiguration> Modules { get; }

        /// <summary>
        /// Register modules
        /// </summary>
        /// <returns></returns>
        ISingularContext RegisterModules();

        /// <summary>
        /// Load resources
        /// </summary>
        /// <returns></returns>
        ISingularContext LoadResources();
    }
}