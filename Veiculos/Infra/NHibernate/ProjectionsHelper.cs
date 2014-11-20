using NHibernate;
using NHibernate.Criterion;
using System;

namespace Veiculos.Infra.NHibernate
{
    public class ProjectionsHelper
    {

        public static IProjection GroupMonthOfDate(String propertyName, String alias)
        {
            String formatedDateSql = string.Format("month({{alias}}.[{0}]) as {1}", propertyName, alias);
            String formatedDateGroupBy = string.Format("month({{alias}}.[{0}])", propertyName);

            return Projections.SqlGroupProjection(formatedDateSql, formatedDateGroupBy, new[] {alias}, new[] {NHibernateUtil.Int32});
        }

    }
}