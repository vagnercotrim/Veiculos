using System.Text.RegularExpressions;
using FluentValidation;
using Veiculos.DAO;

namespace Veiculos.Models.Validation
{
    public class VeiculoValidation : AbstractValidator<Veiculo>
    {

        private readonly VeiculoDAO _dao;

        public VeiculoValidation(VeiculoDAO dao)
        {
            _dao = dao;

            RuleFor(v => v.Marca).NotEmpty();
            RuleFor(v => v.Modelo).NotEmpty();
            RuleFor(v => v.CapacidadeTanque).GreaterThan(0);

            RuleFor(v => v.Placa).Length(8);
            RuleFor(v => v.Placa).Matches(new Regex(@"^[a-zA-Z]{3}\-\d{4}$")).WithMessage("O número da placa é inválido.");

            RuleFor(v => v.Placa).Must((veiculo, placa) => PlacaDisponivel(veiculo)).WithMessage("Esta placa já está em uso.");
        }

        private bool PlacaDisponivel(Veiculo veiculo)
        {
            Veiculo noBanco = _dao.FindByPlaca(veiculo.Placa);

            if (noBanco == null)
                return true;

            return noBanco.Id == veiculo.Id;
        }
    }
}