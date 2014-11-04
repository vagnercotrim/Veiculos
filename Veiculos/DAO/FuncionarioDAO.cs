using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;

namespace Veiculos.DAO
{
    public class FuncionarioDAO
    {

        private readonly GenericDAO<Funcionario> _dao;
        private readonly ISession _session;
        private readonly CriteriaPaginate _paginate;

        public FuncionarioDAO(ISession session, CriteriaPaginate paginate)
        {
            _dao = new GenericDAO<Funcionario>(session);
            _session = session;
            _paginate = paginate;
        }


        public Funcionario Get(int id)
        {
            return _dao.Get(id);
        }

        public IList<Funcionario> GetAll()
        {
            return _dao.GetAll().ToList();
        }

        public void Save(Funcionario funcionario)
        {
            _dao.Save(funcionario);
        }

        public void Update(Funcionario funcionario)
        {
            _dao.Update(funcionario);
        }

        public Funcionario FindByCadastro(String cadastro)
        {
            cadastro = cadastro == null ? "" : cadastro.ToUpper();

            ICriteria criteria = _session.CreateCriteria<Funcionario>()
                                         .Add(Restrictions.Eq("Cadastro", cadastro));

            return criteria.UniqueResult<Funcionario>();
        }

        public Paging<Funcionario> GetAll(int pagina, int registros)
        {
            ICriteria criteria = _session.CreateCriteria<Funcionario>();

            return _paginate.GetResult<Funcionario>(criteria, pagina, registros);
        }

    }
}