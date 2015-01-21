using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singular.Core.Data.Entities;

namespace Singular.Core.Authentication
{
    public class SingularSession
    {
        public SingularSession(SingularUser user)
        {
            User = user;
            SessionId = Guid.NewGuid();
        }

        public SingularUser User { get; private set; }

        public Guid SessionId { get; private set; }
    }
}
