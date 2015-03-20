using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Veiculos.DAO;
using Veiculos.Hubs;
using Veiculos.Models;

namespace Veiculos.Tasks
{
    public class VerificaAtualizacaoHodometro
    {
        private readonly VeiculoDAO _veiculoDao;
        private readonly HodometroDAO _hodometroDao;

        public VerificaAtualizacaoHodometro(VeiculoDAO veiculoDao, HodometroDAO hodometroDao)
        {
            _veiculoDao = veiculoDao;
            _hodometroDao = hodometroDao;
        }

        public void Verifica(int dias)
        {
            IEnumerable<Veiculo> veiculos = _veiculoDao.GetAll(Situacao.Emuso, 1, Int32.MaxValue).List;

            foreach (var veiculo in veiculos)
            {
                Hodometro h = _hodometroDao.UltimoByVeiculo(veiculo.Id);

                if (h != null)
                {
                    if (h.DataLeitura < DateTime.Today.AddDays(-dias))
                    {
                        AlertaHub.SendMessage(String.Format("Registre a quilometragem do veiculo {0}", veiculo.Placa));
                    }
                }
                else
                {
                    AlertaHub.SendMessage(String.Format("Registre a quilometragem do veiculo {0}", veiculo.Placa));
                }
            }
        }
    }
}