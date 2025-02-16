using Common.IntegratedEvents;
using IdentityServer.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Data.Consumers
{
    public class CreatedStudentConsumer(UserManager<ApplicationUser> userManager) : IConsumer<CreatedStudentEvent>
    {
        public async Task Consume(ConsumeContext<CreatedStudentEvent> context)
        {
            await userManager.CreateAsync(new ApplicationUser
            {
                Id = context.Message.id,
                UserName = context.Message.userName
            }, context.Message.password);
            //TODO: what if saving gets failed?
        }
    }
}
