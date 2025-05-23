using System.Globalization;

namespace UniversityWebApp.Services
{
    public class UserNameGenerator
    {
        private static string year = DateTime.UtcNow.AddYears(-622).Year.ToString(CultureInfo.GetCultureInfo("fa-Ir"));
        public static string GenerateStudentUsername(int counter)
        {
            return year + (++counter);
        }
        public static string GenerateTeacherUsername(int counter,string prefix)
        {
            return prefix + (++counter) + year;
        }
    }
}
