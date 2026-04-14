using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToNext;

public sealed class SkipToNextTrackCommandValidator
    : AbstractValidator<SkipToNextTrackCommand>
{
    public SkipToNextTrackCommandValidator()
        => RuleFor(x => x.DeviceId)
            .NotNull().WithMessage("Device ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Device ID is required.");
}
