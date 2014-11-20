using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Identity2Test.Startup))]
namespace Identity2Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
