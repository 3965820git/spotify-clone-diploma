using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UnlinkFromAudioFile;

public sealed class UnlinkTrackFromAudioFileCommandValidator
    : AbstractValidator<UnlinkTrackFromAudioFileCommand>
{
    public UnlinkTrackFromAudioFileCommandValidator()
        => RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
}
