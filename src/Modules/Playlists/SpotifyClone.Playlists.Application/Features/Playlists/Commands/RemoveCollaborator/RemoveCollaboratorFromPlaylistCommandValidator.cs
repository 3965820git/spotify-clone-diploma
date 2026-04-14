using FluentValidation;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.RemoveCollaborator;

public sealed class RemoveCollaboratorFromPlaylistCommandValidator
    : AbstractValidator<RemoveCollaboratorFromPlaylistCommand>
{
    public RemoveCollaboratorFromPlaylistCommandValidator()
    {
        RuleFor(x => x.PlaylistId)
            .NotNull().WithMessage("Playlist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Playlist ID is required.");

        RuleFor(x => x.CollaboratorId)
            .NotNull().WithMessage("Collaborator ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Collaborator ID is required.");
    }
}
