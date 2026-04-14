using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.MarkAsExplicit;

public sealed class MarkTrackAsExplicitCommandValidator
    : AbstractValidator<MarkTrackAsExplicitCommand>
{
    public MarkTrackAsExplicitCommandValidator()
        => RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
}
