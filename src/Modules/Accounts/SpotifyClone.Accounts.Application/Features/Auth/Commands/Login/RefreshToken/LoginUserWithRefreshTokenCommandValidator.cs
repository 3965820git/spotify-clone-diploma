using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.RefreshToken;

public sealed class LoginUserWithRefreshTokenCommandValidator
    : AbstractValidator<LoginUserWithRefreshTokenCommand>
{
    public LoginUserWithRefreshTokenCommandValidator()
        => RuleFor(x => x.RawToken)
        .NotNull().WithMessage("Refresh token is required.")
        .NotEmpty().WithMessage("Refresh token is required.");
}
