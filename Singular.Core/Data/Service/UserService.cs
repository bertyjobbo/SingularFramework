using System.Linq;
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
        /// <param name="activeAndUnlockedOnly"></param>
        /// <returns></returns>
        public SingularUser GetUserByDomainLogon(string fullDomainLogon, bool activeAndUnlockedOnly = true)
        {
            return getUser(fullDomainLogon, AuthenticationType.Domain, activeAndUnlockedOnly); 
        }

        /// <summary>
        /// Get user by domain logon
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="activeAndUnlockedOnly"></param>
        /// <returns></returns>
        public SingularUser GetUserByActiveDirectoryLogon(string identity, bool activeAndUnlockedOnly = true)
        {
            return getUser(identity, AuthenticationType.ActiveDirectory, activeAndUnlockedOnly);
        }

        /// <summary>
        /// Get user by domain logon
        /// </summary>
        /// <param name="email"></param>
        /// <param name="activeAndUnlockedOnly"></param>
        /// <returns></returns>
        public SingularUser GetUserByEmail(string email, bool activeAndUnlockedOnly = true)
        {
           return getUser(email, AuthenticationType.Forms, activeAndUnlockedOnly);
        }

        private SingularUser getUser(string identity, AuthenticationType authType, bool activeAndUnlockedOnly)
        {
            var active = activeAndUnlockedOnly;
            var locked = !activeAndUnlockedOnly;
            SingularUser user;


            switch (authType)
            {
                case AuthenticationType.ActiveDirectory:
                {
                    user = _repo.Entities.FirstOrDefault(x => x.DomainUsername.ToLower() == identity.ToLower());
                    break;
                }
                case AuthenticationType.Domain:
                {
                    user = _repo.Entities.FirstOrDefault(x=>(x.Domain.ToLower() + "\\" + x.DomainUsername.ToLower()) == identity.ToLower());
                    break;
                }
                default:
                {
                    user = _repo.Entities.FirstOrDefault(x => x.Email.ToLower() == identity.ToLower());
                    break;
                }
                
            }

            if (user != null && activeAndUnlockedOnly && (user.IsLockedOut || !user.IsActive))
            {
                return default(SingularUser);
            }
            
            return user;
        }
    }
}
