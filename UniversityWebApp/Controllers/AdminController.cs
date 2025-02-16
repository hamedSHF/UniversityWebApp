using AutoMapper;
using Common;
using Common.IntegratedEvents;
using MassTransit;
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
    [Authorize(Policy ="Admin")]
    public class AdminController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IOptions<IdentityAddressesOptions> identityOptions;
        private readonly IUserNameGenerator userNameGenerator;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IMapper mapper;
        private readonly ILogger<AdminController> adminLogger;
        public AdminController(ITeacherRepository teacherRepository,
            IStudentRepository studentRepository, 
            ICourseRepository courseRepository,
            IOptions<IdentityAddressesOptions> identityOptions,
            IUserNameGenerator userNameGenerator,
            IPublishEndpoint publishEndpoint,
            IMapper mapper,
            ILogger<AdminController> adminLogger)
        {
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.identityOptions = identityOptions;
            this.userNameGenerator = userNameGenerator;
            this.publishEndpoint = publishEndpoint;
            this.mapper = mapper;
            this.adminLogger = adminLogger;
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
            try
            {
                if (ModelState.IsValid)
                {
                    if (await studentRepository.StudentExists(studentDto.FirstName, studentDto.LastName))
                        return StatusCode(StatusCodes.Status409Conflict, "Student already exists."); 
                    var count = await studentRepository.CountAll();
                    var student = Student.CreateStudent(userNameGenerator.GenerateUserName(count), studentDto);
                    var addedEntity = await studentRepository.Add(student);
                    await studentRepository.SaveChanges();
                    if (addedEntity != null)
                    {
                        string userName = addedEntity.StudentUserName;
                        await publishEndpoint.Publish(new CreatedStudentEvent
                            (addedEntity.StudentId.ToString(),userName,
                            PasswordCreator.CreatePassword(userName,addedEntity.FirstName,addedEntity.LastName)));
                        return RedirectToAction(nameof(Confirmation), new { message = $"User with Id {addedEntity.StudentId} added." });
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                adminLogger.LogError(ex, "Error occured while adding student.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
