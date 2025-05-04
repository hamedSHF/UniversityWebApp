using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface IMajorRepository : IRepository<Major>
    {
        Task<IEnumerable<Major>> GetAllWithTopics();
        Task<Major?> GetMajor(string title, bool includeTopics = false);
        Task<bool> Exists(string title);
    }
}
