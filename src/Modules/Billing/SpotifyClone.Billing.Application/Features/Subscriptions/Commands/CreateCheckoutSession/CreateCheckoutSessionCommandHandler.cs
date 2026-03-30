using SpotifyClone.Billing.Application.Abstractions.Data;
using SpotifyClone.Billing.Application.Abstractions.Services;
using SpotifyClone.Billing.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Billing.Application.Features.Subscriptions.Commands.CreateCheckoutSession;

internal sealed class CreateCheckoutSessionCommandHandler(
    ISubscriptionReadService subscriptionReadService,
    IPaymentProviderService checkoutService,
    ICurrentUser currentUser)
    : ICommandHandler<CreateCheckoutSessionCommand, CreateCheckoutSessionCommandResult>
{
    private readonly ISubscriptionReadService _subscriptionReadService = subscriptionReadService;
    private readonly IPaymentProviderService _checkoutService = checkoutService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<CreateCheckoutSessionCommandResult>> Handle(
        CreateCheckoutSessionCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return Result.Failure<CreateCheckoutSessionCommandResult>(SubscriptionErrors.NotLoggedIn);
        }

        if (await _subscriptionReadService.UserHasActiveSubscriptionAsync(
            UserId.From(_currentUser.Id), cancellationToken))
        {
            return Result.Failure<CreateCheckoutSessionCommandResult>(SubscriptionErrors.AlreadyActivated);
        }

        string checkoutUrl = await _checkoutService.CreateCheckoutSessionUrlAsync(
            _currentUser.Id,
            _currentUser.Email,
            cancellationToken);

        return new CreateCheckoutSessionCommandResult(checkoutUrl);
    }
}
