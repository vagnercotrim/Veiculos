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
    public class CombustivelController : Controller
    {

        private readonly CombustivelDAO _dao;
        private readonly CombustivelValidation _validation;

        public CombustivelController(CombustivelDAO dao, CombustivelValidation validation)
        {
            _dao = dao;
            _validation = validation;
        }


        public ActionResult Index()
        {
            IList<Combustivel> combustiveis = _dao.GetAll();

            return View(combustiveis);
        }


        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        [Transaction]
        public ActionResult Novo(Combustivel combustivel)
        {
            try
            {
                ValidationResult result = _validation.Validate(combustivel);

                if (result.IsValid)
                {
                    _dao.Save(combustivel);

                    return RedirectToAction("Index", "Combustivel");
                }

                return View(combustivel);

            }
            catch (Exception)
            {
                return View(combustivel);
            }
        }
        public ActionResult Editar(int id)
        {
            Combustivel combustivel = _dao.Get(id);

            if (combustivel == null)
                return RedirectToAction("Index", "Combustivel");

            return View(combustivel);
        }

        [HttpPost]
        [Transaction]
        public ActionResult Editar(Combustivel combustivel)
        {
            try
            {
                Combustivel noBanco = _dao.Get(combustivel.Id);
                noBanco.Atualiza(combustivel);

                ValidationResult result = _validation.Validate(noBanco);

                if (result.IsValid)
                {
                    _dao.Update(noBanco);

                    return RedirectToAction("Index", "Combustivel");
                }

                return View(combustivel);

            }
            catch (Exception)
            {
                return View(combustivel);
            }
        }
        
    }
}