namespace UniversityWebApp.Model.ResponseTypes
{
    public record MajorResponse(string title,
        ICollection<string> topics = default,
        ICollection<Student> students = default);
}
