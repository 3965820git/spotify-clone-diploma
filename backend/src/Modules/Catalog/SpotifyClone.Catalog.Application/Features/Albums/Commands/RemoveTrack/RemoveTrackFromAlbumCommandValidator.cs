using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.RemoveTrack;

public sealed class RemoveTrackFromAlbumCommandValidator
    : AbstractValidator<RemoveTrackFromAlbumCommand>
{
    public RemoveTrackFromAlbumCommandValidator()
    {
        RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");

        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
    }
}
