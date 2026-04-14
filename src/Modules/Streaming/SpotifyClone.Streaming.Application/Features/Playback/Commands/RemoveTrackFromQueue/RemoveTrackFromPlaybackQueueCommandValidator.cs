using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.RemoveTrackFromQueue;

public sealed class RemoveTrackFromPlaybackQueueCommandValidator
    : AbstractValidator<RemoveTrackFromPlaybackQueueCommand>
{
    public RemoveTrackFromPlaybackQueueCommandValidator()
        => RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
}
