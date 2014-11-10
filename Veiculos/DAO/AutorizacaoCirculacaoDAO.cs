using System;
using NHibernate;
using NHibernate.Criterion;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;

namespace Veiculos.DAO
{
    public class AutorizacaoCirculacaoDAO
    {

        private readonly GenericDAO<AutorizacaoCirculacao> _dao;
        private readonly ISession _session;
        private readonly CriteriaPaginate _paginate;

        public AutorizacaoCirculacaoDAO(ISession session, CriteriaPaginate paginate)
        {
            _dao = new GenericDAO<AutorizacaoCirculacao>(session);
            _session = session;
            _paginate = paginate;
        }

        public AutorizacaoCirculacao Get(int id)
        {
            return _dao.Get(id);
        }

        public void Save(AutorizacaoCirculacao autorizacao)
        {
            _dao.Save(autorizacao);
        }

        public void Update(AutorizacaoCirculacao autorizacao)
        {
            _dao.Update(autorizacao);
        }
        
        public Paging<AutorizacaoCirculacao> GetAll(int pagina, int registros)
        {
            ICriteria criteria = _session.CreateCriteria<AutorizacaoCirculacao>()
                .AddOrder(Order.Desc("Ano"))
                .AddOrder(Order.Desc("Numero"));

            return _paginate.GetResult<AutorizacaoCirculacao>(criteria, pagina, registros);
        }

        public int ProximoNumero(int ano)
        {
            try
            {
                IFutureValue<int> criteria = _session.CreateCriteria<AutorizacaoCirculacao>()
                    .Add(Restrictions.Eq("Ano", ano))
                    .SetProjection(Projections.Max("Numero"))
                    .FutureValue<int>();

                return criteria.Value + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

    }
}