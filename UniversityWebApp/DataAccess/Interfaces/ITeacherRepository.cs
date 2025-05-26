using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetAll(bool includeCourses);
        Task<Teacher?> GetTeacherById(int id);
        Task<bool> Exists(string firstName,  string lastName);
    }
}
