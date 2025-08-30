namespace UniversityWebApp.Services
{
    public class FileInfoConstants
    {
        public const string TeacherRabbitCache = "TeacherSubmitionFailure";
        public const string StudentRabbitCache = "StudentSubmitionFailure";
        public static string RabbitCachePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RabbitMqCache");
    }
}
