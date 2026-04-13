using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.UnlinkCover;

public sealed class UnlinkCoverFromGenreCommandValidator
    : AbstractValidator<UnlinkCoverFromGenreCommand>
{
    public UnlinkCoverFromGenreCommandValidator()
        => RuleFor(x => x.GenreId)
            .NotNull().WithMessage("Genre ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Genre ID is required.");
}
