using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Artists.Queries.GetDetails;

public sealed class GetArtistDetailsQueryValidator
    : AbstractValidator<GetArtistDetailsQuery>
{
    public GetArtistDetailsQueryValidator()
        => RuleFor(x => x.ArtistId)
        .NotNull().WithMessage("Artist ID is required.")
        .NotEqual(Guid.Empty).WithMessage("Artist ID is required.");
}
