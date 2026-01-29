namespace SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;

public interface IUnitOfWork
{
    Task<int> Commit(CancellationToken cancellationToken = default);
}
