using Microsoft.Extensions.Logging;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Accounts.Application.Behaviors;

public sealed class AccountsTransactionalPipelineBehavior<TRequest, TResponse>(
    IAccountsUnitOfWork unit,
    ILogger<AccountsTransactionalPipelineBehavior<TRequest, TResponse>> logger)
    : TransactionalPipelineBehaviorBase<TRequest, TResponse>(
        unit, typeof(IAccountsPersistentCommand), typeof(IAccountsPersistentCommand<>), logger)
    where TRequest : notnull
    where TResponse : IResult;
