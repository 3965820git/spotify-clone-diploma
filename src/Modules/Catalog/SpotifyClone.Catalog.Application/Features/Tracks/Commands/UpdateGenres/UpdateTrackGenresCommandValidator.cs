using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.UpdateGenres;

public sealed class UpdateTrackGenresCommandValidator
    : AbstractValidator<UpdateTrackGenresCommand>
{
    public UpdateTrackGenresCommandValidator()
    {
        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");

        RuleFor(x => x.Genres)
            .NotEmpty().WithMessage("Genres are required.");
    }
}
