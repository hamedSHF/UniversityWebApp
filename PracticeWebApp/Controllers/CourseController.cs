using Microsoft.AspNetCore.Mvc;
using PracticeWebApp.DataAccess;
using PracticeWebApp.DataAccess.Interfaces;
using PracticeWebApp.Model;

namespace PracticeWebApp.Controllers
{
    [ApiController]
    [Route("/api/course")]
    public class CourseController : ControllerBase
    {
        private readonly IRepository<Course> repository;
        public CourseController(IRepository<Course> repository)
        {
            this.repository = repository;
        }
    }
}
