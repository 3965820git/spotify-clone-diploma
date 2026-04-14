using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Queries.GetAllByArtist;

public sealed class GetAllAlbumsByArtistQueryValidator
    : AbstractValidator<GetAllAlbumsByArtistQuery>
{
    public GetAllAlbumsByArtistQueryValidator()
        => RuleFor(x => x.ArtistId)
            .NotNull().WithMessage("Artist ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");
}
