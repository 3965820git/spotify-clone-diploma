using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.CorrectTitle;

public sealed class CorrectTrackTitleCommandValidator
    : AbstractValidator<CorrectTrackTitleCommand>
{
    public CorrectTrackTitleCommandValidator()
    {
        RuleFor(x => x.TrackId)
            .NotNull().WithMessage("Track ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Track ID is required.");

        RuleFor(x => x.Title)
            .NotNull().WithMessage("New title is required.")
            .NotEmpty().WithMessage("New title is required.");
    }
}
