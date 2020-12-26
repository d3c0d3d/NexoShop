using System.Web.Mvc;

namespace NexoShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = $"{nameof(NexoShop)} - Version: {typeof(MvcApplication).Assembly.GetName().Version}" ;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Fale Conosco";

            return View();
        }
    }
}