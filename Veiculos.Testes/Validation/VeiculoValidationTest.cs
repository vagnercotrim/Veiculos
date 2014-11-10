using FluentNHibernate.Conventions;
using FluentValidation.Results;
using NUnit.Framework;
using System;
using System.Linq;
using Veiculos.DAO;
using Veiculos.Models;
using Veiculos.Models.Validation;
using Veiculos.Testes.DAO;
using Veiculos.Testes.Helper;

namespace Veiculos.Testes.Validation
{

    [TestFixture]
    class VeiculoValidationTest : InMemoryDatabaseTest
    {
        private VeiculoDAO _dao;
        private VeiculoValidation _validation;

        [SetUp]
        public void SetUp()
        {
            _dao = new VeiculoDAO(Session, null);
            _validation = new VeiculoValidation(_dao);

            VeiculoSeed.CriaVariosVeiculos(_dao, 2012, 2013);
        }

        [Test]
        public void DeveGerarErroPlacaEmUso()
        {
            Veiculo veiculo = new Veiculo { Placa = "DDD-2012" };

            ValidationResult results = _validation.Validate(veiculo);

            Assert.IsTrue(!results.Errors.Where(e => e.ErrorMessage == "Esta placa já está em uso.").IsEmpty());
        }

        [Test]
        public void DeveGerarErroNumeroDapLacaInvalido()
        {
            Veiculo veiculo = new Veiculo { Placa = "DD2-X012" };

            ValidationResult results = _validation.Validate(veiculo);

            Assert.IsTrue(!results.Errors.Where(e => e.ErrorMessage == "O número da placa é inválido.").IsEmpty());
        }

        [Test]
        public void DeveGerarErroSeCapacidadeDoTanqueForIgualAZero()
        {
            Veiculo veiculo = new Veiculo { Placa = "DDD-2012" };

            ValidationResult results = _validation.Validate(veiculo);

            Assert.IsTrue(!results.Errors.Where(e => e.PropertyName == "CapacidadeTanque").IsEmpty());

        }

        [Test]
        public void VeiculoNaoDeveGerarErros()
        {
            Veiculo veiculo = new Veiculo
            {
                AnoFabricacao = 2014,
                AnoModelo = 2014,
                CapacidadeTanque = 80,
                Combustivel = new Combustivel {Id = 1, Descricao = "Gasolina", Preco = 3.23M},
                DataAquisicao = new DateTime(2014, 5, 5),
                Fabricante = "Ford",
                Modelo = "Fiesta",
                Id = 0,
                Placa = "ASD-3210"
            };

            ValidationResult results = _validation.Validate(veiculo);

            Assert.IsTrue(results.IsValid);
        }

    }
}
