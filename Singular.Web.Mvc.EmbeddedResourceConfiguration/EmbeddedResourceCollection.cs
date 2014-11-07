using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.EmbeddedResourceConfiguration
{
    public class EmbeddedResourceCollection  :IEmbeddedResourceCollection
    {
        public EmbeddedResourceCollection(object objectFromAssembly, string defaultNamespace, string areaName)
        {
            Files = new List<string>();
            Folders  = new List<string>();
            AssemblyObject = objectFromAssembly;
            Assembly = objectFromAssembly.GetType().Assembly;
            DefaultNamespace = defaultNamespace;
            AreaName = areaName;
        }

        public object AssemblyObject { get; private set; }

        public IEmbeddedResourceCollection SetRouteFolder(string fullPath)
        {
            RouteFolder = fullPath;
            return this;
        }

        public IEmbeddedResourceCollection SetProjectFileName(string fileName)
        {
            ProjectFileName = fileName;
            return this;
        }

        public IEmbeddedResourceCollection AddFolder(string folder)
        {
            Folders.Add(folder);
            return this;
        }

        public IEmbeddedResourceCollection AddFile(string file)
        {
            if (file.ToLower().Contains(".cshtml") || file.ToLower().Contains(".vbhtml"))
            {
                throw new Exception("Cannot add .cshtml or .vbhtml. RazorGenerator will take care of that!");
            }
            Files.Add(file);
            return this;
        }

        public ICollection<string> Folders { get;  set; }
        public ICollection<string> Files { get;  set; }
        public string RouteFolder { get; private set; }
        public Assembly Assembly { get; private set; }
        public string ProjectFileName { get; private set; }
        public string DefaultNamespace { get; private set; }
        public string AreaName { get; private set; }
    }
}
