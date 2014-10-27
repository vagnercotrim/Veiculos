using System.Web.Mvc;
using NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Veiculos.Infra.NHibernate;

namespace Veiculos.Infra.NInject
{
    public class NHibernateModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ISessionFactory>().ToProvider<SessionFactoryProvider>().InSingletonScope();
            Kernel.Bind<ISession>().ToMethod(context => Kernel.Get<ISessionFactory>().OpenSession()).InRequestScope();

            Kernel.BindFilter<TransactionFilter>(FilterScope.Action, 0).WhenActionMethodHas<TransactionAttribute>();
        }
    }
}