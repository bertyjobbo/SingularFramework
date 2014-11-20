using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Core.Authentication
{
    public class SingularUser
    {
        /// <summary>
        /// Logon name
        /// </summary>
        public string LogonName { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }
}
