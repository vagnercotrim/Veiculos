using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace Veiculos.Infra.NHibernate
{
    public class CriteriaPaginate
    {

        public Paging<T> GetResult<T>(ICriteria criteria, int pageNum, int pageSize)
        {
            var count = TotalCount(Clone(criteria));

            IEnumerable<T> lista = criteria.SetFirstResult((pageNum - 1) * pageSize)
                                           .SetMaxResults(pageSize)
                                           .List<T>();

            return new Paging<T>(lista, pageSize, pageNum, count);
        }

        private long TotalCount(ICriteria criteria)
        {
            return criteria.SetProjection(Projections.RowCountInt64()).UniqueResult<long>();
        }

        private ICriteria Clone(ICriteria criteria)
        {
            ICriteria criteriaClone = criteria.Clone() as ICriteria;
            criteriaClone.ClearOrders();

            return criteriaClone;
        }

    }
}