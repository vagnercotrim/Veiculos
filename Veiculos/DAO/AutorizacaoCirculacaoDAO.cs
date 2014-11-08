﻿using NHibernate;
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
            ICriteria criteria = _session.CreateCriteria<AutorizacaoCirculacao>();

            return _paginate.GetResult<AutorizacaoCirculacao>(criteria, pagina, registros);
        }

    }
}