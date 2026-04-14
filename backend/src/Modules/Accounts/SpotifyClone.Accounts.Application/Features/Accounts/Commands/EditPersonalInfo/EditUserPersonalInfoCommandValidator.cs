using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.EditPersonalInfo;

public sealed class EditUserPersonalInfoCommandValidator
    : AbstractValidator<EditUserPersonalInfoCommand>
{
    public EditUserPersonalInfoCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required.")
            .NotEqual(Guid.Empty).WithMessage("User ID is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEqual(string.Empty).WithMessage("Password cannot be empty.");

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required.");

        RuleFor(x => x.BirthDateUtc)
            .NotEmpty().WithMessage("Birth date UTC is required.");
    }
}
