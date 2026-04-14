using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.Delete;

public sealed class DeleteGenreCommandValidator
    : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
        => RuleFor(x => x.GenreId)
            .NotNull().WithMessage("Genre ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Genre ID is required.");
}
