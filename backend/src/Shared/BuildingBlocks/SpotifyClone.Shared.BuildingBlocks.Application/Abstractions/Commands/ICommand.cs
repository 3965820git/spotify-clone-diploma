using MediatR;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Commands;

public interface IPersistentCommand : ICommand;

public interface IPersistentCommand<TResponse> : ICommand<TResponse>;

public interface ICommand
    : IBaseCommand, IRequest<Result>;

public interface ICommand<TResponse>
    : IBaseCommand, IRequest<Result<TResponse>>;

public interface IBaseCommand;
