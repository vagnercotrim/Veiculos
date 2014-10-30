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

        public ActionResult Index(int pagina = 1)
        {
            Paging<Veiculo> veiculos = _veiculoDao.GetAll(pagina, 5);

            return View(veiculos);
        }

        public ActionResult Novo()
        {
            ViewBag.Combustiveis = _combustivelDao.GetAll();

            return View();
        }

        [HttpPost]
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

                return View(veiculo);

            }
            catch (Exception)
            {
                return View(veiculo);
            }
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Combustiveis = _combustivelDao.GetAll();

            Veiculo veiculo = _veiculoDao.Get(id);

            if (veiculo == null)
                return RedirectToAction("Index", "Veiculo");

            return View(veiculo);
        }

        [HttpPost]
        [Transaction]
        public ActionResult Editar(Veiculo veiculo)
        {
            try
            {
                Veiculo noBanco = _veiculoDao.Get(veiculo.Id);
                noBanco.Atualiza(veiculo);

                ValidationResult result = _validation.Validate(noBanco);

                if (result.IsValid)
                {
                    _veiculoDao.Update(noBanco);

                    return RedirectToAction("Detalhar", new {id = noBanco.Id});
                }

                return View(veiculo);

            }
            catch (Exception)
            {
                return View(veiculo);
            }
        }
        
        public ActionResult Detalhar(int id)
        {
            Veiculo veiculo = _veiculoDao.Get(id);

            if (veiculo == null)
                return RedirectToAction("Index", "Veiculo");

            return View(veiculo);
        }

    }
}