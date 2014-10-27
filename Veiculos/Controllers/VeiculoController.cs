using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veiculos.DAO;
using Veiculos.Models;

namespace Veiculos.Controllers
{
    public class VeiculoController : Controller
    {

        private readonly VeiculoDAO _veiculoDao;

        public VeiculoController(VeiculoDAO veiculoDao)
        {
            _veiculoDao = veiculoDao;
        }

        public ActionResult Index()
        {
            IList<Veiculo> veiculos = _veiculoDao.GetAll();

            return View(veiculos);
        }
    }
}