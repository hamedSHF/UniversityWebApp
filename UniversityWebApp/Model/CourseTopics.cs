using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model
{
    public class CourseTopics
    {
        public string Title { get; set; }
        public ICollection<Major> Majors { get; private set; } = new List<Major>();
        public ICollection<Course> Courses { get; private set; } = new List<Course>();
    }
}
