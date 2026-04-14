using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Models;

public sealed record RefreshTokenEnvelope(
    UserId UserId,
    string RawToken,
    DateTimeOffset ExpiresAt,
    bool IsActive);
