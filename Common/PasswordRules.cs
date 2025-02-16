
using System.Text.RegularExpressions;

namespace Common
{
    public sealed class PasswordRules
    {
        //Order is important!
        public static List<Rules> CheckRules = new List<Rules>
        {
            Rules.ContainsDigit,
            Rules.ContainsUpperCase,
            Rules.ContainsLowerCase,
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
