using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Media.Queries.GetAudioAsset;

public sealed class GetAudioAssetQueryValidator
    : AbstractValidator<GetAudioAssetQuery>
{
    public GetAudioAssetQueryValidator()
        => RuleFor(x => x.AudioId)
            .NotNull().WithMessage("Audio ID is required.")
            .NotEmpty().WithMessage("Audio ID is required.");
}
