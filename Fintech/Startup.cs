using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fintech.Startup))]
namespace Fintech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
