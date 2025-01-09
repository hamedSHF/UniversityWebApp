using PracticeWebApp.Model.Constants;
using System.ComponentModel.DataAnnotations;

namespace PracticeWebApp.Model
{
    public class Student
    {
        [Key]
        public string StudentId { get; set; }
        public string Std_UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public Gender Gender { get; set; }
        public ICollection<Course> Courses { get; private set; } = new List<Course>();
    }
}
