using System.Web.Mvc;

namespace TriageService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
