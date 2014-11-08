using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veiculos.DAO;
using Veiculos.Infra.NHibernate;
using Veiculos.Models;

namespace Veiculos.Controllers
{
    public class AutorizacaoController : Controller
    {

        private readonly AutorizacaoCirculacaoDAO _autorizacaoCirculacaoDao;

        public AutorizacaoController(AutorizacaoCirculacaoDAO autorizacaoCirculacaoDao)
        {
            _autorizacaoCirculacaoDao = autorizacaoCirculacaoDao;
        }


        [Route("autorizacao")]
        public ActionResult Index(int pagina = 1)
        {
            Paging<AutorizacaoCirculacao> veiculos = _autorizacaoCirculacaoDao.GetAll(pagina, 5);

            return View(veiculos);
        }

    }
}