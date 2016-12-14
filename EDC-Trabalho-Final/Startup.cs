using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EDC_Trabalho_Final.Startup))]
namespace EDC_Trabalho_Final
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
