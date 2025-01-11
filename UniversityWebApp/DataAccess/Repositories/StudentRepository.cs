using Microsoft.EntityFrameworkCore;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;

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
            return addedStudent.Entity;
        }

        public async Task Delete(Student entity)
        {
            dbContext.Remove(entity);
        }

        public async Task<List<Student>> GetAll()
        {
            return await dbContext.Students.Include(x => x.Courses).ToListAsync();
        }

        public async Task<Student> Update(Student entity)
        {
            var updatedStudent = dbContext.Update(entity);
            return updatedStudent.Entity;
        }
        public async Task<int> CountAll()
        {
            return await dbContext.Students.CountAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
