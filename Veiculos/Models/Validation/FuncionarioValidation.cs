using FluentValidation;
using Veiculos.DAO;

namespace Veiculos.Models.Validation
{
    public class FuncionarioValidation : AbstractValidator<Funcionario>
    {

        private readonly FuncionarioDAO _dao;

        public FuncionarioValidation(FuncionarioDAO dao)
        {
            _dao = dao;

            Validate();
        }

        private void Validate()
        {
            RuleFor(f => f.Cadastro).NotEmpty();
            RuleFor(f => f.Nome).NotEmpty();
            RuleFor(f => f.Cargo).NotEmpty();

            RuleFor(f => f.Cadastro)
                .Must((funcionario, cadastro) => CadastroDisponivel(funcionario))
                .WithMessage("Este cadastro já está em uso.");
        }

        private bool CadastroDisponivel(Funcionario funcionario)
        {
            Funcionario noBanco = _dao.FindByCadastro(funcionario.Cadastro);

            if (noBanco == null)
                return true;

            return noBanco.Id == funcionario.Id;
        }
    }
}