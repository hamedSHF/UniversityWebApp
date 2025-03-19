using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model
{
    public class Major
    {
        public string Title { get; set; }
        public ICollection<Student> Students { get; private set; } = new List<Student>();
        public ICollection<CourseTopics> Topics { get; private set; } = new List<CourseTopics>();
    }
}
