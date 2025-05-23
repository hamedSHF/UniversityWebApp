
namespace Common.IntegratedEvents
{
    public record CreatedStudentEvent(string id,string userName,string password);
    public record CreatedTeacherEvent(string id, string userName, string password);
}
