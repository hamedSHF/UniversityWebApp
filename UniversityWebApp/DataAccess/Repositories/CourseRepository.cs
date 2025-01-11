using Microsoft.EntityFrameworkCore;
using UniversityWebApp.DataAccess;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Repositories
{
    public class CourseRepository : IRepository<Course>
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

        public async Task Delete(Course entity)
        {
            dbContext.Remove(entity);
        }

        public async Task<List<Course>> GetAll()
        {
            return await dbContext.Courses.Include(x => x.Students).ToListAsync();
        }

        public async Task<Course> GetById(int id)
        {
            return await dbContext.Courses.FirstOrDefaultAsync(x => x.CourseID == id);
        }

        public async Task<Course> Update(Course entity)
        {
            var updatedEntity = dbContext.Update(entity);
            return updatedEntity.Entity;
        }
        public async Task<int> CountAll()
        {
            return await dbContext.Courses.CountAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
