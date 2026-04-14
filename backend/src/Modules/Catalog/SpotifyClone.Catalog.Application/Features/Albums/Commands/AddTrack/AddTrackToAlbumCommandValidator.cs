using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Albums.Commands.AddTrack;

public sealed class AddTrackToAlbumCommandValidator
    : AbstractValidator<AddTrackToAlbumCommand>
{
    public AddTrackToAlbumCommandValidator()
    {
        RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");

        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
    }
}
