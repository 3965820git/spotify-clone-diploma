using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.Rename;

public sealed class RenameGenreCommandValidator
    : AbstractValidator<RenameGenreCommand>
{
    public RenameGenreCommandValidator()
    {
        RuleFor(x => x.GenreId)
            .NotNull().WithMessage("Genre ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Genre ID is required.");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name is required.");
    }
}
