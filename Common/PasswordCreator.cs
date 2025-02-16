using System.Text.RegularExpressions;

namespace Common
{
    public class PasswordCreator
    {
        public static string CreatePassword(string userName,string firstName,string lastName)
        {
            string password = string.Empty;
            foreach (var item in PasswordRules.CheckRules)
            {
                if(item == Rules.ContainsDigit)
                {
                    password += Regex.Match(userName, @"[\d]+").Value;
                }
                if(item == Rules.ContainsUpperCase)
                {
                    password += firstName.ToUpper().First();
                }
                if(item == Rules.ContainsLowerCase)
                {
                    password += lastName.ToLower().First();
                }
            }
            return password;
        }
    }
}
