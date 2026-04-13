using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.AddTrackToQueue;

public sealed class AddTrackToPlaybackQueueCommandValidator
    : AbstractValidator<AddTrackToPlaybackQueueCommand>
{
    public AddTrackToPlaybackQueueCommandValidator()
        => RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
}
