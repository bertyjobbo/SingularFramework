using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Core.Authentication
{
    public class UserFactory: IUserFactory
    {
        public SingularUser Build(string authenticationType, string logonName)
        {
            throw new NotImplementedException();
             return new SingularUser();
        }
    }
}
