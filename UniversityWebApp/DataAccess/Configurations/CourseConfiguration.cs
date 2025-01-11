using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.CourseID);
            builder.Property(x => x.CourseID)
                .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Students)
                .WithMany(z => z.Courses);

            builder.HasOne(x => x.Teacher)
                .WithMany(z => z.Courses);

            builder.Property(x => x.CourseName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CourseDescription)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.Property(x => x.Capacity)
                .HasMaxLength(60);
        }
    }
}
