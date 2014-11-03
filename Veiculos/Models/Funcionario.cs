using System;

namespace Veiculos.Models
{
    public class Funcionario
    {

        public virtual int Id { get; set; }
        
        private string _cadastro;

        public virtual String Cadastro
        {
            get { return _cadastro; }
            set { _cadastro = value.ToUpper(); }
        }

        public virtual String Nome { get; set; }

        public virtual String Cargo { get; set; }

        public virtual bool Ativo { get; set; }

        public Funcionario()
        {
            Ativo = true;
        }

    }
}