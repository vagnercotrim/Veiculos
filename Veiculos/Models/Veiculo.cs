using System;

namespace Veiculos.Models
{
    public class Veiculo
    {
        
        public virtual int Id { get; set; }

        public virtual int AnoModelo { get; set; }

        public virtual int AnoFabricacao { get; set; }

        public virtual String Fabricante { get; set; }

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

        public virtual int CapacidadeTanque { get; set; }

        public Veiculo()
        {
            Situacao = Situacao.Emuso;
        }

        public virtual void Atualiza(Veiculo veiculo)
        {
            AnoModelo = veiculo.AnoModelo;
            AnoFabricacao = veiculo.AnoFabricacao;
            Fabricante = veiculo.Fabricante;
            Modelo = veiculo.Modelo;
            DataAquisicao = veiculo.DataAquisicao;
            Combustivel = veiculo.Combustivel;
            CapacidadeTanque = veiculo.CapacidadeTanque;
            Placa = veiculo.Placa;
        }

    }
}