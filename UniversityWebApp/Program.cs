using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Net.Http.Headers;
using UniversityWebApp.ConfigOptions;
using UniversityWebApp.DataAccess;
using UniversityWebApp.EndPoints;
using UniversityWebApp.Model;
using UniversityWebApp.Model.DTOs;
using UniversityWebApp.Services;
using UniversityWebApp.Services.CodeGenerator;
using UniversityWebApp.Services.Registration;

namespace UniversityWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string ApiKeyRequestHeaderName = "X-Api-Key";
            //Config serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/universityinfo.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            var identitySection = builder.Configuration.GetSection(IdentityAddressesOptions.SectionName);
            builder.AddInfraServices();
            builder.AddCustomAuthentication();
            // Add services to the container.
            builder.Services.Configure<IdentityAddressesOptions>(identitySection);
            builder.Services.AddHttpClient<IRegistrationService<AddTeacherDto>, TeacherService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.
                    GetSection(IdentityAddressesOptions.SectionName)["IdentityServerSecure"]);
                var apiKey = builder.Configuration.GetSection(ApiKeyOptions.SectionName)[ApiKeyOptions.IdentityKeyName];
                client.DefaultRequestHeaders.Add(ApiKeyRequestHeaderName, apiKey);
            });
            builder.Services.AddMvc();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSerilog();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            
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
            app.MapGroup("api/teacher")
                .MapTeacherEndpoints()
                .WithTags("Teacher api");
            app.Run();
        }
    }
}
