using Microsoft.EntityFrameworkCore;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly UniversityDbContext dbContext;
        public CourseRepository(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Course> Add(Course entity)
        {
            var addedEntity = await dbContext.AddAsync(entity);
            return addedEntity.Entity;
        }

        public void Delete(Course entity)
        {
            dbContext.Remove(entity);
        }

        public async Task<IEnumerable<Course>> GetAll(bool includeStudents)
        {
            if(includeStudents)
                return await dbContext.Courses.Include(x => x.Students).ToListAsync();
            return await dbContext.Courses.ToListAsync();
        }

        public void Update(Course entity)
        {
            dbContext.Update(entity);
        }
        public async Task<int> CountAll()
        {
            return await dbContext.Courses.CountAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await dbContext.Courses.ToListAsync();
        }

        public async Task<Course?> GetById(int courseId)
        {
            return await dbContext.Courses.FirstOrDefaultAsync(x => x.CourseID == courseId);
        }
    }
}
