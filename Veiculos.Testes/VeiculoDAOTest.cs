using NUnit.Framework;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;

namespace Veiculos.Testes
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
            for (int i = 2001; i < 2027; i++)
                dao.Save(new Veiculo {AnoFabricacao = i, AnoModelo = i, Marca = "Ma", Modelo = "Mo", Placa = "DDD" + i});
        }

        [Test]
        public void DeveRealizarUmaConsultaCom5ItensPorPaginaERetornar6Paginas()
        {
            Paging<Veiculo> veiculos = _dao.GetAll(1, 5);

            Assert.AreEqual(veiculos.TotalPage, 6);
        }
    }
}
