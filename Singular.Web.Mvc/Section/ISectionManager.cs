using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.Section
{
    public interface ISectionManager
    {
        IList<Section> GetSections();
        ISectionManager AddSection(Section section);
    }
}
