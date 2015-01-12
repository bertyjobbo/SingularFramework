using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Useful
{
    public static class KeyValuePairExtensions
    {
        public static bool IsKeyValuePairNull<T1,T2>(this KeyValuePair<T1, T2> kv)
        {
            return kv.Equals(default(KeyValuePair<T1, T2>));
        }
    }
}
