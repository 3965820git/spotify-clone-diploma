using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.Create;

public sealed class CreateMoodCommandValidator
    : AbstractValidator<CreateMoodCommand>
{
    public CreateMoodCommandValidator()
        => RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name is required.");
}
