using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veiculos.DAO;
using Veiculos.Models;

namespace Veiculos.Testes.DAO
{
    class VeiculoSeed
    {

        public static void CriaVariosVeiculos(VeiculoDAO dao, int inicio, int fim)
        {
            for (int i = inicio; i <= fim; i++)
                dao.Save(new Veiculo {AnoFabricacao = i, AnoModelo = i, Fabricante = "Fa", Modelo = "Mo", Placa = "DDD-" + i});
        }

    }
}
