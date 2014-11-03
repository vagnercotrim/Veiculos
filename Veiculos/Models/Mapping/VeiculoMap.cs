using FluentNHibernate.Mapping;

namespace Veiculos.Models.Mapping
{
    public class VeiculoMap : ClassMap<Veiculo>
    {

        public VeiculoMap()
        {
            Id(v => v.Id);

            Map(v => v.AnoModelo).Not.Nullable();
            Map(v => v.AnoFabricacao).Not.Nullable();
            Map(v => v.Marca).Not.Nullable();
            Map(v => v.Modelo).Not.Nullable();
            Map(v => v.Placa).Not.Nullable().Unique();
            Map(v => v.DataAquisicao).Not.Nullable();
            Map(v => v.Situacao).Not.Nullable();
            Map(v => v.CapacidadeTanque).Not.Nullable();

            References(v => v.Combustivel);
        }

    }
}