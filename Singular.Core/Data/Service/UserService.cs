using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Enums;
using Singular.Core.Data.Repository;
using Singular.Core.Data.Transaction;

namespace Singular.Core.Data.Service
{
    /// <summary>
    /// User service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<SingularUser> _repo;
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// User service
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="uow"></param>
        public UserService(IRepository<SingularUser> repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        /// <summary>
        /// Get user by domain logon
        /// </summary>
        /// <param name="fullDomainLogon"></param>
        /// <returns></returns>
        public SingularUser GetUserByDomainLogon(string fullDomainLogon)
        {
            var user =
                _repo.EntitiesReadOnly.FirstOrDefault(
                    x =>
                        (x.Domain + "\\" + x.DomainUsername).ToLower() == fullDomainLogon.ToLower() &&
                        x.AuthenticationType == AuthenticationType.Domain);

            
            return user;
        }
    }
}
