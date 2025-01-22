using Microsoft.AspNetCore.Mvc;

namespace UniversityWebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
