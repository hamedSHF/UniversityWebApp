using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetStudent(string userId, bool includeCourses);
        Task<bool> Exists(string firstName, string lastName);
        Task<Guid> GetIdByUserName(string userName);
    }
}
