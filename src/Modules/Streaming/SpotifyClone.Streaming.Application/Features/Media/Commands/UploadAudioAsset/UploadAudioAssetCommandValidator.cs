using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.UploadAudioAsset;

public sealed class UploadAudioAssetCommandValidator
    : AbstractValidator<UploadAudioAssetCommand>
{
    public UploadAudioAssetCommandValidator()
    {
        RuleFor(x => x.FileName)
            .NotNull().WithMessage("File name is required.")
            .NotEmpty().WithMessage("File name is required.");

        RuleFor(x => x.FileStream)
            .NotNull().WithMessage("File stream is required.");

        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
    }
}
