using FluentValidation;
using IdentityServer.Models;

namespace IdentityServer.Validators
{
    public class CreateTeacherValidator : AbstractValidator<CreateTeacherRequest>
    {
        public CreateTeacherValidator()
        {
            RuleFor(x => x.Firstname)
                .NotEmpty()
                .NotNull()
                .Must(name =>
                {
                    foreach (var word in name)
                    {
                        if (!Char.IsLetter(word))
                            return false;
                    }
                    return true;
                });

            RuleFor(x => x.Lastname)
                .NotEmpty()
                .NotNull()
                .Must(name =>
                {
                    foreach (var word in name)
                    {
                        if (!Char.IsLetter(word))
                            return false;
                    }
                    return true;
                });

            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull();
        }
    }
}
