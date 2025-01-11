using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebApp.Model
{
    public class Teacher
    {
        [Key]
        public Guid TeacherId { get; set; }
        public string TeacherUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly StartAt { get; set; }
        public DateOnly EndAt { get; set; }
        public string Degree { get; set; }
        public ICollection<Course> Courses { get; private set; } = new List<Course>();
    }
}
