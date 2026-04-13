using SpotifyClone.Catalog.Domain.Aggregates.Tracks.Exceptions;

namespace SpotifyClone.Catalog.Domain.Aggregates.Tracks.Rules;

public static class TrackDurationRules
{
    public static readonly TimeSpan MaxDuration = TimeSpan.FromHours(24);

    public static void Validate(TimeSpan duration)
    {
        if (duration <= TimeSpan.Zero)
        {
            throw new InvalidTrackDurationDomainException("Duration must be greater that zero.");
        }
        else if (duration > MaxDuration)
        {
            throw new InvalidTrackDurationDomainException(
                $"Duration hours must be less than {MaxDuration.TotalHours}.");
        }
    }
}
