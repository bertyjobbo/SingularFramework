using System.Collections.Generic;
using Singular.Core.Context;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Repository;
using Singular.Core.Data.Transaction;

namespace Singular.Core.Data.Service
{
    public class PermissionService: IPermissionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<SingularUser> _repo;
        private ISingularContext _ctx;

        public PermissionService(ISingularContext ctx, IRepository<SingularUser> repo,IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
            _ctx = ctx;
        }


        public bool UserIsAllowed(int id, IList<string> allowedUsers, IList<string> allowedRoles, IList<string> allowedModules)
        {
            // check current
            if (_ctx.IsAuthenticated && _ctx.CurrentUser.Id == id)
            {
                if (_ctx.CurrentUser.IsSuperUser) return true;

                return permissionCheck(_ctx.CurrentUser, allowedUsers, allowedRoles, allowedModules);
            }
            else
            {
                // find user
                //var user = ...

                // return
                // return permissionCheck(_ctx.CurrentUser, allowedUsers, allowedRoles, allowedModules);
            }
            
            //
            return false;
        }

        private bool permissionCheck(SingularUser currentUser, IList<string> allowedUsers, IList<string> allowedRoles, IList<string> allowedModules)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
