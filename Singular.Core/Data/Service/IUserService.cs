using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singular.Core.Data.Entities;

namespace Singular.Core.Data.Service
{
    /// <summary>
    /// User service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get user by domain logon
        /// </summary>
        /// <param name="fullDomainLogon"></param>
        /// <param name="activeAndUnlockedOnly"></param>
        /// <returns></returns>
        SingularUser GetUserByDomainLogon(string fullDomainLogon, bool activeAndUnlockedOnly = true);

        /// <summary>
        /// Get user by domain logon
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="activeAndUnlockedOnly"></param>
        /// <returns></returns>
        SingularUser GetUserByActiveDirectoryLogon(string identity, bool activeAndUnlockedOnly = true);

        /// <summary>
        /// Get user by domain logon
        /// </summary>
        /// <param name="email"></param>
        /// <param name="activeAndUnlockedOnly"></param>
        /// <returns></returns>
        SingularUser GetUserByEmail(string email, bool activeAndUnlockedOnly = true);
    }
}
