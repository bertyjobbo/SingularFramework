using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Singular.Web.Mvc.Common.HtmlExtensions
{
    public class NgForm : IDisposable
    {
         readonly TextWriter _writer;

        public NgForm(ViewContext viewContext)
        {
            _writer = viewContext.Writer;
        }

        public void Dispose()
        {
            _writer.Write("</form>");
        }
    }
}
