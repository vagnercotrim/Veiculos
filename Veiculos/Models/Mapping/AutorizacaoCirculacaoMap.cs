using FluentNHibernate.Mapping;

namespace Veiculos.Models.Mapping
{
    public class AutorizacaoCirculacaoMap : ClassMap<AutorizacaoCirculacao>
    {

        public AutorizacaoCirculacaoMap()
        {
            Id(a => a.Id);

            Map(a => a.Numero).Not.Nullable();
            Map(a => a.Ano).Not.Nullable();
            Map(a => a.Data).Not.Nullable();
            Map(a => a.Inicio).Not.Nullable();
            Map(a => a.Termino).Not.Nullable();
            Map(a => a.Finalidade).Not.Nullable();
            Map(a => a.Observacao).Not.Nullable();

            References(a => a.Veiculo);
            References(a => a.Motorista);
            References(a => a.QuemAutorizou);
        }

    }
}