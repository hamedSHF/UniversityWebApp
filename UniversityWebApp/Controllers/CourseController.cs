using Microsoft.AspNetCore.Mvc;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;

namespace UniversityWebApp.Controllers
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
