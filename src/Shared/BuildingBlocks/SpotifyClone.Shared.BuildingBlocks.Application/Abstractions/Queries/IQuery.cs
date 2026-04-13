using MediatR;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
