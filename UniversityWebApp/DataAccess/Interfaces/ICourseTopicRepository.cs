using System.Diagnostics.Eventing.Reader;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface ICourseTopicRepository : IRepository<CourseTopics>
    {
        Task<CourseTopics?> GetById(int id, bool includeCourses = false);
        Task<bool> Exists(int id);
    }
}
