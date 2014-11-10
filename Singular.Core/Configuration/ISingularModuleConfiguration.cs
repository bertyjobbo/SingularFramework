using Castle.MicroKernel.Registration;
using System;

namespace Singular.Core.Configuration
{
    /// <summary>
    /// Module config
    /// </summary>
    public interface ISingularModuleConfiguration
    {
        /// <summary>
        /// Area name
        /// </summary>
        string AreaName { get; }

        /// <summary>
        /// App start
        /// </summary>
        void OnAppStart();
    }
}