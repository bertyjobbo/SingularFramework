using System.Data.Entity;
using Singular.Core.Authentication;
using Singular.Core.Data.Entities;

namespace Singular.Core.Data.DataContext
{
    public class CoreEntityFrameworkContext:DbContext
    {
        public DbSet<User> SingularUsers { get; set; }
    }
}
