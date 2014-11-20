using NUnit.Framework;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;
using Veiculos.Testes.Helper;

namespace Veiculos.Testes.DAO
{
    [TestFixture]
    public class VeiculoDAOTest : InMemoryDatabaseTest
    {

        private CriteriaPaginate _paginate;
        private VeiculoDAO _dao;

        [SetUp]
        public void SetUp()
        {
            _paginate = new CriteriaPaginate();
            _dao = new VeiculoDAO(Session, _paginate);

            VeiculoSeed.CriaVariosVeiculos(_dao, 2001, 2027);
        }

        [Test]
        public void DeveRealizarUmaConsultaCom31RegistrosERetornar6Paginas()
        {
            Paging<Veiculo> veiculos = _dao.GetAll(null, 1, 5);

            Assert.AreEqual(veiculos.TotalPage, 6);
        }

        [Test]
        public void DeveRealizarUmaConsultaERetornar27Registros()
        {
            Paging<Veiculo> veiculos = _dao.GetAll(null, 1, 5);

            Assert.AreEqual(veiculos.TotalCount, 27);
        }

    }
}
