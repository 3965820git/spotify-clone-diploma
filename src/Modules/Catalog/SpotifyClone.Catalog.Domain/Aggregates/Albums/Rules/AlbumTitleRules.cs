using System.Text.RegularExpressions;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Albums.Rules;

public static class AlbumTitleRules
{
    public const short MaxLength = 25;

    public static void Validate(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        if (!Regex.IsMatch(title, @"^[a-zA-Z0-9\s.,?!-_]*$"))
        {
            throw new InvalidAlbumTitleDomainException("Title contains invalid characters.");
        }

        if (title.Length > MaxLength)
        {
            throw new InvalidAlbumTitleDomainException($"Title exceeds maximum length of {MaxLength} characters.");
        }
    }
}
