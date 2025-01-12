using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetAll(bool includeCourses); 
    }
}
