using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Verify;

public sealed class VerifyUserPasswordResetCommandValidator
    : AbstractValidator<VerifyUserPasswordResetCommand>
{
    public VerifyUserPasswordResetCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.");
    }
}
