using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace Veiculos.Infra.NHibernate
{
    public class QueryPaginate
    {

        private readonly ISession _session;

        public QueryPaginate(ISession session)
        {
            _session = session;
        }

        public IList<T> GetPagedData<T>(ICriteria criteria, int page, int pageSize, out long count)
        {
            var all = new List<T>();

            ICriteria criteriaRowCount = criteria.Clone() as ICriteria;
            
            IList results = _session.CreateMultiCriteria()
                                .Add(criteria.SetFirstResult((page - 1) * pageSize).SetMaxResults(pageSize))
                                .Add(criteriaRowCount.SetProjection(Projections.RowCountInt64()))
                                .List();

            foreach (var o in (IList)results[0])
                all.Add((T)o);

            count = (long)((IList)results[1])[0];
            return all;
        }

    }
}