using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.HtmlExtensions
{
    public enum IncludeBehaviour
    {
        None=0,
        MinifyWhenNotDebug,
        MinifyAlways
    }
}
