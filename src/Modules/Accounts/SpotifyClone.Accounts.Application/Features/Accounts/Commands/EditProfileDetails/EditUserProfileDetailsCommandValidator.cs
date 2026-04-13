using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.EditProfileDetails;

public sealed class EditUserProfileDetailsCommandValidator
    : AbstractValidator<EditUserProfileDetailsCommand>
{
    public EditUserProfileDetailsCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required.")
            .NotEqual(Guid.Empty).WithMessage("User ID is required.");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("Display name is required.");
    }
}
