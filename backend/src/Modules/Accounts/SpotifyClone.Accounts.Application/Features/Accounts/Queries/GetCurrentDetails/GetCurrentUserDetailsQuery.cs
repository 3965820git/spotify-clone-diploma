using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Accounts.Application.Features.Accounts.Queries.GetCurrentDetails;

public sealed record GetCurrentUserDetailsQuery
    : IQuery<CurrentUserDetails>;
