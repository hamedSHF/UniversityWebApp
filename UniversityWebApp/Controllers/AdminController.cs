using AutoMapper;
using Common;
using Common.IntegratedEvents;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniversityWebApp.ConfigOptions;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;
using UniversityWebApp.Model.DTOs;
using UniversityWebApp.Services;
using UniversityWebApp.ViewModels;
using ResponseType = UniversityWebApp.Model.ResponseTypes.Response;

namespace UniversityWebApp.Controllers
{
    [Authorize(Policy ="Admin")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMajorRepository majorRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IOptions<IdentityAddressesOptions> identityOptions;
        private readonly IUserNameGenerator userNameGenerator;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly ILogger<AdminController> adminLogger;
        private readonly IMapper mapper;
        public AdminController(ITeacherRepository teacherRepository,
            IStudentRepository studentRepository, 
            ICourseRepository courseRepository,
            IMajorRepository majorRepository,
            IOptions<IdentityAddressesOptions> identityOptions,
            IUserNameGenerator userNameGenerator,
            IPublishEndpoint publishEndpoint,
            ILogger<AdminController> adminLogger,
            IMapper mapper)
        {
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.majorRepository = majorRepository;
            this.identityOptions = identityOptions;
            this.userNameGenerator = userNameGenerator;
            this.publishEndpoint = publishEndpoint;
            this.adminLogger = adminLogger;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index() => View();
        [HttpGet]
        public IActionResult RegisterStudent() => View();
        [HttpPost]
        public async Task<IActionResult> RegisterStudent([FromForm] AddStudentDto studentDto)
        {
            try
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
                        (addedEntity.StudentId.ToString(), userName,
                        PasswordCreator.CreatePassword(userName, addedEntity.FirstName, addedEntity.LastName)));
                    return RedirectToAction(nameof(Confirmation), new { message = $"User with Id {addedEntity.StudentId} added." });
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                adminLogger.LogError(ex, "Error occured while adding student.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ManageStudents()
        {
            var students = await studentRepository.GetAll();
            return View(students.Select(x => new StudentViewModel
            {
                StudentUserName = x.StudentUserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
            }));
        }
        [HttpGet]
        public IActionResult Confirmation(string message)
        {
            ViewData["ConfirmMsg"] = message;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DetailedStudent(string id)
        {
            var student = await studentRepository.GetStudent(id, true);
            if (student != null)
            {
                return View(DetailedStudentViewModel.CreateDetailedStudent(student));
            }
            return BadRequest($"Student with : {id} id is not existed");
        }
        [HttpPost]
        public async Task<Results<Ok<ResponseType>,BadRequest>> UpdateStudent([FromForm] DetailedStudentViewModel studentViewModel)
        {
            var student = mapper.Map<Student>(studentViewModel);
            student.StudentId = await studentRepository.GetIdByUserName(student.StudentUserName);
            studentRepository.Update(student);
            var result = await studentRepository.SaveChanges();
            return result > 0 ? TypedResults.Ok(new ResponseType("Student updated successfully.",
                $"/Admin/{nameof(ManageStudents)}",Model.ResponseTypes.ResponseActions.Redirect)) 
                : TypedResults.BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> ManageCourses()
        {
            return View(new MajorViewModel { Titles = (await majorRepository.GetAll()).Select(x => x.Title) });
        }
    }
}
