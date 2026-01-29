using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.VerifyPhoneNumber;

public sealed class VerifyPhoneNumberCommandValidator
    : AbstractValidator<VerifyPhoneNumberCommand>
{
    public VerifyPhoneNumberCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required.")
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.PhoneNumber)
            .NotNull().WithMessage("Phone number is required.")
            .NotEmpty().WithMessage("Phone number is required.");

        RuleFor(x => x.Code)
            .NotNull().WithMessage("Code is required.")
            .NotEmpty().WithMessage("Code is required.");
    }
}
