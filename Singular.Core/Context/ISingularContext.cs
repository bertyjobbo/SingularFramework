using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Singular.Core.Authentication;
using Singular.Core.Configuration;
using System;
using System.Collections.Generic;
using Singular.Core.Data.Entities;

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

        /// <summary>
        /// Current user
        /// </summary>
        SingularUser CurrentUser { get; }

        /// <summary>
        /// Set current user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ISingularContext CreateSingularSession(SingularUser user);

        /// <summary>
        /// Remove current user
        /// </summary>
        /// <returns></returns>
        ISingularContext DestroySingularSession();

        /// <summary>
        /// Session
        /// </summary>
        SingularSession Session { get; }

        /// <summary>
        /// Is auth?
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Assemblies
        /// </summary>
        IList<Assembly> Assemblies { get; }

        /// <summary>
        /// Types
        /// </summary>
        IList<Type> Types { get; }
        
        /// <summary>
        /// Get a service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetService<T>();
    }
}