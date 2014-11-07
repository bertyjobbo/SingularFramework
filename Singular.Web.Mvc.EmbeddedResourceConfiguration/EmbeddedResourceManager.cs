using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Singular.Useful;

namespace Singular.Web.Mvc.EmbeddedResourceConfiguration
{
    public class EmbeddedResourceManager : IEmbeddedResourceManager
    {
        #region singleton
        private EmbeddedResourceManager()
        {
            _collections = new List<IEmbeddedResourceCollection>();

        }
        private static IEmbeddedResourceManager _current;

        /// <summary>
        /// Current instance
        /// </summary>
        public static IEmbeddedResourceManager Current
        {
            get
            {
                return _current ?? (_current = new EmbeddedResourceManager());
            }
        }
        #endregion


        // fields/methods
        private readonly ICollection<IEmbeddedResourceCollection> _collections;
        private bool processCollection(IEmbeddedResourceCollection collection, ref string message, bool changeFiles)
        {
            try
            {
                /* NUGET - RAZOR GENERATOR */
                //if (changeFiles && !string.IsNullOrWhiteSpace(RazorGeneratorPsScriptPath))
                //{

                //    using (var ps = PowerShell.Create())
                //    {
                //        ps.AddScript(RazorGeneratorPsScriptPath);
                //        ps.AddParameter("$installPath", "C:\temp");
                //        ps.AddParameter("$toolsPath", RazorGeneratorPsScriptPath.Split('\\').Last());
                //        ps.AddParameter("$package", "RazorGenerator.Mvc");
                //        var projPath = Path.Combine(collection.RouteFolder, collection.ProjectFileName);
                //        ps.AddParameter("$project", projPath);
                //        ps.Invoke();
                //        ps.AddCommand("Enable-RazorGenerator");
                //        ps.AddCommand("Redo-RazorGenerator");
                //        ps.Invoke();
                //    }

                //}

                // create new collection
                var cachedCollection = new CachedEmbeddedResourceCollection(collection);

                // sort out files
                foreach (var webFolder in cachedCollection.InnerCollection.Folders)
                {
                    // get folder
                    var folderPath = Path.Combine(cachedCollection.InnerCollection.RouteFolder, webFolder.Replace("~/", "").Replace("/", "\\"));
                    var dir = new DirectoryInfo(folderPath);
                    var dirFiles = dir.GetFiles();

                    // loop files and add
                    foreach (var fileInfo in dirFiles)
                    {
                        if (fileInfo.Extension == ".cshtml" || fileInfo.Extension == ".vbhtml")
                        {
                            throw new Exception("Cannot add .cshtml or .vbhtml. RazorGenerator will take care of that!");
                        }

                        cachedCollection
                            .InnerCollection
                            .Files
                            .Add(
                                "~/" +
                                (fileInfo.FullName
                                    .Replace(cachedCollection.InnerCollection.RouteFolder, "")
                                    .TrimStart('\\')
                                    .Replace("\\", "/")
                                )
                            );
                    }

                }

                if (changeFiles)
                {
                    // now do csproj file
                    changeCsProjFile(cachedCollection);

                    // now do assembly info
                    changeAssemblyInfo(cachedCollection);
                }

                // finally
                cachedCollection.InnerCollection.Files =
                    cachedCollection
                    .InnerCollection
                    .Files
                    .Distinct()
                    .ToList();
                Cache.Add(cachedCollection);

                //
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        private const string start_of_assembly_info_marker = "/* SINGULAR AUTO STUFF - LEAVE LINE ABOVE AND BELOW */";
        private const string pre_web_resource_marker = "/* WEBRESOURCE_AUTO_GENERATED_BY_SINGULAR */";
        private void changeAssemblyInfo(CachedEmbeddedResourceCollection cachedCollection)
        {
            // get assembly info
            var assemblyInfoPath = Path.Combine(cachedCollection.InnerCollection.RouteFolder,
                "Properties\\AssemblyInfo.cs");

            // collections
            var fileLines = File.ReadAllLines(assemblyInfoPath);
            var newFileLines = new List<string>();
            var lineNumbersToRemove = new List<int>();

            // loop through existing lines and put line numbers in to remove
            for (var i = 0; i < fileLines.Length; i++)
            {
                var line = fileLines[i];
                if (line.Contains(start_of_assembly_info_marker) || string.IsNullOrWhiteSpace(line))
                {
                    lineNumbersToRemove.Add(i);
                }
                if (line.Contains(pre_web_resource_marker))
                {
                    lineNumbersToRemove.Add(i);
                    lineNumbersToRemove.Add(i + 1);
                }
            }

            // add basic lines back
            for (var i = 0; i < fileLines.Length; i++)
            {
                var line = fileLines[i];
                if (!lineNumbersToRemove.Contains(i))
                {
                    newFileLines.Add(line);
                }
            }

            // add new lines
            newFileLines.Add("");
            newFileLines.Add(start_of_assembly_info_marker);
            newFileLines.Add("");
            for (var i = 0; i < cachedCollection.InnerCollection.Files.Count; i++)
            {
                // get file
                var fileToAddOriginal = ((IList<string>)cachedCollection.InnerCollection.Files)[i];

                // change
                var finalString =
                    cachedCollection.InnerCollection.DefaultNamespace + "." +
                    fileToAddOriginal.Replace("~/", "").Replace("/", ".");

                // mimetype
                var outputMime = MimeHelper.GetMimeFromDotlessExtension(finalString.Split('.').Last().ToLower());

                // add web resource
                newFileLines.Add(pre_web_resource_marker);
                newFileLines.Add(string.Format("[assembly: System.Web.UI.WebResource(\"{0}\", \"{1}\")]", finalString, outputMime));


            }

            // save
            File.WriteAllLines(assemblyInfoPath, newFileLines);

        }
        private void changeCsProjFile(CachedEmbeddedResourceCollection cachedCollection)
        {
            // load file
            var xmlDocPath = Path.Combine(cachedCollection.InnerCollection.RouteFolder, cachedCollection.InnerCollection.ProjectFileName);
            var allLines = System.IO.File.ReadAllLines(xmlDocPath).ToList();

            // lines to add after collection
            var contentFoundLines = new List<Tuple<int, string>>();
            var contentRemoveLineNumbers = new List<int>();

            // get includes
            var includesToAddToCsProj = cachedCollection.InnerCollection.Files.Select(x => x.Replace("~/", "").Replace("/", "\\")).ToList();

            // loop
            for (var i = 0; i < allLines.Count; i++)
            {
                var line = allLines[i];

                // loop includes
                foreach (var inc in includesToAddToCsProj)
                {
                    if (line.ToLower().Contains("<Content".ToLower()) && line.ToLower().Contains("Include=\"".ToLower() + inc.ToLower()))
                    {
                        contentFoundLines.Add(new Tuple<int, string>(i, inc));
                        contentRemoveLineNumbers.Add(i);
                        if (allLines[i + 1].ToLower().Contains("DependentUpon".ToLower()))
                        {
                            contentRemoveLineNumbers.Add(i + 1);
                            contentRemoveLineNumbers.Add(i + 2);
                        }
                    }
                    if (line.ToLower().Contains("<EmbeddedResource".ToLower()) && line.ToLower().Contains("Include=\"".ToLower() + inc.ToLower()))
                    {
                        contentRemoveLineNumbers.Add(i);
                        contentFoundLines.Add(new Tuple<int, string>(i, inc));
                    }
                }
            }


            // loop
            var finalLines = new List<string>();
            for (var i = 0; i < allLines.Count; i++)
            {
                var line = allLines[i];
                var foundTuple = contentFoundLines.FirstOrDefault(x => x.Item1 == i);
                if (foundTuple != null)
                {
                    finalLines.Add("    <EmbeddedResource Include=\"" + foundTuple.Item2 + "\" />");
                }

                if (!contentRemoveLineNumbers.Contains(i))
                {

                    finalLines.Add(line);
                }
            }

            // write
            System.IO.File.WriteAllLines(xmlDocPath, finalLines);
        }
        private bool buildResources(ref string msg, bool changeFiles)
        {
            // loop collections
            foreach (var coll in _collections)
            {
                var output = processCollection(coll, ref msg, changeFiles);
                if (!output)
                {
                    return false;
                }
            }

            if (changeFiles)
            {
                try
                {
                    var command = string.Format("\"{0}\" /t:rebuild", SolutionPath);
                    var process = new Process();
                    var startInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Normal,
                        FileName = MsBuildPath,
                        Arguments = command
                    };
                    process.StartInfo = startInfo;
                    process.Start();
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return false;
                }
            }
            //HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceVirtualPathProvider());
            return true;
        }
        private readonly Page _page = new Page();

        /// <summary>
        /// Load resources
        /// </summary>
        public void LoadResources()
        {
            Cache = new List<CachedEmbeddedResourceCollection>();
            var msg = "";
            var ok = buildResources(ref msg, false);
            if (!ok) throw new Exception("Error Loading Resources at EmbeddedResourceManger.LoadResources: " + msg);
        }

        /// <summary>
        /// Configurre
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool BuildResources(ref string msg)
        {
            if (string.IsNullOrWhiteSpace(SolutionPath)) throw new Exception("SetSolutionPath in EmbeddedResourceManager");
            if (string.IsNullOrWhiteSpace(MsBuildPath)) throw new Exception("SetMsBuildPath in EmbeddedResourceManager");
            Cache = new List<CachedEmbeddedResourceCollection>();
            return buildResources(ref msg, true);
        }

        /// <summary>
        /// Create collection
        /// </summary>
        /// <param name="objectFromAssembly"></param>
        /// <param name="defaultNamespace"></param>
        /// <param name="areaName"></param>
        /// <returns></returns>
        public IEmbeddedResourceCollection CreateCollection(object objectFromAssembly, string defaultNamespace, string areaName)
        {


            var coll = new EmbeddedResourceCollection(objectFromAssembly, defaultNamespace, areaName);

            var found = _collections.FirstOrDefault(x => x.Assembly.FullName == coll.Assembly.FullName);
            if (found != null) _collections.Remove(found);

            _collections.Add(coll);
            return coll;
        }

        /// <summary>
        /// The cache
        /// </summary>
        public IList<CachedEmbeddedResourceCollection> Cache { get; private set; }

        /// <summary>
        /// Get path
        /// </summary>
        /// <param name="assemblyObject"></param>
        /// <param name="namespacedPath"></param>
        /// <returns></returns>
        public string GetPath(object assemblyObject, string namespacedPath)
        {
            return _page.ClientScript.GetWebResourceUrl(assemblyObject.GetType(), namespacedPath);
        }

        /// <summary>
        /// Get path
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string GetPath(CachedEmbeddedResourceCollection collection, string virtualPath)
        {
            return GetPath(collection.InnerCollection.AssemblyObject, string.Format("{0}.{1}", collection.InnerCollection.DefaultNamespace,
                virtualPath.Replace("~/", "").Replace("/", ".")));
        }

       /// <summary>
        /// Get path
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="virtualPath"></param>
        /// <param name="assemblifiedName"></param>
        /// <returns></returns>
        public string GetPath(CachedEmbeddedResourceCollection collection, string virtualPath, out string assemblifiedName)
        {
            assemblifiedName = string.Format("{0}.{1}", collection.InnerCollection.DefaultNamespace,
                virtualPath.Replace("~/", "").Replace("/", "."));
            return
                GetPath(collection.InnerCollection.AssemblyObject, assemblifiedName);
        }
        
        /// <summary>
        /// Get path
        /// </summary>
        /// <param name="area"></param>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string GetPath(string area, string virtualPath)
        {
            var cachedColl = Cache.FirstOrDefault(x => x.InnerCollection.AreaName.ToLower() == area.ToLower());
            if (cachedColl == null) return null;
            return GetPath(cachedColl, virtualPath);
        }

        /// <summary>
        /// Get path
        /// </summary>
        /// <param name="area"></param>
        /// <param name="virtualPath"></param>
        /// <param name="assembly"></param>
        /// <param name="assemblifiedName"></param>
        /// <returns></returns>
        public string GetPath(string area, string virtualPath, out Assembly assembly, out string assemblifiedName)
        {
            var cachedColl = Cache.FirstOrDefault(x => x.InnerCollection.AreaName.ToLower() == area.ToLower());
            if (cachedColl == null)
            {
                assembly = null;
                assemblifiedName = null;
                return null;
            }
            assembly = cachedColl.InnerCollection.Assembly;
            return GetPath(cachedColl, virtualPath, out assemblifiedName);
        }

        /// <summary>
        /// Get path
        /// </summary>
        /// <param name="area"></param>
        /// <param name="virtualPath"></param>
        /// <param name="assembly"></param>
        /// <param name="assemblifiedName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPath(string area, string virtualPath, out Assembly assembly, out string assemblifiedName, out Type type)
        {
            var cachedColl = Cache.FirstOrDefault(x => x.InnerCollection.AreaName.ToLower() == area.ToLower());
            if (cachedColl == null)
            {
                assembly = null;
                assemblifiedName = null;
                type = null;
                return null;
            }
            assembly = cachedColl.InnerCollection.Assembly;
            type = cachedColl.InnerCollection.AssemblyObject.GetType();
            return GetPath(cachedColl, virtualPath, out assemblifiedName);
        }

        /// <summary>
        /// Set solution path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEmbeddedResourceManager SetSolutionPath(string path)
        {
            SolutionPath = path;
            return this;
        }

        /// <summary>
        /// Solution path
        /// </summary>
        public string SolutionPath { get; private set; }

        /// <summary>
        /// Set path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEmbeddedResourceManager SetMsBuildPath(string path)
        {
            MsBuildPath = path;
            return this;
        }

        /// <summary>
        /// Path
        /// </summary>
        public string MsBuildPath { get; private set; }

        /// <summary>
        /// True if true
        /// </summary>
        /// <param name="area"></param>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public bool FileIsInCache(string area, string virtualPath)
        {
            var collection = Cache.FirstOrDefault(x => x.InnerCollection.AreaName == area);
            if(collection==null) return false;

            return collection.InnerCollection.Files.Any(x => x == virtualPath);
        }

        /// <summary>
        /// True if true
        /// </summary>
        /// <param name="area"></param>
        /// <param name="virtualPath"></param>
        /// <param name="axdPath"></param>
        /// <returns></returns>
        public bool FileIsInCache(string area, string virtualPath, out string axdPath)
        {
            var collection = Cache.FirstOrDefault(x => x.InnerCollection.AreaName == area);
            if (collection == null)
            {
                axdPath = null;
                return false;
            }
            
            var output = collection.InnerCollection.Files.Any(x => x == virtualPath);
            if (output)
            {
                axdPath = GetPath(collection, virtualPath);
                return true;
            }

            axdPath = null;
            return false;
        }

        /// <summary>
        /// Set nuget path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEmbeddedResourceManager SetRazorGeneratorPsScriptPath(string path)
        {
            RazorGeneratorPsScriptPath = path;
            return this;
        }

        public string RazorGeneratorPsScriptPath { get; private set; }
    }
}