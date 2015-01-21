using System.Collections.Generic;

namespace Singular.Core.Data.Service
{
    public class PermissionService: IPermissionService
    {
        public void Dispose()
        {
            // todo: dispose IPermissionService (probably uow)
        }

        public bool UserIsAllowed(int id, IList<string> allowedUsers, IList<string> allowedRoles, IList<string> allowedModules)
        {
            // todo: PermissionService.UserIsAllowed
            return false;
        }
    }
}
