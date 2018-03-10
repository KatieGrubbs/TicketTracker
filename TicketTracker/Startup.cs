using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketTracker.Startup))]
namespace TicketTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
