using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model.DTOs
{
    public class AddTeacherDto
    {
        [MaxLength(50)]
        [RegularExpression("([a-z A-Z]+)", ErrorMessage = "FirstName should not include numbers")]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(150)]
        [RegularExpression("([a-z A-Z]+)", ErrorMessage = "LastName should not include numbers")]
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateOnly BirthDate { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateOnly StartAt { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateOnly EndAt { get; set; }
        [MaxLength(200)]
        [Required]
        public string Degree { get; set; }
    }
}
