using System;
using System.Threading;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Modules.Core.HtmlExtensions;
using Singular.Useful;

namespace Singular.Modules.Core.Controllers
{
    public class NgViewController: CoreControllerBaseNoAuth
    {
        public NgViewController(ISingularContext ctx) : base(ctx)
        {
            
        }

        public ActionResult Index(string folder, string c, string a, Guid guid)
        {
            Thread.Sleep(2000);
            
            object finalModel = null;

            if (guid.IsNotEmpty() && AltNgExtensions.ViewModelDictionary.ContainsKey(guid))
            {
                var type = AltNgExtensions.ViewModelDictionary[guid];
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