using System.Collections.Generic;
using System.Web.Mvc;
using Veiculos.DAO;
using Veiculos.Models;

namespace Veiculos.Controllers
{
    public class HomeController : Controller
    {

        private readonly VeiculoDAO _veiculoDao;

        public HomeController(VeiculoDAO veiculoDao)
        {
            _veiculoDao = veiculoDao;
        }

        public ActionResult Index()
        {
            ViewBag.emuso = _veiculoDao.QuantidadeVeiculosEmuso();

            return View();
        }

        public ActionResult AngularJS()
        {
            return View();
        }

        public ActionResult Todo()
        {
            var todos = TodoList();

            return Json(todos, JsonRequestBehavior.AllowGet);
        }

        private static IEnumerable<Todo> TodoList()
        {
            yield return (new Todo {Action = "Buy Flowers", Done = false});
            yield return (new Todo {Action = "Get Shoes", Done = false});
            yield return (new Todo {Action = "Collect Tickets", Done = true});
            yield return (new Todo {Action = "Call Joe", Done = false});
        }
    }
}