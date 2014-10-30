using System;

namespace Veiculos.Models
{
    public class Hodometro
    {

        public virtual int Id { get; set; }

        public virtual decimal Quilometragem { get; set; }

        public virtual DateTime DataLeitura { get; set; }

        public virtual DateTime DataRegistro { get; set; }
        
        public virtual Veiculo Veiculo { get; set; }

        public Hodometro()
        {
            DataRegistro = DateTime.Now;
        }

    }
}