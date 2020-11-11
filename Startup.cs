using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetAssociationSports.Startup))]
namespace ProjetAssociationSports
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
