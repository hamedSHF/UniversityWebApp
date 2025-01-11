using Microsoft.EntityFrameworkCore;
using UniversityWebApp.DataAccess;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Repositories
{
    public class TeacherRepository : IRepository<Teacher>
    {
        private readonly UniversityDbContext dbContext;
        public TeacherRepository(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Teacher> Add(Teacher entity)
        {
            var addedInstructor = await dbContext.AddAsync(entity);
            return addedInstructor.Entity;
        }

        public async Task Delete(Teacher entity)
        {
            dbContext.Remove(entity);
        }

        public async Task<List<Teacher>> GetAll()
        {
            return await dbContext.Teachers.Include(x => x.Courses).ToListAsync();
        }
        public async Task<Teacher> Update(Teacher entity)
        {
            var updatedEntity = dbContext.Update(entity);
            return updatedEntity.Entity;
        }
        public async Task<int> CountAll()
        {
            return await dbContext.Teachers.CountAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
