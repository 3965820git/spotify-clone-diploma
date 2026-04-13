using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SeekPosition;

public sealed class SeekPlaybackPositionCommandValidator
    : AbstractValidator<SeekPlaybackPositionCommand>
{
    public SeekPlaybackPositionCommandValidator()
    {
        RuleFor(x => x.DeviceId)
            .NotNull().WithMessage("Device ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Device ID is required.");

        RuleFor(x => x.PositionMs)
            .NotNull().WithMessage("Device ID is required.");
    }
}
