using System;
using System.Web.Mvc;

namespace Veiculos.Infra.NHibernate
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionAttribute : FilterAttribute
    {
    }
}