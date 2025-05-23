using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetAll(bool includeCourses);
        Task<Teacher?> GetTeacherById(string id);
        Task<int> GetIdOfLastRecord();
        Task<bool> Exists(string firstName,  string lastName);
    }
}
