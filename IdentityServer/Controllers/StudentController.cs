using IdentityServer.Models;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenGenerator tokenGenerator;
        private const string studentURL = "https://localhost:7145/Student";
        public StudentController(ILogger<StudentController> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenGenerator tokenGenerator)
        {
            _logger = logger;
            this.signInManager = signInManager;
            this.tokenGenerator = tokenGenerator;
            this.userManager = userManager;
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
                var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password,request.RememberMe,false);
                if (result.Succeeded)
                {
                    var user = await signInManager.UserManager.FindByNameAsync(request.UserName);
                    CookieOptions options = new CookieOptions
                    {
                        Secure = true,
                        HttpOnly = true,
                    };
                    HttpContext.Response.Cookies.
                        Append("Authorization", $"Bearer {this.tokenGenerator.GenerateJwtToken(this.tokenGenerator.GenerateJwtClaims(user.Id,"Student"))}",options);
                    return RedirectPermanent(studentURL + $"?userid={user.Id}");
                }
                else if(result == Microsoft.AspNetCore.Identity.SignInResult.Failed)
                {
                    return NotFound("User is not founded");
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
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
