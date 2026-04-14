using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Genres.Queries.GetDetails;

public sealed class GetGenreDetailsQueryValidator
    : AbstractValidator<GetGenreDetailsQuery>
{
    public GetGenreDetailsQueryValidator()
        => RuleFor(x => x.GenreId)
        .NotNull().WithMessage("Genre ID is required.")
        .NotEqual(Guid.Empty).WithMessage("Genre ID is required.");
}
