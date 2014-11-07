using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Singular.Web.Mvc.EmbeddedResourceConfiguration
{
    public interface IEmbeddedResourceManager
    {
        bool BuildResources(ref string msg);
        void LoadResources();
        IEmbeddedResourceCollection CreateCollection(object objectFromAssembly, string defaultNamespace, string areaName);
        IList<CachedEmbeddedResourceCollection> Cache { get; }
        string GetPath(object assemblyObject, string assemblyfiedPath);
        string GetPath(CachedEmbeddedResourceCollection collection, string virtualPath);
        string GetPath(string area, string virtualPath);
        string GetPath(string area, string virtualPath, out Assembly assembly, out string assemblifiedName);
        string GetPath(string area, string virtualPath, out Assembly assembly, out string assemblifiedName, out Type type);
        string GetPath(CachedEmbeddedResourceCollection collection, string virtualPath, out string assemblifiedName);
        IEmbeddedResourceManager SetSolutionPath(string path);
        string SolutionPath { get; }
        IEmbeddedResourceManager SetMsBuildPath(string path);
        string MsBuildPath { get; }
        bool FileIsInCache(string area, string virtualPath);
        bool FileIsInCache(string area, string virtualPath, out string axdPath);
        IEmbeddedResourceManager SetRazorGeneratorPsScriptPath(string path);
        string RazorGeneratorPsScriptPath { get; }
    }
}