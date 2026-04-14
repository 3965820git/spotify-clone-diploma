using FluentValidation;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.MoveTrack;

public sealed class MoveTrackInPlaylistCommandValidator
    : AbstractValidator<MoveTrackInPlaylistCommand>
{
    public MoveTrackInPlaylistCommandValidator()
    {
        RuleFor(x => x.PlaylistId)
            .NotNull().WithMessage("Playlist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Playlist ID is required.");

        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");

        RuleFor(x => x.TargetPositionIndex)
            .NotNull().WithMessage("Target position index is required.");
    }
}
