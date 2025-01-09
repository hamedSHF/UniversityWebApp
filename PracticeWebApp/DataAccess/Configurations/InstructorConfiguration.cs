using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticeWebApp.Model;

namespace PracticeWebApp.DataAccess.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instrcutor>
    {
        public void Configure(EntityTypeBuilder<Instrcutor> builder)
        {
            builder.HasKey(x => x.InstructorId);
            builder.Property(x => x.InstructorId)
                .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Courses)
                .WithOne(z => z.Instructor);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(x => x.Degree)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
