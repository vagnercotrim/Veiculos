using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace Veiculos.Models.Validation
{
    public class VeiculoValidation : AbstractValidator<Veiculo>
    {

        public VeiculoValidation()
        {
            RuleFor(v => v.Placa).Length(8);
        }

    }
}