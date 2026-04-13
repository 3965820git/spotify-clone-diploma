using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Request;

public sealed class RequestUserPasswordResetCommandValidator
    : AbstractValidator<RequestUserPasswordResetCommand>
{
    public RequestUserPasswordResetCommandValidator()
        => RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");
}
