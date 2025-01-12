using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniversityWebApp.ConfigOptions;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;
using UniversityWebApp.Model.DTOs;
using UniversityWebApp.Services;
using UniversityWebApp.ViewModels;

namespace UniversityWebApp.Controllers
{
    [Route("api/Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IOptions<IdentityAddressesOptions> identityOptions;
        private readonly IUserNameGenerator userNameGenerator;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        public AdminController(ITeacherRepository teacherRepository,
            IStudentRepository studentRepository, 
            ICourseRepository courseRepository,
            IOptions<IdentityAddressesOptions> identityOptions,
            IUserNameGenerator userNameGenerator,
            IIdentityService identityService,
            IMapper mapper)
        {
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.identityOptions = identityOptions;
            this.userNameGenerator = userNameGenerator;
            this.identityService = identityService;
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
                student.RegisterDate = DateTime.Now;
                student.EducationState = Model.Constants.EducationState.Undergraduate;
                var addedEntity = await studentRepository.Add(student);
                if(addedEntity != null)
                {
                    await identityService.CreateUserForIdentity(addedEntity.StudentId.ToString(), addedEntity.StudentUserName,
                        studentDto.Password,identityOptions.Value.IdentityServerSecure);
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
        public async Task<IActionResult> ManageStudents()
        {
            var students = await studentRepository.GetAll(true);
            return View(students.Select(x => new StudentViewModel
            {
                StudentUserName = x.StudentUserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate,
                RegisterDate = x.RegisterDate,
                Gender = x.Gender,
                EducationState = x.EducationState,
                Courses = x.Courses.ToList()
            }));
        }
        [HttpGet]
        public IActionResult Confirmation(string message)
        {
            ViewData["ConfirmMsg"] = message;
            return View();
        }
    }
}
