using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Core.Data.Service;
using Singular.Web.Mvc.Context;

namespace Singular.Web.Mvc.Authentication
{
    public class SingularAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly ISingularContext _ctx = SingularMvcContext.Current;

        public SingularAuthorizeAttribute()
        {
            Modules = new List<string>();
        }

        public SingularAuthorizeAttribute(string modules)
        {
            Modules = modules.Contains(",") ? modules.Split(',').ToList() : new List<string>(1) { modules };
        }

        public IList<string> Modules { get; private set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // ping session first to make sure
            var ping = _ctx.Session;

            // check
            if (!_ctx.IsAuthenticated) return false;
            // get service
            var service = _ctx.GetService<IPermissionService>();
            // check service
            if (service == null) return false;
            // go
            return service.UserIsAllowed(_ctx.CurrentUser.Id,
                Users.Contains(",") ? Users.Split(',').ToList() : new List<string>(1) { Users },
                Roles.Contains(",") ? Roles.Split(',').ToList() : new List<string>(1) { Roles },
                Modules);

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect(VirtualPathUtility.ToAppRelative("~/Singular/Core/Sys/AccessDenied/"), true);
                return;
            }

            filterContext.HttpContext.Response.Redirect(VirtualPathUtility.ToAppRelative("~/Singular/Core/FormsAuth/#/Login/?returnUrl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()) + "&canlogin=1&isAccessDenied=false"), true);

        }
    }
}