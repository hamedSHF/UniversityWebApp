using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface ICourseTopicRepository : IRepository<CourseTopics>
    {
        Task<CourseTopics?> GetTopic(ushort id);
        Task<bool> Exists(ushort id);
    }
}
