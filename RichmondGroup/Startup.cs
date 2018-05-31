using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RichmondGroup.Startup))]
namespace RichmondGroup
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
