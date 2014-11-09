using System;
using System.Web.Mvc;
using FluentValidation.Results;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;
using Veiculos.Models.Validation;

namespace Veiculos.Controllers
{
    public class MotoristaController : Controller
    {

        private readonly MotoristaDAO _motoristaDao;
        private readonly FuncionarioDAO _funcionarioDao;
        private readonly MotoristaValidation _validation;

        public MotoristaController(MotoristaDAO motoristaDao, MotoristaValidation validation, FuncionarioDAO funcionarioDao)
        {
            _motoristaDao = motoristaDao;
            _validation = validation;
            _funcionarioDao = funcionarioDao;
        }

        [Route("motorista/{pagina?}")]
        public ActionResult Index(int pagina = 1)
        {
            Paging<Motorista> veiculos = _motoristaDao.GetAll(pagina, 5);

            return View(veiculos);
        }
        
        [Route("motorista/novo")]
        public ActionResult Novo(int id)
        {
            Motorista motorista = _motoristaDao.FindByFuncionario(id);

            if (motorista != null)
            {
                this.Warning("Já existe um cadastro de motorista para o funcionário.");
                return RedirectToAction("Detalhar", "Motorista", new {id = motorista.Id});
            }

            Funcionario funcionario = _funcionarioDao.Get(id);

            if (funcionario == null)
                return RedirectToAction("Index");

            ViewBag.Funcionario = funcionario;

            return View();
        }

        [HttpPost]
        [Route("motorista/novo")]
        [Transaction]
        public ActionResult Novo(Motorista motorista)
        {
            try
            {
                ValidationResult result = _validation.Validate(motorista);

                if (result.IsValid)
                {
                    _motoristaDao.Save(motorista);

                    return RedirectToAction("Detalhar", "Motorista", new { id = motorista.Id });
                }

                ViewBag.Funcionario = _funcionarioDao.Get(motorista.Funcionario.Id);
                
                return View(motorista);
            }
            catch (Exception)
            {
                return View(motorista);
            }
        }

        [Route("motorista/{id:int}/detalhar")]
        public ActionResult Detalhar(int id)
        {
            Motorista motorista = _motoristaDao.Get(id);

            if (motorista == null)
                return RedirectToAction("Index", "Motorista");

            return View(motorista);
        }
       
    }
}