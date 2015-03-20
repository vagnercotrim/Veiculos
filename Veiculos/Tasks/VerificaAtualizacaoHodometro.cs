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
                if (DeveAtualizarHodometro(veiculo, dias))
                {
                    String texto = String.Format("Registre a quilometragem do veiculo {0}", veiculo.Placa);
                    AlertaHub.SendMessage(texto);
                }
            }
        }

        public bool DeveAtualizarHodometro(Veiculo veiculo, int dias)
        {
            Hodometro h = _hodometroDao.UltimoByVeiculo(veiculo.Id);

            if (h == null)
                return true;

            if (h.DataLeitura < DateTime.Today.AddDays(-dias))
                return true;

            return false;
        }
    }
}