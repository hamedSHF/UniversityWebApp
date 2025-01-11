using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.StudentId);
            builder.Property(x => x.StudentId)
                .ValueGeneratedOnAdd();

            builder.HasIndex(x => x.StudentUserName)
                .IsUnique();

            builder.Property(x => x.StudentUserName)
                .HasColumnName("Std_UserName");

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.RegisterDate)
                .IsRequired();

            builder.Property(x => x.BirthDate)
                .IsRequired();

            builder.Property(x => x.EducationState)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Gender)
                .HasMaxLength(10)
                .IsRequired();

            builder.HasMany(x => x.Courses)
                .WithMany(z => z.Students);
        }
    }
}
