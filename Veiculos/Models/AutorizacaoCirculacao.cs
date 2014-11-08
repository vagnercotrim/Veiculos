using System;

namespace Veiculos.Models
{
    public class AutorizacaoCirculacao
    {

        public virtual int Id { get; set; }

        public virtual int Numero { get; set; }

        public virtual int Ano { get; set; }

        public virtual DateTime Data { get; set; }

        public virtual Veiculo Veiculo { get; set; }

        public virtual Motorista Motorista { get; set; }

        public virtual DateTime Inicio { get; set; }

        public virtual DateTime Termino { get; set; }

        public virtual String Finalidade { get; set; }

        public virtual String Observacao { get; set; }

        public virtual Funcionario QuemAutorizou { get; set; }

    }
}