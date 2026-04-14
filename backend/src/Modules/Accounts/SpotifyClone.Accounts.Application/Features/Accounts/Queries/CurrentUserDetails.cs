using SpotifyClone.Accounts.Application.Models;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Queries;

public sealed record CurrentUserDetails(
    Guid Id,
    string Username,
    string Email,
    string? PhoneNumber,
    bool IsEmailConfirmed,
    bool IsPhoneNumberConfirmed,
    bool IsTwoFactorEnabled,
    string Role,
    string DisplayName,
    string Gender,
    DateTimeOffset? BirthDateUtc,
    ImageMetadataDetails? Avatar);
