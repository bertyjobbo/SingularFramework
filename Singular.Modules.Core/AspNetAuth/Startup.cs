using Microsoft.Owin;
using Owin;
using Singular.Modules.Core.AspNetAuth;

[assembly: OwinStartup(typeof(Startup))]
namespace Singular.Modules.Core.AspNetAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
