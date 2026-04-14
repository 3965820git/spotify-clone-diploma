using FluentValidation;

namespace SpotifyClone.Playlists.Application.Features.Playlists.Queries.GetAllByOwner;

public sealed class GetAllPlaylistsByOwnerQueryValidator
    : AbstractValidator<GetAllPlaylistsByOwnerQuery>
{
    public GetAllPlaylistsByOwnerQueryValidator()
        => RuleFor(x => x.OwnerId)
            .NotNull().WithMessage("Owner ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Owner ID is required.");
}
