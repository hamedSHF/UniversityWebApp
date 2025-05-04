using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface ICourseTopicRepository : IRepository<CourseTopics>
    {
        Task<CourseTopics?> GetTopic(string topic); 
    }
}
