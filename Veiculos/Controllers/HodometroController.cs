using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Results;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;
using Veiculos.Models.Validation;

namespace Veiculos.Controllers
{
    public class HodometroController : Controller
    {

        private readonly HodometroDAO _dao;
        private readonly VeiculoDAO _veiculoDao;
        private readonly HodometroValidation _validation;

        public HodometroController(HodometroDAO dao, VeiculoDAO veiculoDao, HodometroValidation validation)
        {
            _dao = dao;
            _veiculoDao = veiculoDao;
            _validation = validation;
        }

        [Route("veiculo/{id}/hodometro")]
        public ActionResult Index(int id)
        {
            ViewBag.Veiculo = _veiculoDao.Get(id);

            IList<Hodometro> registros = _dao.FindByVeiculo(id);

            return View(registros);
        }

        [Route("veiculo/{id:int}/hodometro/novoregistro")]
        [HttpGet]
        public ActionResult Novo(int id)
        {
            ViewBag.Veiculo = _veiculoDao.Get(id);

            return View();
        }

        [HttpPost]
        [Route("veiculo/{id:int}/hodometro/novoregistro")]
        [Transaction]
        public ActionResult Novo(Hodometro hodometro)
        {
            try
            {
                ValidationResult result = _validation.Validate(hodometro);

                if (result.IsValid)
                {
                    _dao.Save(hodometro);

                    return RedirectToAction("Detalhar", "Veiculo", new {id = hodometro.Veiculo.Id});
                }

                ViewBag.Veiculo = _veiculoDao.Get(hodometro.Veiculo.Id);
                return View(hodometro);
            }
            catch (Exception)
            {
                ViewBag.Veiculo = _veiculoDao.Get(hodometro.Veiculo.Id);
                return View(hodometro);
            }
        }

    }
}