using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Core.Authentication
{
    /// <summary>
    /// Permissions service
    /// </summary>
    public interface IPermissionService
    {
        bool UserIsAllowed(string userName, IList<string> allowedUsers, IList<string> allowedRoles, IList<string> allowedModules);
    }
}
