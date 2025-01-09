using Microsoft.EntityFrameworkCore;
using PracticeWebApp.DataAccess;
using PracticeWebApp.DataAccess.Interfaces;
using PracticeWebApp.Model;

namespace UniversityWebApp.DataAccess.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly UniversityDbContext dbContext;
        public StudentRepository(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Student> Add(Student entity)
        {
            var addedStudent = dbContext.Students.Add(entity);
            await dbContext.SaveChangesAsync();
            return addedStudent.Entity;
        }

        public async Task Delete(Student entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAll()
        {
            return await dbContext.Students.Include(x => x.Courses).ToListAsync();
        }

        public async Task<Student> GetByUserName(string userName)
        {
            return await dbContext.Students.FindAsync(userName);
        }

        public async Task<Student> Update(Student entity)
        {
            var updatedStudent = dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
            return updatedStudent.Entity;
        }
    }
}
