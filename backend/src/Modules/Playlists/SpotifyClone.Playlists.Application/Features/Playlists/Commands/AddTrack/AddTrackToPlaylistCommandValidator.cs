using FluentValidation;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.AddTrack;

public sealed class AddTrackToPlaylistCommandValidator
    : AbstractValidator<AddTrackToPlaylistCommand>
{
    public AddTrackToPlaylistCommandValidator()
    {
        RuleFor(x => x.PlaylistId)
            .NotNull().WithMessage("Playlist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Playlist ID is required.");

        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
    }
}
