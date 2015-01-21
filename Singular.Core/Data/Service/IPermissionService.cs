using System;
using System.Collections.Generic;

namespace Singular.Core.Data.Service
{
    /// <summary>
    /// Permissions service
    /// </summary>
    public interface IPermissionService : IDisposable
    {
        bool UserIsAllowed(int id, IList<string> allowedUsers, IList<string> allowedRoles, IList<string> allowedModules);
    }
}
