using FluentValidation;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.AddCollaborator;

public sealed class AddCollaboratorToPlaylistCommandValidator
    : AbstractValidator<AddCollaboratorToPlaylistCommand>
{
    public AddCollaboratorToPlaylistCommandValidator()
    {
        RuleFor(x => x.PlaylistId)
            .NotNull().WithMessage("Playlist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Playlist ID is required.");

        RuleFor(x => x.CollaboratorId)
            .NotNull().WithMessage("Collaborator ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Collaborator ID is required.");
    }
}
