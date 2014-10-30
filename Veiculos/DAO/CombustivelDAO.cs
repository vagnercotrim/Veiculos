using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Veiculos.Models;

namespace Veiculos.DAO
{
    public class CombustivelDAO
    {
        private readonly GenericDAO<Combustivel> _dao;

        public CombustivelDAO(ISession session)
        {
            _dao = new GenericDAO<Combustivel>(session);
        }
        
        public Combustivel Get(int id)
        {
            return _dao.Get(id);
        }

        public IList<Combustivel> GetAll()
        {
            return _dao.GetAll().ToList();
        }

        public void Save(Combustivel combustivel)
        {
            _dao.Save(combustivel);
        }

        public void Update(Combustivel combustivel)
        {
            _dao.Update(combustivel);
        }

    }
}