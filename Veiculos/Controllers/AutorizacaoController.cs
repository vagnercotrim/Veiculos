using FluentValidation.Results;
using System;
using System.Web.Mvc;
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

        [Route("autorizacao/{pagina?}")]
        public ActionResult Index(int pagina = 1)
        {
            Paging<AutorizacaoCirculacao> veiculos = _autorizacaoCirculacaoDao.GetAll(pagina, 15);
            
            return View(veiculos);
        }

        [Route("autorizacao/nova")]
        public ActionResult NovoSemId()
        {
            this.Info("Selecione um veículo e clique em autorização para continuar.");
            return RedirectToAction("Index", "Veiculo", new { situacao = "Emuso" } );
        }

        [Route("veiculo/{id:int}/autorizacao/nova")]
        public ActionResult Novo(int id)
        {
            Veiculo veiculo = _veiculoDao.Get(id);

            if (veiculo == null)
            {
                this.Info("Veículo não encontrado.");
                return RedirectToAction("Index");
            }

            if (veiculo.Situacao != Situacao.Emuso)
            {
                this.Info("O veículo selecionado não e está mais disponível.");
                return RedirectToAction("Index");
            }

            ViewBag.Veiculo = veiculo;
            LoadResources();

            return View();
        }

        [HttpPost]
        [Route("veiculo/{id:int}/autorizacao/nova")]
        [Transaction]
        public ActionResult Novo(AutorizacaoCirculacao autorizacao)
        {
            autorizacao.Ano = autorizacao.Data.Year;
            autorizacao.Numero = _autorizacaoCirculacaoDao.ProximoNumero(autorizacao.Ano);

            try
            {
                ValidationResult result = _validation.Validate(autorizacao);

                if (result.IsValid)
                {
                    _autorizacaoCirculacaoDao.Save(autorizacao);

                    this.Success("Autorização para circulação cadastrada com sucesso.");
                    return RedirectToAction("Index", "Autorizacao", new {id = autorizacao.Id});
                }

                ViewBag.Veiculo = _veiculoDao.Get(autorizacao.Veiculo.Id);
                LoadResources();

                return View(autorizacao);
            }
            catch (Exception)
            {
                return View(autorizacao);
            }
        }

        [Route("autorizacao/{id:int}/editar")]
        public ActionResult Editar(int id)
        {
            AutorizacaoCirculacao autorizacao = _autorizacaoCirculacaoDao.Get(id);

            if (autorizacao == null)
            {
                this.Info("Autorização para circulação não encontrada.");
                return RedirectToAction("Index");
            }

            LoadResources();

            return View(autorizacao);
        }
        
        [HttpPost]
        [Route("autorizacao/{id:int}/editar")]
        [Transaction]
        public ActionResult Editar(AutorizacaoCirculacao autorizacao)
        {
            try
            {
                ValidationResult result = _validation.Validate(autorizacao);

                if (result.IsValid)
                {
                    _autorizacaoCirculacaoDao.Update(autorizacao);

                    this.Success("Autorização para circulação editada com sucesso.");
                    return RedirectToAction("Index", "Autorizacao", new { id = autorizacao.Id });
                }

                LoadResources();

                return View(autorizacao);   
            }
            catch (Exception)
            {
                return View(autorizacao);   
            }
        }

        [Route("autorizacao/quantitativo")]
        public ActionResult Quantitativo()
        {
            ViewBag.dados = _autorizacaoCirculacaoDao.QuantitativoPorMesEAno();
            return View();
        }

        [Route("autorizacao/{id:int}/imprimir")]
        public ActionResult Imprimir(int id)
        {
            AutorizacaoCirculacao autorizacao = _autorizacaoCirculacaoDao.Get(id);

            if (autorizacao == null)
            {
                this.Info("Autorização para circulação não encontrada.");
                return RedirectToAction("Index");
            }

            return View(autorizacao);
        }

        private void LoadResources()
        {
            ViewBag.Motoristas = _motoristaDao.GetAll();
            ViewBag.Funcionarios = _funcionarioDao.GetAll();
        }

    }
}