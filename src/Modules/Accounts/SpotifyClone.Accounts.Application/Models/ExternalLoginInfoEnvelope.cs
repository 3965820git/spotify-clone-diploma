namespace SpotifyClone.Accounts.Application.Models;

public sealed record ExternalLoginInfoEnvelope(
    string Email,
    string Name,
    string LoginProvider,
    string ProviderKey,
    string ProviderDisplayName);
