using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenCommandValidator
    : AbstractValidator<LoginWithRefreshTokenCommand>
{
    public LoginWithRefreshTokenCommandValidator()
        => RuleFor(x => x.RawToken)
        .NotNull().WithMessage("Refresh token is required.")
        .NotEmpty().WithMessage("Refresh token is required.");
}
