using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_4TierProject.WEB.UI.Startup))]
namespace _4TierProject.WEB.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
