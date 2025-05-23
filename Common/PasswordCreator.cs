using System.Text.RegularExpressions;

namespace Common
{
    public class PasswordCreator
    {
        public static string CreateStudentPassword(string userName,string firstName,string lastName)
        {
            string password = string.Empty;
            foreach (var item in PasswordRules.StudentCheckRules)
            {
                switch (item)
                {
                    case Rules.ContainsDigit:
                        password += Regex.Match(userName, @"[\d]+").Value;
                        break;
                    case Rules.ContainsUpperCase:
                        password += firstName.ToUpper().First();
                        break;
                    case Rules.ContainsLowerCase:
                        password += lastName.ToLower().First();
                        break;
                }
            }
            return password;
        }
        public static string CreateTeacherPassword(string userName, string firstName, string lastName)
        {
            string password = string.Empty;
            foreach(var item in PasswordRules.TeacherCheckRules)
            {
                switch(item)
                {
                    case Rules.ContainsDigit:
                        password += Regex.Match(userName, @"[\d]+").Value;
                        break;
                    case Rules.ContainsUpperCase:
                        password += firstName.ToUpper().First();
                        break;
                    case Rules.ContainsLowerCase:
                        password += lastName.ToLower().First();
                        break;
                    case Rules.ContainsNonAlphanumeric:
                        password += "@";
                        break;
                }
            }
            return password;
        }
    }
}
