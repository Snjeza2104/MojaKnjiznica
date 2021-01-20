using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mojaKnjiznica.Startup))]
namespace mojaKnjiznica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
