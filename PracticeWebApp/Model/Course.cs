
namespace PracticeWebApp.Model
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public Teacher Instructor { get; set; }  
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
