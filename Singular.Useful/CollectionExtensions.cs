using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Useful
{
    public static class CollectionExtensions
    {
        public static bool HasItems<T>(this ICollection<T> collection)
        {
            return collection != null && collection.Count > 0;
        }

        public static bool HasNoItems<T>(this ICollection<T> collection)
        {
            return !HasItems(collection);
        }

        public static bool HasItems<T>(this IList<T> list)
        {
            return list != null && list.Count > 0;
        }

        public static bool HasNoItems<T>(this IList<T> list)
        {
            return !HasItems(list);
        }
    }
}
