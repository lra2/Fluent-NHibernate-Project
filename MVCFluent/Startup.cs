using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCFluent.Startup))]
namespace MVCFluent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
