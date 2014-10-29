using NHibernate;
using NHibernate.Stat;

namespace Veiculos.Infra.NHibernate
{
    public class Statistics
    {

        private readonly ISessionFactory _sessionFactory;

        public Statistics(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IStatistics Stats()
        {
            return _sessionFactory.Statistics;
        }
    }
}