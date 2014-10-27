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

        public VeiculoDAO(ISession session)
        {
            _dao = new GenericDAO<Veiculo>(session);
            _session = session;
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
                                        .Add(Restrictions.Eq("Placa", placa));

            return criteria.UniqueResult<Veiculo>();
        }
        
        public Paging<Veiculo> GetAll(int pagina, int registros)
        {
            CriteriaPaginate paginate = new CriteriaPaginate(_session);

            return paginate.GetResult<Veiculo>(_session.CreateCriteria<Veiculo>(), pagina, registros);
        }

    }
}