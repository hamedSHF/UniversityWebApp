using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniversityWebApp.ConfigOptions;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;
using UniversityWebApp.Services;

namespace UniversityWebApp.Controllers
{
    [Route("api/student")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> logger;
        private readonly IIdentityService authorizationService;
        private readonly IdentityAddressesOptions identityAddressesOptions;
        private readonly IRepository<Student> studentRepository;
        public StudentController(ILogger<StudentController> logger,
            IIdentityService authorizationService,
            IOptions<IdentityAddressesOptions> options,
            IRepository<Student> repository)
        {
            this.logger = logger;
            this.authorizationService = authorizationService;
            identityAddressesOptions = options.Value;
            studentRepository = repository;
        }
    }
}
