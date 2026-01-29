namespace SpotifyClone.Accounts.Application.Abstractions.Services.Models;

public sealed record AccessToken(
    string RawToken,
    DateTimeOffset ExpiresAt);
