using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.SkipToPrevious;

public sealed class SkipToPreviousTrackCommandValidator
    : AbstractValidator<SkipToPreviousTrackCommand>
{
    public SkipToPreviousTrackCommandValidator()
        => RuleFor(x => x.DeviceId)
            .NotNull().WithMessage("Device ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Device ID is required.");
}
