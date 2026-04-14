using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Otp.Verify;

public sealed class VerifyOtpLoginCommandValidator
    : AbstractValidator<VerifyOtpLoginCommand>
{
    public VerifyOtpLoginCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.");
    }
}
