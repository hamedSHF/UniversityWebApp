using PracticeWebApp.Model.Constants;
using System.ComponentModel.DataAnnotations;
using UniversityWebApp.Model.Constants;

namespace UniversityWebApp.Model.DTOs
{
    public class AddStudentDto
    {
        [MaxLength(50)]
        [RegularExpression("[a-z A-Z]")]
        [Required]
        public string FirstName { get; set; } = null!;
        [MaxLength(150)]
        [RegularExpression("[a-z A-Z]")]
        [Required]
        public string LastName { get; set; } = null!;
        [DataType(DataType.Date)]
        [Required]
        public DateOnly BirthDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; }
        public string Gender { get; set; }
        public EducationState EducationState { get; set; }
    }
}
