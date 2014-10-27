using System.Text.RegularExpressions;
using FluentValidation;

namespace Veiculos.Models.Validation
{
    public class VeiculoValidation : AbstractValidator<Veiculo>
    {

        public VeiculoValidation()
        {
            RuleFor(v => v.Marca).NotEmpty();
            RuleFor(v => v.Modelo).NotEmpty();
            
            RuleFor(v => v.Placa).Length(8);
            RuleFor(v => v.Placa).Matches(new Regex(@"^[a-zA-Z]{3}\-\d{4}$")).WithMessage("O número da placa é inválido.");
        }

    }
}