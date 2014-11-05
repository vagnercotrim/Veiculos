using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;

namespace Veiculos.DAO
{
    public class MotoristaDAO
    {

        private readonly GenericDAO<Motorista> _dao;
        private readonly ISession _session;
        private readonly CriteriaPaginate _paginate;

        public MotoristaDAO(ISession session, CriteriaPaginate paginate)
        {
            _dao = new GenericDAO<Motorista>(session);
            _session = session;
            _paginate = paginate;
        }
        
        public Motorista Get(int id)
        {
            return _dao.Get(id);
        }

        public IList<Motorista> GetAll()
        {
            return _dao.GetAll().ToList();
        }

        public void Save(Motorista motorista)
        {
            _dao.Save(motorista);
        }

        public void Update(Motorista motorista)
        {
            _dao.Update(motorista);
        }

        public Motorista FindByNumero(String numero)
        {
            ICriteria criteria = _session.CreateCriteria<Motorista>()
                                         .Add(Restrictions.Eq("Numero", numero));

            return criteria.UniqueResult<Motorista>();
        }

        public Paging<Motorista> GetAll(int pagina, int registros)
        {
            ICriteria criteria = _session.CreateCriteria<Motorista>();

            return _paginate.GetResult<Motorista>(criteria, pagina, registros);
        }

    }
}