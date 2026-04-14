using SpotifyClone.Accounts.Application.Models;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Queries;

public sealed record UserSummary(
    Guid Id,
    string DisplayName,
    ImageMetadataDetails? Avatar);
