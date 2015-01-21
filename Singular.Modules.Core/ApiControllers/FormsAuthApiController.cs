using System.Threading;
using System.Web.Http;
using System.Web.Security;
using Singular.Core.Authentication;
using Singular.Core.Context;
using Singular.Core.Data.Entities;
using Singular.Core.Data.Transaction;
using Singular.Modules.Core.Data.Models;
using Singular.Modules.Core.Data.Services;

namespace Singular.Modules.Core.ApiControllers
{
    /// <summary>
    ///     Forms auth controller
    /// </summary>
    [RoutePrefix("SingularApi/Core/FormsAuth")]
    public class FormsAuthApiController : CoreApiControllerBaseNoAuth
    {
        // fields
        private readonly IAuthenticationService _authService;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="authService"></param>
        public FormsAuthApiController(ISingularContext ctx, IAuthenticationService authService) : base(ctx)
        {
            _authService = authService;
        }

        /// <summary>
        ///     Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public TransactionResult<FormsAuthModel> Login(FormsAuthModel model)
        {
            SingularUser user;
            var result = _authService.CheckLogin(model.Email, model.Password, out user);
            if (result.Success)
            {
                // login
                FormsAuthentication.SetAuthCookie(user.Email, model.RememberMe);
            }
            return result;
        }
    }
}