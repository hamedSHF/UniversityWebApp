using Microsoft.EntityFrameworkCore;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityDbContext dbContext;
        public StudentRepository(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Student> Add(Student entity)
        {
            var addedStudent = await dbContext.Students.AddAsync(entity);
            return addedStudent.Entity;
        }

        public async Task<bool> Delete(Student entity)
        {
            dbContext.Remove(entity);
            return (await SaveChanges()) > 0 ? true : false;
        }

        public async Task<Student> Update(Student entity)
        {
            var updatedStudent = dbContext.Update(entity);
            return updatedStudent.Entity;
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAll(bool includeCourses)
        {
            if (includeCourses)
                return await dbContext.Students.Include(x => x.Courses).ToListAsync();
            return await dbContext.Students.ToListAsync();
        }

        public async Task<int> CountAll()
        {
            return await dbContext.Students.CountAsync();
        }

        public async Task<Student?> GetStudent(string userId, bool includeCourses)
        {
            if (includeCourses)
            {
                return await dbContext.Students.Include(x => x.Courses)
                    .FirstOrDefaultAsync(x => x.StudentId == Guid.Parse(userId));
            }
            return await dbContext.Students.FirstOrDefaultAsync(x => x.StudentId == Guid.Parse(userId));
        }

        public async Task<bool> StudentExists(string firstName, string lastName)
        {
            return await dbContext.Students.
                CountAsync(x => x.FirstName == firstName && x.LastName == lastName) > 0;
        }
    }
}
