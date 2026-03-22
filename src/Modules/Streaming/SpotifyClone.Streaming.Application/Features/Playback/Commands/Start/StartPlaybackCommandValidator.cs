using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Start;

public sealed class StartPlaybackCommandValidator
    : AbstractValidator<StartPlaybackCommand>
{
    public StartPlaybackCommandValidator()
    {
        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");

        RuleFor(x => x.DeviceId)
            .NotNull().WithMessage("Device ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Device ID is required.");

        RuleFor(x => x.ContextType)
            .NotEmpty().WithMessage("Context type is required.");

        RuleFor(x => x.ContextExternalId)
            .NotEqual(Guid.Empty).WithMessage("External context ID is required.");
    }
}
