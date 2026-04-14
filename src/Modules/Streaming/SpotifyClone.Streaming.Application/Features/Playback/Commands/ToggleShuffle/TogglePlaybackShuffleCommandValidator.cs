using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.ToggleShuffle;

public sealed class TogglePlaybackShuffleCommandValidator
    : AbstractValidator<TogglePlaybackShuffleCommand>
{
    public TogglePlaybackShuffleCommandValidator()
        => RuleFor(x => x.DeviceId)
            .NotNull().WithMessage("Device ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Device ID is required.");
}
