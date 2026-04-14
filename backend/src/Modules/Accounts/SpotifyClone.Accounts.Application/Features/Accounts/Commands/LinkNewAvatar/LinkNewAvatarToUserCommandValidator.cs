using FluentValidation;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Commands.LinkNewAvatar;

public sealed class LinkNewAvatarToUserCommandValidator
    : AbstractValidator<LinkNewAvatarToUserCommand>
{
    public LinkNewAvatarToUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required.")
            .NotEqual(Guid.Empty).WithMessage("User ID is required.");

        RuleFor(x => x.ImageWidth)
            .NotNull().WithMessage("Image width is required.");

        RuleFor(x => x.ImageHeight)
            .NotNull().WithMessage("Image height is required.");

        RuleFor(x => x.ImageFileType)
            .NotNull().WithMessage("Image file type is required.");

        RuleFor(x => x.ImageSizeInBytes)
            .NotNull().WithMessage("Image size in bytes is required.");
    }
}
