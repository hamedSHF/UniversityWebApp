using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(x => x.TeacherId);
            builder.Property(x => x.TeacherId)
                .HasMaxLength(16);

            builder.HasIndex(x => x.TeacherUserName)
                .IsUnique();

            builder.Property(x => x.TeacherUserName)
                .HasColumnName("Tch_UserName");

            builder.HasMany(x => x.Courses)
                .WithOne(z => z.Teacher);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Degree)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
