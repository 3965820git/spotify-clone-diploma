using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.Rename;

public sealed class RenameMoodCommandValidator
    : AbstractValidator<RenameMoodCommand>
{
    public RenameMoodCommandValidator()
    {
        RuleFor(x => x.MoodId)
            .NotNull().WithMessage("Mood ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Mood ID is required.");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name is required.");
    }
}
