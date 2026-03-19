using Microsoft.AspNetCore.Mvc;

namespace Gis_Project.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
