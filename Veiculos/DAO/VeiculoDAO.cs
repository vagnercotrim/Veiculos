using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;

namespace Veiculos.DAO
{
    public class VeiculoDAO
    {

        private readonly GenericDAO<Veiculo> _dao;
        private readonly ISession _session;
        private readonly CriteriaPaginate _paginate;

        public VeiculoDAO(ISession session, CriteriaPaginate paginate)
        {
            _dao = new GenericDAO<Veiculo>(session);
            _session = session;
            _paginate = paginate;
        }

        public Veiculo Get(int id)
        {
            return _dao.Get(id);
        }

        public IList<Veiculo> GetAll()
        {
            return _dao.GetAll().ToList();
        }

        public void Save(Veiculo veiculo)
        {
            _dao.Save(veiculo);
        }

        public void Update(Veiculo veiculo)
        {
            _dao.Update(veiculo);
        }

        public Veiculo FindByPlaca(String placa)
        {
            ICriteria criteria = _session.CreateCriteria<Veiculo>()
                                         .Add(Restrictions.Eq("Placa", placa == null ? "": placa.ToUpper()));

            return criteria.UniqueResult<Veiculo>();
        }
        
        public Paging<Veiculo> GetAll(Situacao? situacao, int pagina, int registros)
        {
            ICriteria criteria = _session.CreateCriteria<Veiculo>();

            if (situacao != null)
                criteria = criteria.Add(Restrictions.Eq("Situacao", situacao));

            return _paginate.GetResult<Veiculo>(criteria, pagina, registros);
        }

        public int QuantidadeVeiculosEmuso()
        {
            IFutureValue<int> criteria = _session.CreateCriteria<Veiculo>()
                                                 .Add(Restrictions.Eq("Situacao", Situacao.Emuso))
                                                 .SetProjection(Projections.Count("Id"))
                                                 .FutureValue<int>();

            return criteria.Value;
        }

    }
}