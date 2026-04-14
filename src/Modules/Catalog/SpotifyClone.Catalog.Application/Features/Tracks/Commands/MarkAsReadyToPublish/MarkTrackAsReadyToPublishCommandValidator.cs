using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.MarkAsReadyToPublish;

public sealed class MarkTrackAsReadyToPublishCommandValidator
    : AbstractValidator<MarkTrackAsReadyToPublishCommand>
{
    public MarkTrackAsReadyToPublishCommandValidator()
        => RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
}
