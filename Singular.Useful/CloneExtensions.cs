using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Useful
{
    public static class CloneExtensions
    {
        public static T DeepClone<T>(this T orig)
        {
            var json = orig.ToJson();
            var output = json.FromJson<T>();
            return output;
        }
    }
}
