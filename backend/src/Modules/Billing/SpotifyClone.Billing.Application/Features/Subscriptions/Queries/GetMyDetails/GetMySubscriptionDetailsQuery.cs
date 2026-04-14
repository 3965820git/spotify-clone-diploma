using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Queries.GetMyDetails;

public sealed record GetMySubscriptionDetailsQuery
    : IQuery<SubscriptionDetails>;
