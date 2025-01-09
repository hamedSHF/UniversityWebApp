using Microsoft.EntityFrameworkCore;
using PracticeWebApp.DataAccess;
using UniversityWebApp.ConfigOptions;
using UniversityWebApp.Services;

namespace PracticeWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("UniversityDBConnectionString")
                ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");
            // Add services to the container.
            builder.Services.Configure<IdentityAddressesOptions>(builder.Configuration.GetSection(IdentityAddressesOptions.IdentityAddresses));

            builder.Services.AddDbContext<UniversityDbContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.AddMvc();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();

            

            var app = builder.Build();

            //Create migration of database automatically
            //using (var scope = app.Services.CreateScope())
            //{
            //    var dataContext = scope.ServiceProvider.GetRequiredService<UniversityDbContext>();
            //    dataContext.Database.Migrate();
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllerRoute(name:"default",pattern:"{controller=Home}/{action=Index}/{id:int?}");
            
            app.Run();
        }
    }
}
