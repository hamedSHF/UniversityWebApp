using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
