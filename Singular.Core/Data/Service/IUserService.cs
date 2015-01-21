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
        /// Get by domain logon
        /// </summary>
        /// <param name="fullDomainLogon"></param>
        /// <returns></returns>
        SingularUser GetUserByDomainLogon(string fullDomainLogon);
    }
}
