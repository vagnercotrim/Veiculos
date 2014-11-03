using FluentNHibernate.Mapping;

namespace Veiculos.Models.Mapping
{
    public class FuncionarioMap : ClassMap<Funcionario>
    {
        
        public FuncionarioMap()
        {
            Id(v => v.Id);

            Map(v => v.Cadastro).Not.Nullable().Unique();
            Map(v => v.Nome).Not.Nullable();
            Map(v => v.Cargo).Not.Nullable();
            Map(v => v.Ativo).Not.Nullable();
        }

    }
}