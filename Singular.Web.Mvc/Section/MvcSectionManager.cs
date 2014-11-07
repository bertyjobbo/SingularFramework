using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Web.Mvc.Section
{
    public class MvcSectionManager : IMvcSectionManager
    {
        private MvcSectionManager()
        {
            _sections = new List<MvcSection>();
        }
        private static IMvcSectionManager _current;
        private readonly IList<MvcSection> _sections;
        public static  IMvcSectionManager Current { get { return _current ?? (_current = new MvcSectionManager()); } }

        public IList<MvcSection> GetSections()
        {
            return _sections.OrderBy(x => x.Order).ToList();
        }

        public void AddSection(MvcSection section)
        {
            _sections.Add(section);
        }
    }
}
