using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface IMajorRepository : IRepository<Major>
    {
        Task<IEnumerable<Major>> GetAllWithTopics();
        Task<Major?> FindMajor(string title); 
    }
}
