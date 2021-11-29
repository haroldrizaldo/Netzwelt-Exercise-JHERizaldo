using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Netzwelt_Exercise_ASPNET.Startup))]
namespace Netzwelt_Exercise_ASPNET
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
