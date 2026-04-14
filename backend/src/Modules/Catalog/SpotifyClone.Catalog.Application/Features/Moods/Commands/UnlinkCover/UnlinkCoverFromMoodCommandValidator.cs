using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.UnlinkCover;

public sealed class UnlinkCoverFromMoodCommandValidator
    : AbstractValidator<UnlinkCoverFromMoodCommand>
{
    public UnlinkCoverFromMoodCommandValidator()
        => RuleFor(x => x.MoodId)
            .NotNull().WithMessage("Mood ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Mood ID is required.");
}
