using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Commands.RemoveGalleryImageFromArtist;

public sealed class RemoveGalleryImageFromArtistCommandValidator
    : AbstractValidator<RemoveGalleryImageFromArtistCommand>
{
    public RemoveGalleryImageFromArtistCommandValidator()
        => RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");
}
