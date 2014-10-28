﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Veiculos.Models.Mapping;

namespace Veiculos.Testes
{
    class InMemorySessionFactoryProvider
    {
        private static InMemorySessionFactoryProvider _instance;
        public static InMemorySessionFactoryProvider Instance
        {
            get { return _instance ?? (_instance = new InMemorySessionFactoryProvider()); }
        }

        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        private InMemorySessionFactoryProvider()
        {
            Initialize();
        }

        public void Initialize()
        {
            _sessionFactory = CreateSessionFactory();
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
            .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<VeiculoMap>())
            .ExposeConfiguration(cfg => _configuration = cfg)
            .BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            ISession session = _sessionFactory.OpenSession();

            var export = new SchemaExport(_configuration);
            export.Execute(true, true, false, session.Connection, null);

            return session;
        }

        public void Dispose()
        {
            if (_sessionFactory != null)
                _sessionFactory.Dispose();

            _sessionFactory = null;
            _configuration = null;
        }
    }
}