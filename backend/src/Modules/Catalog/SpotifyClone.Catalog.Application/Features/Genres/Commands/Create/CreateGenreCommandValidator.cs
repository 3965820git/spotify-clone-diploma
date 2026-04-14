using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Genres.Commands.Create;

public sealed class CreateGenreCommandValidator
    : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
        => RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name is required.");
}
