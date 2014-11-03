using FluentValidation;

namespace Veiculos.Models.Validation
{
    public class FuncionarioValidation : AbstractValidator<Funcionario>
    {

        public FuncionarioValidation()
        {
            RuleFor(f => f.Cadastro).NotEmpty();
            RuleFor(f => f.Nome).NotEmpty();
            RuleFor(f => f.Cargo).NotEmpty();
        }

    }
}