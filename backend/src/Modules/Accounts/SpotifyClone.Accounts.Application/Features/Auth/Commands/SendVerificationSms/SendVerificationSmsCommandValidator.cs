using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.SendVerificationSms;

public sealed class SendVerificationSmsCommandValidator
    : AbstractValidator<SendVerificationSmsCommand>
{
    public SendVerificationSmsCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required.")
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.PhoneNumber)
            .NotNull().WithMessage("Phone number is required.")
            .NotEmpty().WithMessage("Phone number is required.");
    }
}
