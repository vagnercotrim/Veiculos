using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Veiculos.Models.Mapping
{
    public class MotoristaMap : ClassMap<Motorista>
    {

        public MotoristaMap()
        {
            Id(m => m.Id);

            Map();

            References(m => m.Funcionario);
        }

        private void Map()
        {
            Map(m => m.Numero).Not.Nullable().Unique();
            Map(m => m.Registro).Not.Nullable().Unique();
            Map(m => m.Categoria).Not.Nullable().Length(2);
            Map(m => m.PrimeiraHabilitacao).Not.Nullable();
            Map(m => m.Emissao).Not.Nullable();
            Map(m => m.Validade).Not.Nullable();
            Map(m => m.Observacao);
        }
    }
}