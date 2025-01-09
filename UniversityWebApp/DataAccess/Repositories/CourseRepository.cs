using Microsoft.EntityFrameworkCore;
using PracticeWebApp.DataAccess.Interfaces;
using PracticeWebApp.Model;

namespace PracticeWebApp.DataAccess.Repositories
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
            await dbContext.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task Delete(Course entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
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
            await dbContext.SaveChangesAsync();
            return updatedEntity.Entity;
        }
    }
}
