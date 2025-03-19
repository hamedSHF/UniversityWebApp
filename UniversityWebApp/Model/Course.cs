using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }
        public CourseTopics CourseTopic { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<CourseDetails> CourseDetails { get; private set; } = new List<CourseDetails>();
        public ICollection<Student> Students { get; private set; } = new List<Student>();
    }
}
