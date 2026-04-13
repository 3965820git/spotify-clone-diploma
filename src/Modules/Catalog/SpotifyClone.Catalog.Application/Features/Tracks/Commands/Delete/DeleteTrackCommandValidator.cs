using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.Delete;

public sealed class DeleteTrackCommandValidator
    : AbstractValidator<DeleteTrackCommand>
{
    public DeleteTrackCommandValidator()
        => RuleFor(x => x.TrackId)
        .NotNull().WithMessage("Track ID is required.")
        .NotEqual(Guid.Empty).WithMessage("Track ID is required.");
}
