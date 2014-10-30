using Ninject.Modules;
using Ninject.Web.Common;
using Veiculos.DAO;

namespace Veiculos.Infra.NInject
{
    public class DAOModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<VeiculoDAO>().To<VeiculoDAO>().InRequestScope();
            Kernel.Bind<CombustivelDAO>().To<CombustivelDAO>().InRequestScope();
        }
    }
}