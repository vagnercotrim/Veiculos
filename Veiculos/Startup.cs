using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common;
using Owin;

[assembly: OwinStartup(typeof(Veiculos.Startup))]
namespace Veiculos
{
    public class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureHangFire(app);
        }

        private void ConfigureHangFire(IAppBuilder app)
        {
            const string conexao = @"Data Source=G1711MAX\sqlexpress;Password=chapado;User ID=sa;Initial Catalog=veiculos;Application Name=Veiculos;";

            app.UseHangfire(config =>
            {
                config.UseSqlServerStorage(conexao);
                config.UseServer();
                config.UseNinjectActivator(GetKernel());
                config.UseDashboardPath("/tarefas");
            });
        }

        private IKernel GetKernel()
        {
            Bootstrapper bootstrapper = new Bootstrapper();

            return bootstrapper.Kernel;
        }
    }
}