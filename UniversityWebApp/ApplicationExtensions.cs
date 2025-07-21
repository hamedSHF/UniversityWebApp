using Common.Options;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;
using UniversityWebApp.ConfigOptions;
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
            builder.Services.AddScoped<ICourseTopicRepository, CourseTopicRepository>();
            builder.Services.AddScoped<IMajorRepository, MajorRepository>();
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
                    cfg.Durable = true;
                });
            });
        }
        public static void AddCustomAuthentication(this IHostApplicationBuilder builder)
        {
            var identitySection = builder.Configuration.GetSection(IdentityAddressesOptions.IdentityAddresses);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authorization = context.Request.Cookies[HeaderNames.Authorization];
                        if (!string.IsNullOrEmpty(authorization) &&
                        authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = authorization.Substring("Bearer ".Length).Trim();
                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        redirect(context, identitySection);
                        return Task.CompletedTask;
                    }
                };
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]))
                };
            });
        }
        private static void redirect(BaseContext<JwtBearerOptions> context, IConfiguration? identitySection)
        {
            if(identitySection is null)
            {
                throw new ArgumentNullException(nameof(identitySection));
            }
            string redirectUrl = string.Empty;
            if (context.Request.Path.HasValue)
            {
                if (context.Request.Path.Value.Contains("Admin"))
                {
                    redirectUrl = identitySection["IdentityServerSecure"] + identitySection["AdminAddresses:Login"];
                }
                else if (context.Request.Path.Value.Contains("Student"))
                {
                    redirectUrl = identitySection["IdentityServerSecure"] + identitySection["StudentAddresses:Login"];
                }
            }
            else
            {
                redirectUrl = identitySection["IdentityServerSecure"];
            }
            context.Response.Redirect(redirectUrl);
        }
    }
}
