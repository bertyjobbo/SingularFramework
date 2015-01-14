using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Singular.Useful;

namespace Singular.Web.Mvc.Authentication
{
    public class SingularNgViewAuthorizeAttribute: SingularAuthorizeAttribute
    {
        public SingularNgViewAuthorizeAttribute() : base()
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
