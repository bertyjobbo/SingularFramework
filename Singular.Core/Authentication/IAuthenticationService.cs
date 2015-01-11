using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singular.Core.Transaction;

namespace Singular.Core.Authentication
{
    public interface IAuthenticationService
    {
        TransactionResult Login(string email, string password);
    }
}
