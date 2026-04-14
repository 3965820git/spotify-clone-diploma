using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.ResetPassword.Confirm;

public sealed class ConfirmUserPasswordResetCommandValidator
    : AbstractValidator<ConfirmUserPasswordResetCommand>
{
    public ConfirmUserPasswordResetCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.");
    }
}
