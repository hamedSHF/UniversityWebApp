using Microsoft.EntityFrameworkCore;
using PracticeWebApp.Model;

namespace PracticeWebApp.DataAccess
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) :
            base(options)
        { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Instrcutor> Instrutors { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
