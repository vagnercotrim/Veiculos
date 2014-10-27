using FluentValidation.Results;
using System;
using System.Collections.Generic;
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
        private readonly VeiculoValidation _validation;

        public VeiculoController(VeiculoDAO veiculoDao, VeiculoValidation validation)
        {
            _veiculoDao = veiculoDao;
            _validation = validation;
        }

        public ActionResult Index()
        {
            IList<Veiculo> veiculos = _veiculoDao.GetAll();

            return View(veiculos);
        }

        public ActionResult Novo()
        {
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

                    return RedirectToAction("Detalhar", "Veiculo", new { id = veiculo.Id });
                }

                return View(veiculo);

            }
            catch (Exception)
            {
                return View(veiculo);
            }
        }

    }
}