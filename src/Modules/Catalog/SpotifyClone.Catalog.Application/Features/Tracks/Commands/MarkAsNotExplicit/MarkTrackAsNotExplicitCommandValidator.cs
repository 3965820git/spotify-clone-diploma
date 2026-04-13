using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.MarkAsNotExplicit;

public sealed class MarkTrackAsNotExplicitCommandValidator
    : AbstractValidator<MarkTrackAsNotExplicitCommand>
{
    public MarkTrackAsNotExplicitCommandValidator()
        => RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
}
