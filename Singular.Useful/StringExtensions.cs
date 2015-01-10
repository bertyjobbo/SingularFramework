using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Useful
{
    public static class StringExtensions
    {
        public static bool HasValue(this string str, bool whitespaceCountsAsEmpty = true)
        {
            return whitespaceCountsAsEmpty ? !string.IsNullOrWhiteSpace(str) : !string.IsNullOrEmpty(str);
        }

        public static bool HasNoValue(this string str, bool whitespaceCountsAsEmpty = true)
        {
            return !HasValue(str, whitespaceCountsAsEmpty);
        }
    }
}
