using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.UnlinkAvatar;

public sealed class UnlinkAvatarFromUserCommandValidator
    : AbstractValidator<UnlinkAvatarFromUserCommand>
{
    public UnlinkAvatarFromUserCommandValidator()
        => RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required.")
            .NotEqual(Guid.Empty).WithMessage("User ID is required.");
}
