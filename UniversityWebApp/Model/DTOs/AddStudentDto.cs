using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UniversityWebApp.Model.Constants;

namespace UniversityWebApp.Model.DTOs
{
    public class AddStudentDto
    {
        [MaxLength(50)]
        [RegularExpression("[a-z A-Z]",ErrorMessage ="FirstName should not include numbers")]
        [Required]
        public string FirstName { get; set; } = null!;
        [MaxLength(150)]
        [RegularExpression("[a-z A-Z]", ErrorMessage="LastName should not include numbers")]
        [Required]
        public string LastName { get; set; } = null!;
        [DataType(DataType.Date)]
        [Required]
        public DateOnly BirthDate { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public string Gender { get; set; }
    }
}
