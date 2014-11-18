using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.Section
{
    public class Section
    {
        public int Order { get; set; }
        public Func<bool> IsActive { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public object RouteValues { get; set; }
        public string AreaName { get; set; }
        public string ImageVirtualPath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

