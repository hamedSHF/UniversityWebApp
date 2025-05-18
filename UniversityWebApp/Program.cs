using Microsoft.EntityFrameworkCore;
using Serilog;
using UniversityWebApp.ConfigOptions;
using UniversityWebApp.DataAccess;
using UniversityWebApp.EndPoints;
using UniversityWebApp.Model;
using UniversityWebApp.Services;
using UniversityWebApp.Services.CodeGenerator;

namespace UniversityWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Config serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/universityinfo.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            var identitySection = builder.Configuration.GetSection(IdentityAddressesOptions.IdentityAddresses);
            builder.AddInfraServices();
            builder.AddCustomAuthentication();
            // Add services to the container.
            builder.Services.Configure<IdentityAddressesOptions>(identitySection);
            builder.Services.AddMvc();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSerilog();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            
            builder.Services.AddSingleton<IUserNameGenerator, UserNameGenerator>();
            builder.Services.AddSingleton<CourseCodeGenerator>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", "Admin");
                });
                options.AddPolicy("Student", policy =>
                {
                    policy.RequireClaim("Role", "Student");
                    policy.RequireAuthenticatedUser();
                });
            });
            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseEndpoints(config =>
            {
                config.MapDefaultControllerRoute();
            });

            app.MapGroup("/api/major")
                .MapMajorEndpoints()
                .WithTags("Majors api");
            app.MapGroup("/api/topic")
                .MapTopicEndpoints()
                .WithTags("Topic api");
            app.MapGroup("/api/course")
                .MapCourseEndpoints()
                .WithTags("Course api");
            app.Run();
        }
    }
}
