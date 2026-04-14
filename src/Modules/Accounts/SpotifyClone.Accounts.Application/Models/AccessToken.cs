namespace SpotifyClone.Accounts.Application.Models;

public sealed record AccessToken(
    string RawToken,
    DateTimeOffset ExpiresAt);
