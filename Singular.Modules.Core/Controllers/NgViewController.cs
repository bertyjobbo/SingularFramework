using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Modules.Core.HtmlExtensions;
using Singular.Modules.Core.ViewModels;
using Singular.Useful;

namespace Singular.Modules.Core.Controllers
{
    public class NgViewController: CoreControllerBaseNoAuth
    {
        private readonly Type _typeOfViewModelBase = typeof (CoreViewModelBase);
        public NgViewController(ISingularContext ctx) : base(ctx)
        {
            
        }

        public ActionResult Index(string folder, string c, string a)
        {
            var file = folder + "/NgView-" + c + "-" + a + ".cshtml";
            Type type = null;
            if (!AltNgExtensions.ViewModelDictionary.ContainsKey(file))
            {
                try
                {
                    // try executing
                    var view = View(file, new Dummy_Ng_View_Class());
                    view.ExecuteResult(ControllerContext);

                    // it's fine so need to put in dictionary and redirect
                    AltNgExtensions.ViewModelDictionary.Add(file,typeof(Dummy_Ng_View_Class));
                    return RedirectToAction("Index", new {folder = folder, c = c, a = a});

                }
                catch(InvalidOperationException ex)
                {
                    // this means it has a model
                    var msg = ex.Message;
                    var reg = new Regex("requires a model item of type '(.*)'", RegexOptions.IgnoreCase);
                    var match = reg.Match(msg);
                    if (match.Groups.Count > 1)
                    {
                        var typeName = match.Groups[1].Value;
                        var types = SingularContext.Types;
                        
                        var typeMatches = 
                            types
                            .Where(x => x.FullName == typeName)
                            .ToList();
                        if (typeMatches.Count > 1) {  throw new Exception(string.Format("Type {0} appears more than once in these assemblies."));}
                        if (typeMatches.Count == 1)
                        {
                            type = typeMatches[0];
                        }
                    }
                }

                AltNgExtensions.ViewModelDictionary.Add(file, type ?? typeof(Dummy_Ng_View_Class));
            }
            else
            {
                type = AltNgExtensions.ViewModelDictionary[file];
            }

            if (type == null || type == typeof(Dummy_Ng_View_Class))
            {
                return View(file);
            }

            var finalModel = 
                    type.IsSubclassOf(_typeOfViewModelBase) ? 
                        Activator.CreateInstance(type, SingularContext) : 
                        Activator.CreateInstance(type);

            return View(file, finalModel);


        }
    }

    class Dummy_Ng_View_Class
    {
        
    }
}