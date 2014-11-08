using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject.Activation;
using Veiculos.Models.Mapping;

namespace Veiculos.Infra.NHibernate
{
    public class SessionFactoryProvider : Provider<ISessionFactory>
    {
        protected override ISessionFactory CreateInstance(IContext context)
        {
            return BuildSessionFactory();
        }

        public ISessionFactory BuildSessionFactory()
        {
            const string conexao = @"Data Source=G1711MAX\sqlexpress;Password=chapado;User ID=sa;Initial Catalog=veiculos;Application Name=Veiculos;";

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                                            .ConnectionString(conexao)
                                            .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<VeiculoMap>())
                .ExposeConfiguration(c => c.SetProperty("generate_statistics", "true"))
                //.ExposeConfiguration(c => new SchemaExport(c).Create(false, true)) // TODO Remover depois de definir todos os models
                .BuildSessionFactory();
        }
    }
}