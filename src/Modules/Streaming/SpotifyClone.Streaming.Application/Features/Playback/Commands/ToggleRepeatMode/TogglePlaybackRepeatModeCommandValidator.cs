using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.ToggleRepeatMode;

public sealed class TogglePlaybackRepeatModeCommandValidator
    : AbstractValidator<TogglePlaybackRepeatModeCommand>
{
    public TogglePlaybackRepeatModeCommandValidator()
        => RuleFor(x => x.DeviceId)
            .NotNull().WithMessage("Device ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Device ID is required.");
}
