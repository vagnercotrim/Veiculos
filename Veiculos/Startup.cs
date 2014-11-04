using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Veiculos.Startup))]
namespace Veiculos
{
    public class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }

    }
}