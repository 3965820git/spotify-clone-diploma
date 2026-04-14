using FluentValidation;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.Delete;

public sealed class DeletePlaylistCommandValidator
    : AbstractValidator<DeletePlaylistCommand>
{
    public DeletePlaylistCommandValidator()
        => RuleFor(x => x.PlaylistId)
            .NotNull().WithMessage("Playlist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Playlist ID is required.");
}
