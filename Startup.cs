using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CloudBread.Startup))]

namespace CloudBread
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}