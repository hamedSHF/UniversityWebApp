using Microsoft.EntityFrameworkCore;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Repositories
{
    public class CourseTopicRepository : ICourseTopicRepository
    {
        private readonly UniversityDbContext dbContext;
        public CourseTopicRepository(UniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<CourseTopics> Add(CourseTopics entity)
        {
            var addedEntity = await dbContext.CourseTopics.AddAsync(entity);
            return addedEntity.Entity;
        }

        public async Task<int> CountAll()
        {
            return await dbContext.CourseTopics.CountAsync();
        }

        public void Delete(CourseTopics entity)
        {
           dbContext.CourseTopics.Remove(entity);
        }

        public async Task<bool> Exists(int id)
        {
            return await dbContext.CourseTopics.CountAsync(x => x.TopicId == id) > 0 ?
                true : false;
        }

        public async Task<IEnumerable<CourseTopics>> GetAll()
        {
            return await dbContext.CourseTopics.ToListAsync();
        }

        public async Task<CourseTopics?> GetById(int id, bool includeCourses = false)
        {

            return includeCourses ? await dbContext.CourseTopics.Include(x => x.Courses)
                .FirstOrDefaultAsync(x => x.TopicId == id) :
                await dbContext.CourseTopics.FirstOrDefaultAsync(x => x.TopicId == id);
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Update(CourseTopics entity)
        {
            dbContext.Update(entity);
        }
    }
}
