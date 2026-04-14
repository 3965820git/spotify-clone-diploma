using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateMainArtists;

public sealed class UpdateTrackMainArtistsCommandValidator
    : AbstractValidator<UpdateTrackMainArtistsCommand>
{
    public UpdateTrackMainArtistsCommandValidator()
    {
        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");

        RuleFor(x => x.MainArtists)
            .NotEmpty().WithMessage("Main artists are required.");
    }
}
