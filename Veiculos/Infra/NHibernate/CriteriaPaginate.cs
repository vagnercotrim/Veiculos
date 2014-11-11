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
            IEnumerable<T> lista = criteria.SetFirstResult((pageNum - 1) * pageSize)
                                           .SetMaxResults(pageSize)
                                           .List<T>();

            var count = TotalCount(criteria);

            return new Paging<T>(lista, pageSize, pageNum, TotalPage(pageSize, count), count);
        }

        private long TotalCount(ICriteria criteria)
        {
            var criteriaRowCount = Clone(criteria);

            return criteriaRowCount.SetProjection(Projections.RowCountInt64()).UniqueResult<long>();
        }

        private ICriteria Clone(ICriteria criteria)
        {
            ICriteria criteriaClone = criteria.Clone() as ICriteria;
            criteriaClone.ClearOrders();

            return criteriaClone;
        }

        private int TotalPage(int pageSize, long count)
        {
            return (int)Math.Ceiling(count / (decimal)pageSize);
        }

    }
}