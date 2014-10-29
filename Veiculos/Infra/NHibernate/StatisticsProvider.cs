using NHibernate;
using NHibernate.Stat;
using Ninject.Activation;

namespace Veiculos.Infra.NHibernate
{
    public class StatisticsProvider : Provider<IStatistics>
    {

        private readonly ISessionFactory _sessionFactory;

        public StatisticsProvider(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        protected override IStatistics CreateInstance(IContext context)
        {
            return _sessionFactory.Statistics;
        }
    }
}