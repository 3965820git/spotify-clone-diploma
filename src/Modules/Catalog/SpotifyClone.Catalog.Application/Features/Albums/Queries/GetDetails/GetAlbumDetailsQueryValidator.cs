using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Queries.GetDetails;

public sealed class GetAlbumDetailsQueryValidator
    : AbstractValidator<GetAlbumDetailsQuery>
{
    public GetAlbumDetailsQueryValidator()
        => RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");
}
