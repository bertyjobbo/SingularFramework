using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Useful
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid guid)
        {
            return guid == _emptyGuid;
        }

        public static bool IsNotEmpty(this Guid guid)
        {
            return !IsEmpty(guid);
        }

        private static readonly Guid _emptyGuid = Guid.Empty;
    }
}
