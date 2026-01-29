using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithPassword;

public sealed class LoginWithPasswordCommandValidator
    : AbstractValidator<LoginWithPasswordCommand>
{
    public LoginWithPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email is required.")
            .NotEmpty().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password is required.")
            .NotEmpty().WithMessage("Password is required.");
    }
}
