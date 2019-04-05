using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RHA.Startup))]
namespace RHA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
