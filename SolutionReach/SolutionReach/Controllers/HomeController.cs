using SolutionReach.Models;
using System.Web.Mvc;

namespace SolutionReach.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var model = new HomePageModel();
            model.Init();

            return View(model);
        }
    }
}
