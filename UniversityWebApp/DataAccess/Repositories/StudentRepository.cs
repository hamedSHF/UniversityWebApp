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

        public void Delete(Student entity)
        {
            dbContext.Remove(entity);
        }

        public void Update(Student entity)
        {
            dbContext.Update(entity);
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> CountAll()
        {
            return await dbContext.Students.CountAsync();
        }

        public async Task<Student?> GetStudent(string userName, bool includeCourses)
        {
            if (includeCourses)
            {
                return await dbContext.Students.Include(x => x.Courses)
                    .FirstOrDefaultAsync(x => x.StudentUserName == userName);
            }
            return await dbContext.Students.FirstOrDefaultAsync(x => x.StudentUserName == userName);
        }
        public async Task<Guid> GetIdByUserName(string userName)
        {
            var student = await dbContext.Students.AsNoTracking().
                SingleOrDefaultAsync(x => x.StudentUserName == userName);
            return student.StudentId;
        }
        public async Task<bool> Exists(string firstName, string lastName)
        {
            return await dbContext.Students.
                CountAsync(x => x.FirstName == firstName && x.LastName == lastName) > 0;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await dbContext.Students.ToListAsync();
        }
    }
}
