using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.EmbeddedResourceConfiguration
{
    public interface IEmbeddedResourceCollection
    {
        IEmbeddedResourceCollection SetRouteFolder(string fullPath);
        IEmbeddedResourceCollection SetProjectFileName(string fileName);
        IEmbeddedResourceCollection AddFolder(string folder);
        IEmbeddedResourceCollection AddFile(string file);
        ICollection<string> Folders { get; set; }
        ICollection<string> Files { get; set; }
        string RouteFolder { get; }
        Assembly Assembly { get; }
        string ProjectFileName { get; }
        string DefaultNamespace { get; }
        string AreaName { get;}
        object AssemblyObject { get; }
    }
}
