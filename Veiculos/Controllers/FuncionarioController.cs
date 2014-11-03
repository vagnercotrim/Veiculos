using System.Web.Mvc;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;

namespace Veiculos.Controllers
{
    public class FuncionarioController : Controller
    {
            
        private readonly FuncionarioDAO _funcionarioDao;

        public FuncionarioController(FuncionarioDAO funcionarioDao)
        {
            _funcionarioDao = funcionarioDao;
        }

        [Route("funcionario")]
        public ActionResult Index(int pagina = 1)
        {
            Paging<Funcionario> funcionarios = _funcionarioDao.GetAll(pagina, 5);

            return View(funcionarios);
        }

    }
}