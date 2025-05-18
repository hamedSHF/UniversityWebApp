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

            builder.Property(x => x.CourseCode)
                .IsRequired()
                .HasMaxLength(50);
            builder.OwnsMany<CourseDetails>(x => x.CourseDetails, builder =>
            {
                builder.Property(x => x.CourseDescription)
                .IsRequired(false)
                .HasMaxLength(500);

                builder.Property(x => x.Capacity)
                    .HasMaxLength(60);

                builder.Property(x => x.ScheduleDay)
                    .IsRequired(false);

                builder.Property(x => x.Location)
                    .IsRequired(false)
                    .HasMaxLength(100);

                builder.Property(x => x.Duration)
                    .IsRequired(false)
                    .HasPrecision(0);

                builder.Property(x => x.StartTime)
                    .IsRequired()
                    .HasPrecision(0);
            });

            builder.HasMany(x => x.Students)
                .WithMany(z => z.Courses);

            builder.HasOne(x => x.Teacher)
                .WithMany(z => z.Courses);

            builder.HasOne(x => x.CourseTopic)
                .WithMany(z => z.Courses);

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();
        }
    }
}
