using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Core.Data.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string NtlmUsername { get; set; }
        public string NtlmDomain { get; set; }
        public string HashedFormsAuthPassword { get; set; }
        public string FormsAuthPasswordSalt { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastFailedLoginAttempt { get; set; }
        public bool IsLockedOut { get; set; }
    }
}
