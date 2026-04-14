using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Password;

public sealed class LoginUserWithPasswordCommandValidator
    : AbstractValidator<LoginUserWithPasswordCommand>
{
    public LoginUserWithPasswordCommandValidator()
    {
        RuleFor(x => x.Identifier)
            .NotNull().WithMessage("Identifier is required.")
            .NotEmpty().WithMessage("Identifier is required.");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password is required.")
            .NotEmpty().WithMessage("Password is required.");
    }
}
