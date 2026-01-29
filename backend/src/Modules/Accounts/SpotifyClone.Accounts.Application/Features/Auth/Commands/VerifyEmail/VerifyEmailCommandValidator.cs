using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.VerifyEmail;

public sealed class VerifyEmailCommandValidator
    : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required.")
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Code)
            .NotNull().WithMessage("Verification code is required.")
            .NotEmpty().WithMessage("Verification code is required.");
    }
}
