using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Singular.Core.Context;
using Singular.Modules.Core.Authentication;
using Singular.Modules.Core.HtmlExtensions;
using Singular.Modules.Core.ViewModels;

namespace Singular.Modules.Core.Controllers
{
    /// <summary>
    ///     NgView controller - ties in with "Html.AltView"
    /// </summary>
    public class NgViewController : CoreControllerBaseNoAuth
    {
        private readonly Type _typeOfViewModelBase = typeof (CoreViewModelBase);

        public NgViewController(ISingularContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        ///     Un authenticated
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="c"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string folder, string c, string a)
        {
            return indexPrivate(folder, c, a, "Index");
        }

        [SingularNgViewAuthorize]
        [HttpGet]
        public ActionResult IndexAuthenticated(string folder, string c, string a)
        {
            return indexPrivate(folder, c, a, "IndexAuthenticated");
        }

        private ActionResult indexPrivate(string folder, string c, string a, string action)
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
                    AltNgExtensions.ViewModelDictionary.Add(file, typeof (Dummy_Ng_View_Class));
                    return RedirectToAction(action, new {folder, c, a});
                }
                catch (InvalidOperationException ex)
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
                        if (typeMatches.Count > 1)
                        {
                            throw new Exception(string.Format("Type {0} appears more than once in these assemblies."));
                        }
                        if (typeMatches.Count == 1)
                        {
                            type = typeMatches[0];
                        }
                    }
                }

                AltNgExtensions.ViewModelDictionary.Add(file, type ?? typeof (Dummy_Ng_View_Class));
            }
            else
            {
                type = AltNgExtensions.ViewModelDictionary[file];
            }

            if (type == null || type == typeof (Dummy_Ng_View_Class))
            {
                return View(file);
            }

            var finalModel =
                type.IsSubclassOf(_typeOfViewModelBase)
                    ? Activator.CreateInstance(type, SingularContext)
                    : Activator.CreateInstance(type);

            return View(file, finalModel);
        }
    }

    internal class Dummy_Ng_View_Class
    {
    }
}