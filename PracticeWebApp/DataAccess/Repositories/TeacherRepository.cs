using Microsoft.EntityFrameworkCore;
using PracticeWebApp.DataAccess.Interfaces;
using PracticeWebApp.Model;

namespace PracticeWebApp.DataAccess.Repositories
{
    public class TeacherRepository : IRepository<Instrcutor>
    {
        private readonly UniversityDbContext dbContext;
        public TeacherRepository(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Instrcutor> Add(Instrcutor entity)
        {
            var addedInstructor = await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return addedInstructor.Entity;
        }

        public async Task Delete(Instrcutor entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Instrcutor>> GetAll()
        {
            return await dbContext.Instrutors.Include(x => x.Courses).ToListAsync();
        }

        public async Task<Instrcutor> GetByUserName(string userName)
        {
            return await dbContext.Instrutors.FirstOrDefaultAsync(x => x.Td_UserName == userName);
        }

        public async Task<Instrcutor> Update(Instrcutor entity)
        {
            var updatedEntity = dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
            return updatedEntity.Entity;
        }
    }
}
