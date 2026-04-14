using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Playback.Commands.Resume;

public sealed class ResumePlaybackCommandValidator
    : AbstractValidator<ResumePlaybackCommand>
{
    public ResumePlaybackCommandValidator()
        => RuleFor(x => x.DeviceId)
            .NotNull().WithMessage("Device ID is requried.")
            .NotEqual(Guid.Empty).WithMessage("Device ID is requried.");
}
