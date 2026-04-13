using FluentValidation;

namespace SpotifyClone.Streaming.Application.Features.Media.Queries.GetImageAsset;

public sealed class GetImageAssetQueryValidator
    : AbstractValidator<GetImageAssetQuery>
{
    public GetImageAssetQueryValidator()
        => RuleFor(x => x.ImageId)
            .NotNull().WithMessage("Image ID is required.")
            .NotEmpty().WithMessage("Image ID is required.");
}
