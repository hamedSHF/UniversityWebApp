using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ILoginService loginService;
        private const string adminURL = "https://localhost:7145/Admin/Index";

        public AdminController(ILogger<AdminController> logger, ILoginService loginService)
        {
            _logger = logger;
            this.loginService = loginService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestBody request)
        {
            if (ModelState.IsValid)
            {
                var result = await loginService.Login(request, "Admin");
                if (result.ResponseState == ResponseState.OK)
                {
                    HttpContext.Response.Cookies.Append("Authorization", String.Format("Bearer {0}", result.JWTToken), new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true
                    });
                    return RedirectPermanent(adminURL);
                }
                else if (result.ResponseState == ResponseState.NotFound)
                {
                    return NotFound($"User with {result.Content} not founded!");
                }
            }
            return BadRequest();
        }
    }
}
