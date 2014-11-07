using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Singular.Modules.Core.Controllers
{
    public static class CoreControllerBaseExtensions
    {
        public static string GetViewPath(this CoreControllerBase ctrl, string virtualPath)
        {
            return "~" + ctrl.ResourceManager.GetPath(ctrl.ControllerContext.RouteData.DataTokens["area"].ToString(),
                virtualPath);
        }
    }
}