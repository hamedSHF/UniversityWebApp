using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeWebApp.Model
{
    public class Teacher
    {
        [Key]
        public string TeacherId { get; set; }
        public string Td_UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateTime WorkFrom { get; set; }
        public string Degree { get; set; }
        public ICollection<Course> Courses { get; private set; } = new List<Course>();
    }
}
