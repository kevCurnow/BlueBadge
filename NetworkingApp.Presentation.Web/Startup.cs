using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetworkingApp.Presentation.Web.Startup))]
namespace NetworkingApp.Presentation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
