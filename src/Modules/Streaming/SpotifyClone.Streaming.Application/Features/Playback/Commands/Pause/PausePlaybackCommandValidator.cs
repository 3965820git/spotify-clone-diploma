using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Pause;

public sealed class PausePlaybackCommandValidator
    : AbstractValidator<PausePlaybackCommand>
{
    public PausePlaybackCommandValidator()
        => RuleFor(x => x.DeviceId)
            .NotNull().WithMessage("Device ID is requried.")
            .NotEqual(Guid.Empty).WithMessage("Device ID is requried.");
}
