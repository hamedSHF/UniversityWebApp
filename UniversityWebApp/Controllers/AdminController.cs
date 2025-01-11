using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;
using UniversityWebApp.Model.DTOs;
using UniversityWebApp.Services;

namespace UniversityWebApp.Controllers
{
    [Route("api/Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Teacher> teacherRepository;
        private readonly IRepository<Student> studentRepository;
        private readonly IUserNameGenerator userNameGenerator;
        private readonly IMapper mapper;
        public AdminController(IRepository<Teacher> teacherRepository,
            IRepository<Student> studentRepository, 
            IRepository<Course> courseRepository,
            IUserNameGenerator userNameGenerator,
            IMapper mapper)
        {
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.userNameGenerator = userNameGenerator;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult RegisterStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterStudent(AddStudentDto studentDto)
        {
            if(ModelState.IsValid)
            {
                var count = await studentRepository.CountAll();
                var student = mapper.Map<Student>(studentDto);
                student.StudentUserName = userNameGenerator.GenerateUserName(count);
                var addedEntity = await studentRepository.Add(student);
                if(addedEntity != null)
                {
                    //Send data to identityServer
                    return RedirectToAction(nameof(Confirmation),$"User with Id {addedEntity.StudentId} added.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult Confirmation(string message)
        {
            ViewData["ConfirmMsg"] = message;
            return View();
        }
    }
}
