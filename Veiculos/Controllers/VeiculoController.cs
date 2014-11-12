using FluentValidation.Results;
using System;
using System.Web.Mvc;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;
using Veiculos.Models.Validation;

namespace Veiculos.Controllers
{
    public class VeiculoController : Controller
    {

        private readonly VeiculoDAO _veiculoDao;
        private readonly CombustivelDAO _combustivelDao;
        private readonly VeiculoValidation _validation;

        public VeiculoController(VeiculoDAO veiculoDao, VeiculoValidation validation, CombustivelDAO combustivelDao)
        {
            _veiculoDao = veiculoDao;
            _validation = validation;
            _combustivelDao = combustivelDao;
        }

        [Route("veiculo/{pagina?}")]
        public ActionResult Index(Situacao? situacao, int pagina = 1)
        {
            ViewBag.Situacao = situacao;

            Paging<Veiculo> veiculos = _veiculoDao.GetAll(situacao, pagina, 5);

            return View(veiculos);
        }

        [Route("veiculo/novo")]
        public ActionResult Novo()
        {
            ViewBag.Combustiveis = _combustivelDao.GetAll();

            return View();
        }

        [HttpPost]
        [Route("veiculo/novo")]
        [Transaction]
        public ActionResult Novo(Veiculo veiculo)
        {
            try
            {
                ValidationResult result = _validation.Validate(veiculo);

                if (result.IsValid)
                {
                    _veiculoDao.Save(veiculo);

                    return RedirectToAction("Detalhar", "Veiculo", new {id = veiculo.Id});
                }

                ViewBag.Combustiveis = _combustivelDao.GetAll();
                return View(veiculo);
            }
            catch (Exception)
            {
                ViewBag.Combustiveis = _combustivelDao.GetAll();
                return View(veiculo);
            }
        }

        [Route("veiculo/{id:int}/editar")]
        public ActionResult Editar(int id)
        {
            ViewBag.Combustiveis = _combustivelDao.GetAll();

            Veiculo veiculo = _veiculoDao.Get(id);

            if (veiculo == null)
                return RedirectToAction("Index", "Veiculo");

            return View(veiculo);
        }

        [HttpPost]
        [Route("veiculo/{id:int}/editar")]
        [Transaction]
        public ActionResult Editar(Veiculo veiculo)
        {
            try
            {
                Veiculo noBanco = _veiculoDao.Get(veiculo.Id);

                ValidationResult result = _validation.Validate(veiculo);

                if (result.IsValid)
                {
                    _veiculoDao.Update(veiculo);

                    return RedirectToAction("Detalhar", new {id = noBanco.Id});
                }

                ViewBag.Combustiveis = _combustivelDao.GetAll();
                return View(veiculo);
            }
            catch (Exception)
            {
                ViewBag.Combustiveis = _combustivelDao.GetAll();
                return View(veiculo);
            }
        }
        
        [Route("veiculo/{id:int}/detalhar")]
        public ActionResult Detalhar(int id)
        {
            Veiculo veiculo = _veiculoDao.Get(id);

            if (veiculo == null)
            {
                this.Warning("Veículo não encontrado.");
                return RedirectToAction("Index", "Veiculo");
            }

            return View(veiculo);
        }

    }
}