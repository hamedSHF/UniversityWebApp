using System.Globalization;

namespace UniversityWebApp.Services
{
    public class UserNameGenerator : IUserNameGenerator
    {
        public string GenerateUserName(int counter)
        {
            var year = DateTime.UtcNow.AddYears(-622).Year.ToString(CultureInfo.GetCultureInfo("fa-Ir"));
            return year + (++counter);
        }
    }
}
