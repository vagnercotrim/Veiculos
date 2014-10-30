using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Veiculos.Models;

namespace Veiculos.DAO
{
    public class HodometroDAO
    {

        private readonly GenericDAO<Hodometro> _dao;
        private readonly ISession _session;

        public HodometroDAO(ISession session)
        {
            _session = session;
            _dao = new GenericDAO<Hodometro>(session);
        }

        public Hodometro Get(int id)
        {
            return _dao.Get(id);
        }

        public IList<Hodometro> GetAll()
        {
            return _dao.GetAll().ToList();
        }

        public void Save(Hodometro hodometro)
        {
            _dao.Save(hodometro);
        }

        public IList<Hodometro> FindByVeiculo(int idVeiculo)
        {
            ICriteria criteria = _session.CreateCriteria<Hodometro>()
                                        .Add(Restrictions.Eq("Veiculo.Id", idVeiculo));

            return criteria.List<Hodometro>();
        }

        public decimal UltimaQuilometragemDoVeiculo(Veiculo veiculo)
        {
            IFutureValue<decimal> criteria = _session.CreateCriteria<Hodometro>()
                .Add(Restrictions.Eq("Veiculo.Id", veiculo.Id))
                .SetProjection(Projections.Max("Quilometragem"))
                .FutureValue<decimal>();

            return criteria.Value;
        }

    }
}