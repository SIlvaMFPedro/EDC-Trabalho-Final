using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(edc_trabalho_final.Startup))]
namespace edc_trabalho_final
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
