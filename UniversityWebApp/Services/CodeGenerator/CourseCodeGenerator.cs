namespace UniversityWebApp.Services.CodeGenerator
{
    public class CourseCodeGenerator : CodeGenerator
    {
        public override ICode GenerateCode(Dictionary<string, string> parts)
        {
            if (parts.TryGetValue("body", out string body) && !string.IsNullOrEmpty(body))
            {
                if (parts.TryGetValue("prefix", out string prefix) && !string.IsNullOrEmpty(prefix))
                {
                    parts.TryGetValue("suffix", out string? suffix);
                    return new CourseCode(prefix, body, suffix);
                }
                throw new Exception("Prefix is not provided");
            }
            else
                throw new Exception("Body is not provided");
        }
    }
}
