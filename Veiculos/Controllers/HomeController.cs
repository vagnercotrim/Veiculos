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
            IList<Todo> todos = new List<Todo>();
            todos.Add(new Todo { Action = "Buy Flowers", Done = false });
            todos.Add(new Todo { Action = "Get Shoes", Done = false });
            todos.Add(new Todo { Action = "Collect Tickets", Done = true });
            todos.Add(new Todo { Action = "Call Joe", Done = false });

            return Json(todos, JsonRequestBehavior.AllowGet);
        }

    }
}