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

            RuleFor(h => h.Quilometragem).GreaterThanOrEqualTo(x => UltimaQuilometragem(x.Veiculo)).WithMessage("A quilometragem deve ser maior que o último registro.");
        }

        public decimal UltimaQuilometragem(Veiculo veiculo)
        {
            try
            {
                return _dao.UltimaQuilometragemDoVeiculo(veiculo);
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}