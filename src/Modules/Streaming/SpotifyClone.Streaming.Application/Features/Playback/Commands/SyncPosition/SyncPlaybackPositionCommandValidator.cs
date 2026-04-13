using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SyncPosition;

public sealed class SyncPlaybackPositionCommandValidator
    : AbstractValidator<SyncPlaybackPositionCommand>
{
    public SyncPlaybackPositionCommandValidator()
    {
        RuleFor(x => x.DeviceId)
            .NotNull().WithMessage("Device ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Device ID is required.");

        RuleFor(x => x.PositionMs)
            .NotNull().WithMessage("Device ID is required.");
    }
}
