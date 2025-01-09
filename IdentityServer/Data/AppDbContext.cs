using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private string[] roles = { "Admin", "Teacher", "Student" };
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Name = roles[0]
                },
                new IdentityRole
                {
                    Name = roles[1]
                },
                new IdentityRole
                {
                    Name = roles[2]
                });
            var user = new ApplicationUser
            {
                UserName = "HamedSHF",
                PhoneNumber = "091000000",
                NormalizedUserName = "HAMEDSHF"
            };
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, "secret@123");
            builder.Entity<ApplicationUser>()
                .HasData(user);
            base.OnModelCreating(builder);
        }
    }
}
