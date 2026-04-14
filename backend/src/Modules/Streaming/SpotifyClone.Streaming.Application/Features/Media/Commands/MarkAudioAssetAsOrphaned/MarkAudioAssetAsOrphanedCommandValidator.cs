using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.MarkAudioAssetAsOrphaned;

public sealed class MarkAudioAssetAsOrphanedCommandValidator
    : AbstractValidator<MarkAudioAssetAsOrphanedCommand>
{
    public MarkAudioAssetAsOrphanedCommandValidator()
        => RuleFor(x => x.AudioAssetId)
        .NotNull().WithMessage("Audio Asset ID is required.")
        .NotEqual(Guid.Empty).WithMessage("Audio Asset ID is required.");
}
