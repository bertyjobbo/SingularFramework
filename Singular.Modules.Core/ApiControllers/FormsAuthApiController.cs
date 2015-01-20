using System.Threading;
using System.Web.Http;
using Singular.Core.Authentication;
using Singular.Core.Context;
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
        public FormsAuthApiController(ISingularContext ctx) : base(ctx)
        {
            _authService = ctx.GetService<IAuthenticationService>();
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
            var result = _authService.Login(model.Email, model.Password);
            return result;
        }
    }
}