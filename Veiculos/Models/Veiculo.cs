using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    }
}