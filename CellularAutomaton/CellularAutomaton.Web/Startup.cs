using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CellularAutomaton.Web.Startup))]
namespace CellularAutomaton.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);

        }
    }
}
