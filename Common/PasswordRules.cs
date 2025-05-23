
using System.Text.RegularExpressions;

namespace Common
{
    public sealed class PasswordRules
    {
        //Order is important!
        public static List<Rules> StudentCheckRules = new List<Rules>
        {
            Rules.ContainsDigit,
            Rules.ContainsUpperCase,
            Rules.ContainsLowerCase,
        };
        public static List<Rules> TeacherCheckRules = new List<Rules>
        {
            Rules.ContainsUpperCase,
            Rules.ContainsLowerCase,
            Rules.ContainsNonAlphanumeric,
            Rules.ContainsDigit
        };
    }
    public enum Rules
    {
        ContainsUpperCase,
        ContainsLowerCase,
        ContainsDigit,
        ContainsNonAlphanumeric,
    }
}
