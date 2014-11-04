using System;
using FluentValidation;
using Veiculos.DAO;

namespace Veiculos.Models.Validation
{
    public class HodometroValidation : AbstractValidator<Hodometro>
    {

        private readonly HodometroDAO _dao;

        public HodometroValidation(HodometroDAO dao)
        {
            _dao = dao;

            Validate();
        }

        private void Validate()
        {
            RuleFor(h => h.DataLeitura).LessThanOrEqualTo(DateTime.Now);
            RuleFor(h => h.Quilometragem).GreaterThan((decimal) 0.0);
            
            RuleFor(h => h.Quilometragem).GreaterThanOrEqualTo(x => _dao.UltimaQuilometragemDoVeiculo(x.Veiculo)).WithMessage("A quilometragem deve ser maior que o último registro.");
        }

    }
}