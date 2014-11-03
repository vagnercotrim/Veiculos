using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veiculos.Models
{
    public class Funcionario
    {

        public int Id { get; set; }

        public String Cadastro { get; set; }

        public String Nome { get; set; }

        public String Cargo { get; set; }

        public bool Ativo { get; set; }

        public Funcionario()
        {
            Ativo = true;
        }

    }
}