using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.UnlinkAvatar;

public sealed class UnlinkAvatarFromArtistCommandValidator
    : AbstractValidator<UnlinkAvatarFromArtistCommand>
{
    public UnlinkAvatarFromArtistCommandValidator()
        => RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");
}
