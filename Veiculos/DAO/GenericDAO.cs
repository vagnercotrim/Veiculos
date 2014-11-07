using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Veiculos.DAO
{
    public class GenericDAO<T> where T : class
    {

        private readonly ISession _session;

        public GenericDAO(ISession session)
        {
            _session = session;
        }

        public T Get(int id)
        {
            return _session.Get<T>(id);
        }

        public IQueryable<T> GetAll()
        {
            return _session.Query<T>();
        }

        public void Save(T t)
        {
            _session.Save(t);
        }

        public void Update(T t)
        {
            _session.Merge(t);
        }

        public void Delete(T t)
        {
            _session.Delete(t);
        }

        public void Refresh(T t)
        {
            _session.Refresh(t);
        }

    }
}