using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.MoveTrack;

public sealed class MoveTrackInAlbumCommandValidator
    : AbstractValidator<MoveTrackInAlbumCommand>
{
    public MoveTrackInAlbumCommandValidator()
    {
        RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");

        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");

        RuleFor(x => x.TargetPositionIndex)
            .NotNull().WithMessage("Target position index is required.");
    }
}
