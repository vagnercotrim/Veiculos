using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Veiculos.Models.Mapping;

namespace Veiculos.Testes.Helper
{
    public class InMemoryDatabaseTest : IDisposable
    {
        private static Configuration _configuration;
        private static ISessionFactory SessionFactory;
        protected ISession Session { get; set; }
        
        [SetUp]
        public void Initialize()
        {
            SessionFactory = Fluently.Configure()
            .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<VeiculoMap>())
            .ExposeConfiguration(cfg => _configuration = cfg)
            .BuildSessionFactory();

            Session = SessionFactory.OpenSession();

            SchemaExport export = new SchemaExport(_configuration);
            export.Execute(true, true, false, Session.Connection, null);
        }

        [TearDown]
        public void Dispose()
        {
            Session.Dispose();
        }
    }
}