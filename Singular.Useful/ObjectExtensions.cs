using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Singular.Useful
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static string ToJson(this object obj)
        {
            if (obj == null) return "";
            return JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(this string str)
        {
            if (str.HasNoValue()) return default (T);
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
