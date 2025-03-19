using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Configurations
{
    public class CourseTopicsConfiguration : IEntityTypeConfiguration<CourseTopics>
    {
        public void Configure(EntityTypeBuilder<CourseTopics> builder)
        {
            builder.HasKey(x => x.Title);

            builder.HasMany(x => x.Majors)
                .WithMany(x => x.Topics);
        }
    }
}
