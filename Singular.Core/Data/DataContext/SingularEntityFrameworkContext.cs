using System.Data.Entity;
using Singular.Core.Authentication;
using Singular.Core.Data.Entities;

namespace Singular.Core.Data.DataContext
{
    public class SingularEntityFrameworkContext : DbContext
    {
        public DbSet<SingularUser> SingularUsers { get; set; }
    }
}
