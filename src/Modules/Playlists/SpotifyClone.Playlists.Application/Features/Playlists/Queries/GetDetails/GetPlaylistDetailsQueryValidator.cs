using FluentValidation;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries.GetDetails;

public sealed class GetPlaylistDetailsQueryValidator
    : AbstractValidator<GetPlaylistDetailsQuery>
{
    public GetPlaylistDetailsQueryValidator()
        => RuleFor(x => x.PlaylistId)
            .NotNull().WithMessage("Playlist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Playlist ID is required.");
}
