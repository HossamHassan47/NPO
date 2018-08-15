using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NPO.Web.Startup))]
namespace NPO.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
