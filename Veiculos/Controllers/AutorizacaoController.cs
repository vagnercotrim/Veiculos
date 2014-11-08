using System;
using System.Web.Mvc;
using FluentValidation.Results;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;
using Veiculos.Models.Validation;

namespace Veiculos.Controllers
{
    public class AutorizacaoController : Controller
    {

        private readonly AutorizacaoCirculacaoDAO _autorizacaoCirculacaoDao;
        private readonly VeiculoDAO _veiculoDao;
        private readonly MotoristaDAO _motoristaDao;
        private readonly FuncionarioDAO _funcionarioDao;
        private readonly AutorizacaoCirculacaoValidation _validation;

        public AutorizacaoController(AutorizacaoCirculacaoDAO autorizacaoCirculacaoDao, VeiculoDAO veiculoDao,
            MotoristaDAO motoristaDao, FuncionarioDAO funcionarioDao, AutorizacaoCirculacaoValidation validation)
        {
            _autorizacaoCirculacaoDao = autorizacaoCirculacaoDao;
            _veiculoDao = veiculoDao;
            _motoristaDao = motoristaDao;
            _funcionarioDao = funcionarioDao;
            _validation = validation;
        }


        [Route("autorizacao")]
        public ActionResult Index(int pagina = 1)
        {
            Paging<AutorizacaoCirculacao> veiculos = _autorizacaoCirculacaoDao.GetAll(pagina, 15);

            return View(veiculos);
        }

        [Route("autorizacao/novo")]
        public ActionResult Novo(int id)
        {
            Veiculo veiculo = _veiculoDao.Get(id);

            if (veiculo == null)
                return RedirectToAction("Index");

            ViewBag.Veiculo = veiculo;

            ViewBag.Motoristas = _motoristaDao.GetAll();
            ViewBag.Funcionarios = _funcionarioDao.GetAll();

            return View();
        }
        
        [HttpPost]
        [Route("autorizacao/novo")]
        [Transaction]
        public ActionResult Novo(AutorizacaoCirculacao autorizacao)
        {
            autorizacao.Numero = 1; // TODO Implementatar classe para gerar o número automaticamente 
            autorizacao.Ano = autorizacao.Data.Year;

            try
            {
                ValidationResult result = _validation.Validate(autorizacao);

                if (result.IsValid)
                {
                    _autorizacaoCirculacaoDao.Save(autorizacao);

                    return RedirectToAction("Index", "Autorizacao", new {id = autorizacao.Id});
                }

                ViewBag.Veiculo = _veiculoDao.Get(autorizacao.Veiculo.Id);

                ViewBag.Motoristas = _motoristaDao.GetAll();
                ViewBag.Funcionarios = _funcionarioDao.GetAll();

                return View(autorizacao);
            }
            catch (Exception)
            {
                return View(autorizacao);
            }
        }

    }
}