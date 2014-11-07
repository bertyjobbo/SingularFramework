using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.EmbeddedResourceConfiguration
{
    public class CachedEmbeddedResourceCollection
    {
        public CachedEmbeddedResourceCollection(IEmbeddedResourceCollection collection)
        {
            InnerCollection = collection;
        }

        public IEmbeddedResourceCollection InnerCollection { get; private set; }
    }
}
