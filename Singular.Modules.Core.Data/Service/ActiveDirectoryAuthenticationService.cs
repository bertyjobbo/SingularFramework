using System;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Transaction;
using Singular.Modules.Core.Data.Models;

namespace Singular.Modules.Core.Data.Service
{
    public class ActiveDirectoryAuthenticationService : IAuthenticationService
    {
        public TransactionResult<FormsAuthModel> CheckLogin(string email, string password, out SingularUser user)
        {
            throw new NotImplementedException("Active Directory login not yet available");
        }

        public void AddUserToContextByLogonName(string name)
        {
            throw new NotImplementedException("Active Directory login not yet available");
        }
    }
}