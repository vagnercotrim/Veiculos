using FluentValidation;

namespace Veiculos.Models.Validation
{
    public class MotoristaValidation : AbstractValidator<Motorista>
    {

        public MotoristaValidation()
        {
            RuleFor(m => m.Numero).NotEmpty();
            RuleFor(m => m.Registro).NotEmpty();
            RuleFor(m => m.Categoria).NotEmpty().Length(1, 2);
            RuleFor(m => m.PrimeiraHabilitacao);
            RuleFor(m => m.Emissao).GreaterThanOrEqualTo(m => m.PrimeiraHabilitacao);
            RuleFor(m => m.Validade).GreaterThanOrEqualTo(m => m.PrimeiraHabilitacao);
        }

    }
}