using SpotifyClone.Accounts.Application.Models;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Queries;

public sealed record UserProfileDetails(
    Guid Id,
    string DisplayName,
    string Role,
    ImageMetadataDetails? Avatar);
