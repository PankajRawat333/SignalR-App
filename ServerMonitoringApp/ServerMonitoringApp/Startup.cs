using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ServerMonitoringApp.Startup))]

namespace ServerMonitoringApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}