using Microsoft.EntityFrameworkCore;
using UniversityWebApp.DataAccess;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Repositories
{
    public class TeacherRepository : ITeacherRepository
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

        public void Delete(Teacher entity)
        {
            dbContext.Remove(entity);
        }
        public void Update(Teacher entity)
        {
            dbContext.Update(entity);
        }
        public async Task<int> CountAll()
        {
            return await dbContext.Teachers.CountAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAll(bool includeCourses)
        {
            if(includeCourses)
                return await dbContext.Teachers.Include(x => x.Courses).ToListAsync();
            return await dbContext.Teachers.ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAll()
        {
            return await dbContext.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetTeacherById(ushort id)
        {
            return await dbContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == id);
        }
    }
}
