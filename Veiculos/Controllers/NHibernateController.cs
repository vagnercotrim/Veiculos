using System.Web.Mvc;
using NHibernate.Stat;

namespace Veiculos.Controllers
{
    public class NHibernateController : Controller
    {

        private IStatistics _statistics;

        public NHibernateController(IStatistics statistics)
        {
            _statistics = statistics;
        }
        
        public ActionResult Index()
        {
            return View(_statistics);
        }

    }
}