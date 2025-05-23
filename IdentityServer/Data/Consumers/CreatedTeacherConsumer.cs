using Common.IntegratedEvents;
using IdentityServer.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Data.Consumers
{
    public class CreatedTeacherConsumer(UserManager<ApplicationUser> userManager) : IConsumer<CreatedTeacherEvent>
    {
        public async Task Consume(ConsumeContext<CreatedTeacherEvent> context)
        {
            await userManager.CreateAsync(new ApplicationUser
            {
                Id = context.Message.id,
                UserName = context.Message.userName
            }, context.Message.password);
        }
    }
}
