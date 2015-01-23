using System;
using System.Linq;
using Singular.Core.Context;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Repository;
using Singular.Core.Data.Transaction;
using Singular.Core.Encryption;
using Singular.Modules.Core.Data.Configuration;
using Singular.Modules.Core.Data.Models;

namespace Singular.Modules.Core.Data.Service
{
    /// <summary>
    /// Forms auth service
    /// </summary>
    public class FormsAuthenticationService : IAuthenticationService
    {
        // fields
        private readonly IRepository<SingularUser> _repo;
        private readonly IUnitOfWork _uow;
        private readonly EncryptionHelper _helper;
        private readonly ITranslationService _translationService;
        private readonly ISingularContext _ctx;
        private readonly SingularFormsAuthenticationConfiguration _config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="uow"></param>
        /// <param name="helper"></param>
        /// <param name="translationService"></param>
        /// <param name="ctx"></param>
        public FormsAuthenticationService(IRepository<SingularUser> repo, IUnitOfWork uow, EncryptionHelper helper, ITranslationService translationService, ISingularContext ctx, SingularFormsAuthenticationConfiguration config)
        {
            _repo = repo;
            _uow = uow;
            _helper = helper;
            _translationService = translationService;
            _ctx = ctx;
            _config = config;
        }

        /// <summary>
        /// Check login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public TransactionResult<FormsAuthModel> CheckLogin(string email, string password, out SingularUser user)
        {
            var res = new TransactionResult<FormsAuthModel>();

            if (_ctx.IsAuthenticated)
            {
                res.AddError(_translationService.GetTranslation("A user is already logged in on this device. Please log out first."));
                user = default(SingularUser);
                return res;
            }
            var encryptedPassword = _helper.EncryptToString(password);
            user =
                _repo
                    .Entities
                    .FirstOrDefault(x =>
                        x.Email.ToLower() == email.ToLower());
            
            if (user == null || !user.IsActive || user.IsLockedOut)
            {
                res.AddError(_translationService.GetTranslation("Your logon details were not recognised"));
            }
            else if (user.EncryptedFormsAuthPassword != encryptedPassword)
            {
                res.AddError(_translationService.GetTranslation("Your logon details were not recognised"));
                user.FailedLoginAttempts++;
                user.LastFailedLoginAttempt = DateTime.UtcNow;
                if (user.FailedLoginAttempts > _config.NumberOfAllowedFailedPasswordAttempts)
                {
                    user.IsLockedOut = true;
                }
            }
            

            if (res.Success)
            {
                // ReSharper disable once PossibleNullReferenceException
                user.IsActive = true;
                user.IsLockedOut = false;
                user.LastLogin = DateTime.UtcNow;
            }

            var commitResult = _uow.Commit();
            if (!commitResult.Success)
            {
                // ToDO: LOG FAILED LOGINS?
                //foreach (var error in commitResult.Errors)
                //{
                //    res.AddError(error);
                //}
            }


            return res;
        }
    }
}
