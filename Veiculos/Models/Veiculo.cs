using System;

namespace Veiculos.Models
{
    public class Veiculo
    {
        
        public virtual int Id { get; set; }

        public virtual int AnoModelo { get; set; }

        public virtual int AnoFabricacao { get; set; }

        public virtual String Marca { get; set; }

        public virtual String Modelo { get; set; }

        private string _placa;

        public virtual String Placa
        {
            get { return _placa; }
            set { _placa = value.ToUpper(); }
        }

        public virtual DateTime DataAquisicao { get; set; }

        public virtual Situacao Situacao { get; set; }

        public virtual Combustivel Combustivel { get; set; }

        public Veiculo()
        {
            Situacao = Situacao.Emuso;
        }

        public virtual void Atualiza(Veiculo veiculo)
        {
            AnoModelo = veiculo.AnoModelo;
            AnoFabricacao = veiculo.AnoFabricacao;
            Marca = veiculo.Marca;
            Modelo = veiculo.Marca;
            DataAquisicao = veiculo.DataAquisicao;
            Combustivel = veiculo.Combustivel;
        }

    }
}