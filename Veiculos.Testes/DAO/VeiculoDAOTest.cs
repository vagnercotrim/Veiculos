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
        public void Init()
        {
            _paginate = new CriteriaPaginate(Session);
            _dao = new VeiculoDAO(Session, _paginate);

            PopulaVeiculo(_dao);
        }

        private void PopulaVeiculo(VeiculoDAO dao)
        {
            for (int i = 2001; i <= 2027; i++)
                dao.Save(new Veiculo {AnoFabricacao = i, AnoModelo = i, Fabricante = "Fa", Modelo = "Mo", Placa = "DDD-" + i});
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
