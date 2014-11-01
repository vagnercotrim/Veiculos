using System.Web.Mvc;
using Veiculos.DAO;

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}