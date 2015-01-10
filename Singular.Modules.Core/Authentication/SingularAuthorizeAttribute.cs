using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Web.Mvc.Context;

namespace Singular.Modules.Core.Authentication
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
            return _ctx.UserIsAllowed(
                Users.Contains(",") ? Users.Split(',').ToList() : new List<string>(1){ Users },
                Roles.Contains(",") ? Roles.Split(',').ToList() : new List<string>(1) { Roles }, 
                Modules);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect(VirtualPathUtility.ToAppRelative("~/Singular/Core/Users/AccessDenied"),true);
                return;
            }
            
            filterContext.HttpContext.Response.Redirect(VirtualPathUtility.ToAppRelative("~/Singular/Core/FormsAuth"),true);
            
        }
    }
}