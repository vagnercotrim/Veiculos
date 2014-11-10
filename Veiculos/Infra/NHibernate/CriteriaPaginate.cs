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

        public Paging<T> GetResult<T>(ICriteria criteria, int pageNum, int pageSize)
        {
            var criteriaRowCount = Clone(criteria);

            IList results = _session.CreateMultiCriteria()
                                .Add(criteria.SetFirstResult((pageNum - 1) * pageSize).SetMaxResults(pageSize))
                                .Add(criteriaRowCount.SetProjection(Projections.RowCountInt64()))
                                .List();

            IEnumerable<T> all = ArrayToEnumerable<T>(results);

            long count = (long)((IList)results[1])[0];

            return new Paging<T>(all, pageSize, pageNum, TotalPage(pageSize, count), count);
        }

        private static ICriteria Clone(ICriteria criteria)
        {
            ICriteria criteriaClone = criteria.Clone() as ICriteria;
            criteriaClone.ClearOrders();

            return criteriaClone;
        }

        private static int TotalPage(int pageSize, long count)
        {
            return (int) Math.Ceiling(count/(decimal) pageSize);
        }

        private IEnumerable<T> ArrayToEnumerable<T>(IList results)
        {
            foreach (var o in (IList) results[0])
                yield return (T) o;
        }
    }
}