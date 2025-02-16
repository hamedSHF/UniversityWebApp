using Common.Options;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServer.Data
{
    public static class ApplicationExtension
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("IdentityUniversityConnectionString")
                ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddMassTransit(x =>
            {
                var brokerOptions = builder.Configuration.GetSection(BrokerOptions.SectionName)
                    .Get<BrokerOptions>();
                if (brokerOptions is null)
                {
                    throw new ArgumentNullException(nameof(BrokerOptions));
                }
                x.AddConsumers(Assembly.GetExecutingAssembly());
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
