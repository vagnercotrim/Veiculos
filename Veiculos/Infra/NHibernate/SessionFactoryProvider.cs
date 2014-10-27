using NHibernate;
using Ninject.Activation;

namespace Veiculos.Infra.NHibernate
{
    public class SessionFactoryProvider : Provider<ISessionFactory>
    {
        protected override ISessionFactory CreateInstance(IContext context)
        {
            var sessionFactory = new SessionFactoryCreator();

            return sessionFactory.CreateSessionFactory();
        }
    }
}