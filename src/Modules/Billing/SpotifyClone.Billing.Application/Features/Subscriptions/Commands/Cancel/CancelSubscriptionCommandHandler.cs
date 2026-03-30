using SpotifyClone.Billing.Application.Abstractions;
using SpotifyClone.Billing.Application.Abstractions.Services;
using SpotifyClone.Billing.Application.Errors;
using SpotifyClone.Billing.Domain.Aggregates.Subscriptions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Commands.Cancel;

internal sealed class CancelSubscriptionCommandHandler(
    ICurrentUser currentUser,
    IPaymentProviderService paymentProviderService,
    IBillingUnitOfWork unit)
    : ICommandHandler<CancelSubscriptionCommand, CancelSubscriptionCommandResult>
{
    private readonly IBillingUnitOfWork _unit = unit;

    public async Task<Result<CancelSubscriptionCommandResult>> Handle(
        CancelSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        if (!currentUser.IsAuthenticated)
        {
            return Result.Failure<CancelSubscriptionCommandResult>(SubscriptionErrors.NotLoggedIn);
        }

        var userId = UserId.From(currentUser.Id);
        Subscription? subscription = await _unit.Subscriptions.GetByUserIdAsync(userId, cancellationToken);
        if (subscription is null)
        {
            return Result.Failure<CancelSubscriptionCommandResult>(SubscriptionErrors.NotFound);
        }

        if (string.IsNullOrWhiteSpace(subscription.ExternalIdentity.SubscriptionId))
        {
            return Result.Failure<CancelSubscriptionCommandResult>(SubscriptionErrors.InvalidState);
        }

        await paymentProviderService.CancelSubscriptionAtPeriodEndAsync(
            subscription.ExternalIdentity.SubscriptionId,
            cancellationToken);

        subscription.Cancel();

        return new CancelSubscriptionCommandResult();
    }
}
