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

        public virtual String Placa { get; set; }
        
        public virtual void Atualiza(Veiculo veiculo)
        {
            AnoModelo = veiculo.AnoModelo;
            AnoFabricacao = veiculo.AnoFabricacao;
            Marca = veiculo.Marca;
            Modelo = veiculo.Marca;
        }

    }
}