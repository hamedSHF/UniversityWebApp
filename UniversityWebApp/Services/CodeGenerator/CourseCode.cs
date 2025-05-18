namespace UniversityWebApp.Services.CodeGenerator
{
    public class CourseCode : ICode
    {
        private readonly string prefix;
        private readonly string body;
        private readonly string? suffix;
        public CourseCode(string prefix, string body, string? suffix)
        {
            this.prefix = prefix;
            this.body = body;
            this.suffix = suffix;
        }

        public string GetCode() => prefix + body + suffix;

        public string[] GetParts()
        {
            return new string[3]
            {
                prefix,
                body,
                suffix ?? string.Empty
            };
        }
    }
}
