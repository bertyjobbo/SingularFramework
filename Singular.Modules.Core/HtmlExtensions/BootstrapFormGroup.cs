using System;
using System.IO;
using System.Web.Mvc;

namespace Singular.Modules.Core.HtmlExtensions
{
    public class BootstrapFormGroup : IDisposable
    {
        private readonly TextWriter _writer;

        public BootstrapFormGroup(ViewContext viewContext)
        {
            _writer = viewContext.Writer;
        }

        public void Dispose()
        {
            _writer.Write("</div>");
        }
    }
}