using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TweeterConnect.Startup))]
namespace TweeterConnect
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
