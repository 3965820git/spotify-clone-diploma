using Microsoft.Extensions.Logging;
using SpotifyClone.Billing.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Billing.Application.Behaviors;

public sealed class BillingTransactionalPipelineBehavior<TRequest, TResponse>(
    IBillingUnitOfWork unit,
    ILogger<BillingTransactionalPipelineBehavior<TRequest, TResponse>> logger)
    : TransactionalPipelineBehaviorBase<TRequest, TResponse>(
        unit, typeof(IBillingPersistentCommand), typeof(IBillingPersistentCommand<>), logger)
    where TRequest : notnull
    where TResponse : IResult;
