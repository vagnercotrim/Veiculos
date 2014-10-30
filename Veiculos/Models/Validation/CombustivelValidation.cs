using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace Veiculos.Models.Validation
{
    public class CombustivelValidation : AbstractValidator<Combustivel>
    {

        public CombustivelValidation()
        {
            RuleFor(c => c.Descricao).NotEmpty().Length(3, 50);
            RuleFor(c => c.Preco).GreaterThan((decimal) 0.0);
        }

    }
}