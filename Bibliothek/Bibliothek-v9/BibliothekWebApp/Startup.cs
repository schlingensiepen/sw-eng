using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BibliothekWebApp.Startup))]
namespace BibliothekWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
