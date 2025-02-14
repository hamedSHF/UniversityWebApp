using IdentityServer.Models;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILoginService loginService;
        private const string studentURL = "https://localhost:7145/Student";
        public StudentController(ILogger<StudentController> logger,
            UserManager<ApplicationUser> userManager,
            ILoginService loginService)
        {
            _logger = logger;
            this.userManager = userManager;
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
            if(ModelState.IsValid)
            {
                var result = await loginService.Login(request, "Student");
                if(result.ResponseState == ResponseState.OK)
                {
                    HttpContext.Response.Cookies.Append("Authorization", $"Bearer {result.JWTToken}",new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true
                    });
                    return RedirectPermanent(studentURL + $"?userid={result.Content}");
                }
                else if(result.ResponseState == ResponseState.NotFound)
                {
                    return NotFound($"User with {result.Content} not founded!");
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if(ModelState.IsValid)
            {
                var result = await userManager.CreateAsync(new ApplicationUser
                {
                    Id = request.UserId,
                    UserName = request.UserName
                }, request.Password);
                if(result.Succeeded)
                {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            return BadRequest(ModelState);
        }
    }
}
