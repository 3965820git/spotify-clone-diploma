using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Media.Commands.UploadImageAsset;

public sealed class UpoadImageAssetCommandValidator
    : AbstractValidator<UploadImageAssetCommand>
{
    public UpoadImageAssetCommandValidator()
        => RuleFor(x => x.FileStream)
            .NotNull().WithMessage("File stream is requried.");
}
