using IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IdentityServer.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public void Login()
        {

        }
        [HttpPost]
        //public Task<IActionResult> Register()
        //{

        //}
    }
}
