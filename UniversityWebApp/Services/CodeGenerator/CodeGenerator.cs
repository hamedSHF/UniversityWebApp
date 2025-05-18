namespace UniversityWebApp.Services.CodeGenerator
{
    public abstract class CodeGenerator
    {
        public abstract ICode GenerateCode(Dictionary<string, string> parts);
    }
}
