using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator
    : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .NotNull().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password is required.")
            .NotEmpty().WithMessage("Password is required.");

        RuleFor(x => x.DisplayName)
            .NotNull().WithMessage("Display name is required.")
            .NotEmpty().WithMessage("Display name is required.");

        RuleFor(x => x.BirthDate)
            .NotNull().WithMessage("Birth date is required.")
            .NotEmpty().WithMessage("Birth date is required.");

        RuleFor(x => x.Gender)
            .NotNull().WithMessage("Gender is required.")
            .NotEmpty().WithMessage("Gender is required.");
    }
}
