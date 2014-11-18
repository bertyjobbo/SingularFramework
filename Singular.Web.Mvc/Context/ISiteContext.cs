using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.Context
{
    public interface ISiteContext
    {
        ISiteContext OverrideBrandView(string viewPath);
        string BrandViewPath { get; }
        ISiteContext OverrideHomepageView(string viewPath);
        string HomepageViewPath { get; }
        ISiteContext SetSiteName(string name);
        string SiteName { get; }
    }
}
