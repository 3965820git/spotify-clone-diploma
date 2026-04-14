using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;

public interface IDomainExceptionMapper
{
    Error MapToError(DomainExceptionBase domainException);
}
