using System.Web.Http;
using System.Web.Mvc;

namespace WebApplication.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class SwaggerPageController : Controller
    {
        public SwaggerPageController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public SwaggerPageController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}