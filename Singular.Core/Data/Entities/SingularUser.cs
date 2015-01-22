using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singular.Core.Data.Enums;

namespace Singular.Core.Data.Entities
{
    public class SingularUser : EntityBase
    {
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string DomainUsername { get; set; }

        [MaxLength(200)]
        public string Domain { get; set; }

        [MaxLength(200)]
        public string EncryptedFormsAuthPassword { get; set; }


        public DateTime? LastLogin { get; set; }
        public DateTime? LastFailedLoginAttempt { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsLockedOut { get; set; }
        public AuthenticationType AuthenticationType { get; set; }
        public bool IsSuperUser { get; set; }
    }
}
