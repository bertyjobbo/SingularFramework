using Singular.Core.Data.Entities;
using Singular.Core.Data.Transaction;
using Singular.Modules.Core.Data.Models;

namespace Singular.Modules.Core.Data.Service
{
    public interface IAuthenticationService
    {
        TransactionResult<FormsAuthModel> CheckLogin(string email, string password, out SingularUser user);
    }
}
