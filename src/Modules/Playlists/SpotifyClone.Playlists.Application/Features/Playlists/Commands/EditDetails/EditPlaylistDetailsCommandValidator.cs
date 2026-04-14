using FluentValidation;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Commands.EditDetails;

public sealed class EditPlaylistDetailsCommandValidator
    : AbstractValidator<EditPlaylistDetailsCommand>
{
    public EditPlaylistDetailsCommandValidator()
    {
        RuleFor(x => x.PlaylistId)
            .NotNull().WithMessage("Playlist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Playlist ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.IsPublic)
            .NotNull().WithMessage("Is public flag is required.");
    }
}
