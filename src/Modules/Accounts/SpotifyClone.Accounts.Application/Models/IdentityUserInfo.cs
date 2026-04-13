using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Application.Models;

public sealed record IdentityUserInfo(
    UserId Id,
    string? Email,
    string? PhoneNumber,
    bool EmailConfirmed,
    bool PhoneNumberConfirmed,
    bool RequiresTwoFactor);
