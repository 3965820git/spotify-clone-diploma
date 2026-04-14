using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Queries.GetProfileDetails;

public sealed record GetUserProfileDetailsQuery(
    Guid UserId)
    : IQuery<UserProfileDetails>;
