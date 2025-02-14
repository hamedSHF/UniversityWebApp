using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model.DTOs;

namespace UniversityWebApp.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> logger;
        private readonly IMapper mapper;
        private readonly IStudentRepository studentRepository;
        public StudentController(ILogger<StudentController> logger,
            IStudentRepository repository,
            IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
            studentRepository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudent()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Subject.Name.ToLower() == "userid")?.Value;
            if(!string.IsNullOrEmpty(userId))
            {
                var student = await studentRepository.GetStudent(userId, true);
                return View(mapper.Map<StudentDto>(student));
            }
            //Token is invalid.Return error page
            return View();
        }
    }
}
