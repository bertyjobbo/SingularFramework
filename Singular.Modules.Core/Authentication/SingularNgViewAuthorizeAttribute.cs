using System.Web;
using System.Web.Mvc;
using Singular.Useful;
using Singular.Web.Mvc.Authentication;

namespace Singular.Modules.Core.Authentication
{
    public class SingularNgViewAuthorizeAttribute : SingularAuthorizeAttribute
    {
        public SingularNgViewAuthorizeAttribute()
        {
        }

        public SingularNgViewAuthorizeAttribute(string modules) : base(modules)
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                var resp = httpContext.Response;
                resp.BufferOutput = true;
                resp.ContentType = "application/json";
                resp.Write(new HttpException(403, "Access Denied").ToJson());
                resp.StatusCode = 403;
                resp.Flush();
                resp.End();
                return false;
            }

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // do nothing
        }
    }
}