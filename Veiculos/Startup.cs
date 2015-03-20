using System;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common;
using Owin;
using Veiculos.Tasks;

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

            RecurringJob.AddOrUpdate<VerificaAtualizacaoHodometro>(v => v.Verifica(3), Cron.Minutely);
        }

        private IKernel GetKernel()
        {
            Bootstrapper bootstrapper = new Bootstrapper();

            return bootstrapper.Kernel;
        }
    }
}