using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Tracks.Commands.Create;

public sealed class CreateTrackCommandValidator
    : AbstractValidator<CreateTrackCommand>
{
    public CreateTrackCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title is required.")
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.ContainsExplicitContent)
            .NotNull().WithMessage("Explicit content flag is required.");

        RuleFor(x => x.AlbumId)
            .NotNull().WithMessage("Album ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Album ID is required.");

        RuleFor(x => x.MainArtists)
            .NotNull().WithMessage("Main artist collection is required.");

        RuleFor(x => x.FeaturedArtists)
            .NotNull().WithMessage("Featured artist collection is required.");

        RuleFor(x => x.Genres)
            .NotNull().WithMessage("Genre collection is required.");
    }
}
