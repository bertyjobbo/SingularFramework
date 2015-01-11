using System;
using System.Threading;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Useful;

namespace Singular.Modules.Core.Controllers
{
    public class NgViewController: CoreControllerBaseNoAuth
    {
        public NgViewController(ISingularContext ctx) : base(ctx)
        {
            
        }

        public ActionResult Index(string folder, string c, string a, string model)
        {
            object finalModel = null;
            if (model.HasValue())
            {
                var type = Type.GetType(model);
                finalModel = Activator.CreateInstance(type, SingularContext);
            }
            
            if (folder.EndsWith("/"))
            {
               folder= folder.TrimEnd('/');
            }


            return View(folder + "/NgView-" + c + "-" + a + ".cshtml", finalModel);
        }
    }
}