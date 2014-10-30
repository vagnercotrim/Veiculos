using FluentNHibernate.Mapping;

namespace Veiculos.Models.Mapping
{
    public class CombustivelMap : ClassMap<Combustivel>
    {

        public CombustivelMap()
        {
            Id(c => c.Id);

            Map(c => c.Descricao).Not.Nullable();
            Map(c => c.Preco).Not.Nullable();
        }

    }
}