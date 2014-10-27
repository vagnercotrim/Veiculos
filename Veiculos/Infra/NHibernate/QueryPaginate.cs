using System;
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

        public Paging<T> GetPagedData<T>(ICriteria criteria, int page, int pageSize)
        {
            var all = new List<T>();

            ICriteria criteriaRowCount = criteria.Clone() as ICriteria;
            
            IList results = _session.CreateMultiCriteria()
                                .Add(criteria.SetFirstResult((page - 1) * pageSize).SetMaxResults(pageSize))
                                .Add(criteriaRowCount.SetProjection(Projections.RowCountInt64()))
                                .List();

            foreach (var o in (IList)results[0])
                all.Add((T)o);

            long count = (long)((IList)results[1])[0];
            int totalPage = (int) Math.Ceiling(count / (decimal)pageSize);

            return new Paging<T>(all, pageSize, page, totalPage, count);
        }

    }
}