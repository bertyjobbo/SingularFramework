using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.Section
{
    public class SectionManager : ISectionManager
    {
        private SectionManager()
        {
            _sections = new List<Section>();
        }
        private static ISectionManager _current;
        private readonly IList<Section> _sections;
        public static  ISectionManager Current { get { return _current ?? (_current = new SectionManager()); } }

        public IList<Section> GetSections()
        {
            return _sections.OrderBy(x => x.Order).ToList();
        }

        public ISectionManager AddSection(Section section)
        {
            _sections.Add(section);
            return this;
        }
    }
}
