using Common;
using Common.Requests;
using FluentValidation;
using IdentityServer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Endpoints
{
    public static class TeacherEndpoint
    {
        public static RouteGroupBuilder MapTeacherEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapPost("/create", CreateTeacher);

            return builder;
        }
        public static async Task<Results<Created, ValidationProblem, BadRequest<string>, ProblemHttpResult>> CreateTeacher(
            [FromBody] CreateTeacherRequest request,
            UserManager<ApplicationUser> userManager,
            IValidator<CreateTeacherRequest> validator)
        {
            var result = await validator.ValidateAsync(request);
            if (result.IsValid)
            {
                var user = await userManager.FindByNameAsync(request.Username);
                if (user is null)
                    return TypedResults.BadRequest($"{request.Username} is already created");
                var registrationResult = await userManager.CreateAsync(new ApplicationUser
                {
                    UserName = request.Username,
                }, PasswordCreator.
                CreateTeacherPassword(
                request.Username,
                request.Firstname,
                request.Lastname));
                if (registrationResult.Succeeded)
                    return TypedResults.Created();
                return TypedResults.Problem(registrationResult.ToString());
            }
            return TypedResults.ValidationProblem(result.ToDictionary());
        }
    }
}
