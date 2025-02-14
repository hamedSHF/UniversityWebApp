using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetAll(bool includeCourses);
        Task<Student?> GetStudent(string userId, bool includeCourses);
    }
}
