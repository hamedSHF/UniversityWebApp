using Common.Options;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UniversityWebApp.DataAccess;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.DataAccess.Repositories;
using UniversityWebApp.Model;

namespace UniversityWebApp
{
    public static class ApplicationExtensions
    {
        public static void AddInfraServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("UniversityDBConnectionString")
                ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

            builder.Services.AddDbContext<UniversityDbContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();

            builder.Services.AddMassTransit(x =>
            {
                var brokerOptions = builder.Configuration.GetSection(BrokerOptions.SectionName)
                    .Get<BrokerOptions>();
                if (brokerOptions is null)
                {
                    throw new ArgumentNullException(nameof(BrokerOptions));
                }
                x.UsingRabbitMq((context, cfg) =>
                {   
                    cfg.Host(brokerOptions.Host, h =>
                    {
                        h.Username(brokerOptions.UserName);
                        h.Password(brokerOptions.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
