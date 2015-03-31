using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudBreadAdminWeb.Startup))]
namespace CloudBreadAdminWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
