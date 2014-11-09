using System;
using System.Web.Mvc;
using FluentValidation.Results;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;
using Veiculos.Models.Validation;

namespace Veiculos.Controllers
{
    public class FuncionarioController : Controller
    {
            
        private readonly FuncionarioDAO _funcionarioDao;
        private readonly FuncionarioValidation _validation;

        public FuncionarioController(FuncionarioDAO funcionarioDao, FuncionarioValidation validation)
        {
            _funcionarioDao = funcionarioDao;
            _validation = validation;
        }

        [Route("funcionario/{pagina?}")]
        public ActionResult Index(int pagina = 1)
        {
            Paging<Funcionario> funcionarios = _funcionarioDao.GetAll(pagina, 5);

            return View(funcionarios);
        }

        [Route("funcionario/novo")]
        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        [Route("funcionario/novo")]
        [Transaction]
        public ActionResult Novo(Funcionario funcionario)
        {
            try
            {
                ValidationResult result = _validation.Validate(funcionario);

                if (result.IsValid)
                {
                    _funcionarioDao.Save(funcionario);

                    return RedirectToAction("Detalhar", "Funcionario", new { id = funcionario.Id });
                }

                return View(funcionario);
            }
            catch (Exception)
            {
                return View(funcionario);
            }
        }
        
        [Route("funcionario/{id:int}/detalhar")]
        public ActionResult Detalhar(int id)
        {
            Funcionario funcionario = _funcionarioDao.Get(id);

            if (funcionario == null)
                return RedirectToAction("Index", "Funcionario");

            return View(funcionario);
        }

    }
}