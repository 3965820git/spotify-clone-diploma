using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Auth.Commands.Login.Otp.Send;

public sealed class SendOtpLoginCommandValidator
    : AbstractValidator<SendOtpLoginCommand>
{
    public SendOtpLoginCommandValidator()
        => RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.");
}
