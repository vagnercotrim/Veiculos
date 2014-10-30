using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veiculos.Models
{
    public class Combustivel
    {

        public virtual int Id { get; set; }

        public virtual String Descricao { get; set; }

        public virtual decimal Preco { get; set; }

    }
}