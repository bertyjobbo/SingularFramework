using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Useful
{
    public static class ExceptionExtensions
    {
        public static string MessageIncludingInnerException(this Exception ex)
        {
            if (ex == null) return null;
            return ex.Message + (ex.InnerException == null ? "" : " (Inner Exception: " + ex.InnerException.Message);
        }
    }
}
