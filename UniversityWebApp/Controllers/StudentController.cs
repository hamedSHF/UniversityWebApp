using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using UniversityWebApp.ConfigOptions;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;
using UniversityWebApp.Services;
using UniversityWebApp.Services.States;

namespace UniversityWebApp.Controllers
{
    [Route("api/student")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> logger;
        private readonly IAuthorizationService authorizationService;
        private readonly IdentityAddressesOptions identityAddressesOptions;
        private readonly IRepository<Student> studentRepository;
        public StudentController(ILogger<StudentController> logger,
            IAuthorizationService authorizationService,
            IOptions<IdentityAddressesOptions> options,
            IRepository<Student> repository)
        {
            this.logger = logger;
            this.authorizationService = authorizationService;
            identityAddressesOptions = options.Value;
            studentRepository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudent(string userId)
        {
            //Validate user and get UserName
            var result = await authorizationService.AuthorizeUserById(userId, identityAddressesOptions.IdentityServerSecure);
            if (result.State == ResponseState.Success)
            {

            }
            //Return html page to the user
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if (student != null)
            {
                var result = await studentRepository.Add(student);
                if (result != null)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
            return StatusCode(406, "The passed value should not be null");
        }
    }
}
