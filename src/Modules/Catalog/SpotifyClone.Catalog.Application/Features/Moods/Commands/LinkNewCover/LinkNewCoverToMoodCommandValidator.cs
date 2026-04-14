using FluentValidation;

namespace SpotifyClone.Catalog.Application.Features.Moods.Commands.LinkNewCover;

public sealed class LinkNewCoverToMoodCommandValidator
    : AbstractValidator<LinkNewCoverToMoodCommand>
{
    public LinkNewCoverToMoodCommandValidator()
    {
        RuleFor(x => x.MoodId)
            .NotNull().WithMessage("Mood ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Mood ID is required.");

        RuleFor(x => x.ImageWidth)
            .NotNull().WithMessage("Image width is required.");

        RuleFor(x => x.ImageHeight)
            .NotNull().WithMessage("Image height is required.");

        RuleFor(x => x.ImageFileType)
            .NotNull().WithMessage("Image file type is required.");

        RuleFor(x => x.ImageSizeInBytes)
            .NotNull().WithMessage("Image size in bytes is required.");
    }
}
