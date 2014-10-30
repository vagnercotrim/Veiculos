using System;
using FluentValidation;

namespace Veiculos.Models.Validation
{
    public class HodometroValidation : AbstractValidator<Hodometro>
    {
        public HodometroValidation()
        {
            RuleFor(h => h.DataLeitura).LessThanOrEqualTo(DateTime.Now);
            RuleFor(h => h.Quilometragem).GreaterThan((decimal) 0.0);
        }

    }
}