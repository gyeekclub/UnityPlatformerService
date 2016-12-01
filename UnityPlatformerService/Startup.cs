using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UnityPlatformerService.Startup))]

namespace UnityPlatformerService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}