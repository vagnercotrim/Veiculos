using FluentNHibernate.Mapping;

namespace Veiculos.Models.Mapping
{
    public class HodometroMap : ClassMap<Hodometro>
    {

        public HodometroMap()
        {
            Id(h => h.Id);

            Map(h => h.Quilometragem).Not.Nullable();
            Map(h => h.DataLeitura).Not.Nullable();
            Map(h => h.DataRegistro).Not.Nullable();

            References(h => h.Veiculo);
        }

    }
}