using Microsoft.EntityFrameworkCore;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Repositories
{
    public class MajorRepository : IMajorRepository
    {
        private readonly UniversityDbContext dbContext;
        public MajorRepository(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Major> Add(Major entity)
        {
            var addedEntity = await dbContext.AddAsync(entity);
            return addedEntity.Entity;
        }

        public async Task<int> CountAll()
        {
            return await dbContext.Majors.CountAsync();
        }

        public void Delete(Major entity)
        {
            dbContext.Majors.Remove(entity);
        }

        public async Task<Major?> FindMajor(string title)
        {
            return await dbContext.Majors.FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<IEnumerable<Major>> GetAll()
        {
            return await dbContext.Majors.ToListAsync();
        }

        public async Task<IEnumerable<Major>> GetAllWithTopics()
        {
            return await dbContext.Majors.Include(x => x.Topics).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Update(Major entity)
        {
            dbContext.Majors.Update(entity);
        }
    }
}
