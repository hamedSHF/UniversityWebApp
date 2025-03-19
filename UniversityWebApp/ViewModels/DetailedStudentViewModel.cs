using UniversityWebApp.Model.Constants;
using System.Globalization;
using UniversityWebApp.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;

namespace UniversityWebApp.ViewModels
{
    public class DetailedStudentViewModel
    {
        public string StudentUserName { get; set; }
        [RegularExpression("[a-zA-Z]+")]
        public string FirstName { get; set; }
        [RegularExpression("[a-zA-Z]+")]
        public string LastName { get; set; }
        [AllowedValues("Male","Female")]
        public string Gender { get; set; }
        public List<string> EducationStates => Enum.GetNames(typeof(EducationState)).ToList();
        public List<Course>? Courses { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Must be in DateOnly format")]
        public DateOnly BirthDate { get; set; }
        [DataType(DataType.DateTime,ErrorMessage ="Must be in DateTime format")]
        public DateTime RegisterDate { get; set; }
        public string EducationState { get; set; }

        public static DetailedStudentViewModel CreateDetailedStudent(Student student)
        {
            return new DetailedStudentViewModel
            {
                StudentUserName = student.StudentUserName,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                RegisterDate = student.RegisterDate,
                Gender = student.Gender,
                Courses = student.Courses.ToList(),
                EducationState = student.EducationState.ToString(),
            };
        }
    }
}
