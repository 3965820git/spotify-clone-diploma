using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.Delete;

public sealed class DeleteMoodCommandValidator
    : AbstractValidator<DeleteMoodCommand>
{
    public DeleteMoodCommandValidator()
        => RuleFor(x => x.MoodId)
            .NotNull().WithMessage("Mood ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Mood ID is required.");
}
