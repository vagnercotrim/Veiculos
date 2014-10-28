using NUnit.Framework;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;

namespace Veiculos.Testes
{
    [SetUpFixture]
    public class VeiculoDAOTest : AbstractInMemoryDataFixture
    {
        
        [Test]
        public void TestMethod1()
        {
            CriteriaPaginate paginate = new CriteriaPaginate(Session);
            VeiculoDAO dao = new VeiculoDAO(Session, paginate);

            for (int i = 2001; i < 2027; i++)
            {
                dao.Save(new Veiculo { AnoFabricacao = i, AnoModelo = i, Marca = "Ma", Modelo = "Mo", Placa = "DDD" + i});
            }
            
            Paging<Veiculo> veiculos = dao.GetAll(1, 5);

            Assert.AreEqual(veiculos.TotalPage, 6);
        }
    }
}
