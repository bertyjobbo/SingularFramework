using System.Collections.Generic;
using Singular.Modules.Core.Data.Services;

namespace Singular.Modules.Core.Data.Models
{
    public class FormsAuthModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}