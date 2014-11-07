using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using System.Web.UI;

namespace Singular.Web.Mvc.HtmlExtensions
{
	public static class EmbeddedContentExtensions
	{
		private readonly static Page _page;

		static EmbeddedContentExtensions()
		{
			EmbeddedContentExtensions._page = new Page();
		}

		public static string EmbeddedContent(this UrlHelper url, object fromAssembly, string virtualPath)
		{
			Type type = fromAssembly.GetType();
			string fullName = type.Assembly.FullName;
			char[] chrArray = new char[] { ',' };
			string name = string.Concat(fullName.Split(chrArray)[0], virtualPath.Replace("~", "").Replace("/", "."));
			return EmbeddedContentExtensions._page.ClientScript.GetWebResourceUrl(type, name);
		}
	}
}