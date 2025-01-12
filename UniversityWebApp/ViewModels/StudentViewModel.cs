using UniversityWebApp.Model.Constants;
using UniversityWebApp.Model;
using System.Globalization;

namespace UniversityWebApp.ViewModels
{
    public class StudentViewModel
    {
        public string StudentUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Gender { get; set; }
        public EducationState EducationState { get; set; }
        public List<Course> Courses { get; set; }

        public string BirthDateString => BirthDate.ToString(CultureInfo.GetCultureInfo("fa-Ir"));
        public string RegisterDateString => RegisterDate.ToString(CultureInfo.GetCultureInfo("fa-Ir"));
        public string EducationStateString => EducationState.ToString();
    }
}
