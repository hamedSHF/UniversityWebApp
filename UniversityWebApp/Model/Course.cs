using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }
        public CourseTopics CourseTopic { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<CourseDetails> CourseDetails { get; private set; } = new List<CourseDetails>();
        public ICollection<Student> Students { get; private set; } = new List<Student>();

        public static Course CreateCourse(string code, DateOnly startTime,
            DateOnly endTime, CourseTopics topic,
            Teacher teacher, IEnumerable<CourseDetails> details)
            => new Course
            {
                CourseCode = code,
                StartTime = startTime,
                EndTime = endTime,
                CourseTopic = topic,
                Teacher = teacher,
                CourseDetails = details.ToList()
            };
    }
}
