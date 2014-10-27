using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace Veiculos.Infra.NHibernate
{
    public class CriteriaPaginate
    {

        private readonly ISession _session;

        public CriteriaPaginate(ISession session)
        {
            _session = session;
        }

        public Paging<T> GetResult<T>(ICriteria criteria, int pageCount, int pageSize)
        {
            ICriteria criteriaRowCount = criteria.Clone() as ICriteria;
            
            IList results = _session.CreateMultiCriteria()
                                .Add(criteria.SetFirstResult((pageCount - 1) * pageSize).SetMaxResults(pageSize))
                                .Add(criteriaRowCount.SetProjection(Projections.RowCountInt64()))
                                .List();

            IEnumerable<T> all = ArrayToEnumerable<T>(results);

            long count = (long)((IList)results[1])[0];
            int totalPage = (int) Math.Ceiling(count / (decimal)pageSize);

            return new Paging<T>(all, pageSize, pageCount, totalPage, count);
        }

        private IEnumerable<T> ArrayToEnumerable<T>(IList results)
        {
            foreach (var o in (IList) results[0])
                yield return (T) o;
        }
    }
}