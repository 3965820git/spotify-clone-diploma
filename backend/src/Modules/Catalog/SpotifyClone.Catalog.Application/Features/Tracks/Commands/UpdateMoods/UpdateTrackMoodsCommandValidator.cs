using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateMoods;

public sealed class UpdateTrackMoodsCommandValidator
    : AbstractValidator<UpdateTrackMoodsCommand>
{
    public UpdateTrackMoodsCommandValidator()
    {
        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");

        RuleFor(x => x.Moods)
            .NotEmpty().WithMessage("Track ID is required.");
    }
}
