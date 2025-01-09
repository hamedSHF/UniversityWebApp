using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticeWebApp.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeWebApp.DataAccess.Configurations
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

            builder.Property(x => x.CourseName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CourseDescription)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(x => x.StartTime)
                .IsRequired()
                .HasConversion(x => x.ToShortDateString(),z => DateTime.Parse(z));

            builder.Property(x => x.EndTime)
                .IsRequired()
                .HasConversion(x => x.ToShortDateString(), z => DateTime.Parse(z));
        }
    }
}
