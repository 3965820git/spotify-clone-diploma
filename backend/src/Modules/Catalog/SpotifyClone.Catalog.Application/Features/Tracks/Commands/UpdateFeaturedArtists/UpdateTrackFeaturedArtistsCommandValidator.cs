using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateFeaturedArtists;

public sealed class UpdateTrackFeaturedArtistsCommandValidator
    : AbstractValidator<UpdateTrackFeaturedArtistsCommand>
{
    public UpdateTrackFeaturedArtistsCommandValidator()
    {
        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");

        RuleFor(x => x.FeaturedArtists)
            .NotEmpty().WithMessage("Featured artists are required.");
    }
}
