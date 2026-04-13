using System.Text.RegularExpressions;
using SpotifyClone.Catalog.Domain.Aggregates.Tracks.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Tracks.Rules;

public static class TrackTitleRules
{
    public const short MaxLength = 128;

    public static void Validate(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        if (!Regex.IsMatch(title, @"^[a-zA-Z0-9\s.,?! _-]*$"))
        {
            throw new InvalidTrackTitleDomainException("Title contains invalid characters.");
        }

        if (title.Length > MaxLength)
        {
            throw new InvalidTrackTitleDomainException($"Title exceeds maximum length of {MaxLength} characters.");
        }
    }
}
