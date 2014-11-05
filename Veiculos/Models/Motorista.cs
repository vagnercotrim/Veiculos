using System;

namespace Veiculos.Models
{
    public class Motorista
    {

        public virtual int Id { get; set; }

        public virtual String Numero { get; set; }

        public virtual String Registro { get; set; }

        public virtual String Categoria { get; set; }

        public virtual DateTime PrimeiraHabilitacao { get; set; }

        public virtual DateTime Emissao { get; set; }
        
        public virtual DateTime Validade { get; set; }

        public virtual String Observacao { get; set; }

        public virtual Funcionario Funcionario { get; set; }

    }
}