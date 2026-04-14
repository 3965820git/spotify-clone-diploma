using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.LinkToAudioFile;

public class LinkTrackToAudioFileCommandValidator
    : AbstractValidator<LinkTrackToAudioFileCommand>
{
    public LinkTrackToAudioFileCommandValidator()
    {
        RuleFor(x => x.AudioFileId)
            .NotNull().WithMessage("Audio ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Audio ID is required.");

        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");

        RuleFor(x => x.Duration)
            .NotNull().WithMessage("Duration is required.");
    }
}
