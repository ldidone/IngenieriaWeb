using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeLoBusco.Startup))]
namespace TeLoBusco
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
