using System.ComponentModel.DataAnnotations;
using UniversityWebApp.Model.Constants;

namespace UniversityWebApp.Model
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }
        public string StudentUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Gender { get; set; }
        public EducationState EducationState { get; set; }
        public ICollection<Course> Courses { get; private set; } = new List<Course>();
    }
}
