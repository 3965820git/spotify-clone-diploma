using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.UnlinkAudioAssetFromTrack;

public sealed class UnlinkAudioAssetFromTrackCommandValidator
    : AbstractValidator<UnlinkAudioAssetFromTrackCommand>
{
    public UnlinkAudioAssetFromTrackCommandValidator()
        => RuleFor(x => x.AudioAssetId)
            .NotNull().WithMessage("Audio asset ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Audio asset ID is required.");
}
