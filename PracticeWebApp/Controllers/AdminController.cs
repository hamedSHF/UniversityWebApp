using Microsoft.AspNetCore.Mvc;
using PracticeWebApp.DataAccess.Interfaces;
using PracticeWebApp.Model;

namespace UniversityWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<Teacher> teacherRepository;
        private readonly IRepository<Student> studentRepository;
        public AdminController(IRepository<Teacher> teacherRepository,
            IRepository<Student> studentRepository, 
            IRepository<Course> courseRepository)
        {
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
