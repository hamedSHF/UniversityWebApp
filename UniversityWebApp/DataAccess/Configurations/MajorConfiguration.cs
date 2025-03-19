﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityWebApp.Model;

namespace UniversityWebApp.DataAccess.Configurations
{
    public class MajorConfiguration : IEntityTypeConfiguration<Major>
    {
        public void Configure(EntityTypeBuilder<Major> builder)
        {
            builder.HasKey(x => x.Title);
            
            builder.HasMany(x => x.Students)
                .WithMany(x => x.Majors);

            builder.HasMany(x => x.Topics)
                .WithMany(x => x.Majors);
        }
    }
}
