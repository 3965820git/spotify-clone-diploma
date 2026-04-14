using SpotifyClone.Billing.Application.Abstractions.Data;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Queries.GetMyDetails;

internal sealed class GetMySubscriptionDetailsQueryHandler(
    ISubscriptionReadService subscriptionReadService,
    ICurrentUser currentUser)
    : IQueryHandler<GetMySubscriptionDetailsQuery, SubscriptionDetails>
{
    private readonly ISubscriptionReadService _subscriptionReadService = subscriptionReadService;

    public async Task<Result<SubscriptionDetails>> Handle(
        GetMySubscriptionDetailsQuery request,
        CancellationToken cancellationToken)
        => await _subscriptionReadService.GetDetailsByUserIdAsync(
            UserId.From(currentUser.Id), cancellationToken);
}
